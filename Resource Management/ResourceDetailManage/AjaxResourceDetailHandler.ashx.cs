using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.SessionState;
using BLL.ResourceDetail;
using BLL.ResourceType;
using BLL.ResourceUpload;
using DAL;
using Entity;
using KASXBean.System;

namespace Resource_Management.ResourceDetailManage
{
    /// <summary>
    /// AjaxResourceDetailHandler 的摘要说明
    /// </summary>
    public class AjaxResourceDetailHandler : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {

                context.Response.ContentType = "text/plain";
                string mode = context.Request["mode"].ToLower();


                ResourceTypeManager typeManager = new ResourceTypeManager();
                //ResourceDetailEntity entity=new ResourceDetailEntity();
                string dir = context.Request["dir"];
                string name = context.Request["name"];
                string peo = context.Request["peo"];
                string memo = context.Request["memo"];
                string id = context.Request["id"];
                ResourceDetailEntity entity = new ResourceDetailEntity();
                entity.Memo = memo;
                entity.Name = name;
                entity.Type = new SpareResourceTypeEntity()
                {
                    Id = CommonDBCheck.ToInt(dir),
                    Name = typeManager.GetInfo(CommonDBCheck.ToInt(dir)).Name,
                    Directory = typeManager.GetInfo(CommonDBCheck.ToInt(dir)).Directory

                };
                
                entity.UploadPeople = peo;
                entity.Url = ConfigurationSettings.AppSettings["UploadFilePath"];
                entity.insert_user = entity.update_user = context.Session["user"] as UserEntity;
                
                switch (mode)
                {
                    case "add":
                        context.Response.Write(Add(entity));
                        break;
                    case "update":
                        entity.Id = CommonDBCheck.ToInt(id);
                        context.Response.Write(Update(entity));
                        break;
                }

            }
            catch (Exception e)
            {
                context.Response.Write("error");
            }



        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private int Add(ResourceDetailEntity info)
        {
            try
            {
                ResourceDetailManager detailManager = new ResourceDetailManager();
                return detailManager.Add(info);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// 更新一条是数据
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private int Update(ResourceDetailEntity info)
        {
            try
            {
                //先删除原来的主表数据
                ResourceDetailManager bll = new ResourceDetailManager();
                bll.Delete(info.Id);
                //增加新的数据到主表
                //bll.Add(info);
                //修改剩余的原来的数据
                ResourceUploadBussiness resourceUpload = new ResourceUploadBussiness();
                DataTable dtUpload = resourceUpload.GetInfosByDetailId(info.Id);
                bll.DeleteUpload(0, info.Id);

                ResourceDetailUploadEntity detailEntity = new ResourceDetailUploadEntity();

                int id = bll.Add(info);
                if (dtUpload != null)
                {
                    foreach (DataRow dataRow in dtUpload.Rows)
                    {
                        detailEntity.Files_name = CommonDBCheck.ToString(dataRow["files_name"]);
                        detailEntity.FilesDir = CommonDBCheck.ToString(dataRow["files_dir"]);
                        detailEntity.Resource_detail_id = id;
                        resourceUpload.Add(detailEntity);
                    }
                }
                return id;

            }
            catch (Exception e)
            {
                throw;
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