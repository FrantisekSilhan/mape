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
});