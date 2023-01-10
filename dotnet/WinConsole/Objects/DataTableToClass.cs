using System.Data;
using System.Reflection;
using SqlServerDataPush.Objects;

namespace Objects;

/// <summary>
/// Util library to convert releational DB datatables into API  objects
/// </summary>
public static class Utils
{
    /// <summary>
    /// Converts a SQL data table into an API object
    /// using a collection map to map correct datatable column/value to the object property
    /// </summary>
    /// <param name="dt">Source data table</param>
    /// <param name="map">the Table Column to Object Property mapping</param>
    /// <typeparam name="T">the API object type</typeparam>
    /// <returns>Populated instance of API object</returns>
    public static IList<T> TableToObjectConverter<T>(DataTable dt, CollectionMap map, SourceWellBore? parent)
        where T : class, new()
    {
        // Clean up the source dt by removing excess columns and renaming the valid columns to match API 
        dt = ReKeyDataTable(dt, map);
        List<string> columnNames = dt.Columns.Cast<DataColumn>().Select(column => column.ColumnName).ToList();

        // Get a property list out of the conversion object
        Type classType = typeof(T);
        IList<PropertyInfo> propertyList = classType.GetProperties();

        // Map dt columns to object properties
        List<T> result = new List<T>();
        try
        {
            foreach (DataRow row in dt.Rows)
            {
                T classObject = new T();
                foreach (PropertyInfo property in propertyList)
                {
                    if (property.CanWrite) // Make sure property isn't read only
                    {
                        if (columnNames.Contains(property.Name.ToLower())) // If property is a column name
                        {
                            object? propertyValue = null;
                            if (property.PropertyType == typeof(string))
                            {
                                if (row[property.Name] == System.DBNull.Value)
                                    propertyValue = "Not Available";
                                else
                                    propertyValue = row[property.Name].ToString();
                            }

                            else if (property.PropertyType == typeof(float)
                                     || property.PropertyType == typeof(float?))
                            {
                                propertyValue = row[property.Name] == System.DBNull.Value
                                    ? 0.0
                                    : Convert.ToDouble(row[property.Name].ToString());
                            }
                            else if (property.PropertyType == typeof(double)
                                     || property.PropertyType == typeof(double?))
                            {
                                propertyValue = row[property.Name] == System.DBNull.Value
                                    ? 0.0
                                    : Convert.ToDouble(row[property.Name].ToString());
                            }

                            else if (property.PropertyType == typeof(Int32)
                                     || property.PropertyType == typeof(Int32?))
                            {
                                propertyValue = row[property.Name] == System.DBNull.Value
                                    ? 0
                                    : Convert.ToInt32(row[property.Name].ToString());
                            }

                            else if (property.PropertyType == typeof(DateTime)
                                     || property.PropertyType == typeof(DateTime?))
                            {
                                propertyValue = row[property.Name] == System.DBNull.Value
                                    ? DateTime.MinValue
                                    : Convert.ToDateTime(row[property.Name].ToString());
                            }
                            else if (property.PropertyType == typeof(DateTimeOffset)
                                 || property.PropertyType == typeof(DateTimeOffset?))
                            {
#pragma warning disable CS8604 // Possible null reference argument.
                                propertyValue = row[property.Name] == System.DBNull.Value
                                    ? DateTimeOffset.MinValue
                                    : DateTimeOffset.Parse(row[columnName: property.Name].ToString());
#pragma warning restore CS8604 // Possible null reference argument.
                            }
                            else if (property.PropertyType.IsEnum)
                            {
                                var enums = Enum.GetValues(property.PropertyType);
                                try
                                {
                                    propertyValue = Enum.Parse(property.PropertyType,
                                        row[property.Name].ToString() ?? string.Empty);
                                }
                                catch
                                {
                                    propertyValue = enums.GetValue(0);
                                }
                            }
                            else
                            {
                                try
                                {
                                    // DateTime to DateTime offset conversion or any other
                                    //  non-compatible cast will crash this line of code.
                                    propertyValue = System.Convert.ChangeType(
                                        row[property.Name], property.PropertyType);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(classType.Name + " " + property.Name + " " + ex.Message);
                                }
                            }

                            // Set the object property value
                            // (hopefully with the correct data type and value!)
                            if (propertyValue != null)
                                property.SetValue(classObject, propertyValue, null);
                        }

                        if (parent != null)
                        {
                            if (property.Name.ToLower() == "sourcewellid")
                                property.SetValue(classObject, parent.SourceWellId);
                            if (property.Name.ToLower() == "sourcewellboreid")
                                property.SetValue(classObject, parent.SourceId);
                            if (property.Name.ToLower() == "uwi")
                                property.SetValue(classObject, parent.SourceWellId);
                        }
                    }
                }
    

                result.Add(classObject);
            }

            return result;
        }
        catch
        {
            return new List<T>();
        }
    }

    /// <summary>
    /// Cleans up the source datatable
    /// </summary>
    /// <param name="dt">source data table</param>
    /// <param name="m">API mapping</param>
    /// <returns>cleaned up datatable with new column names and excess columns removed</returns>
    static DataTable ReKeyDataTable(DataTable dt, CollectionMap m)
    {
        // Loop each mapped column and rename the raw source dt column
        foreach (ColumnMap map in m.Columns)
        {
            if (dt.Columns.Contains(map.Column))
                dt.Columns[map.Column]!.ColumnName = map.MapTo.Trim();
        }

        // Remove any raw source columns that were not mapped to anything
        var dtCopy = dt.Copy();
        foreach (DataColumn c in dt.Columns)
        {
            if (!m.Columns.Exists(x => x.MapTo == c.ColumnName.Trim()))
                dtCopy.Columns.Remove(c.ColumnName.Trim());
            else
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                dtCopy.Columns[name: c.ColumnName.Trim()].ColumnName = c.ColumnName.ToLower();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        // Return a cleaned up dt with correct column names
        return dtCopy;
    }
}