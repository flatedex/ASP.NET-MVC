// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const fileSelector = document.getElementById('file-selector');
const output = document.getElementById('output');
if (window.FileList && window.File) {
    fileSelector.addEventListener('dragover', event => {
        event.stopPropagation();
        event.preventDefault();
        event.dataTransfer.dropEffect = 'copy';
    });
    fileSelector.addEventListener('drop', event => {
        output.innerHTML = '';
        event.stopPropagation();
        event.preventDefault();
        const files = event.dataTransfer.files;
        for (let i = 0; i < files.length; i++) {
            let li = document.createElement('li');
            const file = files[i];
            const name = file.name ? file.name : 'NOT SUPPORTED';
            const type = file.type ? file.type : 'NOT SUPPORTED';
            const size = file.size ? file.size : 'NOT SUPPORTED';
            li.textContent = `name: ${name}, type: ${type}, size: ${size}`;
            output.appendChild(li);
        }
    });
}