using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
//using System.Web.UI.WebControls;
using ZXing;
using ZXing.QrCode;

namespace QrCodeReader
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string amount = "10000";
                string email = "zephrichards1@gmail.com";
                string product = "Visa";

                //Generate Qr code url
                string qrCodeUrl = $"https://yourpaymentgateway.com/payment?amount={amount}&email={email}&product={product}";

                //Generate Qrcode
                Bitmap qrCodeBitmap = GenerateQRCode(qrCodeUrl);
                qrCodeImage.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(ImageToByte(qrCodeBitmap));
                
            }
        }

        private Bitmap GenerateQRCode(string content)
        {
            BarcodeWriter barcodeWriter = new BarcodeWriter();
            barcodeWriter.Format = BarcodeFormat.QR_CODE;
            barcodeWriter.Options = new QrCodeEncodingOptions 
            { 
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = 300,
                Height = 300,
            };
            Bitmap qrCodeBitnap = barcodeWriter.Write(content);
            return qrCodeBitnap;
        }

        private byte[] ImageToByte(Image img)
        {
            using (var stream = new System.IO.MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}