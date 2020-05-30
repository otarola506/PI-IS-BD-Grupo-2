<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" ValidateRequest ="false" AutoEventWireup="true" CodeBehind="EditorResumen.aspx.cs" Inherits="Iteracion_1.EditorResumen" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <script src="https://cdn.tiny.cloud/1/ppfs7sld936k48b757gwua5p0k1knn5by42zeg00gm61xqwb/tinymce/5/tinymce.min.js" referrerpolicy="origin"></script>

    <script>tinymce.init({
    selector: '#<%=txtResumen.ClientID%>'});</script>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>

        <br />
        <span class="auto-style1" style="font-size: large"><strong>Título</strong></span><strong><span class="auto-style1">:</span></strong><br />
        <span class="auto-style1">
            <script>
                $("txtResumen[maxsize]").each(function () {
                    $(this).attr('maxlength', $(this).attr('maxsize'));
                });
            </script>
        <asp:TextBox ID="txtTitulo" runat="server"  Width="1088px" Height="23px" CssClass="auto-style6" style="margin-bottom: 24px" ></asp:TextBox>
        <br />
     <strong><span style="font-size: large">Resumen:</span></strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        <asp:TextBox ID="txtResumen" runat="server" Height="266px" Width="61%"   TextMode="MultiLine" Font-Size="Medium" maxsize="10" CssClass="panel-group" ></asp:TextBox>
        <strong><br />
        </strong><span class="auto-style2"><strong>
     <br />
     <span style="font-size: large">Subir archivo nuevo con cambios:</span><br />
     <asp:FileUpload ID="subirArchivo" runat="server" Width="527px" style="margin-bottom: 30px" />
        <asp:Button ID="btnGuardar" runat="server" OnClick="btnGuardar_Click"  OnClientClick ="return confirm('¿Está seguro enviar a revisión este artículo?')" Text="Someter a Revision" CssClass="panel-group" />
        <asp:Button ID="btnDescargar" runat="server" Text="Descargar" OnClick="btnDescargar_Click" CssClass="auto-style5" style="margin-bottom: 19px" />
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
