using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


using Marinha.Business;
using Shared.NHibernateDAL;


public partial class fchFaturamentoPSAS : MarinhaPageBase
{
    protected PedidoServicoAtividadeSecundaria _pedido;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        dgItem.ItemDataBound += new DataGridItemEventHandler(dgItem_ItemDataBound);
    }

    
    
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.IsPostBack)
		{
		    if(Request["id_pedido"] != null)
                _pedido = PedidoServicoAtividadeSecundaria.Get(Convert.ToInt32(Request["id_pedido"]));
            
            
		   
		    List<PedidoServicoAtividadeSecundariaItem> list = _pedido.Itens.ToList();
            
            //Total HH
            PedidoServicoAtividadeSecundariaItem hh = new PedidoServicoAtividadeSecundariaItem();
		    hh.DescricaoServico = "MÃO DE OBRA (HH) = " + _pedido.QuantidadeHH;
		    hh.Valor = _pedido.TotalFRE170;
            list.Add(hh);

            if (_pedido.Itens.Count > 0 && !_pedido.Itens[0].TipoAtividadeSecundaria.SemTaxas)
            {
                //TOMO
                PedidoServicoAtividadeSecundariaItem tomo = new PedidoServicoAtividadeSecundariaItem();
                tomo.DescricaoServico = string.Format("TOMO ({0}%)", _pedido.TaxaOperacionalMaoObra.ToString("N0"));
                tomo.Valor = _pedido.TOMO;
                list.Add(tomo);

                //TOMS
                PedidoServicoAtividadeSecundariaItem toms = new PedidoServicoAtividadeSecundariaItem();
                toms.DescricaoServico = string.Format("TOMS ({0}%)",
                                                      _pedido.TaxaOperacionalMaterialServico.ToString("N0"));
                toms.Valor = _pedido.TOMS;
                list.Add(toms);

                //TCO
                PedidoServicoAtividadeSecundariaItem tco = new PedidoServicoAtividadeSecundariaItem();
                tco.DescricaoServico = string.Format("TCO ({0}%)", _pedido.TaxaContribuicaoOperacional.ToString("N0"));
                tco.Valor = _pedido.TCO;
                list.Add(tco);
            }

		    //Desconto HH
            PedidoServicoAtividadeSecundariaItem descontoHH = new PedidoServicoAtividadeSecundariaItem();
            descontoHH.DescricaoServico = "Descontos concedidos";
            descontoHH.Valor = _pedido.DescontoConcedidoFRE170;
            list.Add(descontoHH);

            //Total pagar 170
            PedidoServicoAtividadeSecundariaItem total170 = new PedidoServicoAtividadeSecundariaItem();
            total170.DescricaoServico = "Total a Pagar (HH)";
            total170.Valor = _pedido.TotalPagarFRE170;
            list.Add(total170);

            //Total pagar 172
            PedidoServicoAtividadeSecundariaItem total172 = new PedidoServicoAtividadeSecundariaItem();
            total172.DescricaoServico = "Total a Pagar";
            total172.Valor = _pedido.TotalPagarFRE171172;
            list.Add(total172);

            //Total Geral
            PedidoServicoAtividadeSecundariaItem totalGeral = new PedidoServicoAtividadeSecundariaItem();
            totalGeral.DescricaoServico = "TOTAL GERAL";
            totalGeral.Valor = _pedido.TotalGeral;
            list.Add(totalGeral);


            dgItem.DataSource = list;

			Page.DataBind();

            lnkExportar.HRef = "fchFaturamentoPSAS.aspx?id_pedido=" + Request["id_pedido"] + "&word=true";
		}

        if (Request["word"] != null)
        {
            divExportar.Visible = false;

            Response.Clear(); //this clears the Response of any headers or previous output
            Response.Buffer = true; //make sure that the entire output is rendered simultaneously

            Response.ContentEncoding = System.Text.Encoding.UTF7;
            ///
            ///Set content type to MS Excel sheet
            ///Use "application/msword" for MS Word doc files
            ///"application/pdf" for PDF files
            ///

            Response.ContentType = "application/msword";
            StringWriter stringWriter = new StringWriter(); //System.IO namespace should be used

            HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);

            ///
            ///Render the entire Page control in the HtmlTextWriter object
            ///We can render individual controls also, like a DataGrid to be
            ///exported in custom format (excel, word etc)
            ///
            this.RenderControl(htmlTextWriter);
            Response.Write(stringWriter.ToString());
            Response.End();
        }
	}



    void dgItem_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
           if(e.Item.Cells[0].Text.StartsWith("MÃO DE OBRA (HH) ="))
           {
               e.Item.Cells[1].Text = "170";
           }
           else if (e.Item.Cells[0].Text.StartsWith("Descontos concedidos"))
           {
               e.Item.Cells[1].Text = "170";
           }
           else if (e.Item.Cells[0].Text.StartsWith("Total a Pagar (HH)"))
           {
               e.Item.Cells[1].Text = "170";
           }
           else if (e.Item.Cells[0].Text.StartsWith("TOMO"))
           {
               e.Item.Cells[1].Text = "170";
           }
           else if (e.Item.Cells[0].Text.StartsWith("TOMS"))
           {
               e.Item.Cells[1].Text = "172";
           }
           else if (e.Item.Cells[0].Text.StartsWith("TCO"))
           {
               e.Item.Cells[1].Text = "172";
           }
           else if (e.Item.Cells[0].Text.StartsWith("TOTAL GERAL"))
           {
               e.Item.Font.Bold = true;
           }
        }
    }
}
