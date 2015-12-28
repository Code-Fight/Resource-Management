namespace DAL.Data
{
    /// <summary>
    /// 数据库连接字符串信息
    /// </summary>
    public class ConnectionInfo
    {

        #region 构造函数
        public ConnectionInfo()
        {

        }
        /// <summary>
        /// 数据库连接字符串信息 构造函数
        /// </summary>
        /// <param name="server">服务器</param>
        /// <param name="database">数据库</param>
        /// <param name="userid">用户名</param>
        /// <param name="password">密码</param>
        public ConnectionInfo(string server, string database, string userid, string password)
        {
            this.Server = server;
            this.Database = database;
            this.UserId = userid;
            this.Password = password;
        }

        #endregion
        /// <summary>
        /// 数据库服务器地址
        /// </summary>
        public string Server { get; set; }
        /// <summary>
        /// 数据库名称
        /// </summary>
        public string Database { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        public string GetConnectionString()
        {
            return string.Format("Server={0};Database={1};User ID={2};Password={3};", this.Server, this.Database, this.UserId, this.Password);
        }
    }
}
