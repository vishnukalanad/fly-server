using System.Data;
using System.Text.Json;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;

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
    
    // JSON Path outputs
    public IEnumerable<T> LoadJson<T>(string sql, object parameters)
    {
        using var con = new SqlConnection(_connectionString);
        var jsonString = con.QuerySingleOrDefault<string>(sql, parameters);
        if (string.IsNullOrWhiteSpace(jsonString))
        {
            return [];
        }
        return JsonSerializer.Deserialize<IEnumerable<T>>(jsonString) ?? [];
    }

    
    // Query single;
    public T LoadSingleDatum<T>(string sql, object parameters)
    {
        IDbConnection con= new SqlConnection(_connectionString);
        return  con.QuerySingle<T>(sql, parameters);
    }

    // Operational queries;
    public int ExecuteQuery(string sql, object parameters, bool sp = false, string? tvpName = null)
    {
        CommandType commandType = sp ? CommandType.StoredProcedure : CommandType.Text;
        var con = new SqlConnection(_connectionString);
        // return con.Execute(sql, parameters);
        using var cmd = new SqlCommand(sql, con);
        cmd.CommandType = commandType;

        if (!parameters.Equals(null))
        {
            foreach (var prop in parameters.GetType().GetProperties())
            {
                var value  = prop.GetValue(parameters);
                if (value is DataTable dt)
                {
                    var param = cmd.Parameters.Add($"@{prop.Name}", SqlDbType.Structured);
                    param.TypeName = tvpName ?? throw new ArgumentNullException(nameof(tvpName));
                    param.Value = dt;
                }
                else
                {
                    cmd.Parameters.AddWithValue($"@{prop.Name}", value ?? DBNull.Value);
                }
            }
        }
        con.Open();
        return cmd.ExecuteNonQuery();
    }
    
    // Execute iterable queries;
    public int ExecuteQueryMultiple(string sql, IEnumerable<object> parameters)
    {
        IDbConnection con= new SqlConnection(_connectionString);
        return con.Execute(sql, parameters);
    }
}