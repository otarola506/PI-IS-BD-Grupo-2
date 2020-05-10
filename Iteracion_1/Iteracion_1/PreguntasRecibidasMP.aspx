<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PreguntasRecibidasMP.aspx.cs" Inherits="Iteracion_1.PreguntasRecibidasMP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />   
    <asp:Label ID="Label1" runat="server" Text="Preguntas Recibidas" style="font-size: xx-large"></asp:Label>
    <asp:HiddenField ID="hfPregID" runat="server" />
    <p>
        <asp:GridView ID="gvPreguntas" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
            <Columns>
                <asp:BoundField DataField="preguntaRecibida" HeaderText="Preguntas" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <br />
                        <br />
                        <asp:Button ID="FrecButton1" runat="server" CommandArgument = '<%# Eval("preguntaRecibida") %>' title="Agregar a preguntas frecuentes"  OnClick="FButton_OnClick" Text="Agregar" />
                        <asp:Button ID="DescButton" runat="server" CommandArgument = '<%# Eval("pregRecId") %>' OnClientClick ="return confirm('¿Seguro que desea eliminar esta pregunta?')" OnClick="DButton_OnClick" Text="Descartar" />
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
    </p>
        <asp:Button ID="FrecButton2" runat="server" CommandArgument = '<%# Eval("preguntaRecibida") %>' title="Añadir una pregunta a la sección de preguntas frecuentes"  OnClick="FButton_OnClick" Text="Añadir una pregunta frecuente" Width="228px" />
    <p>

    </p>
</asp:Content>
