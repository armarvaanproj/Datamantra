/*
* @example An iframe-based dialog with custom button handling logics.
*/
(function() {
    CKEDITOR.plugins.add('AudioEmbed',
    {
        requires: ['iframedialog'],
        init: function(editor) {
            var me = this;
            var _editorPath = me.path;
            CKEDITOR.dialog.add('AudioEmbedDialog', function() {
                return {
                    title: 'Embed Audio Dialog',
                    minWidth: 500,
                    minHeight: 250,
                    contents:
                       [
                          {
                              id: 'iframe',
                              label: 'Embed Audio',
                              expand: true,
                              elements:
                                   [
                                      {
                                          type: 'html',
                                          id: 'pageAudioEmbed',
                                          label: 'Embed Audio',
                                          style: 'width : 100%;',
                                          html: '<iframe src="' + _editorPath + 'dialogs/audioembed.aspx" frameborder="0" name="iframeAudioEmbed" id="iframeAudioEmbed" allowtransparency="1" style="width:100%;margin:0;padding:0;"></iframe>'
                                      }
                                   ]
                          }
                       ],
                    onOk: function() {
                        var content = $('#iframeAudioEmbed').contents().find('#embed').val();
                        var hcontent = $('#iframeAudioEmbed').contents().find('#hembed').val();
                        $('#iframeAudioEmbed').contents().find('#embed').val('');
                        $('#iframeAudioEmbed').contents().find('#hembed').val('');

                        for (var ck_instance in CKEDITOR.instances) {
                            if (CKEDITOR.instances[ck_instance].focusManager.hasFocus) {
                                CKEDITOR.instances[ck_instance].focus();
                                CKEDITOR.instances[ck_instance].insertHtml('<a href="' + content + '" class="media">' + hcontent + '</a>');
                            }
                        }
                    }
                };
            });

            editor.addCommand('AudioEmbed', new CKEDITOR.dialogCommand('AudioEmbedDialog'));

            editor.ui.addButton('AudioEmbed',
            {
                label: 'Embed Audio',
                command: 'AudioEmbed',
                icon: this.path + 'images/Audio_icon.gif'
            });


        }
    });
})();

