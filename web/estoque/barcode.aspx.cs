using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Drawing.Imaging;

using Shared.BarCode;

public partial class barcode : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["barcode"] != null)
        {
            
            BarCodeWriter bar = new BarCodeWriter((BarCodeType) Convert.ToInt32(Request["barcodetype"]));


            bar.BarHeight = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(Request["Altura"]) * 30));
            //Response.Write(bar.BarHeight.ToString());
            //return;
            bar.BarCodeText = Request["Barcode"];
            bar.BarWidth = 1;
            
            bar.ShowBarCodeText = false;
            
            Bitmap bmp = bar.GetBitMap();
            bmp.SetResolution(300, 300);
            
            Response.ContentType = "image/jpeg";            
            bmp.Save(Response.OutputStream, ImageFormat.Jpeg);
            bmp.Dispose();
        }
    }
}
