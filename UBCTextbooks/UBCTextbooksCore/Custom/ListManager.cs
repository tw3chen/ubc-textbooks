using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using UBCTextbooksCore.Constants;
using UBCTextbooksCore.Data;

namespace UBCTextbooksCore.Custom
{
    public class ListManager
    {
        public SqlDataReader RetrieveCompleteList(string listid)
        {
            try
            {
                int ilistid = Common.SafeIntParse(listid);
                SqlParameter[] spParameters = new SqlParameter[1];
                spParameters[0] = new SqlParameter("@" + UParameter.ListId, ilistid);

                DataAccess data = new DataAccess(AccessType.Visitor);
                return (data.GetSqlDataReader(StoredProcedure.RetrieveCompleteList, spParameters));
            }
            catch (Exception ex)
            {
                Log.LogError(MethodInfo.GetCurrentMethod().Name, ex.Message + ex.StackTrace);
            }
            return null;
        }

        public int? ListInsert(string userid, string bookid, string condition, string price, string area, string location, string notes)
        {
            try
            {
                int iuserid = Common.SafeIntParse(userid);
                int ibookid = Common.SafeIntParse(bookid);
                float fprice = Common.SafeFloatParse(price);
                SqlParameter[] spParameters = new SqlParameter[7];
                spParameters[0] = new SqlParameter("@" + UParameter.UserId, userid);
                spParameters[1] = new SqlParameter("@" + UParameter.BookId, bookid);
                spParameters[2] = new SqlParameter("@" + UParameter.Condition, condition);
                spParameters[3] = new SqlParameter("@" + UParameter.Price, price);
                spParameters[4] = new SqlParameter("@" + UParameter.Area, area);
                spParameters[5] = new SqlParameter("@" + UParameter.Location, location);
                spParameters[6] = new SqlParameter("@" + UParameter.Notes, notes);

                DataAccess data = new DataAccess(AccessType.User);
                return (int?)data.ExecuteScalar(StoredProcedure.ListInsert, spParameters);
            }
            catch (Exception ex)
            {
                Log.LogError(MethodInfo.GetCurrentMethod().Name, ex.Message + ex.StackTrace);
            }
            return null;
        }

        public SqlDataReader RetrieveExtraInfo(string listid)
        {
            try
            {
                int ilistid = Common.SafeIntParse(listid);
                SqlParameter[] spParameters = new SqlParameter[1];
                spParameters[0] = new SqlParameter("@" + UParameter.ListId, ilistid);

                DataAccess data = new DataAccess(AccessType.User);
                return (data.GetSqlDataReader(StoredProcedure.RetrieveExtraInfo, spParameters));
            }
            catch (Exception ex)
            {
                Log.LogError(MethodInfo.GetCurrentMethod().Name, ex.Message + ex.StackTrace);
            }
            return null;
        }

        public ArrayList RetrieveSearch(string searchBy, string keywords, string area, string price, string time, string sortBy)
        {
            try
            {
                int iprice = Common.SafeIntParse(price);
                SqlParameter[] spParameters = new SqlParameter[6];
                spParameters[0] = new SqlParameter("@" + UParameter.SearchType, searchBy);
                spParameters[1] = new SqlParameter("@" + UParameter.Keywords, keywords);
                spParameters[2] = new SqlParameter("@" + UParameter.Area, area);
                spParameters[3] = new SqlParameter("@" + UParameter.Price, iprice);
                spParameters[4] = new SqlParameter("@" + UParameter.EarliestTimeListed, time);
                spParameters[5] = new SqlParameter("@" + UParameter.SortBy, sortBy);

                ArrayList arrayList = new ArrayList();
                DataAccess data = new DataAccess(AccessType.Visitor);
                SqlDataReader reader = data.GetSqlDataReader(StoredProcedure.RetrieveSearch, spParameters);
                while (reader.Read())
                {
                    UList ulist = new UList();
                    ulist.ListId = reader[UParameter.ListId].ToString();
                    ulist.BookName = reader[UParameter.BookName].ToString();
                    ulist.AuthorNames = reader[UParameter.AuthorNames].ToString();
                    ulist.Edition = reader[UParameter.Edition].ToString();
                    ulist.ISBN = reader[UParameter.ISBN].ToString();
                    ulist.EAN = reader[UParameter.EAN].ToString();
                    ulist.Condition = reader[UParameter.Condition].ToString();
                    ulist.Price = reader[UParameter.Price].ToString();
                    ulist.Area = reader[UParameter.Area].ToString();
                    ulist.NumberOfOffers = reader[UParameter.NumberOfOffers].ToString();
                    ulist.TimePosted = reader[UParameter.TimePosted].ToString();
                    arrayList.Add(ulist);
                }
                return arrayList;
            }
            catch (Exception ex)
            {
                Log.LogError(MethodInfo.GetCurrentMethod().Name, ex.Message + ex.StackTrace);
            }
            return null;
        }

