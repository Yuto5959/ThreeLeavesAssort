// メニュー開閉
document.querySelector('.menu-toggle').addEventListener('click', () => {
    document.getElementById('menu').classList.toggle('active');
});

// モーダル開閉
document.querySelectorAll('.modal-link').forEach(link => {
    link.addEventListener('click', (e) => {
        e.preventDefault();
        const modalId = link.getAttribute('data-modal') + '-modal';
        document.getElementById(modalId).classList.add('active');
    });
});

document.querySelectorAll('.close-modal').forEach(button => {
    button.addEventListener('click', () => {
        button.closest('.modal').classList.remove('active');
    });
});