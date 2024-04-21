document.addEventListener("DOMContentLoaded", function () {
    const posts = document.querySelectorAll(".post");
    posts.forEach(post => {
        if (!post.hasAttribute("data-id"))
            return;
        post.addEventListener("click", function () {
            const postId = post.getAttribute("data-id");
            window.location.href = `/Post/${postId}`;
        });
    });

    const mainPost = document.getElementById("post-main");
    if (mainPost) {
        const mainReplies = document.querySelector(".posts__replies");
        const spacer = document.createElement("div");

        const mainPostHeight = mainPost.offsetHeight;
        const mainRepliesHeight = mainReplies ? mainReplies.offsetHeight : 0;

        const totalHeight = mainPost.offsetTop + mainPostHeight + mainRepliesHeight;

        const windowHeight = window.innerHeight;

        const postsMainHeight = totalHeight;
        const spacerHeight = Math.max(totalHeight, windowHeight, postsMainHeight);

        spacer.style.height = `${spacerHeight - mainPost.offsetHeight - mainRepliesHeight - 16}px`;

        //document.querySelector("main").appendChild(spacer);

        /*setTimeout(() => {
            window.scrollTo({
                top: mainPost.offsetTop,
                behavior: "smooth"
            });
        }, 15);*/
    }

    const textareas = document.querySelectorAll("textarea");
    textareas.forEach(textarea => {
        textarea.addEventListener("input", function () {
            const rows = this.value.split("\n").length;
            this.style.height = `${1.25 * Math.max(rows, 3)}em`;
        });
    });

    const hamburger = document.querySelector(".navbar__hamburger");
    const menu = document.querySelector(".navbar__menu");

    hamburger.addEventListener("click", function () {
        menu.classList.toggle("navbar__menu--visible");
        hamburger.classList.toggle("navbar__hamburger--visible");
        localStorage.setItem("navbarOpen", menu.classList.contains("navbar__menu--visible"));
    });

    let isMobileView = false;
    let isNavbarOpen = localStorage.getItem("navbarOpen");

    if (isNavbarOpen === "true") {
        isMobileView = true;
        menu.classList.add("navbar__menu--visible");
        hamburger.classList.add("navbar__hamburger--visible");
        menu.style.transition = "none";
        hamburger.style.transition = "none";
        setTimeout(() => {
            menu.style.transition = "";
            hamburger.style.transition = "";
        }, 0);
        document.querySelectorAll(".navbar__hamburger-line").forEach(line => {
            line.style.transition = "none";
            setTimeout(() => {
                line.style.transition = "";
            }, 0);
        });
    }
    window.addEventListener("resize", function () {
        if (window.innerWidth < 768 && !isMobileView && !isNavbarOpen) {
            isMobileView = true;
            menu.classList.add("navbar__menu--visible");
            hamburger.classList.add("navbar__hamburger--visible");
            document.querySelectorAll(".navbar__hamburger-line").forEach(line => {
                line.style.transition = "none";
                setTimeout(() => {
                    line.style.transition = "";
                }, 0);
            });
        }
        if (window.innerWidth >= 768) {
            if (isMobileView) {
                isMobileView = false;
            }
            isNavbarOpen = false;
        }
    });

    window.dispatchEvent(new Event('resize'));
});