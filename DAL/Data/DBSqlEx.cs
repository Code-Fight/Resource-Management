using System;
using System.Data;

namespace DAL.Data
{
    public class DBSqlEx :IDisposable 
    {
        protected static DBSqlEx _dbsql = null;
        protected IDataBase _database;
        public static  void BeginTransactions()
        {
            if (_dbsql == null)
            {
                _dbsql = new DBSqlEx();
            }
            _dbsql._database.BeginTransaction();
        }

        public static  void Rollbacks()
        {
            if (_dbsql == null)
            {
                _dbsql = new DBSqlEx();
            }
            _dbsql._database.Rollback();
        }
        public static  void Commits()
        {
            if (_dbsql == null)
            {
                _dbsql = new DBSqlEx();
            }
            _dbsql._database.Commit();
        }

       public DBSqlEx()
       {
           _database = DBSqlServer.CurrentDB as DBSqlServer;
           _database.Open();
       }

       public  void Open()
       {
           _database.Open();
       }

       public  void Close()
       {
           _database.Close();
       }

       public  DataSet ExecuteDataSet(string strsql)
       {
           return _database.ExecuteDataSet(strsql);
       }

       public  void ExecuteNonQuery(string strsql)
       {
           _database.ExecuteNonQuery(strsql);
       }
       public  IDataReader ExecuteReader(string strsql)
       {
           return _database.ExecuteReader(strsql);
       }
       public  object ExecuteScalar(string strsql)
       {
           return _database.ExecuteScalar(strsql);
       }

       public  void AddInParameter(string strname, DbType type, object strvalue)
        {
            _database.AddInParameter(strname, type, strvalue);
        }

       public  void AddOutParameter(string strname, DbType type, int size)
        {
            _database.AddOutParameter(strname, type, size);
        }

       public  void ExecuteStoredProcedureNonQuery(string strsql)
        {
            //DBBase.CurrentDB.ExecuteNonQuery(CommandType.StoredProcedure);
        }

       public  DataSet ExecuteStoredProcedureDataSet(string strsql)
        {
            return null;
            //return DBBase.CurrentDB.ExecuteNonQuery(CommandType.StoredProcedure, _dbCommand, _dbTransaction);
        }



       #region IDisposable 成员

       void IDisposable.Dispose()
       {
           _database.Close();
       }

       #endregion
    }
}
