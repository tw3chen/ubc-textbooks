using System;
using System.Collections.Generic;
using System.Reflection;
using System.Data.SqlClient;
using UBCTextbooksCore.Constants;
using UBCTextbooksCore.Data;
using UBCTextbooksCore.Security;

namespace UBCTextbooksCore.Custom
{
    public class CommentManager
    {
        public void InsertCommentOrOffer(string listid, string displayName, string emailaddress, string offerorcomment, string comments)
        {
            try
            {
                SqlParameter[] spParameters = new SqlParameter[5];
                spParameters[0] = new SqlParameter("@" + UParameter.ListId, listid);
                spParameters[1] = new SqlParameter("@" + UParameter.DisplayName, displayName);
                spParameters[2] = new SqlParameter("@" + UParameter.EmailAddress, emailaddress);
                spParameters[3] = new SqlParameter("@" + UParameter.OfferOrComment, offerorcomment);
                spParameters[4] = new SqlParameter("@" + UParameter.Comments, comments);

                DataAccess data = new DataAccess(AccessType.Visitor);
                data.ExecuteNonQuery(StoredProcedure.CommentInsert, spParameters);
            }
            catch (Exception ex)
            {
                Log.LogError(MethodInfo.GetCurrentMethod().Name, ex.Message + ex.StackTrace);
            }
        }

        public SqlDataReader RetrieveComments(string listid)
        {
            try
            {
                int ilistid = Common.SafeIntParse(listid);
                SqlParameter[] spParameters = new SqlParameter[1];
                spParameters[0] = new SqlParameter("@" + UParameter.ListId, ilistid);

                DataAccess data = new DataAccess(AccessType.Visitor);
                return (data.GetSqlDataReader(StoredProcedure.RetrieveComments, spParameters));
            }
            catch (Exception ex)
            {
                Log.LogError(MethodInfo.GetCurrentMethod().Name, ex.Message + ex.StackTrace);
            }
            return null;
        }
    }
}