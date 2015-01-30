using System;
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
    public class BookManager
    {
        public int? BookInsert(string bookName, string authors, string isbn, string imageurl, string price, string binding, string manufacturer, string edition, string ean, string coursecode)
        {
            try
            {
                float fprice = Common.SafeFloatParse(price);
                fprice /= 100F;
                SqlParameter[] spParameters = new SqlParameter[10];
                spParameters[0] = new SqlParameter("@" + UParameter.CourseCode, coursecode);
                spParameters[1] = new SqlParameter("@" + UParameter.BookName, bookName);
                spParameters[2] = new SqlParameter("@" + UParameter.AuthorNames, authors);
                spParameters[3] = new SqlParameter("@" + UParameter.Edition, edition);
                spParameters[4] = new SqlParameter("@" + UParameter.EAN, ean);
                spParameters[5] = new SqlParameter("@" + UParameter.ISBN, isbn);
                spParameters[6] = new SqlParameter("@" + UParameter.ImageUrl, imageurl);
                spParameters[7] = new SqlParameter("@" + UParameter.Price, fprice);
                spParameters[8] = new SqlParameter("@" + UParameter.BookBinding, binding);
                spParameters[9] = new SqlParameter("@" + UParameter.BookManufacturer, manufacturer);

                DataAccess data = new DataAccess(AccessType.User);
                return (int?)data.ExecuteScalar(StoredProcedure.BookInsert, spParameters);
            }
            catch (Exception ex)
            {
                Log.LogError(MethodInfo.GetCurrentMethod().Name, ex.Message + ex.StackTrace);
            }
            return null;
        }

        public int? CourseInsert(string coursecode, string bookid)
        {
            try
            {
                int ubookid = Common.SafeIntParse(bookid);
                SqlParameter[] spParameters = new SqlParameter[2];
                spParameters[0] = new SqlParameter("@" + UParameter.CourseCode, coursecode);
                spParameters[1] = new SqlParameter("@" + UParameter.BookId, bookid);

                DataAccess data = new DataAccess(AccessType.User);
                return (int?)data.ExecuteScalar(StoredProcedure.CourseInsert, spParameters);
            }
            catch (Exception ex)
            {
                Log.LogError(MethodInfo.GetCurrentMethod().Name, ex.Message + ex.StackTrace);
            }
            return null;
        }

        // 2 means repeated course code, null means book in db, 0 means book not in db
        public int? CheckBookInDB(string isbn, string coursecode)
        {
            try
            {
                SqlParameter[] spParameters = new SqlParameter[2];
                spParameters[0] = new SqlParameter("@" + UParameter.ISBN, isbn);
                spParameters[1] = new SqlParameter("@" + UParameter.CourseCode, coursecode);

                DataAccess data = new DataAccess(AccessType.User);
                return (int?)data.ExecuteScalar(StoredProcedure.CheckIfBookInDB, spParameters);
            }
            catch (Exception ex)
            {
                Log.LogError(MethodInfo.GetCurrentMethod().Name, ex.Message + ex.StackTrace);
            }
            return null;
        }

        public Dictionary<string, string> RetrieveBookByISBN(string isbn)
        {
            try
            {
                SqlParameter[] spParameters = new SqlParameter[1];
                spParameters[0] = new SqlParameter("@" + UParameter.ISBN, isbn);

                DataAccess data = new DataAccess(AccessType.User);
                SqlDataReader reader = data.GetSqlDataReader(StoredProcedure.RetrieveBookByISBN, spParameters);

                Dictionary<string, string> bookInfo = new Dictionary<string, string>();
                InitializeBookInfo(bookInfo);
                reader.Read();
                bookInfo[Book.Id] = reader[UParameter.BookId].ToString();
                bookInfo[Book.Name] = reader[UParameter.BookName].ToString();
                bookInfo[Book.ImageUrl] = reader[UParameter.ImageUrl].ToString();
                bookInfo[Book.AuthorNames] = reader[UParameter.AuthorNames].ToString();
                bookInfo[Book.Binding] = reader[UParameter.BookBinding].ToString();
                bookInfo[Book.ISBN] = reader[UParameter.ISBN].ToString();
                bookInfo[Book.EAN] = reader[UParameter.EAN].ToString();
                bookInfo[Book.Manufacturer] = reader[UParameter.BookManufacturer].ToString();
                bookInfo[Book.Edition] = reader[UParameter.Edition].ToString();
                bookInfo[Book.Price] = reader[UParameter.Price].ToString();
                return bookInfo;
            }
            catch (Exception ex)
            {
                Log.LogError(MethodInfo.GetCurrentMethod().Name, ex.Message + ex.StackTrace);
            }
            return null;
        }

        private void InitializeBookInfo(Dictionary<string, string> bookInfo)
        {
            bookInfo.Add(Book.Id, null);
            bookInfo.Add(Book.ImageUrl, null);
            bookInfo.Add(Book.Name, null);
            bookInfo.Add(Book.AuthorNames, null);
            bookInfo.Add(Book.Binding, null);
            bookInfo.Add(Book.ISBN, null);
            bookInfo.Add(Book.EAN, null);
            bookInfo.Add(Book.Manufacturer, null);
            bookInfo.Add(Book.Price, null);
            bookInfo.Add(Book.Edition, null);
        }
    }
}
