<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Articulos.aspx.cs" Inherits="Iteracion_1.Articulos" %>

<asp:Content ID="articulos" ContentPlaceHolderID="MainContent" runat="server">
    
    <div id="articles">
        <asp:GridView ID="articlesTable" runat="server" Height="276px" Width="630px" AutoGenerateColumns="false" CellPadding="3" BorderStyle="None" BorderWidth="1px" OnRowDataBound="OnRowDataBound">
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
                                <asp:TemplateField HeaderText ="Autor">
                    <ItemTemplate>
                        <asp:Label ID="autor" Text='<%# Eval("nombre")%>' runat="server"/>
                    </ItemTemplate>
                </asp:TemplateField>
                                <asp:TemplateField HeaderText ="Resumen">
                    <ItemTemplate>
                        <asp:Label ID="resumen" Text=<%# Eval("resumen")%> runat="server"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton id="lnkVerMas" Text="Ver más" OnClick="lnkVerMasArt" runat="server"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

</asp:Content>

