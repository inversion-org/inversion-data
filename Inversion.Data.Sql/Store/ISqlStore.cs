using System.Data;

namespace Inversion.Data.Store
{
    public interface ISqlStore : IStore
    {
        void Exec(string sql);
        void Exec(string sql, params IDbDataParameter[] parameters);

        void Fill(string sql, DataSet data);
        void Fill(string sql, DataSet data, params IDbDataParameter[] parameters);

        DataSet Query(string sql, params IDbDataParameter[] parameters);
        DataSet Query(string sql);

        IDataReader Read(string sql, params IDbDataParameter[] parameters);
        IDataReader Read(string sql);

        bool Exists(string sql);
        bool Exists(string sql, params IDbDataParameter[] parameters);

        object Scalar(string sql, params IDbDataParameter[] parameters);
        object Scalar(string sql);
    }
}