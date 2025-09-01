using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace fly_server.Data;

public class DataContext
{
    private readonly string _connectionString;
    public DataContext(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")!;
    }

    // Query multiple;
    public IEnumerable<T> LoadData<T>(string sql, object parameters)
    {
        IDbConnection con= new SqlConnection(_connectionString);
        return con.Query<T>(sql, parameters);
    }
    
    // Query single;
    public T LoadSingleDatum<T>(string sql, object parameters)
    {
        IDbConnection con= new SqlConnection(_connectionString);
        return  con.QuerySingle<T>(sql, parameters);
    }

    // Operational queries;
    public int ExecuteQuery(string sql, object parameters)
    {
        IDbConnection con= new SqlConnection(_connectionString);
        return con.Execute(sql, parameters);
    }
    
    // Execute iterable queries;
    public int ExecuteQueryMultiple(string sql, IEnumerable<object> parameters)
    {
        IDbConnection con= new SqlConnection(_connectionString);
        return con.Execute(sql, parameters);
    }
}