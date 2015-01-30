using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using UBCTextbooksCore.Constants;
using UBCTextbooksCore.Custom;
using Amazon.PAAPI;
using System.Configuration;

namespace UBCTextbooksCore.Feed
{
    public class AmazonBook
    {
        public Dictionary<string, string> LookUpBook(string isbn)
        {
            try
            {
                //string s = ConfigurationManager.AppSettings["SiteEmailAddress"];
                Amazon.PAAPI.AWSECommerceServicePortTypeClient amazonClient = new Amazon.PAAPI.AWSECommerceServicePortTypeClient();
                Amazon.PAAPI.ItemSearchRequest request = new Amazon.PAAPI.ItemSearchRequest();
                //request.SearchIndex = "Books";
                //request.Title = "WCF";
                //request.ResponseGroup = new string[] { "Small" };
                request.SearchIndex = "Books";
                request.Power = "ISBN:" + isbn.Trim();
                request.ResponseGroup = new string[] { /*"Large", "Images", "OfferSummary"*/"Large", "Images" };
                request.Sort = "salesrank";

                Amazon.PAAPI.ItemSearch itemSearch = new Amazon.PAAPI.ItemSearch();
                itemSearch.Request = new Amazon.PAAPI.ItemSearchRequest[] { request };
                itemSearch.AWSAccessKeyId = "AKIAJF2SIXKMWXLTYD2A";
                itemSearch.AssociateTag = "ubc05-20";

                // send the ItemSearch request
                Amazon.PAAPI.ItemSearchResponse response = amazonClient.ItemSearch(itemSearch);

                Dictionary<string, string> bookInfo = new Dictionary<string, string>();
                InitializeBookInfo(bookInfo);
                if (response.Items[0].Item.Count() >= 1)
                {
                    Amazon.PAAPI.Item item = response.Items[0].Item[0];
                    bookInfo[Book.ImageUrl] = item.MediumImage.URL;
                    bookInfo[Book.Name] = item.ItemAttributes.Title;
                    bookInfo[Book.Edition] = item.ItemAttributes.Edition;
                    int temp = 1;
                    foreach (string author in item.ItemAttributes.Author)
                    {
                        if (temp == 1)
                        {
                            bookInfo[Book.AuthorNames] += author;
                        }
                        else
                        {
                            bookInfo[Book.AuthorNames] += "/" + author;
                        }
                        temp++;
                    }
                    bookInfo[Book.Binding] = item.ItemAttributes.Binding;
                    bookInfo[Book.EAN] = item.ItemAttributes.EAN;
                    bookInfo[Book.ISBN] = item.ItemAttributes.ISBN;
                    bookInfo[Book.Manufacturer] = item.ItemAttributes.Manufacturer;
                    bookInfo[Book.Price] = item.ItemAttributes.ListPrice.Amount;
                }

                /*string temp = "";
                foreach (var item in response.Items[0].Item)
                {
                    temp += item.ItemAttributes.Title + "\n";
                }*/

                /*XmlDocument doc = new XmlDocument();
                doc.Load(AppendUrl(isbn));
                Dictionary<string, string> bookInfo = new Dictionary<string, string>();
                InitializeBookInfo(bookInfo);
                XmlNodeList lstIsValid = doc.GetElementsByTagName("IsValid");

                if (lstIsValid.Count > 0 && lstIsValid[0].InnerXml == "True")
                {
                    XmlNodeList lstItems = doc.GetElementsByTagName("Item");

                    if (lstItems.Count > 0)
                    {
                        XmlNode nItem = lstItems[0];
                        foreach (XmlNode nChild in nItem.ChildNodes)
                        {
                            if (nChild.Name == "MediumImage")
                            {
                                foreach (XmlNode nURLImg in nChild.ChildNodes)
                                {
                                    if (nURLImg.Name == "URL")
                                    {
                                        bookInfo[Book.ImageUrl] = nURLImg.InnerXml;
                                    }
                                }
                            }
                            else if (nChild.Name == "ItemAttributes")
                            {
                                foreach (XmlNode nIA in nChild.ChildNodes)
                                {
                                    if (nIA.Name == "Title")
                                    {
                                        bookInfo[Book.Name] = nIA.InnerXml;
                                    }
                                    if (nIA.Name == "Edition")
                                    {
                                        bookInfo[Book.Edition] = nIA.InnerXml;
                                    }
                                    if (nIA.Name == "Author")
                                    {
                                        if (moreThanOneAuthor)
                                        {
                                            bookInfo[Book.AuthorNames] += "/" + nIA.InnerXml;
                                        }
                                        else
                                        {
                                            bookInfo[Book.AuthorNames] = nIA.InnerXml;
                                            moreThanOneAuthor = true;
                                        }
                                    }
                                    if (nIA.Name == "Binding")
                                    {
                                        bookInfo[Book.Binding] = nIA.InnerXml;
                                    }
                                    if (nIA.Name == "EAN")
                                    {
                                        bookInfo[Book.EAN] = nIA.InnerXml;
                                    }
                                    if (nIA.Name == "ISBN")
                                    {
                                        bookInfo[Book.ISBN] = nIA.InnerXml;
                                    }
                                    if (nIA.Name == "Manufacturer")
                                    {
                                        bookInfo[Book.Manufacturer] = nIA.InnerXml;
                                    }
                                    if (nIA.Name == "ListPrice")
                                    {
                                        foreach (XmlNode nLP in nIA.ChildNodes)
                                        {
                                            if (nLP.Name == "Amount")
                                            {
                                                bookInfo[Book.Price] = nLP.InnerXml;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }*/
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

        private string AppendUrl(string isbn)
        {
            StringBuilder requestUrl = new StringBuilder(baseurl);
            requestUrl.Append("?");
            requestUrl.Append(AmazonRequest.Service).Append("=");
            requestUrl.Append(service).Append("&");
            requestUrl.Append(AmazonRequest.AccessKey).Append("=");
            requestUrl.Append(key).Append("&");
            requestUrl.Append(AmazonRequest.Operation).Append("=");
            requestUrl.Append(operation).Append("&");
            requestUrl.Append(AmazonRequest.ItemType).Append("=");
            requestUrl.Append(idtype).Append("&");
            requestUrl.Append(AmazonRequest.SearchType).Append("=");
            requestUrl.Append(searchtype).Append("&");
            requestUrl.Append(AmazonRequest.Size).Append("=");
            requestUrl.Append(size).Append("&");
            requestUrl.Append(AmazonRequest.Item).Append("=");
            requestUrl.Append(isbn);
            return requestUrl.ToString();
        }

        private const string baseurl = "http://ca.free.apisigning.com/onca/xml";
        private const string service = "AWSECommerceService";
        private const string key = "1NAZ6Z7QCKNVY2EP0782";
        private const string operation = "ItemLookup";
        private const string idtype = "ISBN";
        private const string searchtype = "Books";
        private const string size = "Medium";
        private bool moreThanOneAuthor = false;
    }
}
