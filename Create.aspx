<%@ Page Language="C#" EnableViewState="true" AutoEventWireup="true" CodeFile="Create.aspx.cs" Inherits="Templates_Create" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div runat="server" id="addTemp" visible="true">
            <label><strong>add template </strong></label>
            <br />
            <asp:Button OnClick="AddTemplate" runat="server" Text="add template" ID="AddTemplatetoCMS"/><br />
        </div>
        
        <div runat="server" id="Outputtemp" visible="false">Template has been added</div>
        <br />

           <div runat="server" id="Outputfolder" visible="false">folders have been added</div>
        <br />

          
        <div runat="server" id="Outputcon" visible="false">Content has been added</div>

        <br />
     
                   <div runat="server" id="Outputmenu" visible="false">menus have been added</div>
        <br />

       
        <div runat="server" id="OutputTax" visible="false">Taxonomy And TaxonomyItems have been added</div>
        <br />
       
        <div runat="server" id="Outputuser" visible="false">Users and UserGroups have been added </div>
        <br />
        
        <div runat="server" id="addAss" visible="true">
            <label><strong>add assets to folder</strong></label><br />
            <asp:Label ID="uxFilePathLabel" AssociatedControlID="uxFilePath" CssClass="span-3 last" runat="server" Text="* File Path:" />
            <asp:FileUpload ID="uxFilePath" size="29.75" CssClass="span-4" runat="server" />
            <asp:Button OnClick="Addassets" runat="server" Text="add assets to folder" ID="AddassetstoCMS" /><br />
        </div>
        <div runat="server" id="Outputasset" visible="false">Assets have been added</div>
        <br />


    </form>
</body>
</html>
