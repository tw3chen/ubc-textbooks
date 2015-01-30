using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using UBCTextbooksCore.Constants;
using UBCTextbooksCore.Data;
using UBCTextbooksCore.Security;

namespace UBCTextbooksCore.Custom
{
    public class AccountManager
    {
        public SqlDataReader RetrieveUserInfo(string userid)
        {
            try
            {
                int iuserid = Common.SafeIntParse(userid);
                SqlParameter[] spParameters = new SqlParameter[1];
                spParameters[0] = new SqlParameter("@" + UParameter.UserId, iuserid);

                DataAccess data = new DataAccess(AccessType.Visitor);
                return (data.GetSqlDataReader(StoredProcedure.RetrieveUserInfo, spParameters));
            }
            catch (Exception ex)
            {
                Log.LogError(MethodInfo.GetCurrentMethod().Name, ex.Message + ex.StackTrace);
            }
            return null;
        }

        public void CreateAccount(string email, string password, string displayName, string guid)
        {
            try
            {
                SqlParameter[] spParameters = new SqlParameter[4];
                spParameters[0] = new SqlParameter("@" + UParameter.EmailAddress, email);
                spParameters[1] = new SqlParameter("@" + UParameter.Password, password);
                spParameters[2] = new SqlParameter("@" + UParameter.DisplayName, displayName);
                spParameters[3] = new SqlParameter("@" + UParameter.UserGuid, guid);

                DataAccess data = new DataAccess(AccessType.Visitor);
                data.ExecuteNonQuery(StoredProcedure.AccountInsert, spParameters);
            }
            catch (Exception ex)
            {
                Log.LogError(MethodInfo.GetCurrentMethod().Name, ex.Message + ex.StackTrace);
            }
        }

        public void EditAccount(int userid, string email, string password, string displayName)
        {
            try
            {
                SqlParameter[] spParameters = new SqlParameter[4];
                spParameters[0] = new SqlParameter("@" + UParameter.UserId, userid);
                spParameters[1] = new SqlParameter("@" + UParameter.EmailAddress, email);
                spParameters[2] = new SqlParameter("@" + UParameter.Password, password);
                spParameters[3] = new SqlParameter("@" + UParameter.DisplayName, displayName);

                DataAccess data = new DataAccess(AccessType.User);
                data.ExecuteNonQuery(StoredProcedure.AccountEdit, spParameters);
            }
            catch (Exception ex)
            {
                Log.LogError(MethodInfo.GetCurrentMethod().Name, ex.Message + ex.StackTrace);
            }
        }

        public bool IsEmailOrNameRepeated(string email, string displayName, int userid)
        {
            try
            {
                DataAccess data = new DataAccess(AccessType.Visitor);
                SqlParameter[] spParameters = new SqlParameter[3];
                spParameters[0] = new SqlParameter("@" + UParameter.EmailAddress, email);
                spParameters[1] = new SqlParameter("@" + UParameter.DisplayName, displayName);
                spParameters[2] = new SqlParameter("@" + UParameter.UserId, userid);
                if ((int)data.ExecuteScalar(StoredProcedure.IsEmailOrNameRepeated, spParameters) > 0)
                    return true;
            }
            catch (Exception ex)
            {
                Log.LogError(MethodInfo.GetCurrentMethod().Name, ex.Message + ex.StackTrace);
            }
            return false;
        }

        public bool ActivateAccount(string userGuid)
        {
            try
            {
                DataAccess data = new DataAccess(AccessType.Visitor);
                SqlParameter[] spParameters = new SqlParameter[1];
                spParameters[0] = new SqlParameter("@" + UParameter.UserGuid, userGuid);
                if ((int)data.ExecuteScalar(StoredProcedure.ActivateEmail, spParameters) > 0)
                    return true;
            }
            catch (Exception ex)
            {
                Log.LogError(MethodInfo.GetCurrentMethod().Name, ex.Message + ex.StackTrace);
            }
            return false;
        }

        public Dictionary<string, string> Login(string email, string password, string keyFile)
        {
            Account account = new Account();
            try
            {
                DataAccess data = new DataAccess(AccessType.Visitor);
                SqlParameter[] spParameters = new SqlParameter[1];
                spParameters[0] = new SqlParameter("@" + UParameter.EmailAddress, email);
                SqlDataReader reader = data.GetSqlDataReader(StoredProcedure.Login, spParameters);
                reader.Read();
                string keyBytes = (string)reader[UParameter.Password];
                if (password == keyBytes)
                {
                    account.userid = (int)reader[UParameter.UserId];
                    account.emailAddress = (String)reader[UParameter.EmailAddress];
                    account.displayName = (String)reader[UParameter.DisplayName];
                    account.password = password;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Log.LogError(MethodInfo.GetCurrentMethod().Name, ex.Message + ex.StackTrace);
            }
            Dictionary<string, string> accountInfo = new Dictionary<string, string>();
            accountInfo.Add(UAccount.EmailAddress, account.emailAddress);
            accountInfo.Add(UAccount.DisplayName, account.displayName);
            accountInfo.Add(UAccount.Password, account.password);
            accountInfo.Add(UAccount.UserId, account.userid.ToString());
            return accountInfo;
        }
    }

    public class Account
    {
        public string emailAddress = String.Empty;
        public string displayName = String.Empty;
        public string password = String.Empty;
        public int userid = 0;
    }
}