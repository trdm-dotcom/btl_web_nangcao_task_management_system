<%@ Page Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="true" CodeBehind="ProjectAddEmployee.aspx.cs" Inherits="btl_web_nangcao_task_management_system.page.project.ProjectAddEmployee" %>

<asp:Content ID="ContentProjectAddEmployee" runat="server" ContentPlaceHolderID="mainContentPlaceHolder">
    <asp:Panel runat="server" ID="Panel1">
        <asp:Label ID="errorMessage" runat="server" CssClass="invalid-feedback"></asp:Label>
        <asp:Label ID="successMessage" runat="server" CssClass="success-feedback"></asp:Label>
        <div class="form-group">
            <asp:Label ID="Label1" runat="server" Text="Select Project:" AssociatedControl="titleTextBox"></asp:Label>
            <asp:DropDownList ID="projectDropDownList" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="projectDropDownList_SelectedIndexChanged">
                <Items>
                    <asp:ListItem Text="-Select-"/>
                </Items>
            </asp:DropDownList>
            <asp:Label ID="feedbackProject" runat="server" CssClass="invalid-feedback"></asp:Label>
        </div>
        <div class="form-group">
            <asp:Label ID="employeesLabel" runat="server" Text="Employee:"></asp:Label>
            <div class="form-row no-gutters">
                <div class="col c-5 m-5 l-5">
                    <asp:ListBox ID="allEmployeeListBox" runat="server" Height="100%" Width="100%" CssClass="form-control"></asp:ListBox>
                </div>
                <div class="col c-2 m-2 l-2">
                    <center>
                       <asp:Button runat="server" ID="singleAddButton" style="margin-bottom: 13px; width: 40px" Text=">" OnClick="singleAddButton_Click"/>
                        <br />
                        <asp:Button runat="server" ID="allAddButton" style="margin-bottom: 13px; width: 40px" Text=">>" OnClick="allAddButton_Click"/>
                        <br />
                    </center>
                </div>
                <div class="col c-5 m-5 l-5">
                    <asp:ListBox ID="selectedEmployeeListBox" runat="server" Height="100%" Width="100%" CssClass="form-control"></asp:ListBox>
                    <asp:Label ID="feedbackEmployee" runat="server" CssClass="invalid-feedback"></asp:Label>
                </div>
            </div>
        </div>
        <asp:Button ID="saveButton" runat="server" Text="Save" OnClick="saveButton_Click" OnClientClick="return validateForm()" />
    </asp:Panel>
    <script>
        const allEmployeeListBox = document.getElementById("<%= allEmployeeListBox.ClientID %>");
        const selectedEmployeeListBox = document.getElementById("<%= selectedEmployeeListBox.ClientID %>");
        const projectDropDownList = document.getElementById("<%= projectDropDownList.ClientID %>");
        const feedbackProject = document.getElementById("<%= feedbackProject.ClientID %>");
        const feedbackEmployee = document.getElementById("<%= feedbackEmployee.ClientID %>");

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
            if (selectedEmployeeListBox.options.length < 1) {
                valid = false;
                selectedEmployeeListBox.classList.add("is-invalid");
                feedbackEmployee.innerText = "Please select employee";
            }
            else {
                selectedEmployeeListBox.classList.remove("is-invalid");
                feedbackEmployee.innerText = null;
            }
            return valid;
        }
    </script>
</asp:Content>

