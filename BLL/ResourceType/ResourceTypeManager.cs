using System;
using System.Data;
using DAL.ResourceType;
using Entity;
using Entity.@base;

namespace BLL.ResourceType
{
    public class ResourceTypeManager
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
                DBResourceType db=new DBResourceType();
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
        public void Add(SpareResourceTypeEntity model)
        {
            try
            {
                DBResourceType db=new DBResourceType();
                db.Add(model);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="model"></param>
        public void Update(SpareResourceTypeEntity model)
        {
            try
            {
                DBResourceType db = new DBResourceType();
                db.Update(model);
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
        public SpareResourceTypeEntity GetInfo(int id)
        {
            try
            {
                DBResourceType db=new DBResourceType();
                return db.GetInfo(id);
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
                DBResourceType db=new DBResourceType();
                if (db.IsExit(id))
                {
                    throw  new Exception("该分类下有资源存在，禁止删除");
                }
                db.Delete(id);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取所有资源类型
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllResourceType()
        {
            try
            {
                DBResourceType db = new DBResourceType();
                return db.GetAllResourceType();
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
