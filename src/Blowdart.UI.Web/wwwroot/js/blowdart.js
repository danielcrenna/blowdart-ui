"use strict";

window.blowdart = {};

window.blowdart.onReady = function() {

};

window.blowdart.highlight = function () {
    document.querySelectorAll("pre code").forEach((block) => {
        hljs.highlightBlock(block);
    });
};

window.blowdart.log = function (message) {
    console.log(message);
};

window.blowdart.showModal = function(id) {
    $(`#${id}`).modal("toggle");
};