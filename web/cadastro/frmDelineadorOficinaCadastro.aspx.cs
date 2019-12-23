using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Marinha.Business;
using Shared.Common;
using Shared.SessionState;
using System.Linq;

public partial class frmDelineadorOficinaCadastro : SortingPageBase
{

    [TransientPageState]
    protected Celula _celula;
    
    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        dgServidores.DeleteCommand += new DataGridCommandEventHandler(dgServidores_DeleteCommand);
        ucMessageBox.MessageBoxClose +=new UserControls_MessageBox.MessageBoxEventHandler(ucMessageBox_MessageBoxClose);
        dlOficinas.ItemCommand += new DataListCommandEventHandler(dlOficinas_ItemCommand);
        btnAdicionar.Click += new EventHandler(btnAdicionar_Click);
    }

    void btnAdicionar_Click(object sender, EventArgs e)
    {
        Servidor servidor = Servidor.Get(Convert.ToInt32(ddlServidor.SelectedValue));
        _celula.Delineadores.Add(servidor);
        _celula.Save();
        Bind();
    }

    

    void dgServidores_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        ucMessageBox.Show("Deseja excluir este registro?", dgServidores.DataKeys[e.Item.ItemIndex]);
    }

    void ucMessageBox_MessageBoxClose(object sender, MessageBoxEventArgs e)
    {
        if(e.Result == MessageBoxResult.Sim)
        {
            _celula.Delineadores.Remove(_celula.Delineadores.Where(s => s.ID == Convert.ToInt32(e.Data)).FirstOrDefault());
            _celula.Save();
            Bind();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {

            dlOficinas.DataSource = Celula.Select().Where(c => c.FlagOficina);
            dlOficinas.DataKeyField = "ID";
            dlOficinas.DataBind();

            Util.FillDropDownList(ddlServidor, Servidor.List(null), ESCOLHA_OPCAO);
			//RegisterDeleteScript();
        }
    }

    #endregion

    #region Bind
    protected override void Bind()
    {
		dgServidores.DataSource = _celula.Delineadores;
        dgServidores.DataKeyField = "ID";
        dgServidores.DataBind();
        dgServidores.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();
    }

    #endregion

    #region Methods

    void dlOficinas_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if(e.CommandName == "Select")
        {
            _celula = Celula.Get(Convert.ToInt32(dlOficinas.DataKeys[e.Item.ItemIndex]));
            Bind();
        }
    }

  

    //[Anthem.Method]
    //public void Excluir(int id)
    //{
    //    Celula celula = Celula.Get(id);
    //    celula.Delete();
    //    Bind();
    //}
    #endregion
}
