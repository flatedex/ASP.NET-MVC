// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    InitDragAndDrop();
});

function InitDragAndDrop() {
    $("#file-selector").on("dragenter", function (evt) {
        evt.preventDefault();
        evt.stopPropogation();
    });
    $("#file-selector").on("dragover", function (evt) {
        evt.preventDefault();
        evt.stopPropogation();
    });
    $("#file-selector").on("drop", function (evt) {
        evt.preventDefault();
        evt.stopPropogation();
    });
}