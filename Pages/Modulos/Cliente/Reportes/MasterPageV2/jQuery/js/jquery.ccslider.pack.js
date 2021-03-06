/* jQuery CCSlider Plugin 1.1.1 
 * Copyright 2011, Nilok Bose  
 * http://codecanyon.net/user/cosmocoder
*/

(function (a) {
    a.fn.ccslider = function (b) {
        var c = a.extend(true, {}, a.fn.ccslider.defaults, b);
        return this.each(function () {
            var u = a(this),
                t = u.wrapInner('<div class="slider-innerWrapper"/>').find(".slider-innerWrapper"),
                ag = a('<div class="slider-controls"/>').appendTo(u),
                e = u.width(),
                k = u.height(),
                ac = e,
                Z = k,
                q = u.find("img"),
                r = q.length,
                ad, ae, w = false,
                ao = c._3dOptions.imageWidth,
                Q = c._3dOptions.imageHeight,
                am = c._3dOptions.innerSideColor,
                W = c._3dOptions.makeShadow,
                al = c._3dOptions.shadowColor,
                x = c._3dOptions.slices,
                S = c._3dOptions.delay,
                R = c._3dOptions.easing,
                ab = c._3dOptions.fallBack,
                f, M = c.startSlide,
                j = false,
                C = false,
                y = false,
                aj;
            if (c.effectType === "3d") {
                if (document.createElement("canvas").getContext) {
                    ae = "3d";
                    ad = c.effect;
                    f = c.animSpeed
                } else {
                    ae = "2d";
                    ad = ab;
                    f = c._3dOptions.fallBackSpeed
                }
            } else {
                ae = "2d";
                ad = c.effect;
                f = c.animSpeed
            }
            u.addClass("ccslider");
            if (c.directionNav) {
                var V = a('<a class="slider-nav prev"/>').appendTo(ag),
                    s = a('<a class="slider-nav next"/>').appendTo(ag);
                V.click(function () {
                    if (aj) {
                        af()
                    }
                    if (ae === "3d") {
                        l("prev")
                    } else {
                        O("prev")
                    }
                });
                s.click(function () {
                    if (aj) {
                        af()
                    }
                    if (ae === "3d") {
                        l("next")
                    } else {
                        O("next")
                    }
                })
            }
            if (c.controlLinks) {
                var v = a('<ul class="control-links" />').appendTo(u),
                    T = "",
                    ah;
                for (ah = 0; ah < r; ah++) {
                    if (c.controlLinkThumbs) {
                        T += '<img src="' + c.controlThumbLocation + q.eq(ah).data("thumbname") + '" />'
//                        T += '<li class="linkThumb" data-index="' + ah + '"><img src="' + c.controlThumbLocation + q.eq(ah).data("thumbname") + '" /></li>'
                    } else {
//                        T += '<li data-index="' + ah + '">' + (ah + 1) + "</li>"
                    }
                }
                v.append(T);
                v.delegate("li", "click", function () {
                    if (ae === "3d") {
                        l(a(this).data("index"))
                    } else {
                        O(a(this).data("index"))
                    }
                })
            }
            function an() {
                if (c.controlLinks) {
                    v.find("li").removeClass("active").eq(M).addClass("active")
                }
            }
            an();

            function Y() {
                if (!y && !C) {
                    aj = setInterval(function () {
                        if (ae === "3d") {
                            l("next")
                        } else {
                            O("next")
                        }
                    }, c.pauseTime)
                }
            }
            function af() {
                clearInterval(aj);
                aj = ""
            }
            if (c.autoPlay) {
                Y()
            }
            if (c.pauseOnHover) {
                u.hover(function () {
                    y = true;
                    af()
                }, function () {
                    y = false;
                    if (aj === "" && c.autoPlay && !j) {
                        Y()
                    }
                })
            }
            if (c.autoPlay) {
                var p = a('<div class="slider-timer pause"/>').appendTo(ag);
                p.click(function () {
                    if (p.hasClass("pause")) {
                        p.removeClass("pause").addClass("play");
                        af();
                        j = true
                    } else {
                        p.removeClass("play").addClass("pause");
                        Y();
                        j = false
                    }
                })
            }
            function A() {
                C = false;
                c.afterSlideChange.call(u[0], M);
                if (c.autoPlay && !j) {
                    Y()
                }
            }
            if (c.captions) {
                var I = a('<div class="slider-caption"/>').appendTo(u);
                if (ae === "3d") {
                    I.width(ao - parseInt(I.css("padding-left"), 10) - parseInt(I.css("padding-right"), 10));
                    I.css({
                        left: (e - ao) / 2,
                        bottom: (k - Q) / 2,
                        right: "auto"
                    })
                }
            }
            function m() {
                if (c.captions) {
                    var b = q.eq(M),
                        ap = "",
                        i = "";
                    if (b.data("captionelem")) {
                        ap = b.data("captionelem");
                        i = a(ap)[0].innerHTML
                    } else {
                        if (b[0].alt) {
                            i = b[0].alt
                        }
                    }
                    if (i) {
                        I[0].innerHTML = i;
                        if (c.captionAnimation === "none") {
                            I.show()
                        } else {
                            if (c.captionAnimation === "fade") {
                                I.fadeIn(c.captionAnimationSpeed)
                            } else {
                                if (c.captionAnimation === "slide") {
                                    I.slideDown(c.captionAnimationSpeed)
                                }
                            }
                        }
                    }
                }
            }
            m();

            function E() {
                if (c.captions) {
                    if (c.captionAnimation === "none") {
                        I.hide()
                    } else {
                        if (c.captionAnimation === "fade") {
                            I.fadeOut(c.captionAnimationSpeed)
                        } else {
                            if (c.captionAnimation === "slide") {
                                I.slideUp(c.captionAnimationSpeed)
                            }
                        }
                    }
                }
            }
            w = ad === "random" ? true : false;

            function H() {
                var i = [];
                if (ae === "3d") {
                    if (W) {
                        i = ["cubeUp", "cubeDown", "cubeRight", "cubeLeft"]
                    } else {
                        i = ["cubeUp", "cubeDown", "cubeRight", "cubeLeft", "flipUp", "flipDown", "flipRight", "flipLeft", "blindsVertical", "blindsHorizontal"]
                    }
                } else {
                    i = ["fade", "horizontalOverlap", "verticalOverlap", "horizontalSlide", "verticalSlide", "horizontalWipe", "verticalWipe", "horizontalSplit", "verticalSplit", "fadeSlide"]
                }
                ad = i[Math.floor(Math.random() * (i.length + 1))];
                if (ad === undefined) {
                    ad = i[0]
                }
            }
            if (w) {
                H()
            }
            if (ae === "3d") {
                t.hide();
                u.css("background", "transparent none");
                ag.width(ao);
                ag.height(Q);
                ag.css({
                    left: (e - ao) / 2,
                    top: (k - Q) / 2
                });
                var G, X, K, z, ak = [],
                    D = [],
                    F = [],
                    o = [],
                    h = [],
                    n = [],
                    d = [];

                function g() {
                    if (ad === "cubeLeft" || ad === "cubeRight") {
                        G = ao;
                        X = Math.round(Q / x);
                        K = ao
                    } else {
                        if (ad === "cubeUp" || ad === "cubeDown") {
                            G = Math.round(ao / x);
                            X = Q;
                            K = Q
                        } else {
                            if (ad === "flipLeft" || ad === "flipRight" || ad === "blindsHorizontal") {
                                G = ao;
                                X = Math.round(Q / x);
                                K = 0
                            } else {
                                if (ad === "flipUp" || ad === "flipDown" || ad === "blindsVertical") {
                                    G = Math.round(ao / x);
                                    X = Q;
                                    K = 0
                                }
                            }
                        }
                    }
                    z = K === 0 ? 2 * ao : K + 300;
                    var b;
                    if (x % 2 === 0) {
                        b = x / 2
                    } else {
                        b = (x + 1) / 2
                    }
                    var c = x,
                        ar;
                    while (c--) {
                        if (c <= b - 1) {
                            ar = 2 + c
                        } else {
                            ar = 2 + x - 1 - c
                        }
                        if (ak[c]) {
                            ak[c].remove()
                        }
                        ak[c] = a('<canvas class="draw"/>').appendTo(u).css({
                            position: "absolute",
                            zIndex: ar
                        });
                        D[c] = ak[c][0].getContext("2d");
                        ak[c][0].width = ac;
                        ak[c][0].height = Z;
                        if (!o[c]) {
                            o[c] = document.createElement("canvas");
                            n[c] = o[c].getContext("2d")
                        }
                        if (!h[c]) {
                            h[c] = document.createElement("canvas");
                            d[c] = h[c].getContext("2d")
                        }
                        o[c].width = h[c].width = G;
                        o[c].height = h[c].height = X;
                        F[c] = new Cube(G, X, K, z, D[c], am, []);
                        if (ad.indexOf("Left") !== -1 || ad.indexOf("Right") !== -1 || ad === "blindsHorizontal") {
                            F[c].position.y = Q / 2 - X / 2 - c * X
                        } else {
                            if (ad.indexOf("Up") !== -1 || ad.indexOf("Down") !== -1 || ad === "blindsVertical") {
                                F[c].position.x = -ao / 2 + G / 2 + c * G
                            }
                        }
                        ai(n[c], q[M], c);
                        F[c].images[0] = o[c];
                        F[c].render()
                    }
                }
                g();
                if (W && ad.indexOf("cube") === 0) {
                    var ba = a('<canvas class="shadow"/>').appendTo(u).css({
                        position: "absolute",
                        zIndex: "1"
                    }),
                        N = ba[0].getContext("2d");
                    N.canvas.width = ac;
                    N.canvas.height = Z + 30;
                    var P = new Plane(ao - 100, Q + 200, Q + 300, N, "#666", "", al);
                    P.position.y = -Q / 2 + 60;
                    P.position.z = Q / 2;
                    P.rotation.x = Math.PI / 2;
                    P.render()
                }
            }
            function l(b) {
                if (!C) {
                    if (!y && aj) {
                        af()
                    }
                    var e = M,
                        az = q[M],
                        aq, ax, au, ar, at = x;
                    if (b === "next") {
                        M++;
                        if (M === r) {
                            M = 0
                        }
                    } else {
                        if (b === "prev") {
                            M--;
                            if (M < 0) {
                                M = r - 1
                            }
                        } else {
                            M = b;
                            if (e < M) {
                                b = "next"
                            } else {
                                b = "prev"
                            }
                        }
                    }
                    var j = q[M];
                    c.beforeSlideChange.call(u[0], M);
                    E();
                    an();
                    C = true;
                    switch (ad) {
                    case "cubeLeft":
                        if (b === "next") {
                            aq = 1;
                            ax = -1
                        } else {
                            aq = 3;
                            ax = 1
                        }
                        au = "y";
                        break;
                    case "cubeRight":
                        if (b === "next") {
                            aq = 3;
                            ax = 1
                        } else {
                            aq = 1;
                            ax = -1
                        }
                        au = "y";
                        break;
                    case "cubeUp":
                        if (b === "next") {
                            aq = 5;
                            ax = 1
                        } else {
                            aq = 4;
                            ax = -1
                        }
                        au = "x";
                        break;
                    case "cubeDown":
                        if (b === "next") {
                            aq = 4;
                            ax = -1
                        } else {
                            aq = 5;
                            ax = 1
                        }
                        au = "x";
                        break;
                    case "flipLeft":
                        if (b === "next") {
                            ax = -1
                        } else {
                            ax = 1
                        }
                        aq = 2;
                        au = "y";
                        break;
                    case "flipRight":
                        if (b === "next") {
                            ax = 1
                        } else {
                            ax = -1
                        }
                        aq = 2;
                        au = "y";
                        break;
                    case "flipUp":
                        if (b === "next") {
                            ax = 1
                        } else {
                            ax = -1
                        }
                        aq = 2;
                        au = "x";
                        break;
                    case "flipDown":
                        if (b === "next") {
                            ax = -1
                        } else {
                            ax = 1
                        }
                        aq = 2;
                        au = "x";
                        break;
                    case "blindsVertical":
                        if (b === "next") {
                            ax = 1
                        } else {
                            ax = -1
                        }
                        aq = 2;
                        au = "y";
                        break;
                    case "blindsHorizontal":
                        if (b === "next") {
                            ax = -1
                        } else {
                            ax = 1
                        }
                        aq = 2;
                        au = "x";
                        break
                    }
                    while (at--) {
                        ai(n[at], az, at);
                        ai(d[at], j, at);
                        F[at].images[0] = o[at];
                        F[at].images[aq] = h[at]
                    }
                    if (ad.indexOf("cube") === 0) {
                        ar = Math.PI / 2
                    } else {
                        ar = Math.PI
                    }
                    var k = au === "y" ? {
                        y: ax * ar
                    } : {
                        x: ax * ar
                    };
                    at = x;
                    while (at--) {
                        F[at].rotation[au] = 0;
                        a(F[at].rotation).delay(at * S).animate(k, {
                            duration: f,
                            easing: R,
                            step: function (i) {
                                this.parent.render()
                            }
                        })
                    }
                    setTimeout(function () {
                        m();
                        A();
                        if (w) {
                            H();
                            g()
                        }
                    }, f + (x - 1) * S)
                }
            }
            function ai(i, a, b) {
                if (ad.indexOf("Up") !== -1 || ad.indexOf("Down") !== -1 || ad === "blindsVertical") {
                    i.drawImage(a, b * a.width / x, 0, a.width / x, a.height, 0, 0, G, X)
                } else {
                    if (ad.indexOf("Left") !== -1 || ad.indexOf("Right") !== -1 || ad === "blindsHorizontal") {
                        i.drawImage(a, 0, b * a.height / x, a.width, a.height / x, 0, 0, G, X)
                    }
                }
            }
            if (ae === "2d") {
                u.width(1);
                u.height(1);
                q.each(function () {
                    var i = a(this);
                    if (u.width() < i.width()) {
                        u.width(i.width());
                        e = u.width()
                    }
                    if (u.height() < i.height()) {
                        u.height(i.height());
                        k = u.height()
                    }
                });
                q.eq(M).css("z-index", "3").fadeIn(600, function () {
                    q.show()
                });
                var U, L, J;

                function B() {
                    if (ad.indexOf("Wipe") !== -1) {
                        if (!U) {
                            U = a('<div class="wipe-div"/>').appendTo(u);
                            U.css({
                                position: "absolute",
                                top: 0,
                                left: 0,
                                width: 0,
                                height: 0,
                                zIndex: 3
                            })
                        }
                    }
                    if (ad.indexOf("Split") !== -1) {
                        if (!L) {
                            L = a('<div class="split1-div"/>').appendTo(u);
                            J = a('<div class="split2-div"/>').appendTo(u);
                            L.css({
                                position: "absolute",
                                zIndex: 4
                            });
                            J.css({
                                position: "absolute",
                                zIndex: 4
                            })
                        }
                    }
                }
                B()
            }
            function O(b) {
                if (!C) {
                    if (!y && aj) {
                        af()
                    }
                    var d = M,
                        ap = q.eq(M),
                        i;
                    if (b === "next") {
                        M++;
                        if (M === r) {
                            M = 0
                        }
                    } else {
                        if (b === "prev") {
                            M--;
                            if (M < 0) {
                                M = r - 1
                            }
                        } else {
                            M = b;
                            if (d < M) {
                                b = "next"
                            } else {
                                b = "prev"
                            }
                        }
                    }
                    i = q.eq(M);
                    c.beforeSlideChange.call(u[0], M);
                    E();
                    an();
                    C = true;
                    q.css("z-index", "1");
                    ap.css("z-index", "2");
                    switch (ad) {
                    case "fade":
                        i.css({
                            opacity: 0,
                            zIndex: 3
                        }).animate({
                            opacity: 1
                        }, f, function () {
                            m();
                            A();
                            if (w) {
                                H();
                                B()
                            }
                        });
                        break;
                    case "horizontalOverlap":
                        if (b === "next") {
                            i.css({
                                left: e,
                                zIndex: 3
                            }).animate({
                                left: 0
                            }, f, function () {
                                m();
                                A();
                                if (w) {
                                    H();
                                    B()
                                }
                            })
                        } else {
                            i.css({
                                left: -e,
                                zIndex: 3
                            }).animate({
                                left: 0
                            }, f, function () {
                                m();
                                A();
                                if (w) {
                                    H();
                                    B()
                                }
                            })
                        }
                        break;
                    case "verticalOverlap":
                        if (b === "next") {
                            i.css({
                                top: -k,
                                zIndex: 3
                            }).animate({
                                top: 0
                            }, f, function () {
                                m();
                                A();
                                if (w) {
                                    H();
                                    B()
                                }
                            })
                        } else {
                            i.css({
                                top: k,
                                zIndex: 3
                            }).animate({
                                top: 0
                            }, f, function () {
                                m();
                                A();
                                if (w) {
                                    H();
                                    B()
                                }
                            })
                        }
                        break;
                    case "horizontalSlide":
                        if (b === "next") {
                            i.css({
                                left: e,
                                zIndex: 3
                            }).animate({
                                left: 0
                            }, f, function () {
                                m();
                                A();
                                if (w) {
                                    H();
                                    B()
                                }
                            });
                            ap.animate({
                                left: -e
                            }, f, function () {
                                ap.css("left", "0")
                            })
                        } else {
                            i.css({
                                left: -e,
                                zIndex: 3
                            }).animate({
                                left: 0
                            }, c.animSpeed, function () {
                                m();
                                A();
                                if (w) {
                                    H();
                                    B()
                                }
                            });
                            ap.animate({
                                left: e
                            }, f, function () {
                                ap.css("left", "0")
                            })
                        }
                        break;
                    case "verticalSlide":
                        if (b === "next") {
                            i.css({
                                top: -k,
                                zIndex: 3
                            }).animate({
                                top: 0
                            }, f, function () {
                                m();
                                A();
                                if (w) {
                                    H();
                                    B()
                                }
                            });
                            ap.animate({
                                top: k
                            }, f, function () {
                                ap.css("top", "0")
                            })
                        } else {
                            i.css({
                                top: k,
                                zIndex: 3
                            }).animate({
                                top: 0
                            }, f, function () {
                                m();
                                A();
                                if (w) {
                                    H();
                                    B()
                                }
                            });
                            ap.animate({
                                top: -k
                            }, f, function () {
                                ap.css("top", "0")
                            })
                        }
                        break;
                    case "horizontalWipe":
                        i.hide();
                        U.css({
                            background: "url(" + i[0].src + ") no-repeat",
                            height: k
                        });
                        U.animate({
                            width: e
                        }, f, function () {
                            U.width(0);
                            i.css("z-index", "3").show();
                            m();
                            A();
                            if (w) {
                                H();
                                B()
                            }
                        });
                        break;
                    case "verticalWipe":
                        i.hide();
                        U.css({
                            background: "url(" + i[0].src + ") no-repeat",
                            width: e
                        });
                        U.animate({
                            height: k
                        }, f, function () {
                            U.height(0);
                            i.css("z-index", "3").show();
                            m();
                            A();
                            if (w) {
                                H();
                                B()
                            }
                        });
                        break;
                    case "verticalSplit":
                        ap.css({
                            opacity: 0
                        });
                        i.css({
                            zIndex: 3
                        });
                        L.css({
                            width: e / 2,
                            height: k,
                            top: 0,
                            left: 0,
                            background: "url(" + ap[0].src + ") no-repeat"
                        });
                        J.css({
                            width: e / 2,
                            height: k,
                            top: 0,
                            right: 0,
                            background: "url(" + ap[0].src + ") -50% 0 no-repeat"
                        });
                        L.animate({
                            width: 0
                        }, f);
                        J.animate({
                            width: 0
                        }, {
                            duration: f,
                            step: function (a) {
                                J.css("background-position", a - e + "px 0")
                            },
                            complete: function () {
                                ap.css("opacity", "1");
                                m();
                                A();
                                if (w) {
                                    H();
                                    B()
                                }
                            }
                        });
                        break;
                    case "horizontalSplit":
                        ap.css({
                            opacity: 0
                        });
                        i.css({
                            zIndex: 3
                        });
                        L.css({
                            width: e,
                            height: k / 2,
                            top: 0,
                            left: 0,
                            background: "url(" + ap[0].src + ") no-repeat"
                        });
                        J.css({
                            width: e,
                            height: k / 2,
                            bottom: 0,

                            left: 0,
                            background: "url(" + ap[0].src + ") 0 -50% no-repeat"
                        });
                        L.animate({
                            height: 0
                        }, f);
                        J.animate({
                            height: 0
                        }, {
                            duration: f,
                            step: function (a) {
                                J.css("background-position", "0" + (a - k) + "px")
                            },
                            complete: function () {
                                ap.css("opacity", "1");
                                m();
                                A();
                                if (w) {
                                    H();
                                    B()
                                }
                            }
                        });
                        break;
                    case "fadeSlide":
                        i.css("z-index", "3");
                        ap.css("z-index", "4");
                        if (b === "next") {
                            ap.animate({
                                left: -e,
                                opacity: 0
                            }, f, function () {
                                ap.css({
                                    left: 0,
                                    opacity: 1,
                                    zIndex: 1
                                });
                                m();
                                A();
                                if (w) {
                                    H();
                                    B()
                                }
                            })
                        } else {
                            ap.animate({
                                left: e,
                                opacity: 0
                            }, f, function () {
                                ap.css({
                                    left: 0,
                                    opacity: 1,
                                    zIndex: 1
                                });
                                m();
                                A();
                                if (w) {
                                    H();
                                    B()
                                }
                            })
                        }
                        break
                    }
                }
            }
        })
    };
    a.fn.ccslider.defaults = {
        effectType: "2d",
        effect: "horizontalSlide",
        //effect: "cubeUp", modificar para cambiar de 2d a 3d, modificar el effectType, y el effect (3d redimensiona automaticamente las imagenes, 2d se debe cambiar el tama�o de las imagenes)
        _3dOptions: {
            imageWidth: 600,
            imageHeight: 300,
            innerSideColor: "#444",
            makeShadow: true,
            shadowColor: "rgba(0, 0, 0, 0.7)",
            slices: 3,
            delay: 200,
            easing: "easeInOutBack",
            fallBack: "fadeSlide",
            fallBackSpeed: 1200
        },
        animSpeed: 2200,
        startSlide: 0,
        directionNav: true,
        controlLinks: true,
        controlLinkThumbs: false,
        controlThumbLocation: "",
        autoPlay: true,
        pauseTime: 3000,
        pauseOnHover: true,
        captions: true,
        captionAnimation: "slide",
        captionAnimationSpeed: 600,
        beforeSlideChange: function (b) {},
        afterSlideChange: function (b) {}
    }
})(jQuery);

