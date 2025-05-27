<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Seguridad_JSC.index" %>
<%@ Register Src="~/Vista/Navbar.ascx" TagPrefix="uc" TagName="Navbar" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="description" content="SEGURIDAD JSC - Especialistas en sistemas de seguridad, cámaras de vigilancia e instalaciones profesionales" />
    <title>SEGURIDAD JSC - Sistemas de Seguridad Profesional</title>
    
    <!-- Favicon -->
    <link rel="icon" type="image/x-icon" href="../Vista/Recursos/Icon-1.jpg" />
    
    <!-- CSS Libraries -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons/font/bootstrap-icons.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/aos/2.3.4/aos.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.2/css/all.min.css" rel="stylesheet" />
    
    <!-- Custom CSS -->

</head>

<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
        <uc:Navbar ID="NavbarControl" runat="server" />

        <!-- Hero Section -->
        <section class="hero-section">
            <div class="hero-content" data-aos="fade-up" data-aos-duration="1000">
                <h1 class="hero-title">SEGURIDAD JSC</h1>
                <p class="hero-subtitle">Protegemos lo que más valoras con tecnología de vanguardia y servicio profesional</p>
                <div class="hero-cta">
                    <a href="/Vista/Productos.aspx" class="btn btn-primary-custom">
                        <i class="bi bi-camera me-2"></i>Ver Productos
                    </a>
                    <a href="/Vista/Cotizacion.aspx" class="btn-outline-custom">
                        <i class="bi bi-calculator me-2"></i>Cotizar Ahora
                    </a>
                </div>
            </div>
        </section>

        <!-- Services Section -->
        <section class="services-section">
            <div class="container">
                <div class="section-title" data-aos="fade-up">
                    <h2>Nuestros Servicios</h2>
                    <p>Soluciones integrales de seguridad adaptadas a tus necesidades</p>
                </div>
                <div class="row g-4">
                    <div class="col-lg-4 col-md-6" data-aos="fade-up" data-aos-delay="100">
                        <div class="service-card">
                            <div class="service-icon">
                                <i class="bi bi-camera-video"></i>
                            </div>
                            <h4>Cámaras de Seguridad</h4>
                            <p>Sistemas de videovigilancia de última generación con calidad HD y visión nocturna para máxima protección.</p>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-6" data-aos="fade-up" data-aos-delay="200">
                        <div class="service-card">
                            <div class="service-icon">
                                <i class="bi bi-tools"></i>
                            </div>
                            <h4>Instalación Profesional</h4>
                            <p>Técnicos certificados que garantizan una instalación segura y eficiente de todos nuestros sistemas.</p>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-6" data-aos="fade-up" data-aos-delay="300">
                        <div class="service-card">
                            <div class="service-icon">
                                <i class="bi bi-headset"></i>
                            </div>
                            <h4>Soporte 24/7</h4>
                            <p>Asistencia técnica continua y mantenimiento preventivo para asegurar el funcionamiento óptimo.</p>
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <!-- About Section -->
        <section class="about-section">
            <div class="container">
                <div class="row align-items-center">
                    <div class="col-lg-6" data-aos="fade-right">
                        <div class="about-text">
                            <h2>¿Quiénes Somos?</h2>
                            <p>
                                SEGURIDAD JSC S.A.S. es una empresa líder especializada en la instalación y venta de cámaras de seguridad, 
                                sistemas de vigilancia y equipos de monitoreo de alta tecnología.
                            </p>
                            <p>
                                Con años de experiencia en el mercado, nos hemos consolidado como la opción preferida para hogares, 
                                empresas y establecimientos comerciales que buscan tranquilidad y protección integral.
                            </p>
                            <p>
                                Nuestro compromiso es brindar soluciones personalizadas que se adapten a las necesidades específicas 
                                de cada cliente, utilizando tecnología de vanguardia y los más altos estándares de calidad.
                            </p>
                        </div>
                    </div>
                    <div class="col-lg-6" data-aos="fade-left">
                        <div class="about-image">
                            <img src="../Vista/Recursos/nosotros.jpg" alt="Equipo SEGURIDAD JSC" />
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <!-- Stats Section -->
        <section class="stats-section">
            <div class="container">
                <div class="row">
                    <div class="col-lg-3 col-md-6" data-aos="fade-up" data-aos-delay="100">
                        <div class="stat-item">
                            <span class="stat-number" data-count="500">0</span>
                            <span class="stat-label">Proyectos Completados</span>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-6" data-aos="fade-up" data-aos-delay="200">
                        <div class="stat-item">
                            <span class="stat-number" data-count="350">0</span>
                            <span class="stat-label">Clientes Satisfechos</span>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-6" data-aos="fade-up" data-aos-delay="300">
                        <div class="stat-item">
                            <span class="stat-number" data-count="5">0</span>
                            <span class="stat-label">Años de Experiencia</span>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-6" data-aos="fade-up" data-aos-delay="400">
                        <div class="stat-item">
                            <span class="stat-number" data-count="24">0</span>
                            <span class="stat-label">Soporte Horas</span>
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <!-- CTA Section -->
        <section class="cta-section">
            <div class="container">
                <div class="cta-content" data-aos="fade-up">
                    <h2>¿Listo para Proteger tu Propiedad?</h2>
                    <p>Contáctanos hoy mismo y recibe una cotización personalizada sin compromiso</p>
                    <div class="hero-cta">
                        <a href="/Vista/Cotizacion.aspx" class="btn btn-primary-custom btn-lg">
                            <i class="bi bi-telephone me-2"></i>Solicitar Cotización
                        </a>
                        <a href="https://wa.me/3138797934" target="_blank" class="btn-outline-custom">
                            <i class="fab fa-whatsapp me-2"></i>WhatsApp
                        </a>
                    </div>
                </div>
            </div>
        </section>

        <!-- Social Buttons -->
        <div class="social-buttons">
            <a href="https://wa.me/3138797934" target="_blank" class="social-btn whatsapp-btn" title="WhatsApp">
                <i class="fab fa-whatsapp"></i>
            </a>
            <a href="https://www.facebook.com/share/15n6DbrzgX/" target="_blank" class="social-btn facebook-btn" title="Facebook">
                <i class="fab fa-facebook-f"></i>
            </a>
        </div>

        <!-- Scroll to Top Button -->
        <button class="scroll-top" id="scrollTop" title="Ir arriba">
            <i class="bi bi-arrow-up"></i>
        </button>

        <asp:HiddenField ID="hfUserRole" runat="server" />
    </form>

    <!-- JavaScript Libraries -->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/aos/2.3.4/aos.js"></script>

    <!-- Custom JavaScript -->
    <script>
        // Initialize AOS (Animate On Scroll)
        AOS.init({
            duration: 1000,
            once: true,
            offset: 100
        });

        // Counter Animation
        function animateCounters() {
            const counters = document.querySelectorAll('.stat-number');
            counters.forEach(counter => {
                const target = parseInt(counter.getAttribute('data-count'));
                let count = 0;
                const increment = target / 100;

                const updateCounter = () => {
                    if (count < target) {
                        count += increment;
                        counter.textContent = Math.ceil(count);
                        setTimeout(updateCounter, 20);
                    } else {
                        counter.textContent = target;
                    }
                };
                updateCounter();
            });
        }

        // Intersection Observer for counter animation
        const statsSection = document.querySelector('.stats-section');
        const statsObserver = new IntersectionObserver((entries) => {
            entries.forEach(entry => {
                if (entry.isIntersecting) {
                    animateCounters();
                    statsObserver.unobserve(entry.target);
                }
            });
        });

        if (statsSection) {
            statsObserver.observe(statsSection);
        }

        // Scroll to top functionality
        const scrollTopBtn = document.getElementById('scrollTop');

        window.addEventListener('scroll', () => {
            if (window.pageYOffset > 300) {
                scrollTopBtn.classList.add('visible');
            } else {
                scrollTopBtn.classList.remove('visible');
            }
        });

        scrollTopBtn.addEventListener('click', () => {
            window.scrollTo({
                top: 0,
                behavior: 'smooth'
            });
        });

        // Prevent back button (if needed)
        window.history.forward();
        function preventBack() {
            window.history.forward();
        }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };

        // Smooth scrolling for anchor links
        document.querySelectorAll('a[href^="#"]').forEach(anchor => {
            anchor.addEventListener('click', function (e) {
                e.preventDefault();
                const target = document.querySelector(this.getAttribute('href'));
                if (target) {
                    target.scrollIntoView({ behavior: 'smooth' });
                }
            });
        });
    </script>
</body>
</html>
