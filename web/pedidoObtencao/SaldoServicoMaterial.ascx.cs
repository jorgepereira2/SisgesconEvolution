using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Marinha.Business;

public partial class pedidoObtencao_SaldoServicoMaterial : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void AtualizarSaldo(int id_servicoMaterial, int ano)
    {
        DataTable dt = ServicoMaterial.SelectSaldo(id_servicoMaterial, ano);
        
        if(dt.Rows.Count > 0)
        {
            lblSaldoAFaturar.Text = Convert.ToDecimal(dt.Rows[0]["ValorAFaturar"]).ToString("C2");
            lblSaldoFaturado.Text = Convert.ToDecimal(dt.Rows[0]["ValorFaturado"]).ToString("C2");
            lblSaldoTotal.Text = (Convert.ToDecimal(dt.Rows[0]["ValorAFaturar"]) + Convert.ToDecimal(dt.Rows[0]["ValorFaturado"])).ToString("C2");

            lblSaldoTotal.UpdateAfterCallBack = true;
            lblSaldoAFaturar.UpdateAfterCallBack = true;
            lblSaldoFaturado.UpdateAfterCallBack = true;
        }
    }
}