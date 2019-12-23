using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.IO;
using System;  

namespace App_Code
{
    public class WebUtil
    {
        public static void ExportToExcel(WebControl dg, Page page, bool clearControls)
        {
            dg.Page.Response.Clear();
            dg.Page.Response.Buffer = true;
            dg.Page.Response.ContentType = "application/vnd.ms-excel";
            dg.Page.Response.AddHeader("content-disposition", "attachment;filename=Relatorio_EXCEL_" + DateTime.Now.ToString() + ".xls");
            dg.Page.Response.Charset = "";
            dg.Page.EnableViewState = false;

            System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);

            if(clearControls)
                ClearControls(dg);

            Panel pn = new Panel();
            pn.Controls.Add(dg);

            pn.RenderControl(oHtmlTextWriter);

            page.Response.Write(oStringWriter.ToString());

            page.Response.End();
        }

        public static void ExportToPDF(WebControl dg, Page page, bool clearControls)
        {
            dg.Page.Response.Clear();
            dg.Page.Response.Buffer = true;
            dg.Page.Response.ContentType = "application/pdf";
            dg.Page.Response.AddHeader("content-disposition", "attachment;filename=Relatorio_PDF_" + DateTime.Now.ToString() + ".pdf");
            dg.Page.Response.Charset = "";
            dg.Page.EnableViewState = false;

            System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);

            if (clearControls)
                ClearControls(dg);

            Panel pn = new Panel();
            pn.Controls.Add(dg);

            pn.RenderControl(oHtmlTextWriter);

            page.Response.Write(oStringWriter.ToString());

            page.Response.End();
        }

        private static void ClearControls(Control control)
        {
            for (int i = control.Controls.Count - 1; i >= 0; i--)
            {
                ClearControls(control.Controls[i]);
            }

            if (!(control is TableCell) && !(control is DataGrid))
            {
                if (control.GetType().GetProperty("SelectedItem") != null)
                {
                    LiteralControl literal = new LiteralControl();
                    control.Parent.Controls.Add(literal);
                    try
                    {
                        literal.Text = (string)control.GetType().GetProperty("SelectedItem").GetValue(control, null);
                    }
                    catch
                    {
                    }
                    control.Parent.Controls.Remove(control);
                }
                else
                    if (control.GetType().GetProperty("Text") != null)
                    {
                        LiteralControl literal = new LiteralControl();
                        control.Parent.Controls.Add(literal);
                        literal.Text = (string)control.GetType().GetProperty("Text").GetValue(control, null);
                        control.Parent.Controls.Remove(control);
                    }
            }
            return;
        }
    }
}