

let theEditor;
function FullCkEditor(id) {
    DecoupledEditor
        .create(document.querySelector('#' + id))
        .then(editor => {
            theEditor = editor;
            const toolbarContainer = document.querySelector('#toolbar-container');
            toolbarContainer.appendChild(editor.ui.view.toolbar.element);
        })
        .catch(error => {
            console.error(error);
        });
}
