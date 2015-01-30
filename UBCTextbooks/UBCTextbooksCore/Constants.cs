using System;

namespace UBCTextbooksCore
{
    namespace Constants
    {
        public static class StoredProcedure
        {
            public const string AccountInsert = "sp_AccountInsert";
            public const string AccountEdit = "sp_AccountEdit";
            public const string IsEmailOrNameRepeated = "sp_IsEmailOrNameRepeated";
            public const string ActivateEmail = "sp_ActivateEmail";
            public const string Login = "sp_Login";
            public const string RetrieveDepartment = "sp_RetrieveDepartment";
            public const string RetrieveArea = "sp_RetrieveArea";
            public const string BookInsert = "sp_BookInsert";
            public const string ListInsert = "sp_ListInsert";
            public const string RetrieveListByUser = "sp_RetrieveListByUser";
            public const string RetrieveExtraInfo = "sp_RetrieveExtraInfo";
            public const string CheckIfBookInDB = "sp_CheckIfBookInDB";
            public const string RetrieveBookByISBN = "sp_RetrieveBookByISBN";
            public const string CourseInsert = "sp_CourseInsert";
            public const string RetrieveCompleteList = "sp_RetrieveCompleteList";
            public const string CommentInsert = "sp_CommentInsert";
            public const string RetrieveComments = "sp_RetrieveComments";
            public const string RetrieveUserInfo = "sp_RetrieveUserInfo";
            public const string RetrieveSearch = "sp_RetrieveSearch";
            public const string InsertLog = "sp_InsertLog";
            public const string RetrieveLog = "sp_RetrieveLog";
        }

        public static class AccessType
        {
            public const string Visitor = "Visitor";
            public const string User = "User";
        }

        public static class UParameter
        {
            public const string SortBy = "SortBy";
            public const string EarliestTimeListed = "EarliestTimeListed";
            public const string Keywords = "Keywords";
            public const string SearchType = "SearchType";
            public const string EmailAddress = "EmailAddress";
            public const string Password = "Password";
            public const string DisplayName = "DisplayName";
            public const string UserGuid = "UserGuid";
            public const string UserId = "UserId";
            public const string Department = "Department";
            public const string Area = "Area";
            public const string Location = "Location";
            public const string Notes = "Notes";
            public const string BookId = "BookId";
            public const string BookName = "BookName";
            public const string AuthorNames = "AuthorNames";
            public const string EAN = "EAN";
            public const string ISBN = "ISBN";
            public const string Edition = "Edition";
            public const string ImageUrl = "ImageUrl";
            public const string Price = "Price";
            public const string BookBinding = "BookBinding";
            public const string BookManufacturer = "BookManufacturer";
            public const string CourseCode = "CourseCode";
            public const string Condition = "Condition";
            public const string ListId = "ListId";
            public const string TimePosted = "TimePosted";
            public const string TimeListed = "TimeListed";
            public const string NumberOfOffers = "NumberOfOffers";
            public const string CourseList = "CourseList";
            public const string RefPrice = "RefPrice";
            public const string Comments = "Comments";
            public const string OfferOrComment = "OfferOrComment";
            public const string MethodName = "MethodName";
            public const string StackTrace = "StackTrace";
        }

        public static class Paths
        {
            public const string KeyFileName = "Key.Config";
            public const string KeyTextName = "Key.txt";
        }

        public static class QueryString
        {
            public const string Email = "email";
            public const string Token = "token";
            public const string ISBN = "isbn";
            public const string CourseCode = "cc";
            public const string BookId = "bid";
            public const string Price = "pr";
            public const string Page = "p";
            public const string InDb = "ib";
            public const string ListId = "listid";
            public const string UserId = "uid";
            public const string SearchBy = "sb";
            public const string Keywords = "k";
            public const string Area = "a";
            public const string OldestPostedTime = "t";
            public const string PriceRange = "pc";
            public const string SortBy = "so";
        }

        public static class USession
        {
            public const string CaptchaText = "CaptchaText";
            public const string IsLoggedIn = "IsLoggedIn";
            public const string AccountInfo = "AccountInfo";
            public const string TempGuid = "TempGuid";
            public const string TempToMail = "TempToMail";
            public const string PageTracker = "PageTracker";
            public const string Book = "Book";
            public const string BookId = "BookId";
        }

        public static class UCookie
        {
            public const string EmailAddress = "EmailAddress";
            public const string Account = "Account";
        }

        public static class StringResource
        {
            public const string FileName = "String";
        }

        public static class UAccount
        {
            public const string EmailAddress = "EmailAddress";
            public const string DisplayName = "DisplayName";
            public const string Password = "Password";
            public const string UserId = "UserId";
        }

        public static class AmazonRequest
        {
            public const string Service = "Service";
            public const string AccessKey = "AWSAccessKeyId";
            public const string Operation = "Operation";
            public const string ItemType = "IdType";
            public const string Item = "ItemId";
            public const string SearchType = "SearchIndex";
            public const string Size = "ResponseGroup";
        }

        public static class Book
        {
            public const string Name = "Name";
            public const string Edition = "Edition";
            public const string AuthorNames = "AuthorNames";
            public const string Binding = "Binding";
            public const string Manufacturer = "Manufacturer";
            public const string EAN = "EAN";
            public const string ISBN = "ISBN";
            public const string ImageUrl = "ImageUrl";
            public const string Price = "Price";
            public const string Id = "Id";
        }
    }
}
