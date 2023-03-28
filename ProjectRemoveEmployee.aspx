﻿<%@ Page Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="true" enableEventValidation="true" CodeBehind="ProjectRemoveEmployee.aspx.cs" Inherits="btl_web_nangcao_task_management_system.page.project.ProjectRemoveEmployee" %>

<asp:Content ID="ContentProjectRemoveEmployee" runat="server" ContentPlaceHolderID="mainContentPlaceHolder">
    <asp:Panel runat="server" ID="Panel1">
        <asp:Label ID="errorMessage" runat="server" CssClass="invalid-feedback"></asp:Label>
        <asp:Label ID="successMessage" runat="server" CssClass="success-feedback"></asp:Label>
        <div class="form-group">
            <asp:Label ID="Label1" runat="server" Text="Select Project:" AssociatedControl="titleTextBox"></asp:Label>
            <asp:DropDownList ID="projectDropDownList" runat="server" AutoPostBack="False" CssClass="form-control">
                <Items>
                    <asp:ListItem Text="-Select-"/>
                </Items>
            </asp:DropDownList>
            <asp:Label ID="feedbackProject" runat="server" CssClass="invalid-feedback"></asp:Label>
        </div>
        <div class="form-group">
            <asp:Label ID="Label2" runat="server" Text="Select Emplyee to remove:" AssociatedControl="removeEmployeeListBox"></asp:Label>
            <asp:ListBox ID="removeEmployeeListBox" runat="server" Height="100%" Width="100%" CssClass="form-control"></asp:ListBox>
            <asp:Label ID="feedbackRemoveEmployee" runat="server" CssClass="invalid-feedback"></asp:Label>
        </div>
        <button id="saveButton" type="button" onclick="doRequest()" >Save</button>
    </asp:Panel>
    <script>
        const projectDropDownList = document.getElementById("<%= projectDropDownList.ClientID %>");
        const feedbackProject = document.getElementById("<%= feedbackProject.ClientID %>");
        const removeEmployeeListBox = document.getElementById("<%= removeEmployeeListBox.ClientID %>");
        const feedbackRemoveEmployee = document.getElementById("<%= feedbackRemoveEmployee.ClientID %>");
        const errorMessage = document.getElementById("<%= errorMessage.ClientID %>");
        const successMessage = document.getElementById("<%= successMessage.ClientID %>");

        function validateForm() {
            let valid = true;
            if (!projectDropDownList.value || projectDropDownList.selectedIndex < 1) {
                valid = false;
                projectDropDownList.classList.add("is-invalid");
                feedbackProject.innerText = "Please select a project";
            }
            else {
                projectDropDownList.classList.remove("is-invalid");
                feedbackProject.innerText = null;
            }
            if (removeEmployeeListBox.options.length < 1) {
                valid = false;
                removeEmployeeListBox.classList.add("is-invalid");
                feedbackRemoveEmployee.innerText = "Please select employee";
            }
            else {
                removeEmployeeListBox.classList.remove("is-invalid");
                feedbackRemoveEmployee.innerText = null;
            }
            return valid;
        }

        projectDropDownList.onchange = function () {
            let projectId = projectDropDownList.value;
            removeEmployeeListBox.innerHTML = null;
            if (projectId && projectDropDownList.selectedIndex > 0) {
                methodGet(`ProjectRemoveEmployee.aspx?action=loadEmployeeInProject&projectId=${projectId}`)
                    .then((data) => {
                        data.forEach((v, k) => {
                            let option = document.createElement("option");
                            option.text = v.employeeName;
                            option.value = v.employeeId;
                            removeEmployeeListBox.add(option);
                        });
                    })
                    .catch((err) => {
                        errorMessage.innerText = err.message;
                    });
            }
        }

        function doRequest() {
            errorMessage.innerText = null;
            successMessage.innerText = null;
            if (validateForm()) {
                let employeeIds = [];
                for (let option of removeEmployeeListBox.options) {
                    if (option.selected) {
                        employeeIds.push(option.value);
                    }
                }
                if (employeeIds.length < 1) {
                    errorMessage.innerHTML = "Please select employee";
                    return;
                }
                const body = {
                    "projectId": projectDropDownList.value,
                    "employeeIds": employeeIds
                };
                methodPost("ProjectRemoveEmployee.aspx/removeEmployee", body)
                    .then((data) => {
                        let response = JSON.parse(data.d);
                        if (response.error) {
                            errorMessage.innerText = response.message;
                        }
                        else {
                            successMessage.innerText = response.message;
                            for (var i = 0; i < removeEmployeeListBox.length; i++) {
                                if (employeeIds.includes(removeEmployeeListBox.options[i].value)) {
                                    removeEmployeeListBox.remove(i);
                                }
                            }
                        }
                    })
                    .catch((err) => {
                        errorMessage.innerText = err.message;
                    });
            }
        }
    </script>
</asp:Content>
