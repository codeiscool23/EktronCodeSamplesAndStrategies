using Ektron.Cms;
using Ektron.Cms.Common;
using Ektron.Cms.Content;
using Ektron.Cms.Framework;
using Ektron.Cms.Framework.Content;
using Ektron.Cms.Framework.Organization;
using Ektron.Cms.Framework.User;
using Ektron.Cms.Organization;
using Ektron.Cms.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Templates_GetForThreeTier : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    //Template   /  Folder  / Content   /    Menu  /   Taxonomy  / TaxonomyItems / Users /Asset
    protected void getInfoFromApplicationLayer(object sender, EventArgs e)
    {
        getTemplate();
        getFolder();
        getContent();
        getMenu();
        getTax();
        getTaxItem();
        getAssetContent();
        getUsers();
        getUserGroups();
    }

    public void getTemplate()
    {
        var td = new TemplateData();
        var tm = new TemplateManager(ApiAccessMode.Admin);
        var tc = new TemplateCriteria();
        tc.AddFilter(TemplateDataProperty.Id, CriteriaFilterOperator.GreaterThan, 0);
        List<TemplateData> tdList = tm.GetList(tc);
        HtmlGenericControl li;

        td = tdList.First(a => a.TemplateName != "");
        td = tm.GetItem(td.Id);
        li = new HtmlGenericControl("li");
        li.InnerText = "Get Item " + ": " + td.TemplateName;
        templateItem.Controls.Add(li);

        int counter = 0;
        foreach (var i in tdList)
        {
            counter++;
            li = new HtmlGenericControl("li");
            li.InnerText = "Item " + counter + ": " + i.TemplateName;
            templateList.Controls.Add(li);
        }

    }
    public void getFolder()
    {
        var fd = new FolderData();
        var fm = new FolderManager(ApiAccessMode.Admin);
        var fc = new FolderCriteria();
        fc.AddFilter(FolderProperty.Id, CriteriaFilterOperator.GreaterThan, 0);
        List<FolderData> fdList = fm.GetList(fc);
        HtmlGenericControl li;

        fd = fdList.First(a => a.Name != "");
        fd = fm.GetItem(fd.Id);
        li = new HtmlGenericControl("li");
        li.InnerText = "Get Item " + ": " + fd.Name;
        folderItem.Controls.Add(li);

        int counter = 0;
        foreach (var i in fdList)
        {
            counter++;
            li = new HtmlGenericControl("li");
            li.InnerText = "Item " + counter + ": " + i.Name;
            folderList.Controls.Add(li);
        }

    }
    public void getContent()
    {
        var cd = new ContentData();
        var cm = new ContentManager(ApiAccessMode.Admin);
        var cc = new ContentCriteria();
        cc.AddFilter(ContentProperty.Title, CriteriaFilterOperator.StartsWith, "Test");
        List<ContentData> cdList = cm.GetList(cc);
        HtmlGenericControl li;

        cd = cdList.First(a => a.Title != "");
        cd = cm.GetItem(cd.Id);
        li = new HtmlGenericControl("li");
        li.InnerText = "Get Item " + ": " + cd.Title;
        contentItem.Controls.Add(li);

        int counter = 0;
        foreach (var i in cdList)
        {
            counter++;
            li = new HtmlGenericControl("li");
            li.InnerText = "Item " + counter + ": " + i.Title;
            contentList.Controls.Add(li);
        }        
    }
    public void getMenu()
    {

        var md = new Ektron.Cms.Organization.MenuData();
        var mm = new MenuManager(ApiAccessMode.Admin);
        var mc = new MenuCriteria();
        
     
        List<Ektron.Cms.Organization.MenuData> mdList = mm.GetMenuList(mc);
        HtmlGenericControl li;

        md = mdList.First(a => a.Text != "");
        md = mm.GetMenu(md.Id);
        li = new HtmlGenericControl("li");
        li.InnerText = "Get Item " + ": " + md.Text;
        menuItem.Controls.Add(li);

        int counter = 0;
        foreach (var i in mdList)
        {
            counter++;
            li = new HtmlGenericControl("li");
            li.InnerText = "Item " + counter + ": " + i.Text;
            menuList.Controls.Add(li);
        } 

    }
   public void getTax()
    {
        var td = new TaxonomyData();
        var tm = new TaxonomyManager(ApiAccessMode.Admin);
        var tc = new TaxonomyCriteria();
        tc.AddFilter(TaxonomyProperty.Name, CriteriaFilterOperator.StartsWith, "Taxonomy");
        List<TaxonomyData> tdList = tm.GetList(tc);
        HtmlGenericControl li;

        td = tdList.First(a => a.Name != "");
        td = tm.GetItem(td.Id);
        li = new HtmlGenericControl("li");
        li.InnerText = "Get Item " + ": " + td.Name;
        taxItem.Controls.Add(li);

        int counter = 0;
        foreach (var i in tdList)
        {
            counter++;
            li = new HtmlGenericControl("li");
            li.InnerText = "Item " + counter + ": " + i.Name;
            taxonomyList.Controls.Add(li);
        } 

    }
   public void getTaxItem()
   {
      
       var tm = new TaxonomyItemManager(ApiAccessMode.Admin);
       var tc = new TaxonomyItemCriteria();
       tc.AddFilter(TaxonomyItemProperty.TaxonomyName, CriteriaFilterOperator.StartsWith, "Taxonomy");
       List<TaxonomyItemData> tdList = tm.GetList(tc);
       HtmlGenericControl li;
          

       int counter = 0;
       foreach (var i in tdList)
       {
           counter++;
           li = new HtmlGenericControl("li");
           li.InnerText = "Item " + counter + ": " + i.Title;
           taxItemlist.Controls.Add(li);
       } 


   }


     public void getAssetContent()
    {
        var ad = new ContentAssetData();
        var am = new AssetManager(ApiAccessMode.Admin);
        var ac = new AssetCriteria();
        ac.AddFilter(AssetProperty.FolderName, CriteriaFilterOperator.StartsWith, "test");
        List<ContentAssetData> adList = am.GetList(ac);
        HtmlGenericControl li;

        ad = adList.First(a => a.Title != "");
        ad = am.GetItem(ad.Id);
        li = new HtmlGenericControl("li");
        li.InnerText = "Get Item " + ": " + ad.Title;
        assetItem.Controls.Add(li);

        int counter = 0;
        foreach (var i in adList)
        {
            counter++;
            li = new HtmlGenericControl("li");
            li.InnerText = "Item " + counter + ": " + i.Title;
            assetList.Controls.Add(li);
        }        
    }

    

        
    
    public void getUsers()
    {
        var ud = new UserData();
        var um = new UserManager(ApiAccessMode.Admin);
        var uc = new UserCriteria();
        uc.AddFilter(UserProperty.Id, CriteriaFilterOperator.GreaterThan, 0);
        uc.AddFilter(UserProperty.Id, CriteriaFilterOperator.LessThan, 50);
        List<UserData> udList = um.GetList(uc);
        HtmlGenericControl li;

        ud = udList.First(a => a.Username != "");
        ud = um.GetItem(ud.Id);
        li = new HtmlGenericControl("li");
        li.InnerText = "Get Item " + ": " + ud.Username;
        userItem.Controls.Add(li);

        int counter = 0;
        foreach (var i in udList)
        {
            counter++;
            li = new HtmlGenericControl("li");
            li.InnerText = "Item " + counter + ": " + i.Username;
            UsersList.Controls.Add(li);
        } 

    }

   
    public void getUserGroups()
    {
        var ugd = new UserGroupData();
        var ugm = new UserGroupManager(ApiAccessMode.Admin);
        var ugc = new UserGroupCriteria();
        ugc.AddFilter(UserGroupProperty.Id, CriteriaFilterOperator.GreaterThan, 0);
        ugc.AddFilter(UserGroupProperty.Id, CriteriaFilterOperator.LessThan, 50);
        List<UserGroupData> ugdList = ugm.GetList(ugc);
        HtmlGenericControl li;

        ugd = ugdList.First(a => a.Name != "");
        ugd = ugm.GetItem(ugd.Id);
        li = new HtmlGenericControl("li");
        li.InnerText = "Get Item " + ": " + ugd.Name;
        userGroupItem.Controls.Add(li);
                
        int counter = 0;
        foreach (var i in ugdList)
        {
            counter++;
            li = new HtmlGenericControl("li");
            li.InnerText = "Item " + counter + ": " + i.Name;
            userGroupsList.Controls.Add(li);
        } 

    }


}