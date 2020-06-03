<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RespuestasPregMP.aspx.cs" Inherits="Iteracion_1.RespuestasPregMP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />   
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

</asp:Content>
