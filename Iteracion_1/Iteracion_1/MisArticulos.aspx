<%@ Page Title="Mis Articulos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MisArticulos.aspx.cs" Inherits="Iteracion_1.MisArticulos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div style="height: 41px">
        <strong>
        <asp:Label ID="lblArticulo" runat="server" style="font-family: Arial; font-size: 20px; position: relative;"></asp:Label>
        </strong>
        <br />
        <asp:Button ID ="btnAgregarArticulo" Text="Crear articulo nuevo" runat="server" OnClick="btnAgregarArticulo_Click" class="btn-success" style="position: relative;" />
        <br />
    </div>
    <br />
    <div>
        <asp:GridView ID="tablaArticulos" runat="server" AutoGenerateColumns="false" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnRowDataBound ="OnRowDataBound" style="position: relative;">
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
            <asp:TemplateField HeaderText = "Id Artículo">
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

            <asp:TemplateField HeaderText ="Estado">
                <ItemTemplate>
                    <asp:Label ID="estadoArt" Text='<%# Eval("estado")%>' runat="server" />
                </ItemTemplate>

            </asp:TemplateField>

            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton id="lnkEdicion" ImageUrl="~/Imagenes/Pencil-icon.png" ToolTip= "Editar" runat ="server" OnClick ="lnkEdicion" Height="30px" Width="30px"/>
                    <asp:ImageButton id="lnkBorrar" ImageUrl="~/Imagenes/trash-icon.png" ToolTip ="Borrar" runat="server" Height="30px" Width="30px" OnClick ="lnkBorrado" OnClientClick ="return confirm('¿Esta seguro de eliminar este artículo?')"/>
                    <asp:ImageButton id="lnkVerMas" ImageUrl="~/Imagenes/Button-Add-icon.png" ToolTip="Ver más" runat="server" Height="30px" Width="30px" OnClick ="lnkVerMasArt"/>
                </ItemTemplate>

            </asp:TemplateField>

        </Columns>
    </asp:GridView>
    </div>
    
</asp:Content>
