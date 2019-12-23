using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Marinha.Business;
using Shared.Common;
using Shared.NHibernateDAL;
using Shared.SessionState;

public partial class frmOMFResponsavelPericia : MarinhaPageBase
{
    #region private variables
    
    protected NotaEntregaMaterialOMF _omf
    {
        get { return (NotaEntregaMaterialOMF)Session["frmOMFResponsavelPericia._omf"]; }
        set { Session["frmOMFResponsavelPericia._omf"] = value; }
    }

    
    #endregion
    
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnNovo.Click += new EventHandler(btnNovo_Click);
        
        this.dgResponsavel.CancelCommand += new DataGridCommandEventHandler(dgMaterial_CancelCommand);
        this.dgResponsavel.ItemCommand += new DataGridCommandEventHandler(dgMaterial_ItemCommand);        
		this.dgResponsavel.ItemDataBound += dgMaterial_OnItemDataBound;
		
        this.btnEnviar.Click += new EventHandler(btnEnviar_Click);

        dgResponsavel.EditCommand += new DataGridCommandEventHandler(dgMaterial_EditCommand);
        dgResponsavel.UpdateCommand += new DataGridCommandEventHandler(dgMaterial_UpdateCommand);


    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Anthem.Manager.Register(this);
        if (!this.IsPostBack)
        {
            
            _omf = NotaEntregaMaterialOMF.Get(Convert.ToInt32(Request["id_omf"]));
            

            Bind();
            Anthem.AnthemClientMethods.Redirect("frmOMFPendente.aspx", btnVoltar);
            
            RegisterDeleteScript();
            
            if(_omf.Itens.Count == 0)
                dgResponsavel.ShowFooter = true;
                        
    
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


    #region Responsaveis
    private void Bind()
    {
        dgResponsavel.DataSource = _omf.ResponsaveisPericia;
        dgResponsavel.DataKeyField = "ID";
        dgResponsavel.DataBind();
        dgResponsavel.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();

    }

    void dgMaterial_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Insert")
        {
            try
            {
                
                TextBox txtNome = (TextBox)e.Item.FindControl("txtNome");
                TextBox txtNIP = (TextBox)e.Item.FindControl("txtNIP");
                TextBox txtGraduacao = (TextBox)e.Item.FindControl("txtGraduacao");
                TextBox txtObservacao = (TextBox)e.Item.FindControl("txtObservacao");
                DropDownList ddlTipoNotificacao = (DropDownList)e.Item.FindControl("ddlTipoNotificacao");
                
                NotaEntregaMaterialOMFResponsavelPericia responsavel = new NotaEntregaMaterialOMFResponsavelPericia();
                responsavel.Nome = txtNome.Text;
                responsavel.NIP = txtNIP.Text;
                responsavel.Graduacao = txtGraduacao.Text;
                responsavel.Observacao = txtObservacao.Text;
                responsavel.TipoNotificacao = TipoNotificacao.Get(Convert.ToInt32(ddlTipoNotificacao.SelectedValue));
                responsavel.OMF = _omf;
                responsavel.Save();
                
                _omf.ResponsaveisPericia.Add(responsavel);
            	
                Bind();
                dgResponsavel.ShowFooter = false;
            }
            catch (Exception ex)
            {
                Anthem.AnthemClientMethods.Alert(ex.Message, this);
            }
        }
    }

    void dgMaterial_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        TextBox txtNome = (TextBox)e.Item.FindControl("txtNome");
        TextBox txtNIP = (TextBox)e.Item.FindControl("txtNIP");
        TextBox txtGraduacao = (TextBox)e.Item.FindControl("txtGraduacao");
        TextBox txtObservacao = (TextBox)e.Item.FindControl("txtObservacao");
        DropDownList ddlTipoNotificacao = (DropDownList)e.Item.FindControl("ddlTipoNotificacao");

        NotaEntregaMaterialOMFResponsavelPericia responsavel = _omf.ResponsaveisPericia.Find(Convert.ToInt32(dgResponsavel.DataKeys[e.Item.ItemIndex]));
        responsavel.Nome = txtNome.Text;
        responsavel.NIP = txtNIP.Text;
        responsavel.Graduacao = txtGraduacao.Text;
        responsavel.Observacao = txtObservacao.Text;
        responsavel.TipoNotificacao = TipoNotificacao.Get(Convert.ToInt32(ddlTipoNotificacao.SelectedValue));
        responsavel.Save();

        dgResponsavel.EditItemIndex = -1;
        Bind();
    }

    void dgMaterial_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dgResponsavel.ShowFooter = false;
        dgResponsavel.EditItemIndex = e.Item.ItemIndex;
        Bind();
    }

    private void dgMaterial_OnItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
        {
            DropDownList ddlTipoNotificacao = (DropDownList) e.Item.FindControl("ddlTipoNotificacao");
            Util.FillDropDownList(ddlTipoNotificacao, TipoNotificacao.List(), ESCOLHA_OPCAO);
            
            if(e.Item.ItemType == ListItemType.EditItem)
            {
                NotaEntregaMaterialOMFResponsavelPericia responsavel = (NotaEntregaMaterialOMFResponsavelPericia) e.Item.DataItem;
                ddlTipoNotificacao.SelectedValue = responsavel.TipoNotificacao.ID.ToString();
            }
        }
    }

    void dgMaterial_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dgResponsavel.ShowFooter = false;
        dgResponsavel.EditItemIndex = -1;
        Bind();
    }

	[Anthem.Method]
	public void Excluir(int id)
	{
	    NotaEntregaMaterialOMFResponsavelPericia r = _omf.ResponsaveisPericia.Find(id);
        r.Delete();
	    _omf.ResponsaveisPericia.Remove(r);
		Bind();
	}

    void btnNovo_Click(object sender, EventArgs e)
    {
        dgResponsavel.ShowFooter = true;
        dgResponsavel.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();
    }
    #endregion


    void btnEnviar_Click(object sender, EventArgs e)
    {
        
        _omf.FinalizarPericia(this.ID_Servidor);
        Anthem.AnthemClientMethods.Redirect("frmOMFPendente.aspx");
    }
    

}
