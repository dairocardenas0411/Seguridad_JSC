
        // Mejorar la experiencia de usuario
    document.addEventListener('DOMContentLoaded', function () {
            // Validación en tiempo real
            const inputs = document.querySelectorAll('input[type="text"], input[type="email"], input[type="number"], textarea');

            inputs.forEach(input => {
        input.addEventListener('blur', function () {
            if (this.value.trim() !== '') {
                this.parentElement.classList.add('valid');
            } else {
                this.parentElement.classList.remove('valid');
            }
        });

    // Efecto de focus mejorado
    input.addEventListener('focus', function () {
        this.parentElement.querySelector('label').style.color = '#22c55e';
                });

    input.addEventListener('blur', function () {
        this.parentElement.querySelector('label').style.color = '#ffffff';
                });
            });

    // Efecto de loading en el botón
    const submitBtn = document.querySelector('.registro-btn');
    if (submitBtn) {
        submitBtn.addEventListener('click', function () {
            this.classList.add('loading');
            this.innerHTML = 'Procesando...';
        });
            }

    // Animación de entrada para el formulario
    const formContainer = document.getElementById('formContainer');
    formContainer.style.opacity = '0';
    formContainer.style.transform = 'translateY(50px)';

            setTimeout(() => {
        formContainer.style.transition = 'all 0.8s cubic-bezier(0.4, 0, 0.2, 1)';
    formContainer.style.opacity = '1';
    formContainer.style.transform = 'translateY(0)';
            }, 200);
        });
