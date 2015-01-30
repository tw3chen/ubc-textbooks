using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using UBCTextbooksCore.Constants;
using UBCTextbooksCore.Data;

namespace UBCTextbooksCore.Custom
{
    public static class Log
    {
        public static void LogError(string methodName, string stackTrace)
        {
            try
            {
                SqlParameter[] spParameters = new SqlParameter[2];
                spParameters[0] = new SqlParameter("@" + UParameter.MethodName, methodName);
                spParameters[1] = new SqlParameter("@" + UParameter.StackTrace, stackTrace);

                DataAccess data = new DataAccess(AccessType.Visitor);
                data.ExecuteNonQuery(StoredProcedure.InsertLog, spParameters);
            }
            catch (Exception ex)
            {

            }
        }

        public static void RetrieveLog()
        {
            SqlParameter[] spParameters = null;

            DataAccess data = new DataAccess(AccessType.Visitor);
            data.GetSqlDataReader(StoredProcedure.RetrieveLog, spParameters);
        }
    }
}
