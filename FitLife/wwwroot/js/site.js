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