function Cube(b, j, f, d, l, e, i) {
    this.width = b;
    this.height = j;
    this.depth = f;
    this.focalLength = d;
    this.ctx = l;
    this.color = e;
    this.images = i;
    this.rotation = {
        x: 0,
        y: 0,
        z: 0,
        parent: this
    };
    this.position = {
        x: 0,
        y: 0,
        z: 0,
        parent: this
    };
    var c = this.ctx.canvas,
        g = c.width,
        h = c.height,
        m = g / 2,
        k = h / 2;
    var a = [make3DPoint(-this.width / 2, this.height / 2, -this.depth / 2), make3DPoint(this.width / 2, this.height / 2, -this.depth / 2), make3DPoint(this.width / 2, -this.height / 2, -this.depth / 2), make3DPoint(-this.width / 2, -this.height / 2, -this.depth / 2), make3DPoint(-this.width / 2, this.height / 2, this.depth / 2), make3DPoint(this.width / 2, this.height / 2, this.depth / 2), make3DPoint(this.width / 2, -this.height / 2, this.depth / 2), make3DPoint(-this.width / 2, -this.height / 2, this.depth / 2)];
    this.position.z += this.depth / 2;
    this.render = function () {
        var o = Transform3DTo2D(a, this.rotation, this.position, this.focalLength, m, k);
        c.width = 1;
        c.width = g;
        var n;
        if (isVisible(o[3], o[0], o[1])) {
            n = [o[0], o[1], o[3], o[2]];
            mapTexture(l, n, this.images[0])
        }
        if (isVisible(o[6], o[5], o[4])) {
            if (this.rotation.x === 0) {
                n = [o[5], o[4], o[6], o[7]]
            } else {
                n = [o[7], o[6], o[4], o[5]]
            }
            mapTexture(l, n, this.images[2])
        }
        if (isVisible(o[2], o[1], o[5]) && this.depth !== 0) {
            if (this.images[1]) {
                n = [o[1], o[5], o[2], o[6]];
                mapTexture(l, n, this.images[1])
            } else {
                l.fillStyle = this.color;
                drawPlane(l, o[1], o[5], o[6], o[2]);
                l.fill()
            }
        }
        if (isVisible(o[7], o[4], o[0]) && this.depth !== 0) {
            if (this.images[3]) {
                n = [o[4], o[0], o[7], o[3]];
                mapTexture(l, n, this.images[3])
            } else {
                l.fillStyle = this.color;
                drawPlane(l, o[4], o[0], o[3], o[7]);
                l.fill()
            }
        }
        if (isVisible(o[0], o[4], o[5]) && this.depth !== 0) {
            if (this.images[4]) {
                n = [o[4], o[5], o[0], o[1]];
                mapTexture(l, n, this.images[4])
            } else {
                l.fillStyle = this.color;
                drawPlane(l, o[4], o[5], o[1], o[0]);
                l.fill()
            }
        }
        if (isVisible(o[7], o[3], o[2]) && this.depth !== 0) {
            if (this.images[5]) {
                n = [o[3], o[2], o[7], o[6]];
                mapTexture(l, n, this.images[5])
            } else {
                l.fillStyle = this.color;
                drawPlane(l, o[3], o[2], o[6], o[7]);
                l.fill()
            }
        }
    }
}
function Plane(b, j, d, l, f, g, h) {
    this.width = b;
    this.height = j;
    this.focalLength = d;
    this.ctx = l;
    this.color = f;
    this.rotation = {
        x: 0,
        y: 0,
        z: 0
    };
    this.position = {
        x: 0,
        y: 0,
        z: 0
    };
    var c = this.ctx.canvas,
        e = c.width,
        i = c.height,
        m = e / 2,
        k = i / 2;
    var a = [make3DPoint(-this.width / 2, this.height / 2, 0), make3DPoint(this.width / 2, this.height / 2, 0), make3DPoint(this.width / 2, -this.height / 2, 0), make3DPoint(-this.width / 2, -this.height / 2, 0)];
    this.render = function () {
        var n = Transform3DTo2D(a, this.rotation, this.position, this.focalLength, m, k);
        c.width = 1;
        c.width = e;
        drawPlane(l, make2DPoint(0, i), make2DPoint(e, i), make2DPoint(e, i - 100), make2DPoint(0, i - 100));
        l.clip();
        l.shadowOffsetX = 0;
        l.shadowOffsetY = 50;
        l.shadowBlur = 30;
        l.shadowColor = h;
        l.fillStyle = this.color;
        drawPlane(l, n[0], n[1], n[2], n[3]);
        l.fill()
    }
}
function make3DPoint(b, d, c) {
    var a = {
        x: b,
        y: d,
        z: c
    };
    return a
}
function make2DPoint(b, c) {
    var a = {
        x: b,
        y: c
    };
    return a
}
function Transform3DTo2D(w, j, D, h, u, t) {
    var k = [],
        A = Math.sin,
        a = Math.cos,
        r = A(j.x),
        e = a(j.x),
        p = A(j.y),
        d = a(j.y),
        o = A(j.z),
        b = a(j.z),
        n, m, l, g, f, s, q, C, B, c;
    var v = w.length;
    while (v--) {
        n = w[v].x;
        m = w[v].y;
        l = w[v].z;
        g = e * m - r * l;
        f = r * m + e * l;
        q = d * f + p * n;
        s = -p * f + d * n;
        C = b * s - o * g;
        B = o * s + b * g;
        n = C + D.x;
        m = B + D.y;
        l = q + D.z;
        c = h / (h + l);
        n = n * c + u;
        m = -(m * c) + t;
        k[v] = make2DPoint(n, m)
    }
    return k
}
function drawPlane(g, f, e, i, h) {
    g.beginPath();
    g.moveTo(f.x, f.y);
    g.lineTo(e.x, e.y);
    g.lineTo(i.x, i.y);
    g.lineTo(h.x, h.y);
    g.closePath()
}
function isVisible(e, d, f) {
    if (((d.y - e.y) / (d.x - e.x) - (f.y - e.y) / (f.x - e.x) < 0) ^ (e.x <= d.x === e.x > f.x)) {
        return true
    } else {
        return false
    }
};

