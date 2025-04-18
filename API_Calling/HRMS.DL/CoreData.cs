using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Management.DataAccessLayer
{
    class CoreData : ICoreData
    {
        public T[] ConvertToList<T>(DataTable dataTable)
        {
            var columns = dataTable.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            var destination = (T)Activator.CreateInstance(typeof(T));
            var coreProperties = destination.GetType().GetProperties().ToList();
            List<T> resultData = new List<T>();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                destination = (T)Activator.CreateInstance(typeof(T));
                foreach (var columnName in columns)
                {
                    var propertyKey = coreProperties.Where(x => x.Name == columnName && x.CanRead).FirstOrDefault();
                    if (propertyKey != null)
                    {
                        var type = propertyKey.PropertyType.Name;
                        if (type == "Int32")
                            propertyKey.SetValue(destination, Convert.ToInt32(dataRow[columnName]));
                        else if (type == "Guid")
                            propertyKey.SetValue(destination, new Guid(dataRow[columnName].ToString()));
                        else if (type == "Boolean")
                            propertyKey.SetValue(destination, Convert.ToBoolean(dataRow[columnName]));
                        else if (type == "String")
                            propertyKey.SetValue(destination, Convert.ToString(dataRow[columnName]));
                        else
                            if (type == "DateTime" && Convert.ToString(dataRow[columnName]) == string.Empty)
                        {

                        }
                        else
                            propertyKey.SetValue(destination, dataRow[columnName]);
                    }
                }
                resultData.Add(destination);
            }

            return resultData.ToArray();
        }
    }
}
