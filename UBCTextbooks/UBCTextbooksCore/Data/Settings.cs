using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UBCTextbooksCore.Constants;
using UBCTextbooksCore.Custom;

namespace UBCTextbooksCore.Data
{
    public class Settings
    {
        public List<string> Department()
        {
            List<string> department = new List<string>();
            try
            {
                DataAccess data = new DataAccess(AccessType.Visitor);
                SqlDataReader reader = data.GetSqlDataReader(StoredProcedure.RetrieveDepartment, null);
                while (reader.Read())
                {
                    department.Add((String)reader[UParameter.Department]);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Log.LogError(MethodInfo.GetCurrentMethod().Name, ex.Message + ex.StackTrace);
            }
            return department;
        }

        public List<string> Area()
        {
            List<string> area = new List<string>();
            try
            {
                DataAccess data = new DataAccess(AccessType.Visitor);
                SqlDataReader reader = data.GetSqlDataReader(StoredProcedure.RetrieveArea, null);
                while (reader.Read())
                {
                    area.Add((String)reader[UParameter.Area]);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Log.LogError(MethodInfo.GetCurrentMethod().Name, ex.Message + ex.StackTrace);
                area.Add(ex.Message + ex.StackTrace);
            }
            return area;
        }
    }
}
