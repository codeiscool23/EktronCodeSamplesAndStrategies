using Ektron.Cms.Extensibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ektron.Cms;
using Ektron.Cms.Common;
using Ektron.Cms.Extensibility.Content;
using Ektron.Cms.Framework.User;
using Ektron.Cms.Framework.Content;
using Ektron.Cms.Framework;

/// <summary>
/// Summary description for UpdateMetadata
/// </summary>
public class UpdateMetadata: ContentStrategy
{
    public override void OnAfterPublishContent(ContentData contentData, CmsEventArgs eventArgs)
    {
        var cm= new ContentManager(ApiAccessMode.LoggedInUser);
        //return the content data for editing as the logged in user
        var cd = cm.GetItem(contentData.Id, true);  
        for(var i=0; i<cd.MetaData.Length; i++)
        {
            //using the id of the metadata you have created to store the date
            if (cd.MetaData[i].Id == 171)
           {
                //if no value exists for this content data property
               if (cd.DateCreated.ToString().IsValueNullOrEmpty())
               {
                   //update the text of the metadata with the current datetime string
                   cd.MetaData[i].Text = DateTime.Now.ToString();

                   cm.UpdateContentMetadata(cd.Id, cd.MetaData[i].Id, cd.MetaData[i].Text);

                   break;
               }
               else
               {
                   cd.MetaData[i].Text = cd.DateCreated.ToString();

                   cm.UpdateContentMetadata(cd.Id, cd.MetaData[i].Id, cd.MetaData[i].Text);

                   break;

               }
           }
        }
    }
    
}