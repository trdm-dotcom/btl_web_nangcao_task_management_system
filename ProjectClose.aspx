﻿<%@ Page Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="true" CodeBehind="ProjectClose.aspx.cs" Inherits="btl_web_nangcao_task_management_system.page.project.ProjectClose" %>

<asp:Content ID="ContentProjectClose" runat="server" ContentPlaceHolderID="mainContentPlaceHolder">
    <div class="messageFeedback">
        <asp:Label ID="errorMessage" runat="server" CssClass="invalid-feedback"></asp:Label>
        <asp:Label ID="successMessage" runat="server" CssClass="success-feedback"></asp:Label>
        <div id="toastBox"></div>
    </div>
    <div class="form-group">
        <asp:Label ID="Label1" runat="server" Text="Select Project to Close:" AssociatedControl="titleTextBox"></asp:Label>
        <asp:DropDownList ID="projectDropDownList" runat="server" AutoPostBack="False" CssClass="form-control">
            <Items>
                <asp:ListItem Text="-Select-" />
            </Items>
        </asp:DropDownList>
        <asp:Label ID="feedbackProject" runat="server" CssClass="invalid-feedback"></asp:Label>
    </div>
    <div class="form-group">
        <asp:Label ID="Label2" runat="server" Text="Status:" AssociatedControl="titleTextBox"></asp:Label>
        <asp:Label ID="statusProjectLabel" runat="server"></asp:Label>
    </div>
    <asp:Button ID="closeButton" runat="server" Text="Close Project" OnClick="closeButton_Click" OnClientClick="return validateForm()" />
    <script>
        const projectDropDownList = document.getElementById("<%= projectDropDownList.ClientID %>");
        const feedbackProject = document.getElementById("<%= feedbackProject.ClientID %>");
        const errorMessage = document.getElementById("<%= errorMessage.ClientID %>");
        const statusProjectLabel = document.getElementById("<%= statusProjectLabel.ClientID %>");
        projectDropDownList.onchange = function () {
            let projectId = projectDropDownList.value;
            if (projectId && projectDropDownList.selectedIndex > 0) {
                methodGet(`ProjectClose.aspx?action=loadProject&project=${projectId}`)
                    .then((data) => {
                        statusProjectLabel.innerText = data.status;
                    })
                    .catch((err) => {
                        showPopupNotification(document.getElementById("toastBox"), err.message, "error");
                    });
            }
            else {
                showPopupNotification(document.getElementById("toastBox"), "Invalid project", "warning");
            }
        }

        function validateForm() {
            let valid = true;
            if (!projectDropDownList.value || projectDropDownList.selectedIndex < 1) {
                valid = false;
                projectDropDownList.classList.add();
                feedbackProject.innerText = "Please select a project";
            }
            else {
                projectDropDownList.classList.remove();
                feedbackProject.innerText = null;
            }
            return valid;
        }
    </script>
</asp:Content>
