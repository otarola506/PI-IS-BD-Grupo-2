<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" ValidateRequest ="false" AutoEventWireup="true" CodeBehind="EditorResumen.aspx.cs" Inherits="Iteracion_1.EditorResumen" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <script src="https://cdn.tiny.cloud/1/ppfs7sld936k48b757gwua5p0k1knn5by42zeg00gm61xqwb/tinymce/5/tinymce.min.js" referrerpolicy="origin"></script>

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
        .auto-style4 {
            font-family: Arial;
            color: #FF0000;
        }
        </style>
</head>
        <br />
        <span class="auto-style1"><strong>Titulo</strong></span><strong><span class="auto-style1">:</span></strong><br />
        <span class="auto-style1">
            <script>
                $("txtResumen[maxsize]").each(function () {
                    $(this).attr('maxlength', $(this).attr('maxsize'));
                });
            </script>
        <asp:TextBox ID="txtTitulo" runat="server"  Width="459px"></asp:TextBox>
        <br />
     <strong>Resumen:</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        <asp:TextBox ID="txtResumen" runat="server" Height="158px" Width="50%"   TextMode="MultiLine" Font-Size="Medium" maxsize="10" ></asp:TextBox>
        <strong><br />
        </strong><span class="auto-style2"><strong>
        <asp:Button ID="btnGuardar" runat="server" OnClick="btnGuardar_Click"  OnClientClick ="return confirm('¿Está seguro que desea guardar este resumen?')" Text="Someter a Revision" />
        <asp:Button ID="btnDescargar" runat="server" Text="Descargar" OnClick="btnDescargar_Click" style="height: 40px" />
     <br />
     Subir archivo nuevamente:<br />
     <asp:FileUpload ID="subirArchivo" runat="server" Width="513px" />
     <br />
        </strong></span>
        </span><span class="auto-style2"><strong>
        <br />
     <asp:Label ID="txtError" runat="server" CssClass="auto-style4"></asp:Label>
     <br />
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

</asp:Content>
