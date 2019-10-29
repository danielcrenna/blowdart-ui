window.blowdart = {};
window.blowdart.highlight = function() {
    document.querySelectorAll("pre code").forEach((block) => {
        hljs.highlightBlock(block);
    });
}