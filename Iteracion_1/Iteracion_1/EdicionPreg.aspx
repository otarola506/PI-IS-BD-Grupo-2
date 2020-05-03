<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EdicionPreg.aspx.cs" Inherits="PreguntasWebForms.EdicionPreg" %>

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
                        <asp:Label ID="Label2" runat="server" Font-Size="Larger" Text="Pregunta"></asp:Label>
                        &nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="TxtBoxPN" Font-Size="Larger" TextMode="MultiLine" runat="server"></asp:TextBox>
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="Button2" runat="server" CommandArgument = '<%# Eval("pregIdPK") %>' OnClick="GEButton_OnClick" Text="Guardar Cambios" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
