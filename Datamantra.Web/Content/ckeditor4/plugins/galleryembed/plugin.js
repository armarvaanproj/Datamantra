/*
* @example An iframe-based dialog with custom button handling logics.
*/
(function() {
    CKEDITOR.plugins.add('GalleryEmbed',
    {
        requires: ['iframedialog'],
        init: function(editor) {
            var me = this;
            var _editorPath = me.path;
            CKEDITOR.dialog.add('GalleryEmbedDialog', function() {
                return {
                    title: 'Embed Gallery Dialog',
                    minWidth: 550,
                    minHeight: 200,
                    contents:
                       [
                          {
                              id: 'iframe',
                              label: 'Embed Gallery',
                              expand: true,
                              elements:
                                   [
                                      {
                                          type: 'html',
                                          id: 'pageGalleryEmbed',
                                          label: 'Embed Gallery',
                                          style: 'width : 100%;',
                                          //html: '<iframe src="' + _editorPath + 'dialogs/galleryembed.aspx" frameborder="0" name="iframeGalleryEmbed" id="iframeGalleryEmbed" allowtransparency="1" style="width:100%;margin:0;padding:0;"></iframe>'
                                          html: '<iframe src="/ResourceLib/_AlbumList" frameborder="0" name="iframeGalleryEmbed" id="iframeGalleryEmbed" allowtransparency="1" style="width:100%;margin:0;padding:0;"></iframe>'
                                      }
                                   ]
                          }
                       ],
                    onOk: function() {
                        var aname = $('#iframeGalleryEmbed').contents().find('input:radio[name=SMalbums]:checked').val();
                        var aid = $('#iframeGalleryEmbed').contents().find('input:radio[name=SMalbums]:checked').prop("id");
                        var galleryHtml = CKEDITOR.dom.element.createFromHtml('<div id="uw_gallery' + aid + '">[' + aname + ']</div>');
                        for (var ck_instance in CKEDITOR.instances) {
                            if (CKEDITOR.instances[ck_instance].focusManager.hasFocus) {
                                CKEDITOR.instances[ck_instance].focus();
                                CKEDITOR.instances[ck_instance].insertElement(galleryHtml);
                            }
                        }
                    }
                };
            });

            editor.addCommand('GalleryEmbed', new CKEDITOR.dialogCommand('GalleryEmbedDialog'));

            editor.ui.addButton('GalleryEmbed',
            {
                label: 'Embed Gallery',
                command: 'GalleryEmbed',
                icon: this.path + 'images/icon.gif'
            });


        }
    });
})();

