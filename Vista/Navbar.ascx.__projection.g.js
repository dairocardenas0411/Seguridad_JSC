
/* BEGIN EXTERNAL SOURCE */

    document.addEventListener("DOMContentLoaded", function () {
        document.getElementById("togglePassword").addEventListener("click", function () {
            var passwordInput = document.getElementById('/*************************/');
            var icon = this.querySelector("i");

            if (passwordInput.type === "password") {
                passwordInput.type = "text";
                icon.classList.remove("bi-eye-slash-fill");
                icon.classList.add("bi-eye-fill");
            } else {
                passwordInput.type = "password";
                icon.classList.remove("bi-eye-fill");
                icon.classList.add("bi-eye-slash-fill");
            }
        });
    });

/* END EXTERNAL SOURCE */
