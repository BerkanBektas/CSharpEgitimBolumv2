<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplicationEgitimi.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Burası web form ön yüzü </title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Burası web form ön yüzü. HTML web sitelerinin ortak dili html'dir.
        </div>
        <p>
            <asp:Button ID="Button1" runat="server" style="height: 26px; width: 56px" Text="Button" />
        </p>
        <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
        <asp:DropDownList ID="DropDownList1" runat="server">
            <asp:ListItem>Elektronik</asp:ListItem>
            <asp:ListItem>Bilgisayar</asp:ListItem>
            <asp:ListItem>Playstation</asp:ListItem>
            <asp:ListItem>Telefon</asp:ListItem>
            <asp:ListItem>Kitap</asp:ListItem>
        </asp:DropDownList>
        <asp:CheckBoxList ID="CheckBoxList1" runat="server" Height="18px">
            <asp:ListItem>elektronik</asp:ListItem>
            <asp:ListItem>Bilgisayar</asp:ListItem>
            <asp:ListItem>Playstation</asp:ListItem>
            <asp:ListItem>Telefon</asp:ListItem>
            <asp:ListItem>Kitap</asp:ListItem>
        </asp:CheckBoxList>
        <asp:Label ID="Label1" runat="server" Text="Label ile ekranda yazı gösteriyoruz"></asp:Label>
        <asp:FileUpload ID="FileUpload1" runat="server" />
        <asp:HiddenField ID="HiddenField2" runat="server" Value="Gizli değer" />
        <asp:HyperLink ID="HyperLink1" runat="server">Site Bağlantısı</asp:HyperLink>
        <asp:Image ID="Image1" runat="server" Height="200px" ImageUrl="~/Images/download.png" />
        <asp:RadioButton ID="RadioButton1" runat="server" Text="Onaylıyorum" OnCheckedChanged="RadioButton1_CheckedChanged" />
    </form>
   
</body>
</html>
