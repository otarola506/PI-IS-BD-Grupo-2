<%@ Page Title="Articulos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Articulos.aspx.cs" Inherits="Iteracion_1.Articulos" %>

<asp:Content ID="articulos" ContentPlaceHolderID="MainContent" runat="server">
    


    <div id="articles">
        <asp:Literal ID="buscandoLiteral" runat="server"></asp:Literal>
        <br />

        <asp:GridView ID="articlesTable" runat="server" AutoGenerateColumns="false" CellPadding="5" BorderStyle="None" BorderWidth="5px" OnRowDataBound="OnRowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="ID">
                    <ItemTemplate>
                        <asp:Label ID="articuloid" Text='<%# Eval("artIdPK")%>' runat="server"/>
                    </ItemTemplate>
                </asp:TemplateField>                
                <asp:TemplateField HeaderText="Titulo del artículo">
                    <ItemTemplate>
                        <asp:Label ID="titulo" Text='<%# Eval("titulo")%>' runat="server"/>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText ="Resumen">
                    <ItemTemplate>
                        <asp:Label ID="resumen" Text=<%# Eval("resumen")%> runat="server"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton id="lnkVerMas" ImageUrl="~/Imagenes/Button-Add-icon.png" ToolTip="Ver más" runat="server" Height="30px" Width="30px" OnClick ="lnkVerMasArt"/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

</asp:Content>

