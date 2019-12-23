using System;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Shared.NHibernateDAL;
using Marinha.Business;
using Shared.SessionState;
using Shared.Common;

public partial class frmRotinaCadastro : MarinhaPageBase
{

    #region Private Member
    [TransientPageState]
    protected Rotina _Rotina;

    #endregion 

    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.btnSalvar.Click += new EventHandler(btnSalvar_Click);
        this.btnNovo.Click += new EventHandler(btnNovo_Click);
        this.ddlCategoriaServico.SelectedIndexChanged += new EventHandler(ddlCategoriaServico_SelectedIndexChanged);

        this.btnNovoMaterial.Click += new EventHandler(btnNovoMaterial_Click);
        this.btnNovoServico.Click += new EventHandler(btnNovoServico_Click);
        this.btnNovoDelineamento.Click += new EventHandler(btnNovoDelineamento_Click);

        this.dgMaterial.ItemDataBound += dgMaterial_OnItemDataBound;
        dgMaterial.ItemCommand +=new DataGridCommandEventHandler(dgMaterial_ItemCommand);
        dgServicos.ItemDataBound += dgServicos_OnItemDataBound;
        dgServicos.ItemCommand +=new DataGridCommandEventHandler(dgServicos_ItemCommand);

        this.dgMaterial.CancelCommand += new DataGridCommandEventHandler(dgMaterial_CancelCommand);
        dgDelineamento.CancelCommand += new DataGridCommandEventHandler(dgDelineamento_CancelCommand);
        dgServicos.CancelCommand += new DataGridCommandEventHandler(dgServicos_CancelCommand);

