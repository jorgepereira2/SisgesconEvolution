using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Marinha.Business;
using Shared.Common;
using Shared.NHibernateDAL;
using Shared.SessionState;
using Anthem;

public partial class frmEntradaAC : MarinhaPageBase
{
    #region private variables

    [TransientPageState] protected PedidoObtencao _ac;
    
    #endregion
    
    #region Initialization

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnEnviar.Click += new EventHandler(btnEnviar_Click);
    }
  
    protected void Page_Load(object sender, EventArgs e)
    {
        Anthem.Manager.Register(this);

        if (!this.IsPostBack)
        {

            if (Request["id_ac"] != null)
            {
                _ac = PedidoObtencao.Get(Convert.ToInt32(Request["id_ac"]));
            }

            Bind();
            Populate();
            Anthem.AnthemClientMethods.Redirect("frmEntradaACPesquisa.aspx", btnVoltar);
        }
    }
    
    private void Populate()
    {
        lblAC.Text = _ac.CodigoComAno;
        
        //lblPS.Text = _ac.TextoPS;
        lblComprador.Text = _ac.Servidor.Identificacao;
        lblDataEmissao.Text = _ac.DataEmissao.ToShortDateString();

        //repPO.DataSource = _ac.POs;
        //repPO.DataBind();
    }

    //protected void repPO_ItemDataBound(object sender, RepeaterItemEventArgs e)
    //{
    //    if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
    //    {
    //        KeyValuePair<int, string> pair = (KeyValuePair<int, string>)e.Item.DataItem;
    //        LinkButton lnkPO = (LinkButton)e.Item.FindControl("lnkPO");
    //        Anthem.AnthemClientMethods.Popup(lnkPO, "fchPedidoObtencao.aspx?id_pedido=" + pair.Key.ToString(), "po", false, false, false, true, true, true, true, 20, 40, 700, 500, false);
    //    }
    //}

    #endregion
   
    #region Bind

    private void Bind()
    {
		dgItem.DataSource = _ac.GetItensEntradaPendente(); 
        dgItem.DataKeyField = "ID";
        dgItem.DataBind();
        dgItem.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();
    }

    #endregion

    #region Enviar

    void btnEnviar_Click(object sender, EventArgs e)
    {
        //List<int> itens = new List<int>();
        var itens = new Dictionary<int, decimal>();

        foreach (DataGridItem item in dgItem.Items)
        {
            if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
            {
                //CheckBox chkMarcado = (CheckBox)item.FindControl("chkMarcado");
                //if(chkMarcado.Checked)
                //{
                //    int id = Convert.ToInt32(dgItem.DataKeys[item.ItemIndex]);
                //    itens.Add(id);
                //}

                //CheckBox chkMarcado = (CheckBox)item.FindControl("chkMarcado");
                var txtQuantidade = (NumericTextBox)item.FindControl("txtQuantidade");

                if (!string.IsNullOrWhiteSpace(txtQuantidade.Text))
                {
                    int id = Convert.ToInt32(dgItem.DataKeys[item.ItemIndex]);

                    var quantidade = Convert.ToDecimal(txtQuantidade.Text);

                    if (quantidade > 0)
                        itens.Add(id, quantidade);
                }
            }
        }

        _ac.SalvarEntradaItens(ID_Servidor, itens);

        Anthem.AnthemClientMethods.Redirect("frmEntradaACPesquisa.aspx");
    }

    #endregion
}

