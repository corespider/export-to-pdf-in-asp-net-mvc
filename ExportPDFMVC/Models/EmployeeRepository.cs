using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;
using System.Threading.Tasks;

namespace ExportPDFMVC.Models
{
    public class EmployeeRepository
    {
        public IDbConnection connection
        {
            get
            {
                return new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            }
        }
        public async Task<object> GetEmployeeList()
        {
            object result = null;
            try
            {
                using (IDbConnection con = connection)
                {
                    string Query = "SELECT EmpID, EmpName, Mobile, PresentAddress, Area, City, Country, Qualification, Email" +
                                    " from EmployeeDetails";
                    con.Open();
                    DynamicParameters param = new DynamicParameters();
                    result = await con.QueryAsync<Employee>(Query, param, commandType: CommandType.Text);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

    }
}