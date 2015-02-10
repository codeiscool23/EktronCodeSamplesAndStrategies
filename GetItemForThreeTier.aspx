<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GetItemForThreeTier.aspx.cs" Inherits="Templates_GetItemForThreeTier" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
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
            <div id="divForFolderList">
                <ul id="folderList" runat="server">
                </ul>
            </div>
            <br />
            <label><strong>div for content list</strong></label>
            <div id="divForContentList">
                <ul id="contentList" runat="server">
                </ul>
            </div>
            <br />
            <label><strong>div for menu list</strong></label>
            <div id="divForMenuList">
                <ul id="menuList" runat="server">
                </ul>
            </div>
            <br />
            <label><strong>div for Taxonomy list</strong></label>
            <div id="divForTaxonomyList">
                <ul id="taxonomyList" runat="server">
                </ul>
            </div>
            <br />
            <label><strong>div for Users list</strong></label>
            <div id="divForUsersList">
                <ul id="UsersList" runat="server">
                </ul>
            </div>
            <br />
            <label><strong>div for User Groups List</strong></label>
            <div id="divForUserGroupsList">
                <ul id="userGroupsList" runat="server">
                </ul>
            </div>

        </div>
    </div>
    </form>
</body>
</html>
