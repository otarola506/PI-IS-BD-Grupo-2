<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PreguntasRecibidasMP.aspx.cs" Inherits="Iteracion_1.PreguntasRecibidasMP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Preguntas" style="font-size: xx-large"></asp:Label>
    <asp:HiddenField ID="hfPregID" runat="server" />
    <p>
        <asp:GridView ID="gvPreguntas" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
            <Columns>
                <asp:BoundField DataField="nombre" HeaderText="Miembro" />
                <asp:BoundField DataField="pregunta" HeaderText="Preguntas" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="FrecButton" runat="server" CommandArgument = '<%# Eval("pregIdPK") + ";" + Eval("frecuente") %>' title="Agregar a preguntas frecuentes" OnClientClick ="return confirm('¿Seguro de que desea agregar esta pregunta a la sección de preguntas frecuentes?')" OnClick="FButton_OnClick" Text="Agregar" />
                        <asp:Button ID="EditButton" runat="server" CommandArgument = '<%# Eval("pregIdPK") + ";" + Eval("pregunta") %>' OnClick="EButton_OnClick" Text="Editar" />
                        <asp:Button ID="DescButton" runat="server" CommandArgument = '<%# Eval("pregIdPK") %>' OnClientClick ="return confirm('¿Seguro que desea eliminar esta pregunta?')" OnClick="DButton_OnClick" Text="Descartar" />
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
</asp:Content>
