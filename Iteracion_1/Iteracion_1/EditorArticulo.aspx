<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest ="false" CodeBehind="EditorArticulo.aspx.cs" Inherits="Iteracion_1.EditorArticulo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <!-- Latest compiled and minified CSS -->
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" >

     <script src="https://cdn.tiny.cloud/1/ppfs7sld936k48b757gwua5p0k1knn5by42zeg00gm61xqwb/tinymce/5/tinymce.min.js" referrerpolicy="origin"></script>

      <script>tinymce.init({
  selector: '#<%=txtArticulo.ClientID%>',
  height: 400,
  plugins: [
    "advlist autolink lists link image charmap print preview anchor",
    "searchreplace visualblocks code fullscreen",
    "insertdatetime media table paste imagetools wordcount"
    ],
  image_dimensions: false,
   image_description: false,
  image_caption: true,
  toolbar: "insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image",
    content_css: '//www.tiny.cloud/css/codepen.min.css',
});</script>



    <script>tinymce.init({
    selector: '#<%=txtResumen.ClientID%>'});</script>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>


    <style type="text/css">
        .auto-style1 {
            font-size: x-large;
        }
        .auto-style2 {
            font-size: large;
        }
        .auto-style3 {
            width: 137px;
            height: 27px;
            position: absolute;
            top: 25px;
            left: 975px;
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
        <asp:TextBox ID="txtResumen" runat="server" Height="92px" Width="50%"   TextMode="MultiLine" Font-Size="Medium" maxsize="10" ></asp:TextBox>
            <script>
                $("txtResumen[maxsize]").each(function () {
                    $(this).attr('maxlength', $(this).attr('maxsize'));
                });
            </script>
        <br />
        <strong>Seleccione la categoria a la cual pertenece su articulo:<br />
        </strong>
        <asp:CheckBoxList ID="selectCategorias" runat="server" RepeatColumns ="3" style="height: 34px">
        </asp:CheckBoxList>
        <br />
        </span>
        <br />
        <strong>
        <asp:Label ID="lblArticulo" runat="server" CssClass="auto-style1" Text="Articulo:"></asp:Label>
        </strong>
        <br />
        <asp:TextBox ID="txtArticulo" runat="server"  Height=" 400px" Width="50%" TextMode="MultiLine"></asp:TextBox>
        <br />
        <br />
        <span class="auto-style2"><strong>Suba un articulo de su computadora:<asp:FileUpload ID="Seleccionador" runat="server" />
        <asp:Button ID="btnGuardar" runat="server" OnClick="btnGuardar_Click"  OnClientClick ="return confirm('¿Esta seguro que desea guardar este artículo?')" Text="Guardar" />
        <br />
        <br />
        <asp:Label ID="lblMensaje" runat="server" CssClass="auto-style3"></asp:Label>
        <br />
        </strong></span>
        <br />
        <span class="auto-style2"><strong>
        <br />
        <br />
        </strong></span>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    </form>
</body>
</html>
