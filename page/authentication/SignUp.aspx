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
    <script>
        const nameTextBox = document.getElementById("<%= nameTextBox.ClientID %>");
        const emailTextBox = document.getElementById("<%= emailTextBox.ClientID %>");
        const passwordTextBox = document.getElementById("<%= passwordTextBox.ClientID %>");
        const confirmPasswordTextBox = document.getElementById("<%= confirmPasswordTextBox.ClientID %>");
        const feedbackName = document.getElementById("<%= feedbackName.ClientID %>");
        const feedbackEmail = document.getElementById("<%= feedbackEmail.ClientID %>");
        const feedbackPassword = document.getElementById("<%= feedbackPassword.ClientID %>");
        const feedbackConfirmPassword = document.getElementById("<%= feedbackConfirmPassword.ClientID %>");
        const EMAIL_REGEX = new RegExp("^(?!\.)[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$(?<!\.)");
        const NAME_REGEX = new RegExp("^(?<!\.)^[a-zA-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂẾưăạảấầẩẫậắằẳẵặẹẻẽềềểếỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ\s]*$(?<!\.)");
        async function validateForm(){
            let valid = true;
            if(!nameTextBox.value.trim()) {
                valid = false;
                nameTextBox.classList.add("is-invalid"); 
                feedbackName.textContent = "Please enter your name";
            }
            else if (!NAME_REGEX.test(nameTextBox.value.trim())) {
                valid = false;
                nameTextBox.classList.add("is-invalid");
                feedbackName.textContent = "Invalid your name";
            }
            else {
                nameTextBox.classList.remove("is-invalid");
                feedbackName.textContent = null;
            }
            if(!emailTextBox.value.trim()) {
                valid = false;
                emailTextBox.classList.add("is-invalid");
                feedbackEmail.textContent = "Please enter your email";
            }
            else if (!EMAIL_REGEX.test(emailTextBox.value.trim())){
                valid = false;
                emailTextBox.classList.add("is-invalid");
                feedbackEmail.textContent = "Invalid email";
            }
            else if (!(await checkcheckEmailAlreadyExists(emailTextBox.value))) {
                valid = false;
                emailTextBox.classList.add("is-invalid");
                feedbackEmail.textContent = "Email already exists";
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
            if(!confirmPasswordTextBox.value.trim() || passwordTextBox.value.trim() != confirmPasswordTextBox.value.trim()) {
                valid = false;
                confirmPasswordTextBox.classList.add("is-invalid");
                feedbackConfirmPassword.textContent = "Invalid confirm password";
            }
            else {
                confirmPasswordTextBox.classList.remove("is-invalid");
                feedbackConfirmPassword.textContent = null;
            }
            return valid;
        }

        async function checkcheckEmailAlreadyExists(email) {
            let body = {
                "email": emailTextBox.value
            };
            try {
                return await methodGet('/SignUp.aspx/checkEmailAlreadyExists', body).exists; 
            } catch (err) {
                console.error(err);
                return false
            }
        }
    </script>
</asp:Content>