function mapTexture(j, i, e) {
    var h = 5,
        g = 64,
        b = getProjectiveTransform(i);
    var d = b.transformProjectiveVector([0, 0, 1]),
        a = b.transformProjectiveVector([1, 0, 1]),
        f = b.transformProjectiveVector([0, 1, 1]),
        c = b.transformProjectiveVector([1, 1, 1]);
    j.save();
    j.beginPath();
    j.moveTo(d[0], d[1]);
    j.lineTo(a[0], a[1]);
    j.lineTo(c[0], c[1]);
    j.lineTo(f[0], f[1]);
    j.closePath();
    j.clip();
    divide(0, 0, 1, 1, d, a, f, c, b, h, g, j, e);
    j.restore()
}
function divide(o, W, m, V, U, T, S, Q, x, u, l, s, h) {
    var C = Math.abs,
        B = Math.max,
        g = Math.min,
        q = Math.sqrt;
    if (u) {
        var M = [T[0] + S[0] - 2 * U[0], T[1] + S[1] - 2 * U[1]],
            K = [T[0] + S[0] - 2 * Q[0], T[1] + S[1] - 2 * Q[1]],
            I = [M[0] + K[0], M[1] + K[1]],
            E = C((I[0] * I[0] + I[1] * I[1]) / (M[0] * K[0] + M[1] * K[1]));
        M = [T[0] - U[0] + Q[0] - S[0], T[1] - U[1] + Q[1] - S[1]];
        K = [S[0] - U[0] + Q[0] - T[0], S[1] - U[1] + Q[1] - T[1]];
        var A = C(M[0] * K[1] - M[1] * K[0]);
        if ((o === 0 && m === 1) || ((0.25 + E * 5) * A > (l * l))) {
            var c = (o + m) / 2,
                w = (W + V) / 2,
                a = x.transformProjectiveVector([c, w, 1]),
                i = x.transformProjectiveVector([c, W, 1]),
                t = x.transformProjectiveVector([c, V, 1]),
                p = x.transformProjectiveVector([o, w, 1]),
                j = x.transformProjectiveVector([m, w, 1]);
            --u;
            divide(o, W, c, w, U, i, p, a, x, u, l, s, h);
            divide(c, W, m, w, i, T, a, j, x, u, l, s, h);
            divide(o, w, c, V, p, a, S, t, x, u, l, s, h);
            divide(c, w, m, V, a, j, t, Q, x, u, l, s, h);
            return
        }
    }
    s.save();
    s.beginPath();
    s.moveTo(U[0], U[1]);
    s.lineTo(T[0], T[1]);
    s.lineTo(Q[0], Q[1]);
    s.lineTo(S[0], S[1]);
    s.closePath();
    var P = [T[0] - U[0], T[1] - U[1]],
        y = [Q[0] - T[0], Q[1] - T[1]],
        R = [S[0] - Q[0], S[1] - Q[1]],
        k = [U[0] - S[0], U[1] - S[1]];
    var H = C(P[0] * k[1] - P[1] * k[0]),
        G = C(y[0] * P[1] - y[1] * P[0]),
        D = C(R[0] * y[1] - R[1] * y[0]),
        F = C(k[0] * R[1] - k[1] * R[0]),
        n = B(B(H, G), B(F, D)),
        d = 0,
        b = 0,
        L = 0,
        J = 0;
    switch (n) {
    case H:
        s.transform(P[0], P[1], -k[0], -k[1], U[0], U[1]);
        if (m !== 1) {
            L = 1.05 / q(P[0] * P[0] + P[1] * P[1])
        }
        if (V !== 1) {
            J = 1.05 / q(k[0] * k[0] + k[1] * k[1])
        }
        break;
    case G:
        s.transform(P[0], P[1], y[0], y[1], T[0], T[1]);
        if (m !== 1) {
            L = 1.05 / q(P[0] * P[0] + P[1] * P[1])
        }
        if (V !== 1) {
            J = 1.05 / q(y[0] * y[0] + y[1] * y[1])
        }
        d = -1;
        break;
    case D:
        s.transform(-R[0], -R[1], y[0], y[1], Q[0], Q[1]);
        if (m !== 1) {
            L = 1.05 / q(R[0] * R[0] + R[1] * R[1])
        }
        if (V !== 1) {
            J = 1.05 / q(y[0] * y[0] + y[1] * y[1])
        }
        d = -1;
        b = -1;
        break;
    case F:
        s.transform(-R[0], -R[1], -k[0], -k[1], S[0], S[1]);
        if (m !== 1) {
            L = 1.05 / q(R[0] * R[0] + R[1] * R[1])
        }
        if (V !== 1) {
            J = 1.05 / q(k[0] * k[0] + k[1] * k[1])
        }
        b = -1;
        break
    }
    var f = (m - o),
        e = (V - W),
        O = L * f,
        N = J * e;
    var v = h.width,
        z = h.height;
    s.drawImage(h, o * v, W * z, g(m - o + O, 1) * v, g(V - W + N, 1) * z, d, b, 1 + L, 1 + J);
    s.restore()
}
function getProjectiveTransform(b) {
    var c = new Matrix(9, 8, [
        [1, 1, 1, 0, 0, 0, -b[3].x, -b[3].x, -b[3].x],
        [0, 1, 1, 0, 0, 0, 0, -b[2].x, -b[2].x],
        [1, 0, 1, 0, 0, 0, -b[1].x, 0, -b[1].x],
        [0, 0, 1, 0, 0, 0, 0, 0, -b[0].x],
        [0, 0, 0, -1, -1, -1, b[3].y, b[3].y, b[3].y],
        [0, 0, 0, 0, -1, -1, 0, b[2].y, b[2].y],
        [0, 0, 0, -1, 0, -1, b[1].y, 0, b[1].y],
        [0, 0, 0, 0, 0, -1, 0, 0, b[0].y]
    ]);
    var d = c.rowEchelon().values;
    var a = new Matrix(3, 3, [
        [-d[0][8], -d[1][8], -d[2][8]],
        [-d[3][8], -d[4][8], -d[5][8]],
        [-d[6][8], -d[7][8], 1]
    ]);
    return a
};
var Matrix = function (a, c, b) {
        this.w = a;
        this.h = c;
        this.values = b || Matrix.allocate(c)
    };
