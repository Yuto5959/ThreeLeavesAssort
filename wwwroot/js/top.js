document.addEventListener('DOMContentLoaded', () => {
    const menuToggle = document.querySelector('.menu-toggle');
    const menu = document.querySelector('#menu');
    const modalNotice = document.querySelector('#noticeModal');
    const closeModalNotice = document.querySelector('#noticeModal .close-modal');
    const noticeLink = document.querySelector('a[data-modal="notice"]');
    const modalPenName = document.querySelector('#penNameModal');
    const closeModalPenName = document.querySelector('#penNameModal .close-modal');
    const penNameLink = document.querySelector('a[data-modal="penName"]');
    const setPenNameBtn = document.querySelector('#setPenNameBtn');
    const cancelPenNameBtn = document.querySelector('#cancelPenNameBtn');
    const penNameInput = document.querySelector('#penNameInput');

    // 初期値の設定
    if (!localStorage.getItem('penName')) {
        localStorage.setItem('penName', '無名');
    }
    penNameInput.value = localStorage.getItem('penName') || '無名';

    if (menuToggle && menu) {
        // メニューを開閉
        menuToggle.addEventListener('click', () => {
            menu.classList.toggle('active');
        });

        // メニュー外をクリックしたときに閉じる
        document.addEventListener('click', (event) => {
            if (menu.classList.contains('active') && !menu.contains(event.target) && event.target !== menuToggle) {
                menu.classList.remove('active');
            }
            if (modalNotice && modalNotice.classList.contains('active') && !modalNotice.contains(event.target) && event.target !== noticeLink) {
                modalNotice.classList.remove('active');
            }
            if (modalPenName && modalPenName.classList.contains('active') && !modalPenName.contains(event.target) && event.target !== penNameLink) {
                modalPenName.classList.remove('active');
            }
        });
    }

    // 注意事項モーダル
    if (modalNotice && closeModalNotice && noticeLink) {
        noticeLink.addEventListener('click', (event) => {
            event.preventDefault();
            modalNotice.classList.add('active');
        });

        closeModalNotice.addEventListener('click', () => {
            modalNotice.classList.remove('active');
        });
    }

    // ペンネームモーダル
    if (modalPenName && closeModalPenName && penNameLink && setPenNameBtn && cancelPenNameBtn && penNameInput) {
        penNameLink.addEventListener('click', (event) => {
            event.preventDefault();
            modalPenName.classList.add('active');
            penNameInput.value = localStorage.getItem('penName') || '無名';
        });

        closeModalPenName.addEventListener('click', () => {
            modalPenName.classList.remove('active');
        });

        setPenNameBtn.addEventListener('click', () => {
            const penName = penNameInput.value.trim();
            if (penName) {
                localStorage.setItem('penName', penName);
                modalPenName.classList.remove('active');
                alert('ペンネームが設定されました: ' + penName); // フィードバック
            } else {
                alert('ペンネームを入力してください。');
            }
        });

        cancelPenNameBtn.addEventListener('click', () => {
            modalPenName.classList.remove('active');
            penNameInput.value = localStorage.getItem('penName') || '無名';
        });
    }
});