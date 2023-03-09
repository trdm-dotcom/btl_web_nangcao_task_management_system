<%@ Page Language="C#" MasterPageFile="~/page/MasterPage.Master" AutoEventWireup="true" CodeBehind="ProjectClose.aspx.cs" Inherits="btl_web_nangcao_task_management_system.page.project.ProjectClose" %>

<asp:Content ID="ContentProjectClose" runat="server" ContentPlaceHolderID="mainContentPlaceHolder">
    <asp:Panel runat="server" ID="Panel1">
        <asp:Label ID="errorMessage" runat="server" CssClass="invalid-feedback"></asp:Label>
        <div class="form-group">
            <asp:Label ID="Label1" runat="server" Text="Select Project to Close" AssociatedControl="titleTextBox"></asp:Label>
            <asp:DropDownList ID="projectDropDownList" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="projectDropDownList_SelectedIndexChanged" OnDataBound="projectDropDownList_DataBound">
            </asp:DropDownList>
            <asp:Label ID="feedbackProject" runat="server" CssClass="invalid-feedback"></asp:Label>
        </div>
        <div class="form-group">
            <asp:Label ID="Label2" runat="server" Text="Status" AssociatedControl="titleTextBox"></asp:Label>
            <asp:DropDownList ID="statusDropDownList" runat="server" CssClass="form-control" Enabled="False">
            </asp:DropDownList>
        </div>
        <asp:Button ID="closeButton" runat="server" Text="Close Project" OnClick="closeButton_Click"/>
    </asp:Panel>
</asp:Content>
