<%@ Page Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="true" CodeBehind="TaskCreate.aspx.cs" Inherits="btl_web_nangcao_task_management_system.page.task.TaskCreate" %>

<asp:Content ID="ContentTaskCreate" runat="server" ContentPlaceHolderID="mainContentPlaceHolder">
    <asp:Panel runat="server" ID="Panel1">
        <asp:Label ID="errorMessage" runat="server" CssClass="invalid-feedback"></asp:Label>
        <div class="form-group">
            <asp:Label ID="Label5" runat="server" Text="Project:" AssociatedControlID="projectDropDownList"></asp:Label>
            <asp:DropDownList ID="projectDropDownList" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="projectDropDownList_SelectedIndexChanged">
                <Items>
                    <asp:ListItem Text="-Select-"/>
                </Items>
            </asp:DropDownList>
            <asp:Label ID="feedbackProject" runat="server" CssClass="invalid-feedback"></asp:Label>
        </div>
        <div class="form-group">
            <asp:Label ID="Label1" runat="server" Text="Task Title:" AssociatedControl="titleTextBox"></asp:Label>
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
                <asp:Label ID="Label7" runat="server" Text="Reporter:" AssociatedControlID="reporterDropDownList"></asp:Label>
                <asp:DropDownList ID="reporterDropDownList" runat="server" CssClass="form-control">
                    <Items>
                        <asp:ListItem Text="-Select-"/>
                    </Items>
                </asp:DropDownList>
            </div>
            <div class="form-group col c-12 m-3 l-3">
                <asp:Label ID="Label6" runat="server" Text="Assignee:" AssociatedControlID="assigneeDropDownList"></asp:Label>
                <asp:DropDownList ID="assigneeDropDownList" runat="server" CssClass="form-control">
                    <Items>
                        <asp:ListItem Text="-Select-"/>
                    </Items>
                </asp:DropDownList>
            </div>
            <div class="form-group col c-12 m-3 l-3">
                <asp:Label ID="Label8" runat="server" Text="QA:" AssociatedControlID="QADropDownList"></asp:Label>
                <asp:DropDownList ID="QADropDownList" runat="server" CssClass="form-control">
                    <Items>
                        <asp:ListItem Text="-Select-"/>
                    </Items>
                </asp:DropDownList>
            </div>
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
            <div class="form-group col c-12 m-3 l-3">
                <asp:Label ID="Label10" runat="server" Text="Priority" AssociatedControlID="priorityDropDownList"></asp:Label>
                <asp:DropDownList ID="priorityDropDownList" runat="server" CssClass="form-control">
                    <Items>
                        <asp:ListItem Text="-Select-"/>
                        <asp:ListItem Text="LOW" Value="LOW" />
                        <asp:ListItem Text="MEDIUM" Value="MEDIUM" />
                        <asp:ListItem Text="HIGH" Value="HIGH" />
                    </Items>
                </asp:DropDownList>
                <asp:Label ID="feedbackPriority" runat="server" CssClass="invalid-feedback"></asp:Label>
            </div>
        </div>
        <asp:Button ID="saveButton" runat="server" Text="Save" OnClick="saveButton_Click" OnClientClick="return validateForm()" />
    </asp:Panel>
    <script>
        const projectDropDownList = document.getElementById("<%= projectDropDownList.ClientID  %>");
        const titleTextBox = document.getElementById("<%= titleTextBox.ClientID %>");
        const descriptionTextBox = document.getElementById("<%= descriptionTextBox.ClientID %>");
        const startDateTextBox = document.getElementById("<%= startDateTextBox.ClientID %>");
        const estimateDateTextBox = document.getElementById("<%= estimateDateTextBox.ClientID %>");
        const priorityDropDownList = document.getElementById("<%= priorityDropDownList.ClientID %>");

        const feedbackProject = document.getElementById("<%= feedbackProject.ClientID %>");
        const feedbackTitle = document.getElementById("<%= feedbackTitle.ClientID %>");
        const feedbackDescription = document.getElementById("<%= feedbackDescription.ClientID %>");
        const feedbackStartDate = document.getElementById("<%= feedbackStartDate.ClientID %>");
        const feedbackestimateDate = document.getElementById("<%= feedbackestimateDate.ClientID %>");
        const feedbackPriority = document.getElementById("<%= feedbackPriority.ClientID %>");

        function validateForm() {
            let valid = true;
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
            if (!projectDropDownList.value || projectDropDownList.selectedIndex < 1) {
                valid = false;
                projectDropDownList.classList.add("is-invalid");
                feedbackProject.innerText = "Please select a project";
            }
            else {
                projectDropDownList.classList.remove("is-invalid");
                feedbackProject.innerText = null;
            }
            if (!priorityDropDownList.value || priorityDropDownList.selectedIndex < 1) {
                valid = false;
                priorityDropDownList.classList.add("is-invalid");
                feedbackPriority.innerText = "Please select priority for task";
            }
            else {
                priorityDropDownList.classList.remove("is-invalid");
                feedbackPriority.innerText = null;
            }
            return valid;
        }
    </script>
</asp:Content>

