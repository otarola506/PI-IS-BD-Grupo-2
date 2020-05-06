<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgregarRespuesta.aspx.cs" Inherits="Iteracion_1.AgregarRespuesta" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
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
    </form>
</body>
</html>
