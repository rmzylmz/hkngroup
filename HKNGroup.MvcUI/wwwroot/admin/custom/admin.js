/**
 * HKN Group - Custom Admin JavaScript Engine
 * 100% Vanilla JS - Replaces jQuery, Bootstrap JS, MetisMenu, Simplebar, SweetAlert2
 */

(function () {
    "use strict";

    window.AdminUI = {
        init: function () {
            this.bindSidebar();
            this.bindAccordions();
            this.bindActiveMenu();
            this.bindDropdowns();
            this.createModalDOM();
            this.createToastDOM();
            this.initFileUploads();
            this.initQuillEditors();
            this.initFormSubmitProtection();
        },

        initFileUploads: function () {
            // Interactive Dropzones
            const dropzones = document.querySelectorAll('.file-dropzone');
            dropzones.forEach(zone => {
                const input = zone.querySelector('input[type="file"]');
                const card = zone.parentElement.querySelector('.file-preview-card');
                if (!input) return;

                ['dragenter', 'dragover'].forEach(eventName => {
                    zone.addEventListener(eventName, (e) => {
                        e.preventDefault();
                        e.stopPropagation();
                        zone.classList.add('dragover');
                    }, false);
                });

                ['dragleave', 'drop'].forEach(eventName => {
                    zone.addEventListener(eventName, (e) => {
                        e.preventDefault();
                        e.stopPropagation();
                        zone.classList.remove('dragover');
                    }, false);
                });

                zone.addEventListener('drop', (e) => {
                    const dt = e.dataTransfer;
                    if (dt.files && dt.files.length > 0) {
                        input.files = dt.files;
                        input.dispatchEvent(new Event('change', { bubbles: true }));
                    }
                });

                input.addEventListener('change', function () {
                    if (this.files && this.files[0]) {
                        const file = this.files[0];
                        if (card) {
                            const thumb = card.querySelector('.file-preview-thumb');
                            const name = card.querySelector('.file-preview-name');
                            const size = card.querySelector('.file-preview-size');

                            if (name) name.textContent = file.name;
                            if (size) {
                                const sizeKb = (file.size / 1024).toFixed(1);
                                size.textContent = sizeKb > 1024 ? (sizeKb / 1024).toFixed(2) + ' MB' : sizeKb + ' KB';
                            }

                            if (thumb && file.type.startsWith('image/')) {
                                const reader = new FileReader();
                                reader.onload = (e) => { thumb.src = e.target.result; };
                                reader.readAsDataURL(file);
                            }
                            card.classList.add('show');
                        }
                    }
                });

                if (card) {
                    const removeBtn = card.querySelector('.file-preview-remove');
                    if (removeBtn) {
                        removeBtn.addEventListener('click', (e) => {
                            e.preventDefault();
                            input.value = '';
                            card.classList.remove('show');
                        });
                    }
                }
            });
        },

        initQuillEditors: function () {
            if (typeof Quill === 'undefined') return;

            // id="elm1" (eski sistem standardı), textarea.quill-editor, data-editor="quill"
            const textareas = document.querySelectorAll('textarea#elm1, textarea.quill-editor, textarea[data-editor="quill"]');
            textareas.forEach(textarea => {
                if (textarea.dataset.quillReady === "true") return;
                textarea.dataset.quillReady = "true";

                // Editör wrapper div oluştur
                const editorContainer = document.createElement('div');
                editorContainer.className = 'quill-editor-wrapper';
                textarea.parentNode.insertBefore(editorContainer, textarea);

                // Textarea'yı gizle
                textarea.style.display = 'none';

                // Quill editör örneğini başlat
                const quill = new Quill(editorContainer, {
                    theme: 'snow',
                    placeholder: textarea.getAttribute('placeholder') || 'İçeriğinizi buraya detaylı olarak yazabilir, başlıklar, listeler veya görseller ekleyebilirsiniz...',
                    modules: {
                        toolbar: [
                            [{ 'header': [1, 2, 3, 4, false] }],
                            ['bold', 'italic', 'underline', 'strike'],
                            [{ 'color': [] }, { 'background': [] }],
                            [{ 'list': 'ordered' }, { 'list': 'bullet' }],
                            [{ 'align': [] }],
                            ['blockquote', 'code-block'],
                            ['link', 'image'],
                            ['clean']
                        ]
                    }
                });

                // Başlangıç HTML içeriğini yükle
                if (textarea.value) {
                    quill.root.innerHTML = textarea.value;
                }

                // Değişiklikleri ve form gönderimini senkronize et
                const sync = function () {
                    const html = quill.root.innerHTML;
                    if (quill.getText().trim().length === 0 && !html.includes('<img')) {
                        textarea.value = '';
                    } else {
                        textarea.value = html;
                    }
                };

                quill.on('text-change', sync);

                const form = textarea.closest('form');
                if (form) {
                    form.addEventListener('submit', sync);
                }
            });
        },

                initFormSubmitProtection: function () {
            let lastClickedSubmitBtn = null;

            // Form içindeki submit butonuna tıklandığında referansını yakala
            document.addEventListener('click', function (e) {
                const btn = e.target.closest('button[type="submit"], input[type="submit"]');
                if (btn) {
                    lastClickedSubmitBtn = btn;
                }
            }, true);

            // Form submit edildiğinde çift tıklamayı engelle ve "Kaydediliyor..." durumuna geçir
            document.addEventListener('submit', function (e) {
                const form = e.target;
                if (!form || form.tagName !== 'FORM') return;

                // Form zaten gönderiliyorsa mükerrer / çift tıklamayı tamamen engelle
                if (form.dataset.submitting === "true") {
                    e.preventDefault();
                    e.stopPropagation();
                    return false;
                }

                // Tarayıcı HTML5 form doğrulaması (required, type vb.) geçersizse butonu kilitleme
                if (typeof form.checkValidity === 'function' && !form.checkValidity()) {
                    return;
                }

                // Formun gönderildiğini işaretle
                form.dataset.submitting = "true";

                // İlgili submit butonunu bul
                const submitBtn = (lastClickedSubmitBtn && lastClickedSubmitBtn.form === form)
                    ? lastClickedSubmitBtn
                    : form.querySelector('button[type="submit"], input[type="submit"]');

                if (submitBtn) {
                    // Orijinal metni ve genişliği sakla
                    submitBtn.dataset.originalHtml = submitBtn.innerHTML;
                    const currentWidth = submitBtn.offsetWidth;
                    if (currentWidth > 0) {
                        submitBtn.style.minWidth = currentWidth + 'px';
                    }

                    // Tıklamaları derhal engelle
                    submitBtn.style.pointerEvents = 'none';
                    submitBtn.classList.add('disabled');

                    // Yükleniyor metnini belirle (data-loading-text veya butona/forma göre dinamik)
                    let loadingText = submitBtn.dataset.loadingText || form.dataset.loadingText;
                    if (!loadingText) {
                        const btnText = (submitBtn.textContent || '').trim().toLowerCase();
                        const formAction = (form.getAttribute('action') || '').toLowerCase();
                        if (btnText.includes('giriş') || formAction.includes('login') || submitBtn.querySelector('.bx-log-in')) {
                            loadingText = 'Giriş yapılıyor...';
                        } else if (btnText.includes('sil')) {
                            loadingText = 'Siliniyor...';
                        } else if (btnText.includes('gönder') || btnText.includes('yayınla')) {
                            loadingText = 'Gönderiliyor...';
                        } else {
                            loadingText = 'Kaydediliyor...';
                        }
                    }

                    // Buton içeriğini dönen ikonla güncelle
                    submitBtn.innerHTML = '<i class="bx bx-loader-alt bx-spin font-size-16 align-middle me-1"></i> ' + loadingText;

                    // Tarayıcı gönderim işlemini kesintiye uğratmamak için disabled özelliğini hemen ardından ekle
                    setTimeout(function () {
                        submitBtn.disabled = true;
                    }, 10);

                    // Bağlantı kopması veya formun sayfa değiştirmemesi durumunda 20 sn sonra butonu tekrar etkinleştir
                    setTimeout(function () {
                        if (form.dataset.submitting === "true") {
                            form.dataset.submitting = "false";
                            submitBtn.disabled = false;
                            submitBtn.style.pointerEvents = '';
                            submitBtn.classList.remove('disabled');
                            if (submitBtn.dataset.originalHtml) {
                                submitBtn.innerHTML = submitBtn.dataset.originalHtml;
                            }
                        }
                    }, 20000);
                }
            }, false);

            // Tarayıcı geri/ileri (bfcache) önbelleğinden dönüldüğünde butonları sıfırla
            window.addEventListener('pageshow', function () {
                document.querySelectorAll('form[data-submitting="true"]').forEach(function (form) {
                    form.dataset.submitting = "false";
                    const btn = form.querySelector('button[type="submit"], input[type="submit"]');
                    if (btn) {
                        btn.disabled = false;
                        btn.style.pointerEvents = '';
                        btn.classList.remove('disabled');
                        if (btn.dataset.originalHtml) {
                            btn.innerHTML = btn.dataset.originalHtml;
                        }
                    }
                });
            });
        },

        toggleSidebar: function () {
            document.body.classList.toggle("sidebar-enable");
        },

        closeSidebar: function () {
            document.body.classList.remove("sidebar-enable");
        },

        bindSidebar: function () {
            const btn = document.getElementById("vertical-menu-btn");
            if (btn) {
                btn.addEventListener("click", (e) => {
                    e.preventDefault();
                    this.toggleSidebar();
                });
            }

            const overlay = document.querySelector(".rightbar-overlay");
            if (overlay) {
                overlay.addEventListener("click", () => {
                    this.closeSidebar();
                });
            }
        },

        bindAccordions: function () {
            const hasArrows = document.querySelectorAll("#sidebar-menu .has-arrow");
            hasArrows.forEach(link => {
                link.addEventListener("click", function (e) {
                    e.preventDefault();
                    this.classList.toggle("menu-open");
                    const subMenu = this.nextElementSibling;
                    if (subMenu && subMenu.classList.contains("sub-menu")) {
                        subMenu.classList.toggle("show");
                    }
                });
            });
        },

        bindActiveMenu: function () {
            const currentPath = window.location.pathname.toLowerCase().replace(/\/$/, "");
            const menuLinks = document.querySelectorAll("#sidebar-menu a");

            let bestMatch = null;
            let maxMatchLen = 0;

            menuLinks.forEach(link => {
                const href = link.getAttribute("href");
                if (!href || href === "#" || href.startsWith("javascript:")) return;

                const linkPath = new URL(link.href, window.location.origin).pathname.toLowerCase().replace(/\/$/, "");

                if (currentPath === linkPath) {
                    bestMatch = link;
                    maxMatchLen = 9999;
                } else if (linkPath !== "" && linkPath !== "/admin" && linkPath !== "/admin/home" && currentPath.startsWith(linkPath) && linkPath.length > maxMatchLen) {
                    bestMatch = link;
                    maxMatchLen = linkPath.length;
                }
            });

            // Alt sayfa veya detay durumları (Örn: /Admin/Article/Edit/5 -> /Admin/Article veya /Admin/Article/Index)
            if (!bestMatch) {
                const segments = currentPath.split("/").filter(Boolean);
                if (segments.length >= 2) {
                    const controllerSegment = segments[1]; // Örn: "article"
                    menuLinks.forEach(link => {
                        const linkPath = new URL(link.href, window.location.origin).pathname.toLowerCase();
                        const linkSegments = linkPath.split("/").filter(Boolean);
                        if (linkSegments.length >= 2 && linkSegments[1] === controllerSegment) {
                            if (!bestMatch) bestMatch = link;
                        }
                    });
                }
            }

            // Dashboard / Anasayfa kontrolü
            if (!bestMatch && (currentPath === "/admin" || currentPath === "/admin/home" || currentPath === "/admin/home/index" || currentPath === "")) {
                bestMatch = document.querySelector('#sidebar-menu a[href*="Home"]');
            }

            if (bestMatch) {
                bestMatch.classList.add("active");

                // Eğer bir alt menünün içindeyse üst menüyü aç
                const parentSubMenu = bestMatch.closest(".sub-menu");
                if (parentSubMenu) {
                    parentSubMenu.classList.add("show");
                    const parentToggle = parentSubMenu.previousElementSibling;
                    if (parentToggle && parentToggle.classList.contains("has-arrow")) {
                        parentToggle.classList.add("menu-open", "parent-active");
                    }
                }
            }
        },

        bindDropdowns: function () {
            document.addEventListener('click', function (e) {
                const toggle = e.target.closest('[data-bs-toggle="dropdown"], .dropdown-toggle');
                const openDropdowns = document.querySelectorAll('.dropdown.show, .dropdown-menu.show');

                if (toggle) {
                    e.preventDefault();
                    e.stopPropagation();
                    const parent = toggle.closest('.dropdown');
                    const menu = parent ? parent.querySelector('.dropdown-menu') : null;
                    const isOpen = parent ? parent.classList.contains('show') : false;

                    // Diğer açık tüm dropdownları kapat
                    openDropdowns.forEach(function (el) { el.classList.remove('show'); });

                    if (!isOpen && parent && menu) {
                        parent.classList.add('show');
                        menu.classList.add('show');
                    }
                } else if (!e.target.closest('.dropdown-menu')) {
                    // Dropdown dışına tıklandığında açık menüleri kapat
                    openDropdowns.forEach(function (el) { el.classList.remove('show'); });
                }
            });
        },

        createModalDOM: function () {
            if (document.getElementById("admin-modal-overlay")) return;

            const modalHtml = `
                <div id="admin-modal-overlay">
                    <div class="admin-modal-box">
                        <div class="admin-modal-icon warning" id="admin-modal-icon">⚠️</div>
                        <h3 class="admin-modal-title" id="admin-modal-title">Emin misiniz?</h3>
                        <p class="admin-modal-text" id="admin-modal-text">Bu işlem geri alınamaz.</p>
                        <div class="admin-modal-actions">
                            <button type="button" class="btn btn-secondary" id="admin-modal-cancel">İptal</button>
                            <button type="button" class="btn btn-danger" id="admin-modal-confirm">Evet, Onayla</button>
                        </div>
                    </div>
                </div>
            `;
            document.body.insertAdjacentHTML("beforeend", modalHtml);
        },

        createToastDOM: function () {
            if (document.getElementById("admin-toast-container")) return;
            const container = document.createElement("div");
            container.id = "admin-toast-container";
            document.body.appendChild(container);
        },

        toast: function (message, type = "success") {
            const container = document.getElementById("admin-toast-container") || document.body;
            const toast = document.createElement("div");
            toast.className = "admin-toast";
            const icon = type === "success" ? "✓" : "⚠️";
            toast.innerHTML = `<span style="color: ${type === 'success' ? '#10b981' : '#f59e0b'}; font-weight: bold;">${icon}</span> <span>${message}</span>`;
            container.appendChild(toast);

            setTimeout(() => {
                toast.style.opacity = "0";
                toast.style.transform = "translateY(10px)";
                toast.style.transition = "all 0.3s ease";
                setTimeout(() => toast.remove(), 300);
            }, 3000);
        }
    };

    // Polyfill / Bridge for Legacy SweetAlert2 & jQuery in existing Razor views
    window.Swal = {
        fire: function (options) {
            return new Promise((resolve) => {
                if (typeof options === "string") {
                    options = { title: options };
                }

                // If it's a simple auto-dismiss toast (e.g. success message after delete)
                if (options.timer) {
                    AdminUI.toast(options.text || options.title || "İşlem başarılı", options.icon || "success");
                    setTimeout(() => resolve({ isConfirmed: true }), options.timer);
                    return;
                }

                const overlay = document.getElementById("admin-modal-overlay");
                const titleEl = document.getElementById("admin-modal-title");
                const textEl = document.getElementById("admin-modal-text");
                const confirmBtn = document.getElementById("admin-modal-confirm");
                const cancelBtn = document.getElementById("admin-modal-cancel");

                if (!overlay) {
                    resolve({ isConfirmed: confirm(options.text || options.title) });
                    return;
                }

                titleEl.textContent = options.title || "Onay";
                textEl.textContent = options.text || "";
                confirmBtn.textContent = options.confirmButtonText || "Evet, Onayla";
                cancelBtn.textContent = options.cancelButtonText || "İptal";

                cancelBtn.style.display = options.showCancelButton === false ? "none" : "inline-flex";

                overlay.classList.add("active");

                const handleConfirm = () => {
                    cleanup();
                    resolve({ isConfirmed: true });
                };

                const handleCancel = () => {
                    cleanup();
                    resolve({ isConfirmed: false });
                };

                const cleanup = () => {
                    overlay.classList.remove("active");
                    confirmBtn.removeEventListener("click", handleConfirm);
                    cancelBtn.removeEventListener("click", handleCancel);
                };

                confirmBtn.addEventListener("click", handleConfirm);
                cancelBtn.addEventListener("click", handleCancel);
            });
        }
    };

    // Minimal jQuery ($) Selector & Ajax Polyfill for existing views
    if (!window.$) {
        window.$ = function (selector) {
            if (typeof selector === "function") {
                if (document.readyState !== "loading") {
                    selector();
                } else {
                    document.addEventListener("DOMContentLoaded", selector);
                }
                return;
            }

            if (!selector) return { length: 0 };

            let elements = [];
            if (typeof selector === "string") {
                elements = Array.from(document.querySelectorAll(selector));
            } else if (selector instanceof Element || selector instanceof Document || selector === window) {
                elements = [selector];
            } else if (Array.isArray(selector) || selector instanceof NodeList) {
                elements = Array.from(selector);
            }

            return {
                elements: elements,
                length: elements.length,
                remove: function () {
                    elements.forEach(el => el && el.remove && el.remove());
                    return this;
                },
                hide: function () {
                    elements.forEach(el => { if (el && el.style) el.style.display = "none"; });
                    return this;
                },
                show: function () {
                    elements.forEach(el => { if (el && el.style) el.style.display = ""; });
                    return this;
                },
                val: function (v) {
                    if (v !== undefined) {
                        elements.forEach(el => { if (el) el.value = v; });
                        return this;
                    }
                    return elements[0] ? elements[0].value : "";
                },
                text: function (t) {
                    if (t !== undefined) {
                        elements.forEach(el => { if (el) el.textContent = t; });
                        return this;
                    }
                    return elements[0] ? elements[0].textContent : "";
                },
                html: function (h) {
                    if (h !== undefined) {
                        elements.forEach(el => { if (el) el.innerHTML = h; });
                        return this;
                    }
                    return elements[0] ? elements[0].innerHTML : "";
                },
                addClass: function (c) {
                    elements.forEach(el => { if (el && el.classList) el.classList.add(...c.split(" ").filter(Boolean)); });
                    return this;
                },
                removeClass: function (c) {
                    elements.forEach(el => { if (el && el.classList) el.classList.remove(...c.split(" ").filter(Boolean)); });
                    return this;
                },
                toggleClass: function (c) {
                    elements.forEach(el => { if (el && el.classList) el.classList.toggle(c); });
                    return this;
                },
                hasClass: function (c) {
                    return elements.some(el => el && el.classList && el.classList.contains(c));
                },
                css: function (prop, val) {
                    if (typeof prop === "object") {
                        elements.forEach(el => { if (el && el.style) Object.assign(el.style, prop); });
                        return this;
                    }
                    if (val !== undefined) {
                        elements.forEach(el => { if (el && el.style) el.style[prop] = val; });
                        return this;
                    }
                    return elements[0] ? window.getComputedStyle(elements[0])[prop] : "";
                },
                attr: function (name, val) {
                    if (val !== undefined) {
                        elements.forEach(el => { if (el && el.setAttribute) el.setAttribute(name, val); });
                        return this;
                    }
                    return elements[0] && elements[0].getAttribute ? elements[0].getAttribute(name) : undefined;
                },
                on: function (event, handler) {
                    elements.forEach(el => { if (el && el.addEventListener) el.addEventListener(event, handler); });
                    return this;
                },
                bind: function (event, handler) {
                    return this.on(event, handler);
                },
                ready: function (fn) {
                    if (typeof fn === "function") fn.call(elements[0]);
                    return this;
                },
                find: function (subSelector) {
                    const subElements = [];
                    elements.forEach(el => {
                        if (el && el.querySelectorAll) {
                            subElements.push(...Array.from(el.querySelectorAll(subSelector)));
                        }
                    });
                    return window.$(subElements);
                }
            };
        };

        window.$.ajax = function (opts) {
            const method = (opts.type || opts.method || "GET").toUpperCase();
            const headers = { "X-Requested-With": "XMLHttpRequest" };
            if (opts.contentType) headers["Content-Type"] = opts.contentType;

            fetch(opts.url, {
                method: method,
                headers: headers,
                body: method !== "GET" && opts.data ? (typeof opts.data === "string" ? opts.data : JSON.stringify(opts.data)) : undefined
            })
            .then(res => {
                if (!res.ok) throw new Error("HTTP " + res.status);
                const contentType = res.headers.get("content-type");
                if (contentType && contentType.includes("application/json")) {
                    return res.json();
                }
                return res.text();
            })
            .then(data => {
                if (typeof opts.success === "function") {
                    opts.success(data);
                }
            })
            .catch(err => {
                if (typeof opts.error === "function") {
                    opts.error(err);
                }
            });
        };
    }

    // Auto-initialize on DOM ready
    if (document.readyState === "loading") {
        document.addEventListener("DOMContentLoaded", () => AdminUI.init());
    } else {
        AdminUI.init();
    }
})();
