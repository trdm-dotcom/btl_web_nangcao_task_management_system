<%@ Page Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="true" CodeBehind="TaskEdit.aspx.cs" Inherits="btl_web_nangcao_task_management_system.page.task.TaskEdit" %>

<asp:Content ID="ContentTaskEdit" runat="server" ContentPlaceHolderID="mainContentPlaceHolder">
    <asp:Panel runat="server" ID="Panel1">
        <asp:Label ID="errorMessage" runat="server" CssClass="invalid-feedback"></asp:Label>
        <div class="form-group">
            <asp:Label ID="Label5" runat="server" Text="Project:" AssociatedControlID="projectDropDownList"></asp:Label>
            <asp:DropDownList ID="projectDropDownList" runat="server" CssClass="form-control" Enable="false">
            </asp:DropDownList>
            <asp:Label ID="feedbackProject" runat="server" CssClass="invalid-feedback"></asp:Label>
        </div>
        <div class="form-group">
            <asp:Label ID="Label9" runat="server" Text="Task:" AssociatedControlID="taskDropDownList"></asp:Label>
            <asp:DropDownList ID="taskDropDownList" runat="server" CssClass="form-control" Enable="false">
            </asp:DropDownList>
            <asp:Label ID="feedbackTask" runat="server" CssClass="invalid-feedback"></asp:Label>
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
                </asp:DropDownList>
            </div>
            <div class="form-group col c-12 m-3 l-3">
                <asp:Label ID="Label6" runat="server" Text="Assignee:" AssociatedControlID="assigneeDropDownList"></asp:Label>
                <asp:DropDownList ID="assigneeDropDownList" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>
            <div class="form-group col c-12 m-3 l-3">
                <asp:Label ID="Label8" runat="server" Text="QA:" AssociatedControlID="QADropDownList"></asp:Label>
                <asp:DropDownList ID="QADropDownList" runat="server" CssClass="form-control">
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
                </asp:DropDownList>
                <asp:Label ID="feedbackPriority" runat="server" CssClass="invalid-feedback"></asp:Label>
            </div>
        </div>
        <asp:Button ID="updateButton" runat="server" Text="Update" OnClick="updateButton_Click"/>
    </asp:Panel>
</asp:Content>
