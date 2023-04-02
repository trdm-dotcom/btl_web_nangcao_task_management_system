<%@ Page Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="true" CodeBehind="ProjectEdit.aspx.cs" Inherits="btl_web_nangcao_task_management_system.page.ProjectEdit" %>

<asp:Content ID="ContentProjectEdit" runat="server" ContentPlaceHolderID="mainContentPlaceHolder">
    <div class="messageFeedback">
        <asp:Label ID="errorMessage" runat="server" CssClass="invalid-feedback"></asp:Label>
        <asp:Label ID="successMessage" runat="server" CssClass="success-feedback"></asp:Label>
        <div id="toastBox"></div>
    </div>
    <div class="form-group">
        <asp:Label ID="Label2" runat="server" Text="Project Title:" AssociatedControl="titleTextBox"></asp:Label>
        <asp:TextBox ID="titleTextBox" runat="server" CssClass="form-control"></asp:TextBox>
        <asp:Label ID="feedbackTitle" runat="server" CssClass="invalid-feedback"></asp:Label>
    </div>
    <div class="form-group">
        <asp:Label ID="Label3" runat="server" Text="Description:" CssClass="labelForm" AssociatedControl="descriptionTextBox"></asp:Label>
        <asp:TextBox ID="descriptionTextBox" runat="server" TextMode="multiline" Rows="10" CssClass="form-control"></asp:TextBox>
        <asp:Label ID="feedbackDescription" runat="server" CssClass="invalid-feedback"></asp:Label>
    </div>
    <div class="form-row no-gutters">
        <div class="form-group col c-12 m-3 l-3">
            <asp:Label ID="Label4" runat="server" Text="Start Date:" AssociatedControl="startDateTextBox"></asp:Label>
            <asp:TextBox ID="startDateTextBox" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
            <asp:Label ID="feedbackStartDate" runat="server" CssClass="invalid-feedback"></asp:Label>
        </div>
        <div class="form-group col c-12 m-3 l-3">
            <asp:Label ID="Label5" runat="server" Text="Estimate Date:" AssociatedControlID="estimateDateTextBox"></asp:Label>
            <asp:TextBox ID="estimateDateTextBox" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
            <asp:Label ID="feedbackestimateDate" runat="server" CssClass="invalid-feedback"></asp:Label>
        </div>
    </div>
    <div class="form-group">
        <asp:Label ID="Label1" runat="server" Text="Lead:" AssociatedControl="leadDropDownList"></asp:Label>
        <asp:DropDownList ID="leadDropDownList" runat="server" AutoPostBack="false" CssClass="form-control">
            <Items>
                <asp:ListItem Text="-Select-" />
            </Items>
        </asp:DropDownList>
        <asp:Label ID="feedbackLead" runat="server" CssClass="invalid-feedback"></asp:Label>
    </div>
    <asp:Button ID="updateButton" runat="server" Text="update" OnClick="updateButton_Click" OnClientClick="return validateForm()" />
    <script>
        const titleTextBox = document.getElementById("<%= titleTextBox.ClientID %>");
        const descriptionTextBox = document.getElementById("<%= descriptionTextBox.ClientID %>");
        const startDateTextBox = document.getElementById("<%= startDateTextBox.ClientID %>");
        const estimateDateTextBox = document.getElementById("<%= estimateDateTextBox.ClientID %>");
        const leadDropDownList = document.getElementById("<%= leadDropDownList.ClientID %>");


        const feedbackTitle = document.getElementById("<%= feedbackTitle.ClientID %>");
        const feedbackDescription = document.getElementById("<%= feedbackDescription.ClientID %>");
        const feedbackStartDate = document.getElementById("<%= feedbackStartDate.ClientID %>");
        const feedbackestimateDate = document.getElementById("<%= feedbackestimateDate.ClientID %>");
        const feedbackLead = document.getElementById("<%= feedbackLead.ClientID %>");

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
            if (!leadDropDownList.value || leadDropDownList.selectedIndex < 1) {
                valid = false;
                leadDropDownList.classList.add("is-invalid");
                feedbackLead.innerText = "Please select project's lead";
            }
            else {
                leadDropDownList.classList.remove("is-invalid");
                feedbackLead.innerText = null;
            }
            return valid;
        }
    </script>
</asp:Content>

