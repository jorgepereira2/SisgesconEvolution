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
using Marinha.Business;
using Shared.Common;
using Shared.NHibernateDAL;

public partial class ucDefinicaoFinanceiraRelator : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            //Util.FillDropDownList(ddlNaturezaDespesa, NaturezaDespesa.List(), "-- Escolha uma opção --");
            //Util.FillDropDownList(ddlPTRES, PTRES.List(), "-- Escolha uma opção --");
            Util.FillDropDownList(ddlFonteRecurso, FonteRecurso.List(), "-- Escolha uma opção --");
            //Util.FillDropDownList(ddlComprador, Servidor.List(FuncaoServidor.Comprador));
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        btnCancelar.Click += new EventHandler(btnCancelar_Click);
        btnOk.Click += new EventHandler(btnOk_Click);
    }

    void btnCancelar_Click(object sender, EventArgs e)
    {
        winNaturezaDespesa.Hide();
        if(OperacaoCancelada != null)
            OperacaoCancelada(this, new EventArgs());
    }

    void btnOk_Click(object sender, EventArgs e)
    {
        if (DefinicaoInformada != null)
            DefinicaoInformada(this, new EventArgs());
       
    }
   
    public event EventHandler DefinicaoInformada;
    public event EventHandler OperacaoCancelada;
    
    //public string ID_NaturezaDespesa
    //{
    //    get { return ddlNaturezaDespesa.SelectedValue; }
    //}

    //public string ID_Comprador
    //{
    //    get { return ddlComprador.SelectedValue; }
    //}

    //public string ID_PTRES
    //{
    //    get { return ddlPTRES.SelectedValue; }
    //}

    public string ID_FonteRecurso
    {
        get { return ddlFonteRecurso.SelectedValue; }
    }

    public string Comentario
    {
        get { return txtComentario.Text; }
    }

    private int id_po
    {
        get { return (int)ViewState["id_po"]; }
        set { ViewState["id_po"] = value; } 
    }

    public void Show(PedidoObtencao po)
    {
        //ddlNaturezaDespesa.SelectedValue = ObjectReader.ReadID(po.NaturezaDespesa);
        //ddlPTRES.SelectedValue = ObjectReader.ReadID(po.PTRES);
        ddlFonteRecurso.SelectedValue = ObjectReader.ReadID(po.FonteRecurso);

        try
        {
            //ddlComprador.SelectedValue = ObjectReader.ReadID(po.Comprador);    
        }

        catch{}
        
        id_po = po.ID;
        
        Refresh();

        ucSaldo.Atualizar(po);

        ucItemsPO.BindItens(po);

        winNaturezaDespesa.Show();
    }

    private void Refresh()
    {
        //ddlNaturezaDespesa.UpdateAfterCallBack = true;
        //ddlComprador.UpdateAfterCallBack = true;
        //ddlPTRES.UpdateAfterCallBack = true;
        txtComentario.UpdateAfterCallBack = true;
        ddlFonteRecurso.UpdateAfterCallBack = true;
    }

    public void Close()
    {
        winNaturezaDespesa.Hide();
       
        //ddlNaturezaDespesa.SelectedIndex = -1;
        //ddlComprador.SelectedIndex = 0;
        //ddlPTRES.SelectedIndex = -1;
        ddlFonteRecurso.SelectedIndex = -1;
        txtComentario.Text = "";
       
        Refresh();
    }

    //protected void ddlNaturezaDespesa_Changed(object sender, EventArgs e)
    //{
    //    Util.FillDropDownList(ddlSubNaturezaDespesa, SubNaturezaDespesa.List(Convert.ToInt32(ddlNaturezaDespesa.SelectedValue)), "-- Escolha uma opção --");
    //    ddlSubNaturezaDespesa.UpdateAfterCallBack = true;
    //}
}
