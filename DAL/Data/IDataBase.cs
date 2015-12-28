using System.Data;

namespace DAL.Data
{
    public interface IDataBase
    {
        DataSet ExecuteDataSet(string strsql);
        void Open();
        void Close();
        void ExecuteNonQuery(string strsql);
        void GetNonQuerySql(string strsql);
        void ExecuteNonQuery();
        void AddInParameter(string strname, DbType type, object strvalue);


        void AddOutParameter(string strname, DbType type, int size);


        void ExecuteStoredProcedureNonQuery(string strsql);


        DataSet ExecuteStoredProcedureDataSet(string strsql);


        IDataReader ExecuteReader(string strsql);

        object ExecuteScalar(string strsql);


        void BeginTransaction();


        void Rollback();

        void Commit();
 

    }
}
