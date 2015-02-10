using System;
using System.Linq;
using System.Collections.Generic;
using Ektron.Beehive;
using Ektron.Cms;
using Ektron.Cms.BusinessObjects;
using Ektron.Cms.BusinessObjects.Content;
using Ektron.Cms.Content;
using Ektron.Cms.Organization;
using Ektron.Cms.User;
using Ektron.Cms.Common;
using Ektron.Cms.Framework;
using Ektron.Cms.Framework.Content;
using Ektron.Cms.Framework.Organization;
using Ektron.Cms.Framework.Settings;
using Ektron.Cms.Framework.User;
using Ektron.Cms.API.User;
using gnu.javax.crypto.sasl;
using Ektron.Beehive.Repositories;
using javax.security.auth.login;
using Ektron.Beehive.Web.Controllers;
using Ektron.Beehive.Models;


public partial class Templates_SQAtemplate : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void addTemplateandFolderandContent(object sender, EventArgs e)
    {
        bodyIDToHide.Visible = false;
     
        var templateId = AddTemplate();
       
        long[] folderIds = CreateFolders(templateId);
       
        long[] contentIds = contentCreation(folderIds, templateId);
       
        var groupId = CreatUserGroup();
       
        long[] ArrayOfUserIDs = CreateUsers(groupId);
       
        var bhivePage = PageCreation(contentIds);
        PagePublish(bhivePage);

       
        permissionCreation(ArrayOfUserIDs, folderIds, contentIds);
      
        Output9.InnerHtml = "<br/><h3>finshed<h3>";
    }
    //page creation
    protected List<Page> PageCreation(long[] contentIds)
    {
        List<Ektron.Beehive.PageContainer> testPageContainer = new   List<Ektron.Beehive.PageContainer>();
        List<Ektron.Beehive.Page> testPage = new List<Ektron.Beehive.Page>();
        var strings = new String[6];
        strings[0] = TextBox0.Text;
        strings[1] = TextBox1.Text;
        strings[2] = TextBox2.Text;
        strings[3] = TextBox3.Text;
        strings[4] = TextBox4.Text;
        strings[5] = TextBox5.Text;

        for (var c = 0; c < 12; c++)
        {
            testPageContainer.Add(new Ektron.Beehive.PageContainer() { Context = "content", Endpoint = endPointTB.Text + "/api/ektron/content/" + contentIds[c] + "/" });     
        }

        for (var i = 0; i < 6; i++)
        {
            Dictionary<string, PageContainer> PC = new Dictionary<string, PageContainer>();
            PC.Add("MainContent", testPageContainer[2 * i]);
            PC.Add("MainContent2", testPageContainer[2 * i +1]);

            testPage.Add(new Ektron.Beehive.Page() {ID =beehiveSite.Text + "/" + strings[i], Containers = PC, CheckedOutBy = null});
        }
        return testPage;

    }
    //publish all of the pages in the list
    protected void PagePublish(List<Page> testPage)
    {
        var pageRepository = this.CreatePageRepository("admin");
        for (var p = 0; p < 6; p++)
        {
            pageRepository.Publish(testPage[p]);
        }
        Output7.InnerHtml = "<br/><h3>Beehive folder and content created<h3>";
    }
    //mocking the session information
    class FakeCmsSession : ICmsSession
    {
        private UserData user;

        public FakeCmsSession(UserData userToImpersonate)
        {
            this.user = userToImpersonate;
        }

        public UserData Identity
        {
            get { return this.user; }
        }
    }
    //creating the page repository
    private IPageRepository CreatePageRepository(string ownerUserName)
    {
        IContentManager contentManager = ObjectFactory.GetContent();
        IFolderManager folderManager = ObjectFactory.GetFolder();
        IRequestInformationManager requestInformationManager = ObjectFactory.Get<IRequestInformationManager>();
        IUser userManager = ObjectFactory.GetUser();
        IContentPropertyManager contentPropertyManager = ObjectFactory.Get<IContentPropertyManager>();
        IContentStateManager contentStateManager = ObjectFactory.Get<IContentStateManager>();

        var userCriteria = new UserCriteria();
        userCriteria.AddFilter(UserProperty.UserName, CriteriaFilterOperator.EqualTo, ownerUserName);
        userCriteria.PagingInfo.RecordsPerPage = 1;

        ICmsSession session = new FakeCmsSession(userManager.GetList(userCriteria).First());

        return new CmsPageRepository(contentManager, folderManager, requestInformationManager, userManager, contentPropertyManager, contentStateManager, session);
    }
    //create permission specific users group
    public long CreatUserGroup()
    {
        long ugID = 0;
        UserGroupManager userGroupmanager = new UserGroupManager(ApiAccessMode.Admin);

        var uG = new UserGroupData()
        {
            Name = "PermissionsTestingUserGroup",
            IsMemberShipGroup = false
        };
        UserGroupData CheckToSeeIfUserGroupExists;
        Ektron.Cms.API.User.User us = new Ektron.Cms.API.User.User();
        CheckToSeeIfUserGroupExists = us.GetUserGroupByName("PermissionsTestingUserGroup");
        if (CheckToSeeIfUserGroupExists != null)
        {
            ugID = CheckToSeeIfUserGroupExists.Id;
        }
        else
        {
            try
            {
                uG = userGroupmanager.Add(uG);
                ugID = uG.Id;
            }
            catch (Exception ex)
            {
                Response.Write(ex);

            }
        }
        Output5.InnerHtml = "<br/><h3>Group creation completed<h3>";
        return ugID;

    }
    //Add each user to the CMS and add users to their user group    
    public long[] CreateUsers(long userGroupID)
    {
        var uGid = userGroupID;
        string[] names = new string[4] { "FolderAndPagePermission", "FolderAndNotPagePermission", "NotFolderAndPagePermission", "NotFolderAndNotPagePermission" };
        long[] userIDs = new long[4];
        var uM = new UserManager(ApiAccessMode.Admin);
        var uData = new UserData();
        UserGroupManager userGroupmanager = new UserGroupManager(ApiAccessMode.Admin);
        for (var i = 0; i < 4; i++)
        {
            try
            {
                uData.Username = names[i];
                uData.FirstName = names[i];
                uData.LastName = names[i];
                uData.Password = "Ektron";
                uData.Email = "adminA@test.com";
                uData.DisplayName = names[i];
                uData.Address = "1243 Test Addresss Nashua, NH 03063";
                uData.IsMemberShip = false;

                //add custom properties
                uData.CustomProperties = uM.GetCustomPropertyList();
                uData.CustomProperties["Time Zone"].Value = "Central Standard Time";
                uData.CustomProperties["Phone"].Value = "603-333-3333";
                uData = uM.Add(uData);
            }
            catch (UserAlreadyExistsException ex)
            {
                var u = new User();
                uData = u.GetUserByUsername(names[i]);
            }

            userGroupmanager.AddUser(uGid, uData.Id);
            userIDs[i] = uData.Id;
        }
        Output6.InnerHtml = "<br/><h3>Users created<h3>";
        return userIDs;

    }
    //add the content blockTemplate to the CMS
    public long AddTemplate()
    {
        const string fileNameString = "ContentBlock.aspx";
        const string descriptionString = "ContentBlock ";
        const string templateNameString = "ContentBlock";
        var templateManager = new Ektron.Cms.Framework.Content.TemplateManager(ApiAccessMode.Admin);
        //populate template data object defining that is type pb
        var templateData = new TemplateData()
        {
            SubType = EkEnumeration.TemplateSubType.Default,
            TemplateName = templateNameString,
            Description = descriptionString,
            IsToolbarEnabled = true,
            Type = Ektron.Cms.Common.EkEnumeration.TemplateType.Default,
            FileName = fileNameString
        };

        templateManager.Add(templateData);
        Output2.InnerHtml = "<br/><h3>Template added<h3>";
        return templateData.Id;
    }
    public void RemovePermissionsForEveryoneGroup(string itemtype, long id)
    {
        var permissionManager = new PermissionManager(ApiAccessMode.Admin);
        var everyOneGroupUserData = new User().GetUserGroupByName("Everyone");
        //var content = 0;
        //defaults to folder. will change if specified.
        var itemType = Ektron.Cms.Settings.ItemType.Folder;
        permissionManager.DeletePermissionForGroup(itemType, everyOneGroupUserData.Id, id);

    }
    //create each of the needed folders
    public long[] CreateFolders(long templateDataId)
    {
        var fm = new FolderManager(ApiAccessMode.Admin);
        string[] names = new string[6] { "FolderAndBothContentPermission", "FolderAndNoContentPermission", "FolderAndOneContentPermission", "NoFolderAndBothCotentPermission", "NoFolderAndNoContentPermission", "NoFolderAndOneContentPermission" };
        //populate folder data object assigning the PageLayout.aspx id to the folder
        long[] folderIds = new long[6];
        var fd = new FolderData();
        for (var i = 0; i < 6; i++)
        {
            try
            {
                fd.Name = names[i];
                fd.ParentId = 0;
                fd.IsTemplateInherited = false;
                fd.TemplateId = templateDataId;
                fd.IsPermissionsInherited = false;
                fd = fm.Add(fd);

            }
            catch (Exception ex)
            {
                var f = new Ektron.Cms.API.Folder();
                var fID = f.GetFolderId(names[i], 0);

                fd = fm.GetItem(fID);
            }

            folderIds[i] = fd.Id;
            RemovePermissionsForEveryoneGroup("folder", fd.Id);
        }
        Output3.InnerHtml = "<br/><h3>CMS folders created<h3>";
        return folderIds;
    }
    //using the ContentBlock.aspx template that is in all workareas create 12 content items
    public long[] contentCreation(long[] folderIDs, long templateID)
    {
        var cm = new ContentManager(ApiAccessMode.Admin);

        var counter = 0;
        var templateData = new Ektron.Cms.Framework.Content.TemplateManager(ApiAccessMode.Admin).GetItem(templateID);
        long[] contentIds = new long[12];

        for (var i = 0; i < 12; i++)
        {
            if (i != 0 && ((i % 2) == 0))
            {
                counter++;
            }
            var cd = new ContentData();
            cd.Title = "Test content " + i;
            cd.Html = "testing content" + i;
            cd.FolderId = folderIDs[counter];
            cd.TemplateConfiguration = templateData;
            cd.IsPermissionsInherited = false;
            cd = cm.Add(cd);
            contentIds[i] = cd.Id;
            //Console.Write(counter + ",    ");
        }
        Output4.InnerHtml = "<br/><h3>CMS content created<h3>";
        return contentIds;

    }
    //permissions for the folders and content
    public void permissionCreation(long[] userIds, long[] folderIds, long[] contentIds)
    {

        var NewPermissionMgr = new PermissionManager(ApiAccessMode.Admin);
        var uPD = new UserPermissionData()
        {
            UserId = 0,
            ContentId = 0,
            FolderId = 0,
            GroupId = 0,
            CanAdd = true,
            CanAddFolders = true,
            CanAddTask = true,
            CanAddToFileLib = true,
            CanAddToHyperlinkLib = true,
            CanAddToImageLib = true,
            CanAddToQuicklinkLib = true,
            CanApprove = true,
            CanBreakPending = true,
            CanCreateTask = true,
            CanDecline = true,
            CanDelete = true,
            CanDeleteFolders = true,
            CanDeleteTask = true,
            CanDestructTask = true,
            CanEdit = true,
            CanEditApprovals = true,
            CanEditCollections = true,
            CanEditFolders = true,
            CanEditProperties = true,
            CanEditQLinks = true,
            CanEditSumit = true,
            CanEditSummary = true,
            CanHistory = true,
            CanMetadataComplete = true,
            CanOverwriteLib = true,
            CanPreview = true,
            CanPublish = true,
            CanRedirectTask = true,
            CanRestore = true,
            CanSeeProperty = true,
            CanTraverseFolders = true,
            CanView = true,
            IsInherited = false
        };

        //users top loop
        for (var u = 0; u < 4; u++)
        {
            //content child loop
            for (var c = 0; c < 12; c++)
            {
                switch (c)
                {
                    case 0:
                        uPD.FolderId = folderIds[0];
                        uPD.ContentId = contentIds[c];
                        uPD.UserId = userIds[u];
                        NewPermissionMgr.Add(uPD);
                        break;
                    case 1:
                        uPD.FolderId = folderIds[0];
                        uPD.ContentId = contentIds[c];
                        uPD.UserId = userIds[u];
                        NewPermissionMgr.Add(uPD);
                        break;
                    case 5:
                        uPD.FolderId = folderIds[2];
                        uPD.ContentId = contentIds[c];
                        uPD.UserId = userIds[u];
                        NewPermissionMgr.Add(uPD);
                        break;
                    case 7:
                        uPD.FolderId = folderIds[3];
                        uPD.ContentId = contentIds[c];
                        uPD.UserId = userIds[u];
                        NewPermissionMgr.Add(uPD);
                        break;
                    case 8:
                        uPD.FolderId = folderIds[4];
                        uPD.ContentId = contentIds[c];
                        uPD.UserId = userIds[u];
                        NewPermissionMgr.Add(uPD);
                        break;
                    case 11:
                        uPD.FolderId = folderIds[5];
                        uPD.ContentId = contentIds[c];
                        uPD.UserId = userIds[u];
                        NewPermissionMgr.Add(uPD);
                        break;
                    default:
                        break;
                }

            }
            //folder child loop
            for (var f = 0; f < 6; f++)
            {
                if (f < 3)
                {
                    uPD.ContentId = 0;
                    uPD.FolderId = folderIds[f];
                    uPD.UserId = userIds[u];
                    NewPermissionMgr.Add(uPD);
                }
                else
                {
                    break;
                }

            }

            //remote pages
            var beehiveFolder = new Ektron.Cms.API.Folder();
            var fID = beehiveFolder.GetFolderId("Ektron_RemotePages", 0);
            var cm = new ContentManager(ApiAccessMode.Admin);
            ContentCriteria criteria = new ContentCriteria();
            criteria.AddFilter(ContentProperty.FolderId, CriteriaFilterOperator.EqualTo, fID);
            var cl = cm.GetList(criteria);
            if (cl != null)
            {
                if (u == 0 || u == 2)
                {
                    foreach (ContentData cd in cl)
                    {
                        //uPD.FolderId = fID;
                        uPD.ContentId = cd.Id;
                        uPD.UserId = userIds[u];
                        NewPermissionMgr.Add(uPD);
                    }
                }
            }
            if (fID != null)
            {
                if (u < 2)
                {
                    uPD.FolderId = fID;
                    uPD.ContentId = 0;
                    uPD.UserId = userIds[u];
                    NewPermissionMgr.Add(uPD);
                }
                else
                {
                    break;
                }
            }
 
        }

        Output8.InnerHtml = "<br/><h3>Permissions applied to all content and folders<h3>";
    }
 


}


