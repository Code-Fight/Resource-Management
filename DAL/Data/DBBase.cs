using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DAL.Data
{
    /// <summary>
    /// 数据库基类
    /// </summary>
    public class DBBase :IDataBase, IDisposable
    {
        protected Database _db = null;
        protected DbCommand _dbCommand = null;
        protected DbTransaction _dbTransaction = null;
        protected DbConnection _dbConnection = null;
        protected static DBBase _dbb = null;
        protected string _strdatabasename = string.Empty;
        protected static readonly object _lockojb = new object();
        public static DBBase CurrentDB
        {
            get
            {
                if (_dbb == null)
                {
                    lock (_lockojb)
                    {
                        if (_dbb == null)
                        {
                            string strdatabasename = ConfigurationSettings.AppSettings["defaultDatabase"];
                           _dbb = new DBBase(strdatabasename);
                        }
                    }
                }
                return _dbb;

            }
        }

        public DBBase(string strdatabasename)
        {
            try
            {
                _strdatabasename = strdatabasename;
                _db = DatabaseFactory.CreateDatabase(strdatabasename);
                _dbConnection = _db.CreateConnection();
                _dbConnection.Open();
                _dbCommand = _dbConnection.CreateCommand();
            }
            catch (SqlException sqlEx)
            {
                Dispose();
            }
        }

        public DataSet ExecuteDataSet(string strsql)
        {
            try
            {
                _dbCommand = _db.GetSqlStringCommand(strsql);

                //将连接超时时间 赋值给 操作时间--------By Daidaowei 20130409
                _dbCommand.CommandTimeout = _dbConnection.ConnectionTimeout;

                DataSet obj = _db.ExecuteDataSet(_dbCommand);
                return obj;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public void ExecuteNonQuery(string strsql)
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

        public void BeginTransaction()
        {
            if (_dbConnection.State == ConnectionState.Closed)
            {
                _db = DatabaseFactory.CreateDatabase(_strdatabasename);
                _dbConnection = _db.CreateConnection();
                _dbConnection.Open();
                _dbCommand = _dbConnection.CreateCommand();
            }
           _dbTransaction = _dbConnection.BeginTransaction();
        }

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
            Close();
        }

        #endregion

        #region IDataBase 成员


        public void Open()
        {
           
        }

        public void Close()
        {
            _db = null;
            _dbCommand = null;
            _dbTransaction = null;
            _dbb = null;
            if (_dbConnection != null)
            {
                _dbConnection.Close();
            }
            _dbConnection = null;
        }

        #endregion
    }
}
