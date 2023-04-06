<%@ Page Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="true" CodeBehind="UserRemove.aspx.cs" Inherits="btl_web_nangcao_task_management_system.UserRemove" %>

<asp:Content ID="ContentUserRemove" runat="server" ContentPlaceHolderID="mainContentPlaceHolder">
    <div class="messageFeedback">
        <asp:Label ID="errorMessage" runat="server" CssClass="invalid-feedback"></asp:Label>
        <asp:Label ID="successMessage" runat="server" CssClass="success-feedback"></asp:Label>
        <div id="toastBox"></div>
    </div>
    <div class="form-group">
        <asp:Label ID="Label1" runat="server" Text="Employee:" AssociatedControl="employeeDropDownList"></asp:Label>
        <asp:DropDownList ID="employeeDropDownList" runat="server" AutoPostBack="false" CssClass="form-control">
            <Items>
                <asp:ListItem Text="-Select-" />
            </Items>
        </asp:DropDownList>
        <asp:Label ID="feedbackEmployeeDropDownList" runat="server" CssClass="invalid-feedback"></asp:Label>
    </div>
    <div class="form-group">
        <asp:Label ID="Label2" runat="server" Text="Employee role:" AssociatedControl="userDropDownList"></asp:Label>
        <asp:Label ID="userRoleLabel" runat="server" Text=""></asp:Label>
    </div>
    <button type="button" onclick="doRemove()" >Update</button>
    <script>
        const employeeDropDownList = document.getElementById("<%= employeeDropDownList.ClientID %>");
        const feedbackEmployeeDropDownList = document.getElementById("<%= feedbackEmployeeDropDownList.ClientID %>");
        function validateForm() {
            let valid = true;
            if (employeeDropDownList.value == null
                || userDropDownList.selectedIndex < 1) {
                valid = false;
                employeeDropDownList.classList.add("is-invalid");
                feedbackEmployeeDropDownList.innerText = "Please choose employee";
            }
            else {
                employeeDropDownList.classList.remove("is-invalid");
                feedbackEmployeeDropDownList.innerText = null;
            }
            return valid;
        }

        employeeDropDownList.onchange = function () {
            if (employeeDropDownList.value == null
                || userDropDownList.selectedIndex < 1) {
                showPopupNotification(document.getElementById("toastBox"), "Invalid employee", "warning");
                return;
            }
            methodGet(`UserEditRole.aspx/getInfo?employee=${employeeDropDownList.value}`)
                .then((result) => {
                    let response = JSON.parse(result.d);
                    if (response.error) {
                        showPopupNotification(document.getElementById("toastBox"), response.message, "error");
                    }
                    else {
                        userRoleLabel.innerText = response.message;
                    }
                })
                .catch((err) => {
                    console.error(err);
                    showPopupNotification(document.getElementById("toastBox"), "Internal error server", "error");
                });
        }

        function doRemove() {
            if (!validateForm()) {
                return;
            }
            const body = {
                user: userDropDownList.value
            }
            methodPost("UserEditRole.aspx/removeEmployee", body)
                .then((result) => {
                    let response = JSON.parse(result.d);
                    if (response.error) {
                        showPopupNotification(document.getElementById("toastBox"), response.message, "error");
                    }
                    else {
                        showPopupNotification(document.getElementById("toastBox"), response.message, "success");
                    }
                })
                .catch((err) => {
                    console.error(err);
                    showPopupNotification(document.getElementById("toastBox"), "Internal error server", "error");
                });
        }
    </script>
</asp:Content>
