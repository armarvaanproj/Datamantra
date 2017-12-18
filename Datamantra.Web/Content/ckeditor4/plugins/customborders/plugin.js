/*
* @example An iframe-based dialog with custom button handling logics.
*/
(function() {
    CKEDITOR.plugins.add('CustomBorders',
    {
        requires: ['iframedialog'],
        init: function(editor) {
            var me = this;
            var _editorPath = me.path;
            CKEDITOR.dialog.add('CustomBordersDialog', function() {
                return {
                    title: 'Custom Borders Dialog',
                    minWidth: 800,
                    minHeight: 550,
                    contents:
                       [
                          {
                              id: 'iframe',
                              label: 'Custom Borders',
                              //expand: true,
                              elements:
                                   [
                                      {
                                          type: 'html',
                                          id: 'pageCustomBorders',
                                          label: 'Custom Borders',
                                          style: 'width : 100%;',
                                          html: '<iframe src="/MyWebsite/_CustomBorders" frameborder="0" name="iframeCustomBorders" id="iframeCustomBorders" allowtransparency="1" style="width:100%;height:600px;margin:0;padding:0;"></iframe>'
                                      }
                                   ]
                          }
                       ],
                    onOk: function() {
                        //do nothing
                    },
                    onShow: function () {
                        document.getElementById(this.getButton('ok').domId).style.display = 'none';
                        document.getElementById(this.getButton('cancel').domId).style.display = 'none';
                    }
                };
            });

            editor.addCommand('CustomBorders', new CKEDITOR.dialogCommand('CustomBordersDialog'));

            editor.ui.addButton('CustomBorders',
            {
                label: 'Custom Borders',
                command: 'CustomBorders',
                icon: this.path + 'images/icon.gif'
            });
        }
    });
})();

