using System.Data;

namespace fly_server.Helpers;

public class DataTableGenerator
{
    public DataTableGenerator()
    {
        
    }

    public DataTable CreateDataTableValues(string col, string[] values)
    {
        var table= new DataTable();
        table.Columns.Add(new DataColumn(col, typeof(string)));

        foreach (var value in values)
        {
            table.Rows.Add(value.Trim());
        }
        
        return table;
    }
}