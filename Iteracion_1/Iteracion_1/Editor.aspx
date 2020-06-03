<%@ Page Title="EditorTexto" Language="C#" MasterPageFile="~/Site.Master" ValidateRequest="false" AutoEventWireup="true" CodeBehind="Editor.aspx.cs" Inherits="Iteracion_1.Editor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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



    <p>
        <asp:Label ID="lblMensaje" runat="server" CssClass="auto-style1"></asp:Label>
        <br />
        <strong>
        <asp:Label ID="lblTitulo" runat="server" style="font-size: x-large" Text="Titulo"></asp:Label>
        </strong>
    </p>
    <p>
        <asp:TextBox ID="txtTitulo" runat="server" Width="50%"></asp:TextBox>
    </p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p style="font-size: x-large">
        <strong>Resumen:</strong></p>
    <p style="font-size: x-large">
        <strong>
        <asp:TextBox ID="txtResumen" runat="server" Width="50%" TextMode="MultiLine"></asp:TextBox>
        </strong>
    </p>
    <p style="font-size: x-large">
        <strong>Seleccione la categoria:</strong></p>
    <p style="font-size: x-large">
        <asp:CheckBoxList ID="selectCategorias" runat="server">
        </asp:CheckBoxList>
    </p>
    <p style="font-size: x-large">
        <strong>Articulo:</strong></p>
    <p>
        <asp:TextBox ID="txtArticulo" runat="server" Width="80%" TextMode="MultiLine"></asp:TextBox>
    </p>
    <p>
        &nbsp;</p>
    <p style="font-size: x-large">
        <strong>Subir un archivo:</strong></p>
    <p>
        <asp:FileUpload ID="Seleccionador" runat="server" />
    </p>
    <p style="font-size: x-large">
        <strong>Ingrese los nombres de los miembros autores(separados por una coma):</strong></p>
    <p style="font-size: x-large">
        <asp:TextBox ID="txtAutores" runat="server"></asp:TextBox>
    </p>
    <p style="height: 45px">
        <asp:Button ID="VolverButton" runat="server" Height="41px" Text="Volver a Mis Artículos" OnClick="VolverButton_OnClick" Width="159px" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnProgreso" runat="server" Height="41px" OnClick="btnProgreso_Click" Text="Guardar artículo en progreso" Width="217px" />
        <asp:Button ID="btnRevision" runat="server" Height="41px" Text="Guardar y enviar a revisión" Width="201px" OnClick="btnRevision_Click" OnClientClick ="return confirm('¿Está seguro que desea guardar y enviar a revisión este artículo?')" />
    </p>
    <p>
        &nbsp;</p>
    <p>
        <asp:Label ID="lblprueba" runat="server"></asp:Label>
    </p>
    <p>
        
    </p>
</asp:Content>
