using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inversion.Data
{
    public class SqlStore : Store, ISqlStore
    {
        public override void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Exec(string sql)
        {
            throw new NotImplementedException();
        }

        public void Exec(string sql, params IDbDataParameter[] parameters)
        {
            throw new NotImplementedException();
        }

        public void Fill(string sql, DataSet data)
        {
            throw new NotImplementedException();
        }

        public void Fill(string sql, DataSet data, params IDbDataParameter[] parameters)
        {
            throw new NotImplementedException();
        }

        public DataSet Query(string sql, params IDbDataParameter[] parameters)
        {
            throw new NotImplementedException();
        }

        public DataSet Query(string sql)
        {
            throw new NotImplementedException();
        }

        public IDataReader Read(string sql, params IDbDataParameter[] parameters)
        {
            throw new NotImplementedException();
        }

        public IDataReader Read(string sql)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string sql)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string sql, params IDbDataParameter[] parameters)
        {
            throw new NotImplementedException();
        }

        public object Scalar(string sql, params IDbDataParameter[] parameters)
        {
            throw new NotImplementedException();
        }

        public object Scalar(string sql)
        {
            throw new NotImplementedException();
        }
    }
}