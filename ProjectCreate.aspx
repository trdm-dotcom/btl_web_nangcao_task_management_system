<%@ Page Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ProjectCreate.aspx.cs" Inherits="btl_web_nangcao_task_management_system.page.ProjectCreate" %>

<asp:Content ID="ContentProjectCreate" runat="server" ContentPlaceHolderID="mainContentPlaceHolder">
    <asp:Panel runat="server" ID="Panel1">
        <asp:Label ID="errorMessage" runat="server" CssClass="invalid-feedback"></asp:Label>
        <div class="form-group">
            <asp:Label ID="Label1" runat="server" Text="Project Title:" AssociatedControl="titleTextBox"></asp:Label>
            <asp:TextBox ID="titleTextBox" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:Label ID="feedbackTitle" runat="server" CssClass="invalid-feedback"></asp:Label>
        </div>
        <div class="form-group">
            <asp:Label ID="Label2" runat="server" Text="Description:" CssClass="labelForm" AssociatedControl="descriptionTextBox"></asp:Label>
            <asp:TextBox ID="descriptionTextBox" runat="server" TextMode="multiline" Rows="10" CssClass="form-control"></asp:TextBox>
            <asp:Label ID="feedbackDescription" runat="server" CssClass="invalid-feedback"></asp:Label>
        </div>
        <div class="form-row no-gutters">
            <div class="form-group col c-12 m-3 l-3">
                <asp:Label ID="Label3" runat="server" Text="Start Date:" AssociatedControl="startDateTextBox"></asp:Label>
                <asp:TextBox ID="startDateTextBox" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                <asp:Label ID="feedbackStartDate" runat="server" CssClass="invalid-feedback"></asp:Label>
            </div>
            <div class="form-group col c-12 m-3 l-3">
                <asp:Label ID="Label4" runat="server" Text="Estimate Date:" AssociatedControlID="estimateDateTextBox"></asp:Label>
                <asp:TextBox ID="estimateDateTextBox" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                <asp:Label ID="feedbackestimateDate" runat="server" CssClass="invalid-feedback"></asp:Label>
            </div>
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
                        <asp:Button runat="server" ID="singleRemoveButton" style="margin-bottom: 13px; width: 40px" Text="<" OnClick="singleRemoveButton_Click"/>
                        <br />
                        <asp:Button runat="server" ID="singleAallRemoveButtonddButton" style="margin-bottom: 13px; width: 40px" Text="<<" OnClick="singleAallRemoveButtonddButton_Click"/>
                    </center>
                </div>
                <div class="col c-5 m-5 l-5">
                    <asp:ListBox ID="selectedEmployeeListBox" runat="server" Height="100%" Width="100%" CssClass="form-control"></asp:ListBox>
                    <asp:Label ID="feedbackEmployee" runat="server" CssClass="invalid-feedback"></asp:Label>
                </div>
            </div>
        </div>
        <div class="form-group">
            <asp:Label ID="Label5" runat="server" Text="Leader:" AssociatedControl="leadDropDownList"></asp:Label>
            <asp:DropDownList ID="leadDropDownList" runat="server" AutoPostBack="false" CssClass="form-control">
                <Items>
                    <asp:ListItem Text="-Select-"/>
                </Items>
            </asp:DropDownList>
            <asp:Label ID="feedbackLead" runat="server" CssClass="invalid-feedback"></asp:Label>
        </div>
        <asp:Button ID="saveButton" runat="server" Text="Save" OnClick="saveButton_Click" OnClientClick="return validateForm()" />
    </asp:Panel>
    <script>
        const titleTextBox = document.getElementById("<%= titleTextBox.ClientID %>");
        const descriptionTextBox = document.getElementById("<%= descriptionTextBox.ClientID %>");
        const startDateTextBox = document.getElementById("<%= startDateTextBox.ClientID %>");
        const estimateDateTextBox = document.getElementById("<%= estimateDateTextBox.ClientID %>");
        const leadDropDownList = document.getElementById("<%= leadDropDownList.ClientID %>");
        const allEmployeeListBox = document.getElementById("<%= allEmployeeListBox.ClientID %>");
        const selectedEmployeeListBox = document.getElementById("<%= selectedEmployeeListBox.ClientID %>");


        const feedbackTitle = document.getElementById("<%= feedbackTitle.ClientID %>");
        const feedbackDescription = document.getElementById("<%= feedbackDescription.ClientID %>");
        const feedbackStartDate = document.getElementById("<%= feedbackStartDate.ClientID %>");
        const feedbackestimateDate = document.getElementById("<%= feedbackestimateDate.ClientID %>");
        const feedbackLead = document.getElementById("<%= feedbackLead.ClientID %>");
        const feedbackEmployee = document.getElementById("<%= feedbackEmployee.ClientID %>");

        function validateForm() {
            let valid = true;
            let now = Date.parse(new Date().toJSON().slice(0, 10));
            if (!titleTextBox.value.trim()) {
                valid = false;
                titleTextBox.classList.add("is-invalid");
                feedbackTitle.innerText = "Please enter project title";
            }
            else {
                titleTextBox.classList.remove("is-invalid");
                feedbackTitle.innerText = null;
            }
            if (!descriptionTextBox.value.trim()) {
                valid = false;
                descriptionTextBox.classList.add("is-invalid");
                feedbackDescription.innerText = "Please enter project description";
            }
            else {
                descriptionTextBox.classList.remove("is-invalid");
                feedbackDescription.innerText = null;
            }
            if (!startDateTextBox.value.trim()) {
                valid = false;
                startDateTextBox.classList.add("is-invalid");
                feedbackStartDate.innerText = "Please enter project start date";
            }
            else if (Date.parse(startDateTextBox.value.trim()) < now) {
                valid = false;
                startDateTextBox.classList.add("is-invalid");
                feedbackStartDate.innerText = "Please enter valid date";
            }
            else {
                startDateTextBox.classList.remove("is-invalid");
                feedbackStartDate.innerText = null;
            }
            if (!estimateDateTextBox.value.trim()) {
                valid = false;
                estimateDateTextBox.classList.add("is-invalid");
                feedbackestimateDate.innerText = "Please enter estimate date";
            }
            else if (Date.parse(estimateDateTextBox.value.trim()) < now) {
                valid = false;
                estimateDateTextBox.classList.add("is-invalid");
                feedbackestimateDate.innerText = "Please enter valid date";
            }
            else {
                estimateDateTextBox.classList.remove("is-invalid");
                feedbackestimateDate.innerText = null;
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
            if (!leadDropDownList.value) {
                valid = false;
                leadDropDownList.classList.add("is-invalid");
                feedbackLead.innerText = "Please select project's lead";
            }
            else {
                leadDropDownList.classList.remove("is-invalid");
                feedbackLead.innerText = null;
            }
            return true;
        }
    </script>
</asp:Content>