Matrix.allocate = function (a, e) {
    var b = [],
        d = e,
        c = a;
    while (d--) {
        b[d] = [];
        while (c--) {
            b[d][c] = 0
        }
    }
    return b
};
Matrix.cloneValues = function (b) {
    clone = [];
    for (var c = 0, a = b.length; c < a; ++c) {
        clone[c] = [].concat(b[c])
    }
    return clone
};
Matrix.prototype.transformProjectiveVector = function (b) {
    var c = [];
    for (var e = 0; e < this.h; ++e) {
        c[e] = 0;
        for (var a = 0; a < this.w; ++a) {
            c[e] += this.values[e][a] * b[a]
        }
    }
    var d = 1 / (c[c.length - 1]);
    for (var e = 0; e < this.h; ++e) {
        c[e] *= d
    }
    return c
};
Matrix.prototype.rowEchelon = function () {
    if (this.w <= this.h) {
        throw "Matrix rowEchelon size mismatch"
    }
    var h = Matrix.cloneValues(this.values);
    for (var a = 0; a < this.h; ++a) {
        var f = h[a][a];
        while (f == 0) {
            for (var g = a + 1; g < this.h; ++g) {
                if (h[g][a] != 0) {
                    var i = h[g];
                    h[g] = h[a];
                    h[a] = i;
                    break
                }
            }
            if (g == this.h) {
                return new Matrix(this.w, this.h, h)
            } else {
                f = h[a][a]
            }
        }
        var b = 1 / f;
        for (var e = a; e < this.w; ++e) {
            h[a][e] *= b
        }
        for (var d = 0; d < this.h; ++d) {
            if (d == a) {
                continue
            }
            var c = h[d][a];
            h[d][a] = 0;
            for (var e = a + 1; e < this.w; ++e) {
                h[d][e] -= c * h[a][e]
            }
        }
    }
    return new Matrix(this.w, this.h, h)
};