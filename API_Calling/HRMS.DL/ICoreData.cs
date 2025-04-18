using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Management.DataAccessLayer
{
    public interface ICoreData
    {
        T[] ConvertToList<T>(DataTable dataTable);
    }
}
