﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AgregarPregFrecMP.aspx.cs" Inherits="Iteracion_1.AgregarPregFrecMP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />   
    <div>
        <asp:Label ID="AddPregFrecLabel" runat="server" Text="Agregar pregunta frecuente" style="font-size:xx-large"></asp:Label>
        <table>
            <tr>
                <td>
                    <asp:Label ID="PregLabel" runat="server" Font-Size="Larger" Text="Pregunta"></asp:Label>
                    &nbsp;&nbsp;<br />
                    &nbsp;
                    <asp:TextBox ID="TxtBoxP" Font-Size="Larger" TextMode="MultiLine" runat="server" Height="129px" Width="1584px"></asp:TextBox>
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="RespLabel" runat="server" Font-Size="Larger" Text="Respuesta"></asp:Label>
                    &nbsp;&nbsp;<br />
                    &nbsp;
                    <asp:TextBox ID="TxtBoxR" Font-Size="Larger" TextMode="MultiLine" runat="server" Height="129px" Width="1584px"></asp:TextBox>
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="VolverButton" runat="server" Text="Volver" OnClick="VolverButton_OnClick" Width="125px" />
                    &nbsp;&nbsp;
                    <asp:Button ID="GRButton" runat="server" OnClientClick ="return confirm('¿Seguro que desea agregar esta pregunta con esta respuesta a la sección de preguntas frecuentes?')" OnClick="GRButton_OnClick" Text="Añadir a preguntas frecuentes" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
