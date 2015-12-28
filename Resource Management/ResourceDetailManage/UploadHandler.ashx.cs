using System;
using System.Configuration;
using System.IO;
using System.Web;
using BLL.ResourceType;
using BLL.ResourceUpload;
using DAL;
using Entity;

namespace Resource_Management.ResourceDetailManage
{
    /// <summary>
    /// UploadHandler 的摘要说明
    /// </summary>
    public class UploadHandler : IHttpHandler 
    {
       
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Charset = "utf-8";

            HttpPostedFile file = context.Request.Files["Filedata"];
            string id = context.Request["id"];
            string dir = context.Request["dir"];
            string name = context.Request["name"];
            string peo = context.Request["peo"];
            
            
            ResourceTypeManager typeManager=new ResourceTypeManager();

            string uploadPath = HttpContext.Current.Server.MapPath(ConfigurationSettings.AppSettings["UploadFilePath"] + typeManager.GetInfo(CommonDBCheck.ToInt(dir)).Directory + "/");
               // HttpContext.Current.Server.MapPath(@context.Request["folder"]) + "\\";

            if (file != null)
            {
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                string fileName = file.FileName.Substring(0, file.FileName.IndexOf('.')) + "_" + name + "_" + peo + "_" +
                                  DateTime.Now.ToString("yyyyMMddHHmmssfff") +
                                  file.FileName.Substring(file.FileName.IndexOf('.'),file.FileName.Length - file.FileName.IndexOf('.'));
                file.SaveAs(uploadPath + fileName);
                //下面这句代码缺少的话，上传成功后上传队列的显示不会自动消失
                ResourceDetailUploadEntity uploadEntity=new ResourceDetailUploadEntity();
                uploadEntity.Resource_detail_id = CommonDBCheck.ToInt(id);
                uploadEntity.Files_name = fileName;
                uploadEntity.FilesDir = typeManager.GetInfo(CommonDBCheck.ToInt(dir)).Directory;
                ResourceUploadBussiness uploadBussiness=new ResourceUploadBussiness();
                uploadBussiness.Add(uploadEntity);
                context.Response.Write("1");
            }
            else
            {
                context.Response.Write("0");
            }  
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}