        dgDelineamento.ItemDataBound += new DataGridItemEventHandler(dgDelineamento_ItemDataBound);
        dgDelineamento.ItemCommand += new DataGridCommandEventHandler(dgDelineamento_ItemCommand);

        
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
        	FillPage();
            if (Request["ID_Rotina"] != null)
            {
                _Rotina = Rotina.Get(Convert.ToInt32(Request["ID_Rotina"]));
                PopulateFields();
                RegisterDelete();
                RegisterDeleteScript("ExcluirDelineamento");
                RegisterDeleteScript("ExcluirItemServico");
                RegisterDeleteScript("ExcluirItemMaterial");
            }
            else
            {
                _Rotina = new Rotina();
            }

           
             Anthem.AnthemClientMethods.Redirect("frmRotinaPesquisa.aspx", btnVoltar);
             RegisterDeleteScript();
            
        }
    }

	private void FillPage()
	{
		Util.FillDropDownList(ddlCategoriaServico, CategoriaServico.List(false), ESCOLHA_OPCAO);
	}

    private void PopulateFields()
    {
        txtDescricao.Text = _Rotina.Descricao;
        chkFlagAtivo.Checked = _Rotina.FlagAtivo;
    }

    private void ClearFields()
    {
        txtDescricao.Text = "";
        chkFlagAtivo.Checked = true;
        
        txtDescricao.UpdateAfterCallBack = true;
        chkFlagAtivo.UpdateAfterCallBack = true;

    }

    private void FillObject()
    {
        _Rotina.Descricao = PageReader.ReadString(txtDescricao);
        _Rotina.FlagAtivo = chkFlagAtivo.Checked;

    }

    private void BindMaterial()
    {
        IList<RotinaCategoriaServicoItemOrcamento> list = new List<RotinaCategoriaServicoItemOrcamento>();

        if (_Rotina.CategoriasServico.Where(c => c.CategoriaServico.ID == Convert.ToInt32(ddlCategoriaServico.SelectedValue)).Count() > 0)
            list = _Rotina.CategoriasServico.Where(c => c.CategoriaServico.ID == Convert.ToInt32(ddlCategoriaServico.SelectedValue)).First().ItensOrcamento;

        var materiais = list.Where(i => i.ServicoMaterial.TipoServicoMaterial == TipoServicoMaterial.Material);
        dgMaterial.DataSource = materiais;
        dgMaterial.DataKeyField = "ID";
        dgMaterial.DataBind();
        dgMaterial.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();

        var total = materiais.Sum(x => x.ServicoMaterial.PrecoEstimadoVenda * x.Quantidade);
        lblValorTotalMaterial.Text = string.Format("Valor Total: {0:c2}", total);
        lblValorTotalMaterial.UpdateAfterCallBack = true;

        UpdateTotal();
    }

    private void BindServico()
    {
        IList<RotinaCategoriaServicoItemOrcamento> list = new List<RotinaCategoriaServicoItemOrcamento>();

        if (_Rotina.CategoriasServico.Where(c => c.CategoriaServico.ID == Convert.ToInt32(ddlCategoriaServico.SelectedValue)).Count() > 0)
            list = _Rotina.CategoriasServico.Where(c => c.CategoriaServico.ID == Convert.ToInt32(ddlCategoriaServico.SelectedValue)).First().ItensOrcamento;

        var servicos = list.Where(i => i.ServicoMaterial.TipoServicoMaterial == TipoServicoMaterial.Servico);

        dgServicos.DataSource = servicos;
        dgServicos.DataKeyField = "ID";
        dgServicos.DataBind();
        dgServicos.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();

        var total = servicos.Sum(x => x.ServicoMaterial.PrecoEstimadoVenda * x.Quantidade);
        lblValorTotalServico.Text = string.Format("Valor Total: {0:c2}", total);
        lblValorTotalServico.UpdateAfterCallBack = true;

        UpdateTotal();
    }

    
    #endregion

    #region Events 
    void btnSalvar_Click(object sender, EventArgs e)
    {
        FillObject();
        _Rotina.Save();
        ShowSuccessMessage();
        RegisterDelete();
    }
	
    void btnNovo_Click(object sender, EventArgs e)
    {
        ClearFields();
        _Rotina = new Rotina();
    }
    #endregion


    #region Material

    private void dgMaterial_OnItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Footer)
        {
            DropDownList ddlOrigemMaterial = (DropDownList)e.Item.FindControl("ddlOrigemMaterial");
            Util.FillDropDownList(ddlOrigemMaterial, typeof(OrigemMaterial));

            DropDownList ddlCelula = (DropDownList)e.Item.FindControl("ddlCelula");
            Util.FillDropDownList(ddlCelula, Celula.List(null, true), ESCOLHA_OPCAO);
         


        }
        else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            //PedidoServicoItemOrcamento orcamento = (PedidoServicoItemOrcamento)e.Item.DataItem;
            
        }
    }

    void dgMaterial_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Insert")
        {
            try
            {
                BuscaServicoMaterial ucServicoMaterial = (BuscaServicoMaterial)e.Item.FindControl("ucServicoMaterialNovo");
                Anthem.NumericTextBox txtQuantidade = (Anthem.NumericTextBox)e.Item.FindControl("txtQuantidadeNovo");
                DropDownList ddlCelula = (DropDownList)e.Item.FindControl("ddlCelula");
                DropDownList ddlOrigemMaterial = (DropDownList)e.Item.FindControl("ddlOrigemMaterial");

                if (ucServicoMaterial.SelectedValue == "" || ucServicoMaterial.SelectedValue == "0")
                {
                    ShowMessage("Campo Material obrigatório");
                    return;
                }

                RotinaCategoriaServico rotinaCategoriaServico = GetRotinaCategoriaServico();

                RotinaCategoriaServicoItemOrcamento item = new RotinaCategoriaServicoItemOrcamento();
                item.RotinaCategoriaServico = rotinaCategoriaServico;
                item.OrigemMaterial = (OrigemMaterial) Convert.ToInt32(ddlOrigemMaterial.SelectedValue);
                item.Quantidade = Convert.ToDecimal(txtQuantidade.Text);
                item.ServicoMaterial = ServicoMaterial.Get(Convert.ToInt32(ucServicoMaterial.SelectedValue));
                item.Celula = Celula.Get(Convert.ToInt32(ddlCelula.SelectedValue));
                //item.Fornecedor = Fornecedor.Get(Convert.ToInt32(ucDadosComplementares.ID_Fornecedor));

                //item.Celula = _delineamentoOficina.Oficina;
                //item.ServidorDelineamento = _delineamentoOficina.Servidor;
                item.Save();
                rotinaCategoriaServico.ItensOrcamento.Add(item);


                BindMaterial();
                dgMaterial.ShowFooter = false;
            }
            catch (Exception ex)
            {
                Anthem.AnthemClientMethods.Alert(ex.Message, this);
            }
        }
        //else if (e.CommandName == "DadosComplementares")
        //{
        //    if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.EditItem)
        //    {
        //        int id = Convert.ToInt32(dgServicos.DataKeys[e.Item.ItemIndex]);
        //        //PedidoServicoItemOrcamento item = _delineamentoOrcamento.ItensOrcamento.Find(id);
        //        //ucDadosComplementares.Show();
        //        //ucDadosComplementares.Fill(item);
        //        //ucDadosComplementares.ID_Item = item.ID;
        //    }
        //    else
        //    {
        //        //ucDadosComplementares.ID_Item = Int32.MinValue;
        //        //ucDadosComplementares.Show();
        //    }


        //}
    }

    [Anthem.Method]
    public void ExcluirItemMaterial(int id)
    {
        RotinaCategoriaServico rotinaCategoriaServico = GetRotinaCategoriaServico();
        rotinaCategoriaServico.ItensOrcamento.Find(id).Delete();
        rotinaCategoriaServico.ItensOrcamento.Remove(rotinaCategoriaServico.ItensOrcamento.Find(id));
        BindMaterial();
    }

    void dgMaterial_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dgMaterial.ShowFooter = false;
        dgMaterial.EditItemIndex = -1;
        BindMaterial();
    }
    #endregion

    #region Servico

    void dgServicos_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Insert")
        {
            try
            {
                BuscaServicoMaterial ucServicoMaterial = (BuscaServicoMaterial)e.Item.FindControl("ucServicoMaterialNovo");
                Anthem.NumericTextBox txtQuantidade = (Anthem.NumericTextBox)e.Item.FindControl("txtQuantidadeNovo");
                DropDownList ddlCelula = (DropDownList)e.Item.FindControl("ddlCelula");

                if (ucServicoMaterial.SelectedValue == "" || ucServicoMaterial.SelectedValue == "0")
                {
                    ShowMessage("Campo Serviço obrigatório");
                    return;
                }

                RotinaCategoriaServico rotinaCategoriaServico = GetRotinaCategoriaServico();

                RotinaCategoriaServicoItemOrcamento item = new RotinaCategoriaServicoItemOrcamento();
                item.RotinaCategoriaServico = rotinaCategoriaServico;
                item.OrigemMaterial = OrigemMaterial.Obtencao;
                item.Quantidade = Convert.ToDecimal(txtQuantidade.Text);
                item.ServicoMaterial = ServicoMaterial.Get(Convert.ToInt32(ucServicoMaterial.SelectedValue));
                item.Celula = Celula.Get(Convert.ToInt32(ddlCelula.SelectedValue));
                //item.Fornecedor = Fornecedor.Get(Convert.ToInt32(ucDadosComplementares.ID_Fornecedor));
                
                //item.Celula = _delineamentoOficina.Oficina;
                //item.ServidorDelineamento = _delineamentoOficina.Servidor;
                item.Save();
                rotinaCategoriaServico.ItensOrcamento.Add(item);
                

                BindServico();
                dgServicos.ShowFooter = false;
            }
            catch (Exception ex)
            {
                Anthem.AnthemClientMethods.Alert(ex.Message, this);
            }
        }
        //else if (e.CommandName == "DadosComplementares")
        //{
        //    if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.EditItem)
        //    {
        //        int id = Convert.ToInt32(dgServicos.DataKeys[e.Item.ItemIndex]);
        //        //PedidoServicoItemOrcamento item = _delineamentoOrcamento.ItensOrcamento.Find(id);
        //        //ucDadosComplementares.Show();
        //        //ucDadosComplementares.Fill(item);
        //        //ucDadosComplementares.ID_Item = item.ID;
        //    }
        //    else
        //    {
        //        //ucDadosComplementares.ID_Item = Int32.MinValue;
        //        //ucDadosComplementares.Show();
        //    }


        //}
    }



    private void dgServicos_OnItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Footer)
        {
            DropDownList ddlCelula = (DropDownList)e.Item.FindControl("ddlCelula");
            Util.FillDropDownList(ddlCelula, Celula.List(null, true), ESCOLHA_OPCAO);
        }
        else if (e.Item.ItemType == ListItemType.EditItem)
        {
            //e.Item.Cells[1].Attributes.Add("colspan", "4");
            //e.Item.Cells[2].Visible = false;
            //e.Item.Cells[3].Visible = false;
            //e.Item.Cells[4].Visible = false;
        }
        else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            
        }
    }

    void dgServicos_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dgServicos.ShowFooter = false;
        dgServicos.EditItemIndex = -1;
        BindServico();
    }

    [Anthem.Method]
    public void ExcluirItemServico(int id)
    {
        RotinaCategoriaServico rotinaCategoriaServico = GetRotinaCategoriaServico();
        rotinaCategoriaServico.ItensOrcamento.Find(id).Delete();
        rotinaCategoriaServico.ItensOrcamento.Remove(rotinaCategoriaServico.ItensOrcamento.Find(id));
        BindServico();
    }
    #endregion

    #region Delineamento

    private void BindDelineamento()
    {
        IList<RotinaCategoriaServicoDelineamento> list = new List<RotinaCategoriaServicoDelineamento>();

        if (_Rotina.CategoriasServico.Where(c => c.CategoriaServico.ID == Convert.ToInt32(ddlCategoriaServico.SelectedValue)).Count() > 0)
            list = _Rotina.CategoriasServico.Where(c => c.CategoriaServico.ID == Convert.ToInt32(ddlCategoriaServico.SelectedValue)).First().ItensDelineamento;
        dgDelineamento.DataSource = list;
        dgDelineamento.DataKeyField = "ID";
        dgDelineamento.DataBind();
        dgDelineamento.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();

        // lblHomemHoraTotal.Text = string.Format("HH Total: {0}", _delineamentoOrcamento.HomemHoraTotal);
        // lblHomemHoraTotal.UpdateAfterCallBack = true;
        
    }

    void dgDelineamento_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Footer)
        {
            DropDownList ddlCelula = (DropDownList)e.Item.FindControl("ddlCelulaNovo");
            Util.FillDropDownList(ddlCelula, Celula.List(null, true), ESCOLHA_OPCAO);
        }
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            //PedidoServicoDelineamento delineamento = (PedidoServicoDelineamento)e.Item.DataItem;
            //if (delineamento.ServidorDelineamento.ID != this.ID_Servidor)
            //{
            //    e.Item.Cells[e.Item.Cells.Count - 1].Visible = false;
            //}
        }
    }

    void dgDelineamento_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Insert")
        {
            try
            {
                DropDownList ddlCelula = (DropDownList)e.Item.FindControl("ddlCelulaNovo");
                Anthem.NumericTextBox txtHomemHora = (Anthem.NumericTextBox)e.Item.FindControl("txtHomemHoraNovo");
                TextBox txtDescricaoServico = (TextBox)e.Item.FindControl("txtDescricaoServicoNovo");

                RotinaCategoriaServicoDelineamento delineamento = new RotinaCategoriaServicoDelineamento();
                delineamento.RotinaCategoriaServico = GetRotinaCategoriaServico();
                delineamento.Celula = Celula.Get(Convert.ToInt32(ddlCelula.SelectedValue));
                delineamento.HomemHora = Convert.ToInt32(txtHomemHora.Text);
                delineamento.DescricaoServicoOficina = PageReader.ReadString(txtDescricaoServico);
                delineamento.Save();
                delineamento.RotinaCategoriaServico.ItensDelineamento.Add(delineamento);
                
                BindDelineamento();
                dgDelineamento.ShowFooter = false;
            }
            catch (Exception ex)
            {
                Anthem.AnthemClientMethods.Alert(ex.Message, this);
            }
        }
    }

    [Anthem.Method]
    public void ExcluirDelineamento(int id)
    {
        RotinaCategoriaServico rotinaCategoriaServico = GetRotinaCategoriaServico();
        rotinaCategoriaServico.ItensDelineamento.Find(id).Delete();
        rotinaCategoriaServico.ItensDelineamento.Remove(rotinaCategoriaServico.ItensDelineamento.Find(id));
        BindDelineamento();
    }

    void dgDelineamento_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dgDelineamento.EditItemIndex = -1;
        dgDelineamento.ShowFooter = false;
        BindDelineamento();
    }

    #endregion

    [Anthem.Method]
    public void Excluir()
    {
        try
        {
            _Rotina.Delete();
            ShowSuccessMessage();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message);
        }
    }

    private void RegisterDelete()
    {
        Anthem.Manager.AddScriptAttribute(btnExcluir, "onclick", string.Format("javascript:Excluir({0});", _Rotina.ID));
        btnExcluir.UpdateAfterCallBack = true;
    }

    void ddlCategoriaServico_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindMaterial();
        BindServico();
        BindDelineamento();

    }


    protected void ucServicoMaterial_SelectedValueChanged(object sender, BuscaServicoMaterialEventArgs e)
    {
        BuscaServicoMaterial uc = (BuscaServicoMaterial)sender;
        DataGridItem item = (DataGridItem)uc.Parent.Parent;
        


        if (e.ServicoMaterial.TipoServicoMaterial == TipoServicoMaterial.Servico)
        {
            Anthem.TextBox txtCodigoMaterial = (Anthem.TextBox)item.FindControl("txtCodigoServico");
            txtCodigoMaterial.Text = e.ServicoMaterial.CodigoInterno;
            txtCodigoMaterial.UpdateAfterCallBack = true;
        }
        else if (e.ServicoMaterial.TipoServicoMaterial == TipoServicoMaterial.Material)
        {
            Anthem.DropDownList ddlOrigemMaterial = (Anthem.DropDownList)item.FindControl("ddlOrigemMaterial");
            ddlOrigemMaterial.SelectedValue = Convert.ToInt32(e.ServicoMaterial.OrigemMaterial).ToString();
            ddlOrigemMaterial.Enabled = true;
            ddlOrigemMaterial.UpdateAfterCallBack = true;
            Anthem.TextBox txtCodigoMaterial = (Anthem.TextBox)item.FindControl("txtCodigoMaterial");
            txtCodigoMaterial.Text = e.ServicoMaterial.CodigoInterno;
            txtCodigoMaterial.UpdateAfterCallBack = true;
        }
    }

    protected void txtCodigoMaterial_TextChanged(object sender, EventArgs e)
    {
        TextBox txtCodigoMaterial = (TextBox)sender;

        TipoServicoMaterial tipo;
        if (txtCodigoMaterial.ID == "txtCodigoMaterial")
            tipo = TipoServicoMaterial.Material;
        else
            tipo = TipoServicoMaterial.Servico;

        ServicoMaterial sm = ServicoMaterial.GetByCodigo(txtCodigoMaterial.Text, tipo);
        if (sm != null)
        {
            DataGridItem item = (DataGridItem)txtCodigoMaterial.Parent.Parent;
            BuscaServicoMaterial ucServicoMaterial = (BuscaServicoMaterial)item.FindControl("ucServicoMaterialNovo");
            ucServicoMaterial.FireEvent(sm.ID.ToString());
        }
    }

    void btnNovoMaterial_Click(object sender, EventArgs e)
    {
        dgMaterial.ShowFooter = true;
        dgMaterial.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();
    }

    void btnNovoDelineamento_Click(object sender, EventArgs e)
    {
        dgDelineamento.ShowFooter = true;
        //Bind();
        dgDelineamento.UpdateAfterCallBack = true;
    }

    void btnNovoServico_Click(object sender, EventArgs e)
    {
        dgServicos.ShowFooter = true;
        dgServicos.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();
    }

    private RotinaCategoriaServico GetRotinaCategoriaServico()
    {
        RotinaCategoriaServico rotinaCategoriaServico = _Rotina.CategoriasServico.Where(c => c.CategoriaServico.ID == Convert.ToInt32(ddlCategoriaServico.SelectedValue)).FirstOrDefault();
        if (rotinaCategoriaServico == null)
        {
            rotinaCategoriaServico = new RotinaCategoriaServico();
            rotinaCategoriaServico.CategoriaServico = CategoriaServico.Get(Convert.ToInt32(ddlCategoriaServico.SelectedValue));
            rotinaCategoriaServico.Rotina = _Rotina;
            rotinaCategoriaServico.Save();
            _Rotina.CategoriasServico.Add(rotinaCategoriaServico);
        }
        return rotinaCategoriaServico;
    }

    private void UpdateTotal()
    {
        decimal total = 0;
        if (_Rotina.CategoriasServico.Where(c => c.CategoriaServico.ID == Convert.ToInt32(ddlCategoriaServico.SelectedValue)).Count() > 0)
        {
            total = _Rotina.CategoriasServico.Where(c => c.CategoriaServico.ID == Convert.ToInt32(ddlCategoriaServico.SelectedValue)).First().ItensOrcamento.Sum(x => x.ServicoMaterial.PrecoEstimadoVenda * x.Quantidade);    
        }
        
        lblTotal.Text = total.ToString("C2");
        lblTotal.AutoUpdateAfterCallBack = true;
    }
   
}
