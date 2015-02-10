<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SQAtemplate.aspx.cs" Inherits="Templates_SQAtemplate" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="SQATemplate.css" rel="stylesheet" />
    <script src="../../UX/vendor/jQuery/jquery.min.js"></script>
    <script type="text/javascript">

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div runat="server" id="Output1"></div>
        <div runat="server" id="Output2"></div>
        <div runat="server" id="Output3"></div>
        <div runat="server" id="Output4"></div>
        <div runat="server" id="Output5"></div>
        <div runat="server" id="Output6"></div>
        <div runat="server" id="Output7"></div>
        <div runat="server" id="Output8"></div>
        <div runat="server" id="Output9"></div>
        <div id="bodyIDToHide" runat="server">
            <label><strong>Please enter your endpoint</strong></label><br />
            <asp:TextBox runat="server" Width="250" Text="Ex: http://mainline120814" ID="endPointTB"> </asp:TextBox><br />
            <label><strong>Please enter your test beehive site address</strong></label><br />
            <asp:TextBox runat="server" Width="250" Text="Ex: http://beehive120814" ID="beehiveSite"></asp:TextBox><br />
            <label><strong>Please enter your 1st beehive test page name</strong></label><br />
            <asp:TextBox runat="server" Width="250" Text="test.htm" ID="TextBox0"></asp:TextBox><br />
            <label><strong>Please enter your 2nd beehive test page name</strong></label><br />
            <asp:TextBox runat="server" Width="250" Text="test2.htm" ID="TextBox1"></asp:TextBox><br />
            <label><strong>Please enter your 3rd beehive test page name</strong></label><br />
            <asp:TextBox runat="server" Width="250" Text="test3.htm" ID="TextBox2"></asp:TextBox><br />
            <label><strong>Please enter your 4th beehive test page name</strong></label><br />
            <asp:TextBox runat="server" Width="250" Text="test4.htm" ID="TextBox3"></asp:TextBox><br />
            <label><strong>Please enter your 5th beehive test page name</strong></label><br />
            <asp:TextBox runat="server" Width="250" Text="test5.htm" ID="TextBox4"></asp:TextBox><br />
            <label><strong>Please enter your 6th beehive test page name</strong></label><br />
            <asp:TextBox runat="server" Width="250" Text="test6.htm" ID="TextBox5"></asp:TextBox><br />
            <label><strong>The button below will add beehive pages, corresponding remote folder, cms folders, content, users, permissions</strong></label><br />
            <asp:Button OnClick="addTemplateandFolderandContent" runat="server" Text="Add pages, folders, content, users, permissions to CMS and Beehive" ID="submit" /><br />
        </div>
    </form>
</body>
</html>
