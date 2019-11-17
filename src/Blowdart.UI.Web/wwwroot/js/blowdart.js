// ReSharper disable UndeclaredGlobalVariableUsing
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

window.blowdart.showCollapsible = function(id) {
    $(`#${id}`).collapse("toggle");
};

window.blowdart.toast = function(headerText, timestamp, body, delay) {
    if ($("#toast-panel").length === 0) {
        $("#after-render").append(`
<div aria-live="polite' aria-atomic="true" style="min-height: 200px;">
    <div id='toast-panel' style='position: absolute; top:0; right:20px'>
    </div>
</div>`);
    }

    const d = delay ? `data-delay='${delay}'` : "data-autohide='false'";
    const toast = `
        <div class="toast" role="alert" aria-live="assertive" aria-atomic="true"${d}>
          <div class="toast-header">
            <i class="oi oi-chat" />
            <strong class="mr-auto">${headerText}</strong>
            <small class="text-muted">${timestamp}</small>
            <button type="button" class="ml-2 mb-1 close" data-dismiss="toast" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="toast-body">
            ${body}
          </div>
        </div>`;

    $("#toast-panel").append(toast);
    $(".toast").toast("show");
    $(".toast").on("hidden.bs.toast", e => {
        $(e.currentTarget).remove();
    });
};