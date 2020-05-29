<%@ Page Title="Contenido Articulo" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MostrarContenido.aspx.cs" Inherits="Iteracion_1.MostrarContenido" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p class="text-center">
        <br />
    &nbsp;
        <asp:Label ID="lblMensaje" runat="server" Font-Size="X-Large"></asp:Label>
    </p>
    <p>
        <asp:Label ID="Label1" runat="server" style="font-family: Arial; font-weight: bold; font-size: 16pt" Text="Titulo"></asp:Label>
    </p>

    <p>
        <asp:Label ID="lblTitulo" runat="server" style="width: 35px; height: 16px; left: 15px; top: 143px; font-family: Arial; font-size: 14pt"></asp:Label>
    </p>

    <p>
        <asp:Label ID="Label3" runat="server" style="font-family: Arial; font-weight: bold; font-size: 16pt" Text="Autor(es)"></asp:Label>
        <br />
        <asp:Label ID="lblAutores" runat="server" style="width: 35px; height: 16px; left: 15px; top: 143px; font-family: Arial; font-size: 14pt"></asp:Label>
    </p>
    <p style="height: 28px">
        <strong><span style="font-size: large">Numero de visitas :</span></strong><asp:Label ID="lblvisitas" runat="server"></asp:Label>
    </p>
    <p style="height: 28px; font-size: large">
        <strong>Puntuacion :<asp:Label ID="lblPuntuacion" runat="server"></asp:Label>
        </strong>
    </p>

    <p>
        <asp:Label ID="resumen" runat="server" style="font-family: Arial; font-weight: bold; font-size: 16pt" Text="Resumen"></asp:Label>
    </p>
    <p>
        <asp:Label ID="lblResumen" runat="server" style="font-family: Arial; font-size: 14pt"></asp:Label>
    </p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        <asp:Label ID="Label2" runat="server" style="font-size: 16pt; font-family: Arial; font-weight: bold" Text="Contenido"></asp:Label>
    </p>
    <p>
        <asp:Label ID="lblContenido" runat="server" style="font-family: Arial; font-size: 14pt"></asp:Label>
    </p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        <asp:Label ID="lblCategorias" runat="server" style="font-size: 16pt; font-family: Arial; font-weight: bold" Text="Categorias"></asp:Label>
    </p>
    <p>
        <asp:Label ID="lblCategoria" runat="server" style="font-family: Arial; font-size: 14pt"></asp:Label>
    </p>
    <p style="font-size: large">
        <strong>¿Que opina del articulo?</strong></p>
    <p style="font-size: large">
        <asp:DropDownList ID="opVotos" runat="server">
            <asp:ListItem Value="0">No votar</asp:ListItem>
            <asp:ListItem Value="1">Me gustó</asp:ListItem>
            <asp:ListItem Value="2">No me gustó</asp:ListItem>
        </asp:DropDownList>
        <asp:Button ID="btnEnviar" runat="server" OnClick="Button1_Click" Text="Enviar" />
    </p>
    <p style="font-size: large">
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
</asp:Content>
