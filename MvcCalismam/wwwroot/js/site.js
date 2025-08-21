(function () {
    // === Dark Mode Toggle ===
    const key = "theme";
    const mq = window.matchMedia("(prefers-color-scheme: dark)");
    const root = document.documentElement;

    function apply(t) { root.classList.toggle("dark", t === "dark"); }

    let theme = localStorage.getItem(key) || (mq.matches ? "dark" : "light");
    apply(theme);

    function onPrefChange(e) {
        if (!localStorage.getItem(key)) {
            theme = e.matches ? "dark" : "light";
            apply(theme);
        }
    }
    if (mq.addEventListener) mq.addEventListener("change", onPrefChange);
    else if (mq.addListener) mq.addListener(onPrefChange);

    document.addEventListener("DOMContentLoaded", function () {
        const btn = document.getElementById("themeToggle");
        if (btn) {
            const setIcon = () => btn.textContent = root.classList.contains("dark") ? "☀️" : "🌙";
            setIcon();
            btn.addEventListener("click", () => {
                theme = root.classList.contains("dark") ? "light" : "dark";
                localStorage.setItem(key, theme);
                apply(theme);
                setIcon();
            });
        }
    });
})();

// === Password show/hide + strength meter ===
(function () {
    document.addEventListener('DOMContentLoaded', () => {
        const inp = document.getElementById('password');
        const btn = document.getElementById('togglePass');
        const bar = document.getElementById('passBar');
        const txt = document.getElementById('passText');
        if (!inp || !btn || !bar) return;

        btn.addEventListener('click', () => {
            inp.type = inp.type === 'password' ? 'text' : 'password';
            btn.textContent = inp.type === 'password' ? 'Göster' : 'Gizle';
        });

        const score = (s) => {
            let v = 0; if (!s) return 0;
            if (s.length >= 8) v++;
            if (/[A-Z]/.test(s)) v++;
            if (/[a-z]/.test(s)) v++;
            if (/[0-9]/.test(s)) v++;
            if (/[^A-Za-z0-9]/.test(s)) v++;
            return v;
        };

        inp.addEventListener('input', () => {
            const v = score(inp.value);
            const w = Math.floor(v / 5 * 100);
            bar.style.width = w + '%';
            bar.className = '';
            bar.classList.add(v < 3 ? 'pass-weak' : (v < 4 ? 'pass-mid' : 'pass-strong'));
            if (txt) txt.textContent = v < 3 ? 'Zayıf' : (v < 4 ? 'Orta' : 'Güçlü');
        });
    });
})();

// === Live validation for TC (11 digits) and Email ===
(function () {
    document.addEventListener('DOMContentLoaded', () => {
        const tc = document.getElementById('Tc');
        const em = document.getElementById('Email');
        if (tc) {
            tc.addEventListener('input', () => {
                const ok = /^\d{11}$/.test(tc.value);
                tc.classList.toggle('is-invalid', !ok);
                tc.classList.toggle('is-valid', ok);
            });
        }
        if (em) {
            em.addEventListener('input', () => {
                const ok = /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(em.value);
                em.classList.toggle('is-invalid', !ok);
                em.classList.toggle('is-valid', ok);
            });
        }
    });
})();
