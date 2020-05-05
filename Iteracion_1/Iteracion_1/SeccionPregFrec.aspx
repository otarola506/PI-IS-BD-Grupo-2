<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SeccionPregFrec.aspx.cs" Inherits="Iteracion_1.SeccionPregFrec" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="Label1" runat="server" Text="Preguntas Frecuentes" AutoSize="True" style="font-weight: 700; font-size: xx-large"></asp:Label>
        <p>
            <asp:GridView ID="gvPregFrec" runat="server" AutoGenerateColumns="False" >
                <Columns>
                    <asp:BoundField DataField="miembroIdFK" HeaderText="MiembroID" />
                    <asp:BoundField DataField="pregIdPK" HeaderText="PregID" />
                    <asp:BoundField DataField="pregunta" HeaderText="Preguntas Frecuentes" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="VerRespButton" runat="server" CommandArgument = '<%# Eval("pregIdPK") %>' OnClick="VerRespButton_OnClick" Text="Ver respuestas" />
                            <asp:Button ID="RespButton" runat="server" CommandArgument = '<%# Eval("pregIdPK") %>' OnClick="RespButton_OnClick" Text="Escribir Respuesta" />
                        </ItemTemplate>                       
                    </asp:TemplateField>                                
                </Columns>
            </asp:GridView>
        </p>
        <p style="height: 41px">
            Nota: Si tiene alguna pregunta que no se encuentre en esta sección, puede emitirla en el siguiente espacio. Si su pregunta es frecuente se añadirá a la sección</p>
        <div>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Font-Size="Larger" Text="Pregunta"></asp:Label>
                        &nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="TxtBoxPN" Font-Size="Larger" TextMode="MultiLine" runat="server" Height="43px" style="margin-bottom: 0px" Width="861px"></asp:TextBox>
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="EnviarPregButton" runat="server"  Text="Enviar Pregunta" style="margin-top: 4px" OnClick="EnviarPregButton_OnClick" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
