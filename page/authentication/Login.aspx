<%@ Page Language="C#"  MasterPageFile="~/page/authentication/MasterPageAuthentication.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="btl_web_nangcao_task_management_system.page.authentication.Login" %>

<asp:Content ID="ContentLogin" runat="server" ContentPlaceHolderID="mainContentPlaceHolder">
    <asp:Label ID="errorMessage" runat="server" CssClass="invalid-feedback"></asp:Label>
    <div class="form-group">
        <asp:TextBox runat="server" ID="emailTextBox" TextMode="Email" placeholder="Enter your email" CssClass="form-control"></asp:TextBox>
        <asp:Label ID="feedbackEmail" runat="server" CssClass="invalid-feedback"></asp:Label>
    </div>
    <div class="form-group">
        <asp:TextBox runat="server" ID="passwordTextBox" TextMode="Password" placeholder="Enter your password" CssClass="form-control"></asp:TextBox>
        <asp:Label ID="feedbackPassword" runat="server" CssClass="invalid-feedback"></asp:Label>
    </div>
    <asp:Button runat="server" CssClass="button" ID="loginButton" Text="Login" OnClick="loginButton_Click" />
    <div class="signup">
        <span class="signup">Don't have an account?<a href="SignUp.aspx">signup</a></span>
    </div>
</asp:Content>