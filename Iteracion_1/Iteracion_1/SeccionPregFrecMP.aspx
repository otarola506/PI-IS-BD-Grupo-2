<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SeccionPregFrecMP.aspx.cs" Inherits="Iteracion_1.SeccionPregFrecMP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
 
    <asp:Label ID="Label1" runat="server" Text="Preguntas Frecuentes" AutoSize="True" style="font-weight: 700; font-size: xx-large"></asp:Label> 
    <asp:GridView ID="gvPregFrec" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" 
                      BorderStyle="None" BorderWidth="1px" CellPadding="3" style="margin-right: 0px" >
            <Columns>
                <asp:BoundField DataField="pregunta" HeaderText="Preguntas frecuentes" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <br />
                        <br />
                        <asp:Button ID="VerRespButton" runat="server" CommandArgument = '<%# Eval("pregIdPK") + ";" + Eval("pregunta") + ";" + Eval("respuesta") %>' OnClick="VerRespButton_OnClick" Text="Ver respuesta" />
                        <asp:Button ID="EditarButton" runat="server" CommandArgument = '<%# Eval("pregIdPK") + ";" + Eval("pregunta") + ";" + Eval("respuesta") %>' Text="Editar" OnClick="EditarButton_OnClick" /> 
                        <asp:Button ID="BorrarPFButton" runat="server" CommandArgument = '<%# Eval("pregIdPK") %>' OnClientClick ="return confirm('¿Seguro que desea eliminar esta pregunta?')"  OnClick="BorrarPFButton_OnClick" Text="Borrar" />
                    </ItemTemplate>                       
                </asp:TemplateField>                                
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />
        </asp:GridView>
    <br />

    <!--
    <p style="height: 41px; font-size: large;">
        Nota: Si tiene alguna pregunta que no se encuentre en esta sección, puede emitirla en el siguiente espacio. Si su pregunta es frecuente se añadirá a la sección.</p>
    <div>
        <table>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Font-Size="Larger" Text="Pregunta"></asp:Label>
                    &nbsp;&nbsp;&nbsp;
                    <br />
                    <asp:TextBox ID="TxtBoxPN" Font-Size="Larger" TextMode="MultiLine" runat="server" Height="43px" style="margin-bottom: 0px" Width="861px"></asp:TextBox>
                    <br />
                </td>
            </tr>
            <tr>
                <td style="height: 45px">
                    <asp:Button ID="EnviarPregButton" runat="server"  Text="Enviar Pregunta" style="margin-top: 4px" OnClientClick ="return confirm('¿Seguro que desea enviar esta pregunta?')" OnClick="EnviarPregButton_OnClick" />
                </td>
            </tr>
        </table>
    </div>
    -->
</asp:Content>
