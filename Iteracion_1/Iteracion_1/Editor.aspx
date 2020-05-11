<%@ Page Title="EditorTexto" Language="C#" MasterPageFile="~/Site.Master" ValidateRequest="false" AutoEventWireup="true" CodeBehind="Editor.aspx.cs" Inherits="Iteracion_1.Editor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://cdn.tiny.cloud/1/ppfs7sld936k48b757gwua5p0k1knn5by42zeg00gm61xqwb/tinymce/5/tinymce.min.js" referrerpolicy="origin"></script>

   <script>tinymce.init({ selector: '#<%=txtResumen.ClientID%>'});</script>
    <script type="text/javascript" src="tinymce/tinymce.min.js"></script>
    <script>
        tinymce.init({
            selector: '#<%=txtArticulo.ClientID%>',
            content_css: '//www.tiny.cloud/css/codepen.min.css',
            plugins:
                [
                "advlist autolink lists link image charmap print preview anchor",
                "searchreplace visualblocks code fullscreen",
                "insertdatetime media table paste imagetools wordcount", "image code"
                ],
           toolbar: "insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent  | undo redo | link image | code",
          image_dimensions: false,
          image_description: false,
          image_caption: true,
          // enable title field in the Image dialog
          image_title: true, 
          // enable automatic uploads of images represented by blob or data URIs
          automatic_uploads: true,
          // add custom filepicker only to Image dialog
          file_picker_types: 'image',
          file_picker_callback: function(cb, value, meta) {
            var input = document.createElement('input');
            input.setAttribute('type', 'file');
            input.setAttribute('accept', 'image/*');

            input.onchange = function() {
              var file = this.files[0];
              var reader = new FileReader();
      
              reader.onload = function () {
                var id = 'blobid' + (new Date()).getTime();
                var blobCache =  tinymce.activeEditor.editorUpload.blobCache;
                var base64 = reader.result.split(',')[1];
                var blobInfo = blobCache.create(id, file, base64);
                blobCache.add(blobInfo);

                // call the callback and populate the Title field with the file name
                cb(blobInfo.blobUri(), { title: file.name });
              };
              reader.readAsDataURL(file);
            };
    
            input.click();
          }
        });
</script>


    <p>
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
    <p>
        &nbsp;</p>
    <p>
        <asp:Button ID="btnGuardar" runat="server" Height="41px" Text="Guardar" Width="124px" OnClick="btnGuardar_Click" OnClientClick ="return confirm('¿Está seguro que desea guardar este artículo?')" />
    </p>
    <p>
        <asp:Label ID="lblprueba" runat="server"></asp:Label>
    </p>
    <p>
        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
    </p>
</asp:Content>
