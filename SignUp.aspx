<%@ Page Language="C#" MasterPageFile="MasterPageAuthentication.Master" AutoEventWireup="true"
    CodeBehind="SignUp.aspx.cs" Inherits="btl_web_nangcao_task_management_system.page.authentication.SignUp" %>

<asp:Content ID="ContentSignup" runat="server" ContentPlaceHolderID="mainContentPlaceHolder">
    <asp:Label ID="errorMessage" runat="server" CssClass="invalid-feedback"></asp:Label>
    <div class="form-group">
        <asp:TextBox runat="server" ID="nameTextBox" placeholder="Enter your name" CssClass="form-control">
        </asp:TextBox>
        <asp:Label ID="feedbackName" runat="server" CssClass="invalid-feedback"></asp:Label>
    </div>
    <div class="form-group">
        <asp:TextBox runat="server" ID="emailTextBox" TextMode="Email" placeholder="Enter your email"
            CssClass="form-control"></asp:TextBox>
        <asp:Label ID="feedbackEmail" runat="server" CssClass="invalid-feedback"></asp:Label>
    </div>
    <div class="form-group">
        <asp:TextBox runat="server" ID="passwordTextBox" TextMode="Password" placeholder="Create your password"
            CssClass="form-control"></asp:TextBox>
        <asp:Label ID="feedbackPassword" runat="server" CssClass="invalid-feedback"></asp:Label>
    </div>
    <div class="form-group">
        <asp:TextBox runat="server" ID="confirmPasswordTextBox" TextMode="Password"
            placeholder="Confirm your password" CssClass="form-control"></asp:TextBox>
        <asp:Label ID="feedbackConfirmPassword" runat="server" CssClass="invalid-feedback"></asp:Label>
    </div>
    <asp:Button runat="server" CssClass="button" ID="signupButton" Text="Signup" OnClick="signupButton_Click"/>
    <div class="signup">
        <span class="signup">Already have an account?<a href="Login.aspx">Login</a></span>
    </div>
    <script>
        const signupButton = document.getElementById("<%= signupButton.ClientID %>");
        const nameTextBox = document.getElementById("<%= nameTextBox.ClientID %>");
        const emailTextBox = document.getElementById("<%= emailTextBox.ClientID %>");
        const passwordTextBox = document.getElementById("<%= passwordTextBox.ClientID %>");
        const confirmPasswordTextBox = document.getElementById("<%= confirmPasswordTextBox.ClientID %>");
        const feedbackName = document.getElementById("<%= feedbackName.ClientID %>");
        const feedbackEmail = document.getElementById("<%= feedbackEmail.ClientID %>");
        const feedbackPassword = document.getElementById("<%= feedbackPassword.ClientID %>");
        const feedbackConfirmPassword = document.getElementById("<%= feedbackConfirmPassword.ClientID %>");
        const EMAIL_REGEX = new RegExp("^(?!\\.)[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$(?<!\\.)");
        const NAME_REGEX = new RegExp("^(?<!\\.)^[a-zA-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂẾưăạảấầẩẫậắằẳẵặẹẻẽềềểếỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ\\s]*$(?<!\\.)");

        signupButton.onclick = async function validateForm() {
            let valid = true;
            if (!nameTextBox.value.trim()) {
                valid = false;
                nameTextBox.classList.add("is-invalid");
                feedbackName.innerText = "Please enter your name";
            }
            else if (!NAME_REGEX.test(nameTextBox.value.trim())) {
                valid = false;
                nameTextBox.classList.add("is-invalid");
                feedbackName.innerText = "Invalid your name";
            }
            else {
                nameTextBox.classList.remove("is-invalid");
                feedbackName.innerText = null;
            }
            if (!emailTextBox.value.trim()) {
                valid = false;
                emailTextBox.classList.add("is-invalid");
                feedbackEmail.innerText = "Please enter your email";
            }
            else if (!EMAIL_REGEX.test(emailTextBox.value.trim())) {
                valid = false;
                emailTextBox.classList.add("is-invalid");
                feedbackEmail.innerText = "Invalid email";
            }
            else {
                let result = await checkcheckEmailAlreadyExists(emailTextBox.value);
                let exists = JSON.parse(result.d).exists;
                if (exists) {
                    valid = false;
                    emailTextBox.classList.add("is-invalid");
                    feedbackEmail.innerText = "Email already exists";
                }
                else {
                    emailTextBox.classList.remove("is-invalid");
                    feedbackEmail.innerText = null;
                }
            }
            if (!passwordTextBox.value.trim()) {
                valid = false;
                passwordTextBox.classList.add("is-invalid");
                feedbackPassword.innerText = "Please enter your password";
            }
            else {
                passwordTextBox.classList.remove("is-invalid");
                feedbackPassword.innerText = null;
            }
            if (!confirmPasswordTextBox.value.trim() || passwordTextBox.value.trim() != confirmPasswordTextBox.value.trim()) {
                valid = false;
                confirmPasswordTextBox.classList.add("is-invalid");
                feedbackConfirmPassword.innerText = "Invalid confirm password";
            }
            else {
                confirmPasswordTextBox.classList.remove("is-invalid");
                feedbackConfirmPassword.innerText = null;
            }
            return valid;
        }

        async function checkcheckEmailAlreadyExists(email) {
            const body = {
                "email": email
            };
            try {
                return await methodPost('SignUp.aspx/checkEmailAlreadyExists', body);
            }
            catch (err) {
                console.error("error request ajax", err);
                return false;
            }
        }
    </script>
</asp:Content>
