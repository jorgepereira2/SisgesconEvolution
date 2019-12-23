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

public partial class NotaEmpenho : System.Web.UI.UserControl
{
    private PedidoObtencao _po
    {
        get { return (PedidoObtencao)Session["NotaEmpenho_po"]; }
        set { Session["NotaEmpenho_po"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            Util.FillDropDownList(ddlProjeto, Projeto.List(), "-- Escolha uma opção --");
            //Util.FillDropDownList(ddlNaturezaDespesa, NaturezaDespesa.List(), "-- Escolha uma opção --");
            //Util.FillDropDownList(ddlPTRES, PTRES.List(), "-- Escolha uma opção --");
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        btnCancelar.Click += new EventHandler(btnCancelar_Click);
        btnFinalizar.Click += new EventHandler(btnFinalizar_Click);
        dgItem.ItemDataBound += new DataGridItemEventHandler(dgItem_ItemDataBound);
        btnAdicionar.Click += new EventHandler(btnAdicionar_Click);

        dgEmpenho.DeleteCommand += new DataGridCommandEventHandler(dgEmpenho_DeleteCommand);
    }

    void dgItem_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            pedidoObtencao_SaldoServicoMaterial uc = (pedidoObtencao_SaldoServicoMaterial)e.Item.FindControl("ucSaldo");
            PedidoObtencaoItem item = (PedidoObtencaoItem)e.Item.DataItem;
            uc.AtualizarSaldo(item.ServicoMaterial.ID, anoPO);
        }
    }

    void btnCancelar_Click(object sender, EventArgs e)
    {
        winNotaEmpenho.Hide();
        if(OperacaoCancelada != null)
            OperacaoCancelada(this, new EventArgs());
    }

    void btnFinalizar_Click(object sender, EventArgs e)
    {
        if (NotaInformada != null)
            NotaInformada(this, new EventArgs());
       
    }
   
    public event EventHandler NotaInformada;
    public event EventHandler OperacaoCancelada;

    public string NumeroNotaEmpenho
    {
        get { return txtNotaEmpenho.Text; }
    }

    public string CodigoGestao
    {
        get { return txtCodigoGestao.Text; }
    }
    //public string NumeroLancamento
    //{
    //    get { return txtNumeroLancamento.Text; }
    //}
    public string Lista
    {
        get { return txtLista.Text; }
    }
    public string ID_Projeto
    {
        get { return ddlProjeto.SelectedValue; }
    }
    //public string ID_NaturezaDespesa
    //{
    //    get { return ddlNaturezaDespesa.SelectedValue; }
    //}
    public string ID_PTRES
    {
        //get { return ddlPTRES.SelectedValue; }
        get { return txtPTRES.Text; }
    }
    public string Comentario
    {
        get { return txtComentario.Text; }
    }

    //private int id_po
    //{
    //    get { return (int)(ViewState["id_po"] ?? 0); }
    //    set { ViewState["id_po"] = value; }
    //}

    private int anoPO;

    public void Show(PedidoObtencao po)
    {
        _po = po;

        //ddlProjeto.SelectedValue = ObjectReader.ReadID(po.Projeto);
        ////ddlNaturezaDespesa.SelectedValue = ObjectReader.ReadID(po.NaturezaDespesa);
        //ddlPTRES.SelectedValue = ObjectReader.ReadID(po.PTRES);
        //txtCodigoGestao.Text = po.CodigoGestao;
        //txtLista.Text = po.Lista;
        //txtNumeroLancamento.Text = po.NumeroLancamento;

        txtPTRES.Text = po.PTRESS;

        //id_po = po.ID;
        AtualizaSaldo();

        anoPO = po.DataEmissao.Year;
        dgItem.ShowFooter = false;
        dgItem.DataSource = po.Itens;
        dgItem.DataKeyField = "ID";
        dgItem.DataBind();
        dgItem.UpdateAfterCallBack = true;

        BindEmpenho();

        Refresh();

        winNotaEmpenho.Show();
    }

    protected void AtualizaSaldo(object sender, EventArgs e)
    {
        AtualizaSaldo();
    }

