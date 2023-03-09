<%@ Page Language="C#" MasterPageFile="~/page/MasterPage.Master" AutoEventWireup="true" CodeBehind="ProjectEdit.aspx.cs" Inherits="btl_web_nangcao_task_management_system.page.ProjectEdit" %>

<asp:Content ID="ContentProjectEdit" runat="server" ContentPlaceHolderID="mainContentPlaceHolder">
    <asp:Panel runat="server" ID="Panel1">
        <asp:Label ID="errorMessage" runat="server" CssClass="invalid-feedback"></asp:Label>
        <div class="form-group">
            <asp:Label ID="Label1" runat="server" Text="Select Project to Edit:" AssociatedControl="projectDropDownList"></asp:Label>
            <asp:DropDownList ID="projectDropDownList" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="projectDropDownList_SelectedIndexChanged" OnDataBound="projectDropDownList_DataBound">
            </asp:DropDownList>
            <asp:Label ID="feedbackProject" runat="server" CssClass="invalid-feedback"></asp:Label>
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
        <asp:Button ID="updateButton" runat="server" Text="update" OnClick="updateButton_Click"/>
    </asp:Panel>
</asp:Content>

