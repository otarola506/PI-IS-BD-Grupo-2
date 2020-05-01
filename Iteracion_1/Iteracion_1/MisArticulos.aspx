<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MisArticulos.aspx.cs" Inherits="Iteracion_1.MisArticulos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:GridView ID="tablaArticulos" runat="server" AutoGenerateColumns="false" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="415px" OnRowDataBound ="OnRowDataBound" style="height: 127px; position: absolute; left: 16px; top: 93px">
        <FooterStyle BackColor="White" ForeColor="#000066" />
        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
        <RowStyle ForeColor="#000066" />
        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#007DBB" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#00547E" />
        <Columns>
            <asp:TemplateField HeaderText ="Id Artículo">
                <ItemTemplate>
                    <asp:Label ID="artId" Text='<%# Eval("artIdPK")%>' runat="server" />
                </ItemTemplate>

            </asp:TemplateField>

            <asp:TemplateField HeaderText ="Título del articulo">
                <ItemTemplate>
                    <asp:Label  ID="titulo" Text='<%# Eval("titulo")%>' runat="server" />
                </ItemTemplate>

            </asp:TemplateField>

            <asp:TemplateField HeaderText ="Resumen">
                <ItemTemplate>
                    <asp:Label ID="resumen" Text='<%# Eval("resumen")%>' runat="server" />
                </ItemTemplate>

            </asp:TemplateField>

            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton id="lnkEditar" Text="Editar" runat="server" OnClick ="lnkEdicion"/>
                    <asp:LinkButton id="lnkBorrar" Text="Borrar" runat="server" OnClick ="lnkBorrado"/>
                    <asp:LinkButton id="lnkVerMas" Text="Ver más" runat="server" OnClick ="lnkVerMasArt"/>
                </ItemTemplate>

                
                    

            </asp:TemplateField>

        </Columns>
    </asp:GridView>
    <br />
    <br />
    <asp:Button ID ="btnAgregarArticulo" Text="Crear articulo nuevo" runat="server" OnClick="btnAgregarArticulo_Click" style="width: 195px; height: 26px; position: absolute; left: 494px; top: 95px" />
    <br />
    <br />
    
    <asp:Label ID ="lblFallo" runat ="server" ForeColor ="Red" style="width: 53px; height: 16px; position: absolute; left: 14px; top: 255px" />
    
    <br />
    <asp:Label ID ="lblExito" runat ="server" ForeColor ="Green" style="width: 52px; height: 16px; position: absolute; left: 16px; top: 233px" />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <p>
    </p>
</asp:Content>
