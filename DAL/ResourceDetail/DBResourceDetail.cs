using System;
using System.Data;
using System.Text;
using DAL.Data;
using Entity;
using Entity.@base;

namespace DAL.ResourceDetail
{
    public class DBResourceDetail : DBSql
    {
     

        #region 增加一条数据

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ResourceDetailEntity model)
        {
            try
            {
                string strSql = AddSql(model);
                return CommonDBCheck.ToInt(ExecuteScalar(strSql));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private string AddSql(ResourceDetailEntity model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" insert into dbo.t_resource_detail(");
            strSql.Append("name,");
            strSql.Append("type,");
            strSql.Append("type_id,");
            strSql.Append("url,");
            strSql.Append("memo,");
            strSql.Append("insert_people,");
            strSql.Append("upload_people,");
            strSql.Append("insert_time");
            strSql.Append(")values(");
            strSql.Append("'" + model.Name + "',");
            if (model.Type!=null)
            {
                strSql.Append("'" + model.Type.Name + "',");
                strSql.Append("'" + model.Type.Id + "',");
            }
            strSql.Append("'" + model.Url + "',");
            strSql.Append("'" + model.Memo + "',");
            if (model.insert_user != null)
            {
                strSql.Append("'" + model.insert_user.Name + "',");
            }
            else
            {
                strSql.Append("NULL,");
            }
            strSql.Append("'" + model.UploadPeople + "',");
            strSql.Append("getdate())");
            strSql.Append("SELECT @@IDENTITY");

            return strSql.ToString();
        }

        #endregion

        #region 删除一条数据

        public void Delete(int id)
        {
            try
            {
                string strSql = "delete from dbo.t_resource_detail where id = '" + id + "'";
                ExecuteNonQuery(strSql);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion


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
            strSql.Append(" select count(1) from t_resource_detail LEFT JOIN dbo.t_resource_type ON t_resource_type.id=t_resource_detail.type_id where 1=1 and t_resource_detail.delete_flag=0");
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
            strSql.Append("AS ( SELECT   ROW_NUMBER() OVER ( ORDER BY t_resource_detail.insert_time desc ) AS Rownumber ,");
            strSql.Append("t_resource_detail.insert_time ,");
            strSql.Append("t_resource_detail.id ,");
            strSql.Append("t_resource_detail.name name,");
            strSql.Append("t_resource_type.name type,");
            strSql.Append("type_id ,");
            strSql.Append("url ,");
            strSql.Append("memo,");
            strSql.Append("upload_people ");
            strSql.Append("FROM  dbo.t_resource_detail LEFT JOIN dbo.t_resource_type ON t_resource_type.id=t_resource_detail.type_id WHERE  1 = 1 and t_resource_detail.delete_flag=0");
            if (Pager.StrParm.Length > 0)
            {
                strSql.Append(Pager.StrParm);
            }
            strSql.Append(")");


            strSql.Append(" SELECT *");
            strSql.Append(" FROM OrderedOrders  where RowNumber between " + ((Pager.PageIndex - 1) * Pager.PageSize + 1) +
                          " and " + Pager.PageIndex * Pager.PageSize);
            return strSql.ToString();
        }

        #endregion

        #region 通过ID取得数据信息
        /// <summary>
        /// 通过ID取得数据信息
        /// </summary>
        /// <param name="id"></param>
        public ResourceDetailEntity GetInfo(int id)
        {
            ResourceDetailEntity model = new ResourceDetailEntity();
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
            strSql.Append(" select * from dbo.t_resource_detail where delete_flag=0");
            strSql.Append(" and id = '" + id + "'");
            

            return strSql.ToString();
        }
        private ResourceDetailEntity ReadBind(IDataReader dataReader)
        {
            ResourceDetailEntity model = new ResourceDetailEntity();
            model.Id=CommonDBCheck.ToInt(dataReader["id"]);
            model.Name = CommonDBCheck.ToString(dataReader["name"]);
            if (model.Type==null)
            {
                model.Type=new SpareResourceTypeEntity();
            }
            model.Type.Id = CommonDBCheck.ToInt(dataReader["type_id"]);
            model.Type.Name = CommonDBCheck.ToString(dataReader["type"]);
            model.Url = CommonDBCheck.ToString(dataReader["url"]);
            model.Memo = CommonDBCheck.ToString(dataReader["memo"]);
            model.UploadPeople = CommonDBCheck.ToString(dataReader["upload_people"]);
            return model;
        }

        #endregion

        #region 更新一条数据

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(ResourceDetailEntity model)
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

        private string UpdateSql(ResourceDetailEntity model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" UPDATE dbo.t_resource_detail SET");
            if (model.Name.Length>0)
            {
                strSql.Append(" name = '" + model.Name + "',");
            }
            if (model.Memo.Length > 0)
            {
                strSql.Append(" memo = '" + model.Memo + "',");
            }
            if (model.Type!=null&&model.Type.Id>0)
            {
                strSql.Append(" type_id = '" + model.Type.Id + "',");
                strSql.Append(" type = '" + model.Type.Name + "',");
            }
            if (model.UploadPeople.Length > 0)
            {
                strSql.Append(" upload_people = '" + model.UploadPeople + "',");
            }
           

          
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
