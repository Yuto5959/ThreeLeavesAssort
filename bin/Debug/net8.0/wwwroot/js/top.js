document.addEventListener('DOMContentLoaded', () => {
    const menuToggle = document.querySelector('.menu-toggle');
    const menu = document.querySelector('.menu');
    const modalLinks = document.querySelectorAll('.modal-link');
    const closeModals = document.querySelectorAll('.close-modal');
    const modals = document.querySelectorAll('.modal');

    menuToggle.addEventListener('click', () => {
        menu.classList.toggle('active');
    });

    modalLinks.forEach(link => {
        link.addEventListener('click', (e) => {
            e.preventDefault();
            const modalId = link.getAttribute('data-modal');
            document.getElementById(`${modalId}-modal`).classList.add('active');
        });
    });

    closeModals.forEach(closeBtn => {
        closeBtn.addEventListener('click', () => {
            modals.forEach(modal => modal.classList.remove('active'));
        });
    });

    // モーダル外クリックで閉じる
    window.addEventListener('click', (e) => {
        if (!e.target.closest('.modal-content') && !e.target.closest('.modal-link')) {
            modals.forEach(modal => modal.classList.remove('active'));
        }
    });
});