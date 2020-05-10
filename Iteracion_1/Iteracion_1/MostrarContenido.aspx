<%@ Page Title="Contenido Articulo" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MostrarContenido.aspx.cs" Inherits="Iteracion_1.MostrarContenido" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <br />
        <asp:Label ID="Label1" runat="server" style="font-family: Arial; font-weight: bold; font-size: 16pt" Text="Titulo"></asp:Label>
    </p>
    <p>
        <asp:Label ID="lblTitulo" runat="server" style="width: 35px; height: 16px; left: 15px; top: 143px; font-family: Arial; font-size: 14pt"></asp:Label>
    </p>
    <p>
        <asp:Label ID="resumen" runat="server" style="font-family: Arial; font-weight: bold; font-size: 16pt" Text="Resumen"></asp:Label>
        <asp:GridView ID="grCategorias" runat="server" AutoGenerateColumns ="false" style="width: 162px; height: 64px; position: absolute; left: 813px; top: 115px">
            <Columns>
                <asp:TemplateField HeaderText="Categorias del Articulo">
                    <ItemTemplate>
                    <asp:Label ID="categoria" Text='<%# Eval("nombre")%>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
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
