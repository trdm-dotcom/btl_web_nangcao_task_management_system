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
    <script>
        const emailTextBox = document.getElementById("<%= emailTextBox.ClientID %>");
        const passwordTextBox = document.getElementById("<%= passwordTextBox.ClientID %>");
        const feedbackEmail = document.getElementById("<%= feedbackEmail.ClientID %>");
        const feedbackPassword = document.getElementById("<%= feedbackPassword.ClientID %>");
        function validateForm() {
            let valid = true;
            if(!emailTextBox.value.trim()) {
                valid = false;
                emailTextBox.classList.add("is-invalid");
                feedbackEmail.textContent = "Please enter your email";
            }
            else {
                emailTextBox.classList.remove("is-invalid");
                feedbackEmail.textContent = null;
            }
            if(!passwordTextBox.value.trim()) {
                valid = false;
                passwordTextBox.classList.add("is-invalid");
                feedbackPassword.textContent = "Please enter your password";
            } 
            else {
                passwordTextBox.classList.remove("is-invalid");
                feedbackPassword.textContent = null;
            }
            return valid;
        }
    </script>
</asp:Content>