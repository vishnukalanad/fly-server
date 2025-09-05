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

    public DataTable CreateNestedDataTable<T>(IEnumerable<T> items)
    {
        var table = new DataTable();
        var props = typeof(T).GetProperties();
        foreach (var prop in props) 
        {
            var propType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
            table.Columns.Add(prop.Name, propType);
        }

        foreach (var item in items)
        {
            var values = new object[props.Length]; 
            for (int i = 0; i < props.Length; i++)
            {
                values[i] = props[i].GetValue(item) ?? DBNull.Value;
            }
            table.Rows.Add(values);
        }
        
        return table;
    }
}