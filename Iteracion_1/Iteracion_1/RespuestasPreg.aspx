<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RespuestasPreg.aspx.cs" Inherits="Iteracion_1.RespuestasPreg" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="Label1" runat="server" Text="Respuestas" style="font-size: xx-large"></asp:Label>
        <p>
            <asp:GridView ID="gvRptasPreg" runat="server" AutoGenerateColumns="False" Width="412px" >
                <Columns>
                    <asp:BoundField DataField="miembroIdFK" HeaderText="MiembroID" />
                    <asp:BoundField DataField="repId" HeaderText="RespID" />
                    <asp:BoundField DataField="respuesta" HeaderText="Respuestas" />                              
                </Columns>
            </asp:GridView>
        </p>
        <asp:Button ID="VolverButton" runat="server" Text="Volver a Preguntas Frecuentes" OnClick="VolverButton_OnClick" Width="192px" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="ERButton" runat="server" style="margin-left: 25px" Text="Escribir Respuesta" CommandArgument='<%# Eval("pregIdPK") %>' OnClick="ERButton_OnClick" />
    </form>
</body>
</html>
