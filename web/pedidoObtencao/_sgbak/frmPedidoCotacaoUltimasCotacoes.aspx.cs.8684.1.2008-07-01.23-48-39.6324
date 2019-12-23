using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Marinha.Business;
using Shared.Common;
using Shared.NHibernateDAL;
using Shared.SessionState;

public partial class frmPedidoCotacaoUltimasCotacoes : MarinhaPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            ServicoMaterial material = ServicoMaterial.Get(Convert.ToInt32(Request["id_servicoMaterial"]));
            lblServicoMaterial.Text = material.Descricao;

            dgItem.DataSource = PedidoCotacaoItem.GetUltimasCompras(material.ID, 4);
            dgItem.DataBind();
        }
    }

}
