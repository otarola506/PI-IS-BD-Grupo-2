<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EdicionRespMP.aspx.cs" Inherits="Iteracion_1.EdicionRespMP" %>
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
                    <asp:Button ID="Button2" runat="server" CommandArgument = '<%# Eval("pregIdFK") + ";" + Eval("repId") %>' OnClientClick ="return confirm('¿Seguro que desea editar esta respuesta?')" OnClick="GEButton_OnClick" Text="Guardar Cambios" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
