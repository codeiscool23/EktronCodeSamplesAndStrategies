<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GetListForThreeTier.aspx.cs" Inherits="Templates_GetForThreeTier" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <label><em>get information from the application layer</em></label>
            <asp:Button runat="server" ID="buttonForGets" OnClick="getInfoFromApplicationLayer" />
            <br />
            <br />


            <label><strong>div For Temp List</strong></label>
            <div id="divForTempList">
                <ul id="templateList" runat="server">
                </ul>
            </div>
            <br />
            <label><strong>div For Temp Item</strong></label>
            <div id="DiveFortemplateItem">
                <ul id="templateItem" runat="server">
                </ul>
            </div>
            <br />
            <label><strong>div for Folder List</strong></label>
            <div id="divForFolder">
                <ul id="folderList" runat="server">
                </ul>
            </div>
            <br />
             <label><strong>div for Folder Item</strong></label>
            <div id="divForFolderItem">
                <ul id="folderItem" runat="server">
                </ul>
            </div>
            <br />
            <label><strong>div for content list</strong></label>
            <div id="divForContent">
                <ul id="contentList" runat="server">
                </ul>
            </div>
            <br />
             <label><strong>div for Content Item</strong></label>
            <div id="divForContentItem">
                <ul id="contentItem" runat="server">
                </ul>
            </div>
            <br />
            <label><strong>div for menu list</strong></label>
            <div id="divForMenu">
                <ul id="menuList" runat="server">
                </ul>
            </div>
            <br />
             <label><strong>div for menu Item</strong></label>
            <div id="divForMenuItem">
                <ul id="menuItem" runat="server">
                </ul>
            </div>
            <br />
            <label><strong>div for Taxonomy list</strong></label>
            <div id="divForTaxonomy">
                <ul id="taxonomyList" runat="server">
                </ul>
            </div>
            <br />
            
             <label><strong>div for Taxonomy Item</strong></label>
            <div id="divForTaxItem">
                <ul id="taxItem" runat="server">
                </ul>
            </div>
            <br />
             <label><strong>div for Taxonomy Item list</strong></label>
            <div id="divForTaxonomyItemlist">
                <ul id="taxItemlist" runat="server">
                </ul>
            </div>
            <br />
            
           
            <label><strong>div for Users list</strong></label>
            <div id="divForUsers">
                <ul id="UsersList" runat="server">
                </ul>
            </div>
            <br />
            
             <label><strong>div for User Item</strong></label>
            <div id="divforUserItem">
                <ul id="userItem" runat="server">
                </ul>
            </div>
            <br />
            <label><strong>div for User Groups List</strong></label>
            <div id="divForUserGroups">
                <ul id="userGroupsList" runat="server">
                </ul>
            </div>
            <br />
            
             <label><strong>div for User Group Item</strong></label>
            <div id="divForUserGroupItem">
                <ul id="userGroupItem" runat="server">
                </ul>
            </div>
            <br />
            <label><strong>div for Asset List</strong></label>
            <div id="divForAssetList">
                <ul id="assetList" runat="server">
                </ul>
            </div>
            <br />
            
             <label><strong>div for Asset Item</strong></label>
            <div id="divForAssetItem">
                <ul id="assetItem" runat="server">
                </ul>
            </div>
            <br />




        </div>
    </form>
</body>
</html>
