// Home Hero Banner JavaScript
// File: wwwroot/js/home-hero.js

document.addEventListener('DOMContentLoaded', function () {
    // Initialize carousel
    initializeCarousel();

    // Add button effects
    addButtonEffects();

    // Add ripple effects
    addRippleEffects();
});

// Initialize Bootstrap Carousel
function initializeCarousel() {
    const carouselElement = document.getElementById('promoCarousel');
    if (carouselElement) {
        const carousel = new bootstrap.Carousel(carouselElement, {
            interval: 5000,
            wrap: true,
            touch: true
        });

        // Add pause on hover
        carouselElement.addEventListener('mouseenter', function () {
            carousel.pause();
        });

        carouselElement.addEventListener('mouseleave', function () {
            carousel.cycle();
        });
    }
}

// Add hover effects to buttons
function addButtonEffects() {
    const buttons = document.querySelectorAll('.cta-button');

    buttons.forEach(button => {
        // Mouse enter effect
        button.addEventListener('mouseenter', function () {
            this.style.transform = 'translateY(-3px) scale(1.05)';
            this.style.transition = 'all 0.3s ease';
        });

        // Mouse leave effect
        button.addEventListener('mouseleave', function () {
            this.style.transform = 'translateY(0) scale(1)';
        });

        // Focus effect for accessibility
        button.addEventListener('focus', function () {
            this.style.boxShadow = '0 0 0 3px rgba(255, 215, 0, 0.4)';
        });

        button.addEventListener('blur', function () {
            this.style.boxShadow = '0 8px 20px rgba(0,0,0,0.2)';
        });
    });
}

// Add ripple effect to CTA buttons
function addRippleEffects() {
    const buttons = document.querySelectorAll('.cta-button');

    buttons.forEach(button => {
        button.addEventListener('click', function (e) {
            // Create ripple element
            const ripple = createRipple(e, this);

            // Add ripple to button
            this.style.position = 'relative';
            this.style.overflow = 'hidden';
            this.appendChild(ripple);

            // Remove ripple after animation
            setTimeout(() => {
                if (ripple.parentNode) {
                    ripple.remove();
                }
            }, 600);
        });
    });
}

// Create ripple element
function createRipple(event, element) {
    const ripple = document.createElement('span');
    const rect = element.getBoundingClientRect();
    const size = Math.max(rect.width, rect.height);
    const x = event.clientX - rect.left - size / 2;
    const y = event.clientY - rect.top - size / 2;

    ripple.style.cssText = `
        position: absolute;
        border-radius: 50%;
        background: rgba(255, 255, 255, 0.6);
        transform: scale(0);
        animation: ripple 0.6s linear;
        left: ${x}px;
        top: ${y}px;
        width: ${size}px;
        height: ${size}px;
        pointer-events: none;
    `;

    return ripple;
}

// Add CSS for ripple animation if not already present
function addRippleCSS() {
    if (!document.querySelector('#ripple-style')) {
        const style = document.createElement('style');
        style.id = 'ripple-style';
        style.textContent = `
            @keyframes ripple {
                to {
                    transform: scale(4);
                    opacity: 0;
                }
            }
        `;
        document.head.appendChild(style);
    }
}

// Initialize ripple CSS
addRippleCSS();

// Feature card animations
function initializeFeatureCards() {
    const cards = document.querySelectorAll('.feature-card');

    cards.forEach((card, index) => {
        // Stagger animation on load
        card.style.opacity = '0';
        card.style.transform = 'translateY(20px)';

        setTimeout(() => {
            card.style.transition = 'all 0.6s ease';
            card.style.opacity = '1';
            card.style.transform = 'translateY(0)';
        }, index * 150);

        // Add hover glow effect
        card.addEventListener('mouseenter', function () {
            this.style.boxShadow = '0 15px 35px rgba(255, 215, 0, 0.3)';
        });

        card.addEventListener('mouseleave', function () {
            this.style.boxShadow = 'none';
        });
    });
}

// Initialize feature cards when DOM is ready
document.addEventListener('DOMContentLoaded', function () {
    setTimeout(initializeFeatureCards, 500);
});

// Smooth scroll for anchor links
function initializeSmoothScroll() {
    const links = document.querySelectorAll('a[href^="#"]');

    links.forEach(link => {
        link.addEventListener('click', function (e) {
            e.preventDefault();
            const target = document.querySelector(this.getAttribute('href'));

            if (target) {
                target.scrollIntoView({
                    behavior: 'smooth',
                    block: 'start'
                });
            }
        });
    });
}

// Initialize smooth scroll
document.addEventListener('DOMContentLoaded', initializeSmoothScroll);

// Performance optimization: Intersection Observer for animations
function initializeIntersectionObserver() {
    const observer = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add('animate-in');
            }
        });
    }, {
        threshold: 0.1,
        rootMargin: '50px'
    });

    // Observe elements that should animate on scroll
    const animateElements = document.querySelectorAll('.feature-card, .promo-banner, .product-banner');
    animateElements.forEach(el => observer.observe(el));
}

// Initialize intersection observer if supported
if ('IntersectionObserver' in window) {
    document.addEventListener('DOMContentLoaded', initializeIntersectionObserver);
}