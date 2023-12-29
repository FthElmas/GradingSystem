const showHiddenPass = (loginPass, loginEye) => {
    const input = document.getElementById(loginPass),
        iconEye = document.getElementById(loginEye)

    iconEye.addEventListener('click', () => {
        // Change password to text
        if (input.type === 'password') {
            // Switch to text
            input.type = 'text'

            // Icon change
            iconEye.classList.add('ri-eye-line')
            iconEye.classList.remove('ri-eye-off-line')
        } else {
            // Change to password
            input.type = 'password'

            // Icon change
            iconEye.classList.remove('ri-eye-line')
            iconEye.classList.add('ri-eye-off-line')
        }
    })
}

showHiddenPass('login-pass', 'login-eye')

function toggleForm(formType) {
    var loginForm = document.getElementById('login-form');
    var registerForm = document.getElementById('register-form');
    var forgotPasswordForm = document.getElementById('forgot-password-form');

    if (formType === 'register') {
        loginForm.style.display = 'none';
        registerForm.style.display = 'block';
        forgotPasswordForm.style.display = 'none';
    } else if (formType === 'forgot-password') {
        loginForm.style.display = 'none';
        registerForm.style.display = 'none';
        forgotPasswordForm.style.display = 'block';
    } else {
        loginForm.style.display = 'block';
        registerForm.style.display = 'none';
        forgotPasswordForm.style.display = 'none';
    }
}