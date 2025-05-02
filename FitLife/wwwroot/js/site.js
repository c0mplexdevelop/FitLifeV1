let togglePasswordVisibility = () => {
    const passwordField = document.getElementById('password');
    const passwordToggleVisibilityButton = document.getElementById('password-visibility-button');

    if (passwordField.type === 'password') {
        passwordField.type = 'text';
        passwordToggleVisibilityButton.classList.remove('fa-eye-slash');
        passwordToggleVisibilityButton.classList.add('fa-eye');
    } else {
        passwordField.type = 'password';
        passwordToggleVisibilityButton.classList.remove('fa-eye');
        passwordToggleVisibilityButton.classList.add('fa-eye-slash');
    }
}




//FOR CLOSING LOG OUT VALIDATION INTERFACE
let closeLogoutValidation = () => {
    const validationContainer = document.getElementById("logout-validation");
    if (validationContainer) {
        validationContainer.classList.remove("overlay");
        validationContainer.classList.add("hidden");

        window.location.href = "/login";
    }

}

//FOR CLOSING LOG IN VALIDATION INTERFACE
let closeLogInValidation = () => {
    const validationInterface = document.getElementById("login-validation");
    validationInterface.classList.remove("overlay");
    validationInterface.classList.add("hidden");

    window.location.href = "/login";
}

//FOR CLOSING REGISTERED SUCCESSFULLY IN SIGN UP INTERFACE
let registeredSuccessfully = () => {
    const successInterface = document.getElementById("signup-successfully");
    successInterface.classList.remove("overlay");
    successInterface.classList.add("hidden");

    window.location.href = "/";
}