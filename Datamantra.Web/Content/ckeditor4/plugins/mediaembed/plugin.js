/*
* @example An iframe-based dialog with custom button handling logics.
*/
(function() {
    CKEDITOR.plugins.add('MediaEmbed',
    {
        requires: ['iframedialog'],
        init: function(editor) {
            var me = this;
            var _editorPath = me.path;
            CKEDITOR.dialog.add('MediaEmbedDialog', function() {
                return {
                    title: 'Embed Media Dialog',
                    minWidth: 550,
                    minHeight: 250,
                    contents:
                       [
                          {
                              id: 'iframe',
                              label: 'Embed Media',
                              expand: true,
                              elements:
                                   [
                                      {
                                          type: 'html',
                                          id: 'pageMediaEmbed',
                                          label: 'Embed Media',
                                          style: 'width : 100%;',
                                          html: '<iframe src="' + _editorPath + 'dialogs/mediaembed.html" frameborder="0" name="iframeMediaEmbed" id="iframeMediaEmbed" allowtransparency="1" style="width:100%;height:200px;margin:0;padding:0;"></iframe>'
                                      }
                                   ]
                          }
                       ],
                    onOk: function () {
                        var content = $('#iframeMediaEmbed').contents().find('#embed').val();
                        var mediaHtml = CKEDITOR.dom.element.createFromHtml('<div class="media_embed">' + content + '</div>');
                        for (var ck_instance in CKEDITOR.instances) {
                            if (CKEDITOR.instances[ck_instance].focusManager.hasFocus) {
                                CKEDITOR.instances[ck_instance].focus();
                                CKEDITOR.instances[ck_instance].insertElement(mediaHtml);
                            }
                        }
                    }
                };
            });

            editor.addCommand('MediaEmbed', new CKEDITOR.dialogCommand('MediaEmbedDialog'));

            editor.ui.addButton('MediaEmbed',
            {
                label: 'Embed Media',
                command: 'MediaEmbed',
                icon: this.path + 'images/icon.gif'
            });
        }
    });
})();
