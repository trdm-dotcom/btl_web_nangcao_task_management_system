<%@ Page Language="C#" MasterPageFile="~/page/authentication/MasterPageAuthentication.Master" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="btl_web_nangcao_task_management_system.page.authentication.SignUp" %>

<asp:Content ID="ContentSignup" runat="server" ContentPlaceHolderID="mainContentPlaceHolder">
    <asp:Label ID="errorMessage" runat="server" CssClass="invalid-feedback"></asp:Label>
    <div class="form-group">
         <asp:TextBox runat="server" ID="nameTextBox" placeholder="Enter your name" CssClass="form-control"></asp:TextBox>
         <asp:Label ID="feedbackName" runat="server" CssClass="invalid-feedback"></asp:Label>
    </div>
    <div class="form-group">
        <asp:TextBox runat="server" ID="emailTextBox" TextMode="Email" placeholder="Enter your email" CssClass="form-control"></asp:TextBox>
        <asp:Label ID="feedbackEmail" runat="server" CssClass="invalid-feedback"></asp:Label>
    </div>
    <div class="form-group">
        <asp:TextBox runat="server" ID="passwordTextBox" TextMode="Password" placeholder="Create your password" CssClass="form-control"></asp:TextBox>
        <asp:Label ID="feedbackPassword" runat="server" CssClass="invalid-feedback"></asp:Label>
    </div>
    <div class="form-group">
        <asp:TextBox runat="server" ID="confirmPasswordTextBox" TextMode="Password" placeholder="Confirm your password" CssClass="form-control"></asp:TextBox>
        <asp:Label ID="feedbackConfirmPassword" runat="server" CssClass="invalid-feedback"></asp:Label>
    </div>
    <asp:Button runat="server" CssClass="button" ID="signupButton" Text="Signup" OnClick="signupButton_Click"/>
    <div class="signup">
        <span class="signup">Already have an account?<a href="Login.aspx">Login</a></span>
     </div>
    <script><
</asp:Content>
