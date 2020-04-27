<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="editorTexto.aspx.cs" Inherits="Editor.editorTexto" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <!-- Latest compiled and minified CSS -->
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" >

    <script src="https://cdn.tiny.cloud/1/no-api-key/tinymce/5/tinymce.min.js" referrerpolicy="origin"></script>
      <script>tinymce.init({
  selector: '#<%=txtArticulo.ClientID%>',
  height: 400,
  plugins: [
    "advlist autolink lists link image charmap print preview anchor",
    "searchreplace visualblocks code fullscreen",
    "insertdatetime media table paste imagetools wordcount"
  ],
  toolbar: "insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image",
  content_css: '//www.tiny.cloud/css/codepen.min.css'
});</script>
    <script>tinymce.init({selector:'#<%=txtResumen.ClientID%>'});</script>

    <style type="text/css">
        .auto-style1 {
            font-size: x-large;
        }
        .auto-style2 {
            font-size: large;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
        <strong>
        <asp:Label ID="lblTitulo" runat="server" Text="Titulo : " CssClass="auto-style1"></asp:Label>
        </strong>
        <asp:TextBox ID="txtTitulo" runat="server" Width="469px"></asp:TextBox>
        <br />
        <span class="auto-style1"><strong>Resumen:</strong></span><br />
        <span class="auto-style1">
        <asp:TextBox ID="txtResumen" runat="server" Height="35px" Width="50%" OnTextChanged="txtResumen_TextChanged" TextMode="MultiLine"></asp:TextBox>
        </span>
        <br />
        <br />
        <strong>
        <asp:Label ID="lblArticulo" runat="server" CssClass="auto-style1" Text="Articulo:"></asp:Label>
        </strong>
        <br />
        <asp:TextBox ID="txtArticulo" runat="server" OnTextChanged="TextBox1_TextChanged" Height=" 400px" Width="50%" TextMode="MultiLine"></asp:TextBox>
        <br />
        <br />
        <span class="auto-style2"><strong>Suba un articulo de su computadora:<asp:FileUpload ID="Seleccionador" runat="server" />
        <asp:Button ID="btnGuardar" runat="server" OnClick="btnGuardar_Click" Text="Guardar" />
        <br />
        <br />
        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </strong></span>
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:Label ID="PruebaTirulo" runat="server" Text="----titulo"></asp:Label>
        <br />
        <br />
        <asp:Label ID="pruebaArticulo" runat="server" Text="----Articulo"></asp:Label>
    </form>
</body>
</html>
