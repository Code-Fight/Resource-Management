using System;
using System.Data;
using DAL.ResourceDetail;
using DAL.ResourceUpload;
using Entity;
using Entity.@base;

namespace BLL.ResourceDetail
{
    public class ResourceDetailManager
    {

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="Pager"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public DataTable GetList(PagerQueryParam Pager, out int total)
        {
            try
            {
                DBResourceDetail db = new DBResourceDetail();
                return db.GetList(Pager, out total);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model"></param>
        public int Add(ResourceDetailEntity model)
        {
            try
            {
                DBResourceDetail db = new DBResourceDetail();
                return db.Add(model);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            try
            {
                DBResourceDetail db = new DBResourceDetail();
                db.Delete(id);
                
               
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// 删除上传的文件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="detail_id"></param>
        public void DeleteUpload(int id,int detail_id)
        {
            try
            {
                DBResourceUpload dbResourceUpload = new DBResourceUpload();
                dbResourceUpload.Delete(id, detail_id);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// 通过ID取得数据信息
        /// </summary>
        /// <param name="id"></param>
        public ResourceDetailEntity GetInfo(int id)
        {
            try
            {
                DBResourceDetail db = new DBResourceDetail();
                return db.GetInfo(id);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void Update(ResourceDetailEntity model)
        {
            try
            {
                DBResourceDetail db=new DBResourceDetail();
                db.Update(model);
            }
            catch (Exception e)
            {
                throw;
            }
        }

    }
}
