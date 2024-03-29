using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace first.data
{
    public class DataContextDapper
    {

       // private readonly IConfiguration _config;

        private readonly string? _connectionString;

        public DataContextDapper(IConfiguration config)
        {

            //_config = config;
            _connectionString = config.GetConnectionString("DefaultConnection");

        }

        public IEnumerable<T> LoadData<T>(string sql)
        {

            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.Query<T>(sql);
        }

        public T LoadSingleDataa<T>(string sql)
        {

            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.QuerySingle<T>(sql);
        }

        public bool ExecuteSql(string sql)
        {

            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.Execute(sql) > 0;
        }

        public int ExecuteSqlWithRowCount(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.Execute(sql);
        }
    }
}