<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EdicionPregMP.aspx.cs" Inherits="Iteracion_1.EdicionPregMP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Font-Size="Larger" Text="Pregunta"></asp:Label>
                    &nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="TxtBoxPN" Font-Size="Larger" TextMode="MultiLine" runat="server"></asp:TextBox>
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="VolverButton" runat="server" Text="Volver" OnClick="VolverButton_OnClick" Width="125px" />
                    &nbsp;&nbsp;
                    <asp:Button ID="Button2" runat="server" CommandArgument = '<%# Eval("pregIdPK") %>' OnClientClick ="return confirm('¿Seguro que desea editar esta pregunta?')" OnClick="GEButton_OnClick" Text="Guardar Cambios" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
