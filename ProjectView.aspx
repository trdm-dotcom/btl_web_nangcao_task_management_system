<%@ Page Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="true" CodeBehind="ProjectView.aspx.cs" Inherits="btl_web_nangcao_task_management_system.ProjectView" %>

<asp:Content ID="ContentProjectView" runat="server" ContentPlaceHolderID="mainContentPlaceHolder">
    <div class="messageFeedback">
        <asp:Label ID="errorMessage" runat="server" CssClass="invalid-feedback"></asp:Label>
        <asp:Label ID="successMessage" runat="server" CssClass="success-feedback"></asp:Label>
        <div id="toastBox"></div>
    </div>
    <div class="form-group">
        <asp:Label ID="Label1" runat="server" Text="Project Title:" CssClass="labelForm"></asp:Label>
        <asp:Label ID="projectTitleLabel" runat="server"></asp:Label>
    </div>
    <div class="form-group">
        <asp:Label ID="Label2" runat="server" Text="Description:" CssClass="labelForm"></asp:Label>
        <asp:Label ID="projectDescriptionLabel" runat="server"></asp:Label>
    </div>
    <div class="form-group">
        <asp:Label ID="Label3" runat="server" Text="Start Date:" CssClass="labelForm"></asp:Label>
        <asp:Label ID="projectStartDateLabel" runat="server"></asp:Label>
    </div>
    <div class="form-group">
        <asp:Label ID="Label4" runat="server" Text="Estimate Date:" CssClass="labelForm"></asp:Label>
        <asp:Label ID="projectEstimateDateLabel" runat="server"></asp:Label>
    </div>
    <div class="form-group">
        <asp:Label ID="Label5" runat="server" Text="Lead:" CssClass="labelForm"></asp:Label>
        <asp:Label ID="projectLeadLabel" runat="server"></asp:Label>
    </div>
    <div class="form-group">
        <asp:Label ID="Label6" runat="server" Text="Status:" CssClass="labelForm"></asp:Label>
        <asp:Label ID="projectStatusLabel" runat="server"></asp:Label>
    </div>
</asp:Content>
