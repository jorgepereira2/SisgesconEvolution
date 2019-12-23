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

public partial class Relatorios_ucColumnManager : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        RegisterClientScript();
        this.btnAplicar.Click += new EventHandler(btnAplicar_Click);
        btnExportar.Click += new EventHandler(btnExportar_Click);
        btnMarcarTodos.Click += BtnMarcarTodos_OnClick;
        btnExportarPDF.Click += new EventHandler(btnExportarPDF_Click);
    }

    private void BtnMarcarTodos_OnClick(object sender, EventArgs e)
    {
        gvManager.CheckAll();
        pnManager.UpdateAfterCallBack = true;
    }

    void btnExportarPDF_Click(object sender, EventArgs e)
    {
        Control control = this.Page.FindControl(gvManager.GridViewID);
        if (control != null)
            App_Code.WebUtil.ExportToPDF((WebControl)control, this.Page, true);
    }
    
    void btnExportar_Click(object sender, EventArgs e)
    {
        Control control = this.Page.FindControl(gvManager.GridViewID);
        if(control != null)
            App_Code.WebUtil.ExportToExcel((WebControl)control , this.Page, true);
    }

    public event EventHandler ColumnsChanged;

    void btnAplicar_Click(object sender, EventArgs e)
    {
        gvManager.UpdateGridView();
        
        if(ColumnsChanged != null)
            ColumnsChanged(this, new EventArgs());
    }
    
    public void SetValues()
    {
        gvManager.SetValues();
    }

    private void RegisterClientScript()
    {
        if(Page.ClientScript.IsClientScriptBlockRegistered("ColumnManager"))
            return;
        
        string script =
            @"
        <script language='javascript'>
        function ShowHideContent(ancora)
        {
            var divContent = document.getElementById('divContent');            
            var divLink = document.getElementById('divLink');
            
            if(divContent.style.display == 'none')
            {
                divContent.style.display = '';
                divLink.style.borderBottomWidth = '1px';
                    
                if(document.all)
                    ancora.innerText = 'Ocultar Opções';
                else
                    ancora.textContent = 'Ocultar Opções';
            }
            else
            {
                divContent.style.display = 'none';
                divLink.style.borderBottomWidth = '0px';
                
                if(document.all)
                    ancora.innerText = 'Mostrar Opções';
                else
                    ancora.textContent = 'Mostrar Opções';
            }
        }
        </script>";
        
        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ColumnManager", script);
    }
}
