<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BusquedaTopico.aspx.cs" Inherits="Iteracion_1.BusquedaTopico" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" method="post">
        <div>
        </div>
        <asp:DropDownList AppendDataBoundItems="true" ID="DropDownList1" runat="server" DataSourceID="SqlDataSource1" DataTextField="nombre" DataValueField="categoriaIdPK" style="z-index: 1; top: 44px; position: absolute">
            <asp:ListItem Text="Select a Category" Value="null" Selected="true"></asp:ListItem>
        </asp:DropDownList>
        <asp:GridView ID="tabla" runat="server" style="top: 160px; left: 33px; position: absolute; height: 152px; width: 232px">
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:BD_Grupo2ConnectionString %>" SelectCommand="SELECT * FROM [Categoria] ORDER BY [nombre]"></asp:SqlDataSource>
        <asp:TextBox ID="TextBox1" runat="server" OnTextChanged="TextBox1_TextChanged" style="z-index: 1; left: 28px; top: 97px; position: absolute"></asp:TextBox>
        <asp:Button ID="Btn_ConsultaXTopico" runat="server" OnClick="Button1_Click" style="z-index: 1; left: 227px; top: 97px; position: absolute" Text="Consultar" />
    </form>
</body>
</html>
