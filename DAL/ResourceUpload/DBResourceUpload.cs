﻿using System;
using System.Data;
using System.Text;
using DAL.Data;
using Entity;
using Entity.@base;

namespace DAL.ResourceUpload
{
    public class DBResourceUpload : DBSql
    {
        #region 获取所有数据

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetList(PagerQueryParam Pager, out int total)
        {
            StringBuilder strSql = new StringBuilder();
            try
            {
                strSql.Append(RecordCountSql(Pager.StrParm));
                object obj = ExecuteScalar(strSql.ToString());
                if (obj != null && obj != DBNull.Value)
                {
                    total = int.Parse(obj.ToString());
                }
                else
                {
                    total = 0;
                }
                if (total == 0)
                {
                    return null;
                }

                DataSet ds = ExecuteDataSet(GetListSql(Pager));
                return ds.Tables[0];
            }
            catch (Exception e)
            {

                throw e;
            }
        }



        private string RecordCountSql(string strparm)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select count(1) from dbo.t_resource_detail_upload where 1=1 ");
            if (strparm.Length > 0)
            {
                strSql.Append(strparm);

            }

            return strSql.ToString();
        }

        private string GetListSql(PagerQueryParam Pager)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("WITH OrderedOrders ");
            strSql.Append("AS ( SELECT   ROW_NUMBER() OVER ( ORDER BY id ) AS Rownumber ,");
            strSql.Append("id ,");
            strSql.Append("resource_detail_id ,");
            strSql.Append("files_dir ,");
            strSql.Append("files_name ");
            strSql.Append("FROM  dbo.t_resource_detail_upload WHERE  1 = 1 ");
            if (Pager.StrParm.Length > 0)
            {
                strSql.Append(Pager.StrParm);
            }
            strSql.Append(")");


            strSql.Append(" SELECT *");
            strSql.Append(" FROM OrderedOrders");
            //strSql.Append(" FROM OrderedOrders  where RowNumber between " + ((Pager.PageIndex - 1)*Pager.PageSize + 1) +
            //              " and " + Pager.PageIndex*Pager.PageSize);
            return strSql.ToString();
        }

        #endregion

        #region 增加一条数据

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(ResourceDetailUploadEntity model)
        {
            try
            {
                string strSql = AddSql(model);
                ExecuteNonQuery(strSql.ToString());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private string AddSql(ResourceDetailUploadEntity model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" insert into dbo.t_resource_detail_upload(");
            strSql.Append("resource_detail_id,");
            strSql.Append("files_dir,");
            strSql.Append("files_name");
            strSql.Append(")values(");
            strSql.Append("'" + model.Resource_detail_id + "',");
            strSql.Append("'" + model.FilesDir + "',");
            strSql.Append("'" + model.Files_name + "'");
            strSql.Append(")");

            return strSql.ToString();
        }

        #endregion
        
        #region 通过ID取得数据信息
        /// <summary>
        /// 通过ID取得数据信息
        /// </summary>
        /// <param name="id"></param>
        public ResourceDetailUploadEntity GetInfo(int id)
        {
            ResourceDetailUploadEntity model = new ResourceDetailUploadEntity();
            try
            {
                string strSql = GetInfoSql(id);
                using (IDataReader dataReader = ExecuteReader(strSql))
                {
                    if (dataReader.Read())
                    {
                        model = ReadBind(dataReader);
                    }
                }
                return model;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private string GetInfoSql(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select id,resource_detail_id,files_name from dbo.t_resource_detail_upload where 1=1");
            strSql.Append(" and resource_detail_id = '" + id + "'");
            

            return strSql.ToString();
        }
        private ResourceDetailUploadEntity ReadBind(IDataReader dataReader)
        {
            ResourceDetailUploadEntity model = new ResourceDetailUploadEntity();
            model.Id = CommonDBCheck.ToInt(dataReader["id"]);
            model.Resource_detail_id = CommonDBCheck.ToInt(dataReader["resource_detail_id"]);
            model.Files_name = CommonDBCheck.ToString(dataReader["files_name"]);
            return model;
        }

        #endregion

        #region 通过主表id获取所有的上传文件信息

        /// <summary>
        /// 通过主表id获取所有的上传文件信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetInfosByDetailId(int id)
        {
            try
            {
                string str = "select * from dbo.t_resource_detail_upload where resource_detail_id='" + id + "'";
                return ExecuteDataSet(str).Tables[0];
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion


        #region 删除一条数据

        public void Delete(int id,int detail_id)
        {
            try
            {
                StringBuilder strSql=new StringBuilder();
                strSql.Append("delete from dbo.t_resource_detail_upload where 1=1");
                if (detail_id>0)
                {
                    strSql.Append(" and resource_detail_id = '" + detail_id + "'");
                }
                if (id>0)
                {
                    strSql.Append(" and id='" + id + "'");
                }
                ExecuteNonQuery(strSql.ToString());
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion

        #region 检测该分类是否已经被使用

        /// <summary>
        /// 检测该分类是否已经被使用
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true 正在使用</returns>
        public bool IsExit(int id)
        {
            try
            {
                string str = "select count(*) from dbo.t_resource_detail where  type_id='" + id + "'";
                if (CommonDBCheck.ToInt(ExecuteScalar(str)) > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion

        #region 获取所有资源类型
        /// <summary>
        /// 获取所有资源类型
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllResourceType()
        {
            try
            {
                string strSql = "select id,name from dbo.t_resource_type where" +
                                " delete_flag=0";
                return ExecuteDataSet(strSql).Tables[0];
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion

        #region 更新一条数据

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(SpareResourceTypeEntity model)
        {
            try
            {
                string strSql = UpdateSql(model);
                ExecuteNonQuery(strSql.ToString());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private string UpdateSql(SpareResourceTypeEntity model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" UPDATE dbo.t_resource_type SET");
            strSql.Append(" name = '" + model.Name + "',");
            if (model.update_user != null)
            {
                strSql.Append(" update_people = '" + model.update_user.Name + "',");

            }
            strSql.Append(" update_time = getdate()");

            strSql.Append(" where id = '" + model.Id + "'");

            return strSql.ToString();
        }

        #endregion

      
     


        
    }
}
    