        public ArrayList RetrieveList(string userid)
        {
            try
            {
                int iuserid = Common.SafeIntParse(userid);
                SqlParameter[] spParameters = new SqlParameter[1];
                spParameters[0] = new SqlParameter("@" + UParameter.UserId, userid);

                ArrayList arrayList = new ArrayList();
                DataAccess data = new DataAccess(AccessType.User);
                SqlDataReader reader = data.GetSqlDataReader(StoredProcedure.RetrieveListByUser, spParameters);
                while (reader.Read())
                {
                    UList ulist = new UList();
                    ulist.ListId = reader[UParameter.ListId].ToString();
                    ulist.BookName = reader[UParameter.BookName].ToString();
                    ulist.AuthorNames = reader[UParameter.AuthorNames].ToString();
                    ulist.Edition = reader[UParameter.Edition].ToString();
                    ulist.ISBN = reader[UParameter.ISBN].ToString();
                    ulist.EAN = reader[UParameter.EAN].ToString();
                    ulist.Condition = reader[UParameter.Condition].ToString();
                    ulist.Price = reader[UParameter.Price].ToString();
                    ulist.Area = reader[UParameter.Area].ToString();
                    ulist.NumberOfOffers = reader[UParameter.NumberOfOffers].ToString();
                    ulist.TimePosted = reader[UParameter.TimePosted].ToString();
                    arrayList.Add(ulist);
                }
                return arrayList;
            }
            catch (Exception ex)
            {
                Log.LogError(MethodInfo.GetCurrentMethod().Name, ex.Message + ex.StackTrace);
            }
            return null;
        }
    }

    public class UList
    {
        private string courseCode;
        private string listId;
        private string bookName;
        private string authorNames;
        private string edition;
        private string isbn;
        private string ean;
        private string condition;
        private string price;
        private string area;
        private string numberOfOffers;
        private string timePosted;

        public string CourseCode
        {
            get { return this.courseCode; }
            set { this.courseCode = value; }
        }
        public string ListId
        {
            get { return this.listId; }
            set { this.listId = value; }
        }
        public string BookName
        {
            get { return this.bookName; }
            set { this.bookName = value; }
        }
        public string AuthorNames
        {
            get { return this.authorNames; }
            set { this.authorNames = value; }
        }
        public string Edition
        {
            get { return this.edition; }
            set { this.edition = value; }
        }
        public string ISBN
        {
            get { return this.isbn; }
            set { this.isbn = value; }
        }
        public string EAN
        {
            get { return this.ean; }
            set { this.ean = value; }
        }
        public string Condition
        {
            get { return this.condition; }
            set { this.condition = value; }
        }
        public string Price
        {
            get { return this.price; }
            set { this.price = value; }
        }
        public string Area
        {
            get { return this.area; }
            set { this.area = value; }
        }
        public string NumberOfOffers
        {
            get { return this.numberOfOffers; }
            set { this.numberOfOffers = value; }
        }
        public string TimePosted
        {
            get { return this.timePosted; }
            set { this.timePosted = value; }
        }
    }
}
