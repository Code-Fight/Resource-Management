using System;
using System.Data;
using DAL.ResourceDetail;
using DAL.ResourceUpload;
using Entity;
using Entity.@base;

namespace BLL.ResourceUpload
{
    public class ResourceUploadBussiness
    {
        /// <summary>
        /// 增加数据
        /// </summary>
        /// <param name="entity"></param>
        public void Add(ResourceDetailUploadEntity entity)
        {
            try
            {
                DBResourceUpload db=new DBResourceUpload();
                db.Add(entity);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public DataTable GetList(PagerQueryParam Pager, out int total,int id)
        {
            try
            {
                DBResourceUpload db = new DBResourceUpload();
                DataTable dt= db.GetList(Pager,out  total);
                if (dt==null)
                {
                    return null;
                }
                DBResourceDetail dbResource=new DBResourceDetail();
                ResourceDetailEntity dbEntity = dbResource.GetInfo(id);
                DataTable dtNew=new DataTable();
                dtNew.Columns.Add("id", typeof(int));
                dtNew.Columns.Add("url", typeof(string));
                dtNew.Columns.Add("name", typeof(string));
                foreach (DataRow dataRow in dt.Rows)
                {
                    dtNew.Rows.Add(new object[] { dataRow["id"], dbEntity.Url + dataRow["files_dir"] +"/"+ dataRow["files_name"], dataRow["files_name"] });
                }
                return dtNew;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// 通过id获取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResourceDetailUploadEntity GetInfo(int id)
        {
            try
            {
                DBResourceUpload db = new DBResourceUpload();
                return db.GetInfo(id); 
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// 通过主表id获取所有的上传文件信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetInfosByDetailId(int id)
        {
            try
            {
                DBResourceUpload db = new DBResourceUpload();
                return db.GetInfosByDetailId(id); 
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
