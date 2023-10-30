using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZ.SQLContext.Helpers
{
    public static class SqlHelper
    {
        public static SqlParameter CreateParameter(string parameterName, object value)
        {
            var sqlParameter = new SqlParameter();

            sqlParameter.ParameterName = $"@{parameterName}";
            sqlParameter.Value = value;

            return sqlParameter;
        }
    }
}
