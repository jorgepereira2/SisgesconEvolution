using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Marinha.Business;
using Shared.Common;
using Shared.NHibernateDAL;
using Shared.SessionState;

public partial class frmOMFItem : MarinhaPageBase
{
    #region private variables
    
    protected NotaEntregaMaterialOMF _omf
    {
        get { return (NotaEntregaMaterialOMF)Session["frmOMFItem._omf"]; }
        set { Session["frmOMFItem._omf"] = value; }
    }

    
    #endregion
    
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnNovoMaterial.Click += new EventHandler(btnNovo_Click);
        
        this.dgMaterial.CancelCommand += new DataGridCommandEventHandler(dgMaterial_CancelCommand);
        this.dgMaterial.ItemCommand += new DataGridCommandEventHandler(dgMaterial_ItemCommand);        
		this.dgMaterial.ItemDataBound += dgMaterial_OnItemDataBound;
		
        this.btnEnviar.Click += new EventHandler(btnEnviar_Click);

        dgMaterial.EditCommand += new DataGridCommandEventHandler(dgMaterial_EditCommand);
        dgMaterial.UpdateCommand += new DataGridCommandEventHandler(dgMaterial_UpdateCommand);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Anthem.Manager.Register(this);
        if (!this.IsPostBack)
        {
            
            _omf = NotaEntregaMaterialOMF.Get(Convert.ToInt32(Request["id_omf"]));
            

            BindMaterial();
            Anthem.AnthemClientMethods.Redirect("frmOMFPendente.aspx", btnVoltar);
            
            RegisterDeleteScript();
            
            if(_omf.Itens.Count == 0)
                dgMaterial.ShowFooter = true;
                        
    
            Populate();
        }
    }

    private void Populate()
    {
        lblNumeroNota.Text = _omf.NumeroNota;
        lblFornecedor.Text = _omf.Fornecedor.RazaoSocial;
        lblStatus.Text = _omf.Status.Descricao;
    }

    #endregion


    #region Material
    private void BindMaterial()
    {
        dgMaterial.DataSource = _omf.Itens;
        dgMaterial.DataKeyField = "ID";
        dgMaterial.DataBind();
        dgMaterial.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();

        lblValorTotal.Text = string.Format("Valor Total: {0:c2}", _omf.ValorTotal);
        lblValorTotal.UpdateAfterCallBack = true;
    }

    void dgMaterial_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Insert")
        {
            try
            {
                
                BuscaServicoMaterial ucServicoMaterial = (BuscaServicoMaterial)e.Item.FindControl("ucServicoMaterial");
                Anthem.NumericTextBox txtQuantidade = (Anthem.NumericTextBox)e.Item.FindControl("txtQuantidade");
                Anthem.NumericTextBox txtValor = (Anthem.NumericTextBox)e.Item.FindControl("txtValor");
                Anthem.TextBox txtLOC = (Anthem.TextBox)e.Item.FindControl("txtLOC");
                DropDownList ddlTipoTAV = (DropDownList)e.Item.FindControl("ddlTipoTAV");
                
				if(ucServicoMaterial.SelectedValue == "" || ucServicoMaterial.SelectedValue == "0")
				{
				    ShowMessage("Campo Serviço/Material obrigatório");
				    return;
				}

                NotaEntregaMaterialOMFItem item = new NotaEntregaMaterialOMFItem();
                item.Quantidade = Convert.ToDecimal(txtQuantidade.Text);
                item.Material = ServicoMaterial.Get(Convert.ToInt32(ucServicoMaterial.SelectedValue));
                item.Valor = Convert.ToDecimal(txtValor.Text);
                item.LOC = PageReader.ReadString(txtLOC);
                item.NotaEntregaMaterialOMF = _omf;
                item.TipoTAV = (TipoTAV) Convert.ToInt32(ddlTipoTAV.SelectedValue);
                item.Save();
                
                _omf.Itens.Add(item);
            	
                BindMaterial();
                dgMaterial.ShowFooter = false;
            }
            catch (Exception ex)
            {
                Anthem.AnthemClientMethods.Alert(ex.Message, this);
            }
        }
    }

    void dgMaterial_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        BuscaServicoMaterial ucServicoMaterial = (BuscaServicoMaterial)e.Item.FindControl("ucServicoMaterial");
        Anthem.NumericTextBox txtQuantidade = (Anthem.NumericTextBox)e.Item.FindControl("txtQuantidade");
        Anthem.NumericTextBox txtValor = (Anthem.NumericTextBox)e.Item.FindControl("txtValor");
        Anthem.TextBox txtLOC = (Anthem.TextBox)e.Item.FindControl("txtLOC");
        DropDownList ddlTipoTAV = (DropDownList)e.Item.FindControl("ddlTipoTAV");

        if (ucServicoMaterial.SelectedValue == "" || ucServicoMaterial.SelectedValue == "0")
        {
            ShowMessage("Campo Serviço/Material obrigatório");
            return;
        }

        NotaEntregaMaterialOMFItem item = _omf.Itens.Find(Convert.ToInt32(dgMaterial.DataKeys[e.Item.ItemIndex]));
        item.Quantidade = Convert.ToDecimal(txtQuantidade.Text);
        item.Material = ServicoMaterial.Get(Convert.ToInt32(ucServicoMaterial.SelectedValue));
        item.Valor = Convert.ToDecimal(txtValor.Text);
        item.LOC = PageReader.ReadString(txtLOC);
        item.TipoTAV = (TipoTAV)Convert.ToInt32(ddlTipoTAV.SelectedValue);
        item.Save();

        dgMaterial.EditItemIndex = -1;
        BindMaterial();
    }

    void dgMaterial_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dgMaterial.ShowFooter = false;
        dgMaterial.EditItemIndex = e.Item.ItemIndex;
        BindMaterial();
    }

    private void dgMaterial_OnItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
        {
            DropDownList ddlTipoTAV = (DropDownList) e.Item.FindControl("ddlTipoTAV");
            Util.FillDropDownList(ddlTipoTAV, typeof(TipoTAV), ESCOLHA_OPCAO);

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                NotaEntregaMaterialOMFItem item = (NotaEntregaMaterialOMFItem) e.Item.DataItem;
                ddlTipoTAV.SelectedValue = Convert.ToInt32(item.TipoTAV).ToString();
                BuscaServicoMaterial ucServicoMaterial = (BuscaServicoMaterial)e.Item.FindControl("ucServicoMaterial");
                ucServicoMaterial.SelectedValue = item.Material.ID.ToString();
                ucServicoMaterial.Text = item.Material.Descricao;
            }    
        }
        
        
    }

    void dgMaterial_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dgMaterial.ShowFooter = false;
        dgMaterial.EditItemIndex = -1;
        BindMaterial();
    }

	[Anthem.Method]
	public void Excluir(int id)
	{
	    NotaEntregaMaterialOMFItem item = _omf.Itens.Find(id);
        item.Delete();
	    _omf.Itens.Remove(item);
		BindMaterial();
	}

    void btnNovo_Click(object sender, EventArgs e)
    {
        dgMaterial.ShowFooter = true;
        dgMaterial.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();
    }
    #endregion

  
    
    #region Selected Index Changed
    
    protected void ucServicoMaterial_SelectedValueChanged(object sender, BuscaServicoMaterialEventArgs e)
    {
        BuscaServicoMaterial uc = (BuscaServicoMaterial)sender;
        DataGridItem item = (DataGridItem)uc.Parent.Parent;
        

        if(e.ServicoMaterial.TipoServicoMaterial == TipoServicoMaterial.Material)
        {
            Anthem.TextBox txtCodigoMaterial = (Anthem.TextBox)item.FindControl("txtCodigoMaterial");
            txtCodigoMaterial.Text = e.ServicoMaterial.CodigoInterno;
            txtCodigoMaterial.UpdateAfterCallBack = true;
        }
    }
    #endregion

    void btnEnviar_Click(object sender, EventArgs e)
    {
        
        _omf.EmitirTAV(this.ID_Servidor);
        Anthem.AnthemClientMethods.Redirect("frmOMFPendente.aspx");
    }
    
   

    protected void txtCodigoMaterial_TextChanged(object sender, EventArgs e)
    {
        TextBox txtCodigoMaterial = (TextBox) sender;

      
        ServicoMaterial sm = ServicoMaterial.GetByCodigo(txtCodigoMaterial.Text, null);
        if(sm != null)
        {
            DataGridItem item = (DataGridItem)txtCodigoMaterial.Parent.Parent;
            BuscaServicoMaterial ucServicoMaterial = (BuscaServicoMaterial) item.FindControl("ucServicoMaterial");
            ucServicoMaterial.FireEvent(sm.ID.ToString());
        }
    }

}
