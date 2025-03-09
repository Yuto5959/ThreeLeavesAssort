document.addEventListener('DOMContentLoaded', () => {
    const canvas = document.getElementById('scrapCanvas');
    const ctx = canvas.getContext('2d');
    const textInput = document.getElementById('textInput');
    const textLength = document.getElementById('textLength');
    const templateItems = document.querySelectorAll('.template-item');
    const orientationItems = document.querySelectorAll('.orientation-item');
    const registerButton = document.getElementById('registerButton');

    let isCanvasUpdated = false;

    const templates = {
        headline: { font: 'bold 20px Impact', fgColor: '#ffffff', bgColor: '#000000' },
        subhead: { font: 'bold 15px Arial', fgColor: '#333333', bgColor: '#f5f5f5' },
        smallhead: { font: 'bold 12px Arial', fgColor: '#555555', bgColor: '#ffffff' },
        body1: { font: '12px Times New Roman', fgColor: '#000000', bgColor: '#f5f5f5' },
        body2: { font: '12px MS Mincho', fgColor: '#000000', bgColor: '#ffffff' },
        body3: { font: '12px Sawarabi Mincho, Noto Serif JP', fgColor: '#000000', bgColor: '#fff5e6' },
        ad1: { font: '12px Comic Sans MS', fgColor: '#ff4500', bgColor: '#fff0f5' },
        ad2: { font: '14px Verdana', fgColor: '#ff00ff', bgColor: '#e6e6fa' },
        ad3: { font: '16px Arial', fgColor: '#00ff00', bgColor: '#f0fff0' },
        pop: { font: 'bold 15px Impact, Arial Black', fgColor: '#ffff00', bgColor: '#ff0000' }, // ポップ: 赤背景、黄色太文字
        horror: { font: 'bold 12px Creepster, Impact', fgColor: '#ff0000', bgColor: '#000000' },
        textbook: { font: '12px Noto Serif JP', fgColor: '#000080', bgColor: '#f0f8ff' } // 教科書: Noto Serif JP
    };

    let selectedTemplate = 'headline';
    let selectedOrientation = 'horizontal';
    templateItems.forEach(item => {
        if (item.dataset.template === selectedTemplate) item.classList.add('active');
        item.addEventListener('click', () => {
            templateItems.forEach(i => i.classList.remove('active'));
            item.classList.add('active');
            selectedTemplate = item.dataset.template;
            drawText();
        });
    });
    orientationItems.forEach(item => {
        if (item.dataset.orientation === selectedOrientation) item.classList.add('active');
        item.addEventListener('click', () => {
            orientationItems.forEach(i => i.classList.remove('active'));
            item.classList.add('active');
            selectedOrientation = item.dataset.orientation;
            drawText();
        });
    });

    // 文字入力後に文字数を自動カウント
    textInput.addEventListener('input', () => {
        const text = textInput.value || '';
        textLength.value = Math.min(text.length, 9);
        drawText();
    });

    // 文字数の入力制限（4～9）
    textLength.addEventListener('input', () => {
        const length = parseInt(textLength.value);
        if (isNaN(length) || length < 4 || length > 9) {
            alert('文字数は4～9の範囲で入力してください。');
            textLength.value = Math.max(4, Math.min(9, textInput.value.length || 4));
        }
    });

    function drawText() {
        ctx.clearRect(0, 0, canvas.width, canvas.height);
        ctx.fillStyle = '#ffffff'; // キャンバス背景を白に
        ctx.fillRect(0, 0, canvas.width, canvas.height);

        const text = textInput.value || 'サンプル';
        const font = templates[selectedTemplate].font;
        const fgColor = templates[selectedTemplate].fgColor;
        const bgColor = templates[selectedTemplate].bgColor;

        ctx.font = font;
        const textHeight = parseInt(font.match(/\d+/)[0]) || 12; // フォントサイズ取得

        if (selectedOrientation === 'vertical') {
            const charWidth = 15;
            const totalHeight = text.length * 15; // 全体の高さ
            const startX = canvas.width / 2 - charWidth / 2;
            const startY = canvas.height / 2 - totalHeight / 2 + 30; // さらに10px下げて合計30px

            // 背景を描画（枠線なし）
            ctx.fillStyle = bgColor;
            ctx.fillRect(
                startX - 5,           // 左端
                startY - 5,           // 上端
                charWidth + 10,       // 幅 + 余白
                totalHeight + 10      // 高さ + 余白
            );

            // 文字を描画
            ctx.fillStyle = fgColor;
            for (let i = 0; i < text.length; i++) {
                ctx.fillText(text[i], startX, startY + i * 15 + (textHeight / 2));
            }
        } else {
            const metrics = ctx.measureText(text);
            const textWidth = metrics.width;

            // 背景を描画（枠線なし）
            ctx.fillStyle = bgColor;
            ctx.fillRect(
                canvas.width / 2 - textWidth / 2 - 5,  // 左端
                canvas.height / 2 - textHeight / 2 - 5, // 上端
                textWidth + 10,                        // 幅 + 余白
                textHeight + 10                        // 高さ + 余白
            );

            // 文字を描画
            ctx.fillStyle = fgColor;
            ctx.fillText(text, canvas.width / 2 - textWidth / 2, canvas.height / 2 + (textHeight / 2));
        }
        isCanvasUpdated = true;
        registerButton.disabled = !isCanvasUpdated;
    }

    registerButton.addEventListener('click', () => {
        if (!isCanvasUpdated) return;

        if (confirm('登録しますか？')) {
            const text = textInput.value || '無題';
            const length = parseInt(textLength.value) || text.length;
            const font = templates[selectedTemplate].font;
            const fgColor = templates[selectedTemplate].fgColor;
            const bgColor = templates[selectedTemplate].bgColor;
            const isVertical = selectedOrientation === 'vertical';

            const now = new Date();
            const id = `${now.getFullYear()}${String(now.getMonth() + 1).padStart(2, '0')}${String(now.getDate()).padStart(2, '0')}${String(now.getHours()).padStart(2, '0')}${String(now.getMinutes()).padStart(2, '0')}${String(now.getSeconds()).padStart(2, '0')}-${Math.random().toString(36).substring(2, 8).toUpperCase()}`;

            fetch('/Home/RegisterScrap', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({
                    id: id,
                    text: text,
                    length: length,
                    font: font,
                    fgColor: fgColor,
                    bgColor: bgColor,
                    isVertical: isVertical
                })
            })
                .then(response => response.json())
                .then(data => {
                    if (data.success) {
                        alert('登録が完了しました。');
                        isCanvasUpdated = false;
                        registerButton.disabled = true;
                    } else {
                        alert('登録に失敗しました。');
                    }
                })
                .catch(error => console.error('Error:', error));
        }
    });

    templateItems.forEach(item => item.addEventListener('click', drawText));
    orientationItems.forEach(item => item.addEventListener('click', drawText));

    drawText();
});