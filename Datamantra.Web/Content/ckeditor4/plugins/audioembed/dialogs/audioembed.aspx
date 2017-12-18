<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Audio Embed</title>
    <style type="text/css">
        body
        {
            font: normal 12px Arial,Helvetica,Tahoma,Verdana,Sans-Serif;
        }
        #embed
        {
            margin: 5px 0 10px 0;
            border: 1px solid #a0a0a0;
            font: normal 11px Courier, Fixedsys, serif;
            color: #333;
        }
        
button:hover, input[type="submit"]:hover, input[type="reset"]:hover, input[type="button"]:hover, button:focus, input[type="submit"]:focus, input[type="reset"]:focus, input[type="button"]:focus, button[selected], input[type="submit"][selected], input[type="reset"][selected], input[type="button"][selected] {
border:1px solid #424242;background:#6e6e6e;color:#fff;cursor:pointer;}

button, input[type="submit"], input[type="reset"], input[type="button"], input[type="file"], .button {
background:#e6e6e6 url(../images/buttonBg.gif) repeat-x center top;border:1px solid #a3a3a3;color:#000000;font-weight:bold;font-size:11px;
padding:2px 5px 1px 5px !important;vertical-align:middle;font-family:Tahoma,Arial, Helvetica, sans-serif;-moz-border-radius:2px;-webkit-radius-border:2px;}
    </style>

    <script type="text/javascript">
        function Show() {
            window.open("/ResourceLib/_AudioBrowser", "AudioBrowser", 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=770px, height=450px, top=0, left=0');
        }
    </script>

</head>
<body>
    <form id="formaudio" action="">
    <div>
        <input type="hidden" id="hembed" name="hembed" /><br />
        <input type="text" size="50" id="embed" name="embed" /><br />
        <input type="button" value="Add From Audio Library" onclick="Show();" />
    </div>
    </form>
</body>
</html>
