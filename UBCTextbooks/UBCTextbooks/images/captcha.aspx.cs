using System;
using System.Drawing;
using System.Drawing.Imaging;
using UBCTextbooksCore.Constants;
using UBCTextbooksCore.Security;

namespace UBCTextbooks.images
{
    public partial class captcha : System.Web.UI.Page
    {
        private void Page_Load(object sender, EventArgs e)
        {
            // Create a CAPTCHA image using the text stored in the Session object.
            Captcha ci = new Captcha(this.Session[USession.CaptchaText].ToString(), 200, 50, "Century Schoolbook");

            // Change the response headers to output a JPEG image.
            this.Response.Clear();
            this.Response.ContentType = "image/jpeg";

            // Write the image to the response stream in JPEG format.
            ci.Image.Save(this.Response.OutputStream, ImageFormat.Jpeg);

            // Dispose of the CAPTCHA image object.
            ci.Dispose();
        }
    }
}