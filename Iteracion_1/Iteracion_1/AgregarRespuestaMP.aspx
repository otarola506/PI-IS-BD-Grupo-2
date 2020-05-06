<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AgregarRespuestaMP.aspx.cs" Inherits="Iteracion_1.AgregarRespuestaMP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Font-Size="Larger" Text="Respuesta"></asp:Label>
                    &nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="TxtBoxRN" Font-Size="Larger" TextMode="MultiLine" runat="server"></asp:TextBox>
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="VolverButton" runat="server" Text="Volver" OnClick="VolverButton_OnClick" Width="125px" />
                    &nbsp;&nbsp;
                    <asp:Button ID="GRButton" runat="server" CommandArgument = '<%# Eval("pregIdPK") %>' OnClick="GRButton_OnClick" Text="Añadir respuesta" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
