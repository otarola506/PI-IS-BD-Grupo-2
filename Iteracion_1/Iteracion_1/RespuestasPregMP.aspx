<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RespuestasPregMP.aspx.cs" Inherits="Iteracion_1.RespuestasPregMP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="PreguntaLabel" runat="server" Text="Pregunta" style="font-size:xx-large"></asp:Label>
    <br />
    <br />
    <asp:Label ID="PregLabel" runat="server" Text="" style="font-size:x-large"></asp:Label>
    <br />
    <br />
    <asp:Label ID="RespuestaLabel" runat="server" Text="Respuesta" style="font-size: xx-large"></asp:Label>
    <br />
    <br />
    <asp:Label ID="RespLabel" runat="server" Text="" style="font-size: x-large"></asp:Label>
    <br />
    <br />
    <asp:Button ID="VolverButton" runat="server" Text="Volver a Preguntas Frecuentes" OnClick="VolverButton_OnClick" Width="214px" />
    &nbsp;&nbsp;
    <asp:Button ID="EditarButton" runat="server" Text="Editar Pregunta Frecuente" OnClick="EditarButton_OnClick" Width="214px" />

</asp:Content>
