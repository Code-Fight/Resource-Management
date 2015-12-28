using System.Collections.Generic;

namespace DAL.Data
{
   /// <summary>
   /// 数据库服务器信息实例
   /// </summary>
    public class SQLServerInfo
    {
        /// <summary>
        /// 服务器名称
        /// </summary>
        public string ServerName { get; set; }
        /// <summary>
        /// 数据库实例名称
        /// </summary>
        public string InstanceName { get; set; }
        /// <summary>
        /// 数据库服务地址
        /// </summary>
        public string ServerInstanceName { get; set; }
        /// <summary>
        /// 是否混合模式验证 Yes No
        /// </summary>
        public string IsClustered { get; set; }
        /// <summary>
        /// 数据库版本信息
        /// </summary>
        public string Version { get; set; }
    }

    public class CmpSQLServerInfo : IComparer<SQLServerInfo>
    {
        int IComparer<SQLServerInfo>.Compare(SQLServerInfo c1, SQLServerInfo c2)
        {
            return c1.ServerInstanceName.CompareTo(c2.ServerInstanceName);
        }
    }
}
