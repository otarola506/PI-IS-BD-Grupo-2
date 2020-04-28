<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MisArticulos.aspx.cs" Inherits="Iteracion_1.MisArticulos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:GridView ID="tablaArticulos" runat="server" AutoGenerateColumns="false" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="415px" >
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
                    <asp:Label Text='<%# Eval("titulo")%>' runat="server" />
                </ItemTemplate>

            </asp:TemplateField>

            <asp:TemplateField HeaderText ="Resumen">
                <ItemTemplate>
                    <asp:Label Text='<%# Eval("resumen")%>' runat="server" />
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
    <asp:Button ID ="btnAgregarArticulo" Text="Crear articulo nuevo" runat="server" />
    <br />
    <asp:Label ID ="lblExito" Text ="" runat ="server" ForeColor ="Green" />
    <br />
    <asp:Label ID ="lblFallo" Text ="" runat ="server" ForeColor ="Red" />
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
    <br />
    <br />
    <p>
    </p>
</asp:Content>
