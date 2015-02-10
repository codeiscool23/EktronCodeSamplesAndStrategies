using System;
using System.Linq;
using System.Collections.Generic;
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
using System.Web;
using System.IO;


public partial class Templates_Create : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void AddTemplate(object sender, EventArgs e)
    {
        long templateId = AddTemplateFunc();
        Outputtemp.Visible = true;

        long[] folderIds = CreateFolders(templateId);
        Outputfolder.Visible = true;
        long[] contentIds = contentCreation(folderIds, templateId);
        Outputcon.Visible = true;

        AddTaxonomyAndTaxonomyItem(contentIds);
        OutputTax.Visible = true;

        CreateMenuForApp();
        Outputmenu.Visible = true;


      

        long groupId = CreatUserGroup();
        long[] ArrayOfUserIDs = CreateUsers(groupId);
        Outputuser.Visible = true;
    }

    protected void Addassets(object sender, EventArgs e)
    {

        CreateAssetsForApp();
        Outputasset.Visible = true;
    }


    public void CreateAssetsForApp()
    {
        var fd = new FolderData();
        var fm = new FolderManager(ApiAccessMode.Admin);
        var fc = new FolderCriteria();
        fc.AddFilter(FolderProperty.FolderName, CriteriaFilterOperator.EqualTo, "testfolder1");
        fd = fm.GetList(fc).First();

        Ektron.Cms.Framework.Content.AssetManager am = new Ektron.Cms.Framework.Content.AssetManager(ApiAccessMode.Admin);
        HttpPostedFile postedFile = uxFilePath.PostedFile;
        int fileLength = postedFile.ContentLength;
        byte[] fileData = new byte[fileLength];
        postedFile.InputStream.Read(fileData, 0, fileLength);
        ContentAssetData contentAssetData = new ContentAssetData()
        {
            FolderId = fd.Id,
            Title = "AssetContent1",
            Teaser = "AssetcontentTeaser",
            File = fileData,
            LanguageId = 1033,
            AssetData = new Ektron.Cms.Common.AssetData()
            {
                FileName = Path.GetFileName(postedFile.FileName)
            }
        };

        am.Add(contentAssetData);

    }

    public void CreateMenuForApp()
    {
        MenuManager menuManager = new MenuManager(ApiAccessMode.Admin);
        Ektron.Cms.Organization.MenuData menuData = new Ektron.Cms.Organization.MenuData();
        for (var i = 0; i < 5; i++)
        {
            menuData.Text = "Menu " + i;
            menuData.Description = "This is the description for menu " + i;
            menuManager.Add(menuData);
        }
    }

    public void AddTaxonomyAndTaxonomyItem(long[] cids)
    {
       
       
        var taxMan = new TaxonomyManager(ApiAccessMode.Admin);
        var taxItemMan = new TaxonomyItemManager(ApiAccessMode.Admin);
        var taxIDArr = new long[5];

        for (var i = 0; i < 5; i++)
        {
            var td = new TaxonomyData();
            td.Name = "Taxonomy " + i;
            td.ParentId = 0;
            td.Description = "Description for Taxonomy " + i;
            td.Visible = true;
            //td.TaxonomyType = Ektron.Cms.Common.EkEnumeration.TaxonomyType.Content;
           td = taxMan.Add(td);
            taxIDArr[i] = td.Id;
        }

        for (var t = 0; t < 5; t++)
        {
            var tid = new TaxonomyItemData();
            tid.TaxonomyId = taxIDArr[t];
            tid.ItemType = EkEnumeration.TaxonomyItemType.Content;
            tid.ItemId = cids[t];
           tid =  taxItemMan.Add(tid);
        }
    }

    //create permission specific users group
    public long CreatUserGroup()
    {
        long ugID = 0;
        UserGroupManager userGroupmanager = new UserGroupManager(ApiAccessMode.Admin);

        var uG = new UserGroupData()
        {
            Name = "ThreeTierTestingUserGroup",
            IsMemberShipGroup = false
        };

        uG = userGroupmanager.Add(uG);
        ugID = uG.Id;


        return ugID;

    }
    //Add each user to the CMS and add users to their user group    
    public long[] CreateUsers(long userGroupID)
    {
        var uGid = userGroupID;
        string[] names = new string[4] { "ThreeTierUser1", "ThreeTierUser2", "ThreeTierUser3", "ThreeTierUser4" };
        long[] userIDs = new long[4];
        var uM = new UserManager(ApiAccessMode.Admin);
        var uData = new UserData();
        UserGroupManager userGroupmanager = new UserGroupManager(ApiAccessMode.Admin);
        for (var i = 0; i < 4; i++)
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

            userGroupmanager.AddUser(uGid, uData.Id);
            userIDs[i] = uData.Id;
        }

        return userIDs;

    }
    //add the content blockTemplate to the CMS
    public long AddTemplateFunc()
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

        templateData = templateManager.Add(templateData);

        return templateData.Id;
    }

    //create each of the needed folders
    public long[] CreateFolders(long templateDataId)
    {
        var fm = new FolderManager(ApiAccessMode.Admin);
        string[] names = new string[5] { "testfolder1", "testfolder2", "testfolder3", "testfolder4", "testfolder5" };
        //populate folder data object assigning the PageLayout.aspx id to the folder
        long[] folderIds = new long[5];
        var fd = new FolderData();
        for (var i = 0; i < 5; i++)
        {

            fd.Name = names[i];
            fd.ParentId = 0;
            fd.IsTemplateInherited = false;
            fd.TemplateId = templateDataId;
            fd.IsPermissionsInherited = false;
            fd = fm.Add(fd);



            folderIds[i] = fd.Id;

        }

        return folderIds;
    }
    //using the ContentBlock.aspx template that is in all workareas create 12 content items
    public long[] contentCreation(long[] folderIDs, long templateID)
    {
        var cm = new ContentManager(ApiAccessMode.Admin);
        var templateData = new Ektron.Cms.Framework.Content.TemplateManager(ApiAccessMode.Admin).GetItem(templateID);
        long[] contentIds = new long[5];

        for (var i = 0; i < 5; i++)
        {


            var cd = new ContentData();
            cd.Title = "Test content " + i;
            cd.Html = "testing content" + i;
            cd.FolderId = folderIDs[i];
            cd.TemplateConfiguration = templateData;
            cd.IsPermissionsInherited = false;
            cd = cm.Add(cd);
            contentIds[i] = cd.Id;
            //Console.Write(counter + ",    ");
        }

        return contentIds;

    }
}