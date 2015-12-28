using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DAL.Data
{
    public class DBSqlServer : IDataBase, IDisposable
    {
        protected Database _db = null;
        protected DbCommand _dbCommand = null;
        protected DbTransaction _dbTransaction = null;
        protected DbConnection _dbConnection = null;
        protected static DBSqlServer _dbb = null;
        protected static readonly object _lockojb = new object();
        public static DBSqlServer CurrentDB
        {
            get
            {
                if (_dbb == null)
                {
                    lock (_lockojb)
                    {
                        if (_dbb == null)
                        {
                            _dbb = new DBSqlServer();
                        }
                    }
                }
                return _dbb;

            }
        }


        protected DBSqlServer()
        {
          
        }

        public DataSet ExecuteDataSet(string strsql)
        {
            try
            {
                _dbCommand = _db.GetSqlStringCommand(strsql);
                DataSet obj = _db.ExecuteDataSet(_dbCommand);
                return obj;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 执行sql语句，不查询
        /// </summary>
        /// <param name="strsql"></param>
        public void ExecuteNonQuery(string strsql)
        {
            try
            {
                _dbCommand = _db.GetSqlStringCommand(strsql);
                _dbCommand.CommandType = CommandType.Text;
                if (_dbTransaction == null || _dbTransaction.Connection == null)
                {
                    _db.ExecuteNonQuery(_dbCommand);
                }
                else
                {
                    
                    _db.ExecuteNonQuery(_dbCommand, _dbTransaction);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void GetNonQuerySql(string strsql)
        {
            _dbCommand = _db.GetSqlStringCommand(strsql);
        }

        public void ExecuteNonQuery()
        {
            _dbCommand.CommandType = CommandType.Text;
            if (_dbTransaction == null || _dbTransaction.Connection == null)
            {
                _db.ExecuteNonQuery(_dbCommand);
            }
            else
            {

                _db.ExecuteNonQuery(_dbCommand, _dbTransaction);
            }
        }
        public void AddInParameter(string strname,DbType type, object strvalue)
        {
            _db.AddInParameter(_dbCommand, strname, type, strvalue); 
        }

        public void AddOutParameter(string strname, DbType type, int size)
        {

            _db.AddOutParameter(_dbCommand, strname, type, size);
        }

        public void ExecuteStoredProcedureNonQuery(string strsql)
        {
            _dbCommand = _db.GetSqlStringCommand(strsql);
            _dbCommand.CommandType = CommandType.StoredProcedure;
            _db.ExecuteNonQuery(_dbCommand, _dbTransaction);
        }

        public DataSet ExecuteStoredProcedureDataSet(string strsql)
        {
            _dbCommand = _db.GetSqlStringCommand(strsql);
            _dbCommand.CommandType = CommandType.StoredProcedure;
            return _db.ExecuteDataSet(_dbCommand, _dbTransaction);
        }

        public IDataReader ExecuteReader(string strsql)
        {
            return _db.ExecuteReader(CommandType.Text, strsql);
        }
        public object ExecuteScalar(string strsql)
        {
            object obj = null;
            _dbCommand = _db.GetSqlStringCommand(strsql);
            obj = _db.ExecuteScalar(_dbCommand);
            return obj;
        }
        /// <summary>
        /// 启动事务
        /// </summary>
        public void BeginTransaction()
        {
            if (_dbConnection.State == ConnectionState.Closed)
            {
                _db = DatabaseFactory.CreateDatabase("KASX");
                _dbConnection = _db.CreateConnection();
                _dbConnection.Open();
                _dbCommand = _dbConnection.CreateCommand();
            }
           _dbTransaction = _dbConnection.BeginTransaction();
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public void Rollback()
        {
            if (_dbTransaction != null)
            {
                try
                {
                    _dbTransaction.Rollback();
                }
                catch (SqlException sqlEx)
                {
                    Dispose();
                }
            }
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public void Commit()
        {
            if (_dbTransaction != null)
            {
                _dbTransaction.Commit();
            }
        }

        #region IDisposable 成员

        public void Dispose()
        {
            _db = null;
            _dbCommand = null;
            _dbTransaction = null;
            if (_dbConnection != null)
            {
                _dbConnection.Close();
            }
            _dbConnection = null;
        }

        public void Open()
        {
            try
            {
                _db = DatabaseFactory.CreateDatabase("KASX");
                _dbConnection = _db.CreateConnection();
                _dbConnection.Open();
                _dbCommand = _dbConnection.CreateCommand();
            }
            catch (SqlException sqlEx)
            {
                Dispose();
            }
        }

        public void Close()
        {
            _db = null;
            _dbCommand = null;
            _dbTransaction = null;
            if (_dbConnection != null)
            {
                _dbConnection.Close();
            }
            _dbConnection = null;
        }

        #endregion
    }
}
