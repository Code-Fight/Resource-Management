using System;
using System.Data;

namespace DAL.Data
{ 
   public class DBSql
   {
       protected static IDataBase _database;
       private static string _strdatabasename;
       public DBSql()
       {

           _database = DBBase.CurrentDB as DBBase;
       }

       public static string DataBaseName
       {
           get
           {
               return _strdatabasename;
           }
           set
           {
               _strdatabasename = value;
           }
       }

       public void Close()
       {
           _database.Close();
           _database = null;
       }

       public DataSet ExecuteDataSet(string strsql)
       {
            try
            {
                return _database.ExecuteDataSet(strsql);
            }
            catch (Exception e)
            {
                Close();
                throw e;
            }
       }

        public void ExecuteNonQuery(string strsql)
        {
            try
            {
                _database.ExecuteNonQuery(strsql);
            }
            catch(Exception e)
            {
                Close();
                throw e;
            }
        }
        public IDataReader ExecuteReader(string strsql)
        {
            try
            {

                return _database.ExecuteReader(strsql);
            }
            catch (Exception e)
            {
                Close();
                throw e;
            }
        }
        public object ExecuteScalar(string strsql)
        {
            try
            {
                return _database.ExecuteScalar(strsql);
            }
            catch (Exception e)
            {
              Close();
              throw e;
            }
        }

        public static void BeginTransactions()
        {
            IDataBase database = DBBase.CurrentDB as DBBase;
            database.BeginTransaction();
        }

        public static void Rollbacks()
        {
            IDataBase database = DBBase.CurrentDB as DBBase;
            database.Rollback();
        }
        public static void Commits()
        {
            IDataBase database = DBBase.CurrentDB as DBBase;
            database.Commit();
        }


        public void BeginTransaction()
        {
            IDataBase database = DBBase.CurrentDB as DBBase;
            database.BeginTransaction();
        }

        public void Rollback()
        {
            IDataBase database = DBBase.CurrentDB as DBBase;
            database.Rollback();
        }
        public void Commit()
        {
            IDataBase database = DBBase.CurrentDB as DBBase;
            database.Commit();
        }

        public void AddInParameter(string strname, DbType type, object strvalue)
        {
            _database.AddInParameter(strname, type, strvalue);
        }

        public void AddOutParameter(string strname, DbType type, int size)
        {
            _database.AddOutParameter(strname, type, size);
        }

        public void ExecuteStoredProcedureNonQuery(string strsql)
        {
            //DBBase.CurrentDB.ExecuteNonQuery(CommandType.StoredProcedure);
        }

        public DataSet ExecuteStoredProcedureDataSet(string strsql)
        {
            return null;
            //return DBBase.CurrentDB.ExecuteNonQuery(CommandType.StoredProcedure, _dbCommand, _dbTransaction);
        }
    }
}
