document.addEventListener('DOMContentLoaded', () => {
    const menuToggle = document.querySelector('.menu-toggle');
    const menu = document.querySelector('#menu');

    if (menuToggle && menu) {
        // メニューを開閉するイベント
        menuToggle.addEventListener('click', () => {
            menu.classList.toggle('active');
        });

        // メニュー外をクリックしたときに閉じる
        document.addEventListener('click', (event) => {
            if (menu.classList.contains('active') && !menu.contains(event.target) && event.target !== menuToggle) {
                menu.classList.remove('active');
            }
        });
    } else {
        console.error('menuToggle or menu element not found');
    }
});
document.addEventListener('touchend', (event) => {
    if (menu.classList.contains('active') && !menu.contains(event.target) && event.target !== menuToggle) {
        menu.classList.remove('active');
    }
});