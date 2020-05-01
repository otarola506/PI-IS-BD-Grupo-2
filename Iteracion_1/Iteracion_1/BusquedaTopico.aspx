<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BusquedaTopico.aspx.cs" Inherits="Iteracion_1.BusquedaTopico" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
        <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource1" DataTextField="nombre" DataValueField="nombre">
        </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:BD_Grupo2ConnectionString2 %>" SelectCommand="SELECT [nombre] FROM [Categoria] ORDER BY [nombre]"></asp:SqlDataSource>
        <asp:Button ID="Btn_ConsultaXTopico" runat="server" OnClick="Button1_Click" style="z-index: 1; left: 164px; top: 37px; position: absolute" Text="Consultar" />
        <asp:GridView ID="tabla" runat="server" style="top: 160px; left: 33px; position: absolute; height: 152px; width: 232px">
        </asp:GridView>
    </form>
</body>
</html>