    private void AtualizaSaldo()
    {
       // if (_po == null) return;
       //// ac.NaturezaDespesa = NaturezaDespesa.Get(Convert.ToInt32(ddlNaturezaDespesa.SelectedValue));
       // _po.PTRES = PTRES.Get(Convert.ToInt32(ddlPTRES.SelectedValue));
       // _po.Projeto = Projeto.Get(Convert.ToInt32(ddlProjeto.SelectedValue));

       // DataSet ds = EntradaValores.SelectSaldo(_po.ChaveFinanceiro);
       // if (ds.Tables[0].Rows.Count > 0)
       // {
       //     DataRow row = ds.Tables[0].Rows[0];
       //     lblComprometido.Text = Convert.ToDecimal(row["ValorComprometido"]).ToString("c");
       //     lblSaldo.Text = (Convert.ToDecimal(row["ValorEntrada"]) - Convert.ToDecimal(row["ValorEmpenhado"])).ToString("c");
       //     lblSaldoTotal.Text = (Convert.ToDecimal(row["ValorEntrada"]) - Convert.ToDecimal(row["ValorComprometido"]) - Convert.ToDecimal(row["ValorEmpenhado"]) - _po.ValorTotal).ToString("c");
       //     lblCusto.Text = _po.ValorTotal.ToString("c");

       //     lblCusto.UpdateAfterCallBack = lblSaldoTotal.UpdateAfterCallBack = lblSaldo.UpdateAfterCallBack = lblComprometido.UpdateAfterCallBack = true;
       // }
    }

    private void Refresh()
    {
        ddlProjeto.UpdateAfterCallBack = true;
        txtPTRES.UpdateAfterCallBack = true;
        //ddlPTRES.UpdateAfterCallBack = true;
        //ddlNaturezaDespesa.UpdateAfterCallBack = true;
        txtCodigoGestao.UpdateAfterCallBack = true;
        txtLista.UpdateAfterCallBack = true;
        //txtNumeroLancamento.UpdateAfterCallBack = true;
    }

    public void Close()
    {
        winNotaEmpenho.Hide();
        txtNotaEmpenho.Text = "";

        ddlProjeto.SelectedIndex = -1;
        txtPTRES.Text = "";
        //ddlPTRES.SelectedIndex = -1;
        //ddlNaturezaDespesa.SelectedIndex = -1;
        txtCodigoGestao.Text = "";
        txtLista.Text = "";
        //txtNumeroLancamento.Text = "";

        Refresh();
            
        txtNotaEmpenho.UpdateAfterCallBack = true;
    }

    #region Empenho

    void BindEmpenho()
    {
        dgEmpenho.DataSource = _po.Empenhos;
        dgEmpenho.DataKeyField = "ID";
        dgEmpenho.DataBind();
        dgEmpenho.UpdateAfterCallBack = true;
    }

    void btnAdicionar_Click(object sender, EventArgs e)
    {
        PedidoObtencaoEmpenho empenho = new PedidoObtencaoEmpenho();
        empenho.PedidoObtencao = _po;
        empenho.Projeto = Projeto.Get(Convert.ToInt32(ddlProjeto.SelectedValue));
        //empenho.PTRES = PTRES.Get(Convert.ToInt32(ddlPTRES.SelectedValue));
        empenho.PTRESS = txtPTRES.Text;
        empenho.Lista = txtLista.Text;
        empenho.NumeroEmpenho = txtNotaEmpenho.Text;
        //empenho.NumeroLancamento = txtNumeroLancamento.Text;
        empenho.CodigoGestao = txtCodigoGestao.Text;
        empenho.Comentario = txtComentario.Text;

        empenho.Save();
        _po.Empenhos.Add(empenho);

        BindEmpenho();
    }

    void dgEmpenho_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        int id = Convert.ToInt32(dgEmpenho.DataKeys[e.Item.ItemIndex]);

        PedidoObtencaoEmpenho empenho = _po.Empenhos.Find(id);

        empenho.Delete();

        _po.Empenhos.Remove(empenho);

        BindEmpenho();
    }

    #endregion
}
