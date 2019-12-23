using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
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

public partial class frmPedidoObtencaoDocumento : MarinhaPageBase
{

    #region Private Member
    [TransientPageState]
    protected PedidoObtencao _po;

    #endregion 

    #region Initialization
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        btnUpload.Click += new EventHandler(btnUpload_Click);
        gvDocumento.RowDeleting += new GridViewDeleteEventHandler(gvDocumento_RowDeleting);
    }

    void gvDocumento_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        _po.Documentos.Find(Convert.ToInt32(e.Keys["ID"])).Delete();
        _po.Documentos.Remove(_po.Documentos.Find(Convert.ToInt32(e.Keys["ID"])));
        Bind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {

            _po = PedidoObtencao.Get(Convert.ToInt32(Request["ID_pedido"]));
            PopulateFields();
            Bind();

            //Anthem.AnthemClientMethods.Redirect("frmPedidoObtencaoDocumentoPesquisa.aspx", btnVoltar);
            RegisterDeleteScript();

        }
    }


    private void PopulateFields()
    {
        lblNumero.Text = _po.CodigoComAno;
        lblAplicacao.Text = _po.Aplicacao;
        lblStatus.Text = _po.DescricaoStatus;

    }
    
    private void Bind()
    {
        gvDocumento.DataSource = _po.Documentos;
        gvDocumento.DataKeyNames = new string[]{"ID"};
        gvDocumento.DataBind();

        gvDocumento.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();
    }
  
    #endregion

    #region Upload 
    
    void btnUpload_Click(object sender, EventArgs e)
    {
        if(FileUpload1.HasFile)
        {
            Stream fs = FileUpload1.PostedFile.InputStream;
            byte[] data = new byte[fs.Length];
            FileUpload1.PostedFile.InputStream.Read(data, 0, data.Length);
            fs.Close();

            PedidoObtencaoDocumento documento = new PedidoObtencaoDocumento(_po);
            documento.Nome = FileUpload1.FileName;
            documento.Documento = data;
            documento.Save();
            _po.Documentos.Add(documento);
            Bind();
        }
    }

    #endregion
    
    [Anthem.Method]
    public void Excluir()
    {
        try
        {
           
            //ShowSuccessMessage();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message);
        }
    }

    private void RegisterDelete()
    {
       // Anthem.Manager.AddScriptAttribute(btnExcluir, "onclick", string.Format("javascript:Excluir({0});", _cliente.ID));
       // btnExcluir.UpdateAfterCallBack = true;
    }
   
}
