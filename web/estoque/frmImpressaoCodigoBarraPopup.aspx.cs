using System;
using System.Data;

using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Marinha.Business;
using Shared.BarCode;
using Shared.SessionState;

public partial class frmImpressaoCodigoBarraPopup : MarinhaPageBase
{
    private MedidaEtiqueta _medida;
    private int _paginaAtual;
	private decimal _separacaoEntrePaginas = 0m;

   
    protected void Page_Load(object sender, EventArgs e)
    {
        _medida = MedidaEtiqueta.Get(Convert.ToInt32(Request["ID_MedidaEtiqueta"]));
        _separacaoEntrePaginas = 0;//Convert.ToDecimal(Request["separacao"]);
        litPrintArea.Text = GetHtml();
    }

 
    private string GetHtml()
    {
        
        List<ServicoMaterial> materiais = ServicoMaterial.Select(Request["listaMaterialID"]);
        
        //Insere etiquetas em branco
        int etiquetasEmBranco = Convert.ToInt32(Request["EtiquetaInicial"]);
        for(int i = 0; i < etiquetasEmBranco;i++)
            materiais.Insert(0,new ServicoMaterial());

        StringBuilder str = new StringBuilder();
        Response.ContentType = "text/html";
        
        int coluna = 1;
        int linha = 1;
        int etiqueta = 1;
        decimal x = 0;
        decimal y = this._medida.MargemSuperior;
        _paginaAtual = 1;
		//str.Append(PageTop());
        foreach (ServicoMaterial material in materiais)
        {
            object[] param = new object[4];
            param[0] = FormataNumero(_medida.LarguraEtiqueta);
            param[1] = FormataNumero(_medida.AlturaEtiqueta);
            param[2] = FormataNumero(x);
            param[3] = FormataNumero(y);

            str.Append(string.Format("<span class=\"divs\" style=\"position:absolute;width:{0}cm;height:{1}cm;left:{2}cm;top:{3}cm;font-size:9px;text-align:center\">",
                  param));

            if (!string.IsNullOrEmpty(material.CodigoInterno))
                str.Append(GetConteudo(material));
            else
                str.Append("<br />");

            str.Append("</span>");

            etiqueta++;

            x += _medida.LarguraEtiqueta + _medida.SeparacaoHorizontal;

            //Verifica se é quebra de linha
            if (coluna == _medida.Colunas)
            {
                linha++;
                coluna = 0;
                x = 0;
                y += _medida.AlturaEtiqueta + _medida.SeparacaoVertical;

                //Verifica se é quebra de pagina
                if (etiqueta == _medida.EtiquetasPorPagina + 1)
                {
                    //str.Append(QuebraPagina());
                    linha = 1;
                    etiqueta = 1;
                    x = 0;
					//str.Append("<div style=\"page-break:always;\" />");
					y += _medida.MargemInferior + _medida.MargemSuperior + _separacaoEntrePaginas; 
                    _paginaAtual++;
                }
            }
            coluna++;
        }
       // str.Append(PageBottom());
        return str.ToString();
    }

    private string GetConteudo(ServicoMaterial material)
    {
        StringBuilder str = new StringBuilder();
        str.AppendFormat("<img src=\"barcode.aspx?barcode={0}&barcodetype={1}&altura={2}\" />",
            material.CodigoInterno, 
            Convert.ToInt32(BarCodeType.CODE39),
            _medida.AlturaConteudo);
        str.AppendFormat("<div >{0}</div>", material.CodigoInterno);
        str.AppendFormat("<div >{0}</div>", material.Descricao);
        return str.ToString();
    }

    private string PageTop()
    {
        return "<div style=\"Z-INDEX: 100; LEFT: " +
            FormataNumero(_medida.MargemEsquerda) + "cm; WIDTH: "
            + FormataNumero(_medida.LarguraPapel) +
			"cm; ZOOM: 0; POSITION: absolute; TOP: " + _medida.MargemSuperior + "cm;\">";
    }

    private string PageBottom()
    {
        return "</div>";
    }
  

    private string FormataNumero(decimal numero)
    {
        return numero.ToString().Replace(",", ".");
    }

}
