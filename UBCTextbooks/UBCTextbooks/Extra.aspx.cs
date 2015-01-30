using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using UBCTextbooksCore.Constants;
using UBCTextbooksCore.Custom;
using UBCTextbooksCore;

namespace UBCTextbooks
{
    public partial class Extra : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string listid = Request.Params[QueryString.ListId];
            ListManager list = new ListManager();
            SqlDataReader reader = list.RetrieveExtraInfo(listid);
            string jsonObject = "[{";
            reader.Read();
            jsonObject += "\"ImageUrl\" : " + "\"" + reader[UParameter.ImageUrl] + "\"" + "," +
                                "\"Location\" : " + "\"" + reader[UParameter.Location] + "\"" + "," +
                                "\"Area\" : " + "\"" + reader[UParameter.Area] + "\"" + "," +
                                "\"Price\" : " + "\"" + reader[UParameter.Price] + "\"" + "," +
                                "\"Binding\" : " + "\"" + reader[UParameter.BookBinding] + "\"" + "," +
                                "\"Manufacturer\" : " + "\"" + reader[UParameter.BookManufacturer] + "\"" + "," +
                                "\"Notes\" : " + "\"" + reader[UParameter.Notes] + "\"" + "," +
                                "\"CourseList\" : " + "\"" + reader[UParameter.CourseList] + "\"";
            jsonObject += "}]";
            jsonObject = jsonObject.Replace("\r\n", "<br />");
            Response.Write(jsonObject);
        }
    }
}