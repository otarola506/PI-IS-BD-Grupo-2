<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RespuestasPregMP.aspx.cs" Inherits="Iteracion_1.RespuestasPregMP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="PregLabel" runat="server" Text="" style="font-size:xx-large"></asp:Label>
    <br />
    <br />
    <asp:Label ID="Label1" runat="server" Text="Respuestas" style="font-size: x-large"></asp:Label>
    <p>
        <asp:GridView ID="gvRptasPreg" runat="server" AutoGenerateColumns="False" Width="412px" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" >
            <Columns>
                <asp:BoundField DataField="nombre" HeaderText="Miembro" />
                <asp:BoundField DataField="respuesta" HeaderText="Respuestas" />                              
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
    <asp:Button ID="VolverButton" runat="server" Text="Volver a Preguntas Frecuentes" OnClick="VolverButton_OnClick" Width="192px" />
    &nbsp;&nbsp;
    <asp:Button ID="ERButton" runat="server" style="margin-left: 25px" Text="Escribir Respuesta" CommandArgument='<%# Eval("pregIdPK") %>' OnClick="ERButton_OnClick" />
</asp:Content>
