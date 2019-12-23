using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using Anthem;
using Marinha.Business;
using Shared.SessionState;

public partial class frmListaUsuarios : MarinhaPageBase
{
    [TransientPageState]
    protected DataTable _dtServidor;

    [TransientPageState]
    protected DataTable _dtGrupo;


    private TipoAgrupamento _tipoAgrupamento
    {
        get { return (TipoAgrupamento)Session["_tipoAgrupamento"]; }
        set { Session["_tipoAgrupamento"] = value; }
    }

    protected enum TipoAgrupamento
    {
        Celula = 0,
        OnlineOffline = 1,
        
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Manager.Register(this);
        if(!this.IsPostBack)
        {
            FillPage();
            timer.Interval = 10000;
            timer.Enabled = true;
        }
    }

    [Anthem.Method]
    public void Agrupar(string value)
    {
        _tipoAgrupamento =  (TipoAgrupamento) Convert.ToInt32(value);
        PopulaGrupo();
        Bind();
    }
   
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);       
        timer.Tick += new EventHandler(timer_Tick);
    }

    void timer_Tick(object sender, EventArgs e)
    {
       Bind();
    }

    private void FillPage()
    {
        _tipoAgrupamento = TipoAgrupamento.OnlineOffline;

        _dtServidor = Servidor.GetDataTableUsuarios();
        _dtServidor.Columns.Add("online", typeof(bool));

        //tira o usuario atual
        DataRow[] rows = _dtServidor.Select("ID_Servidor=" + this.ID_Servidor.ToString());
        _dtServidor.Rows.Remove(rows[0]);
        _dtServidor.AcceptChanges();

        _dtGrupo = new DataTable();
        _dtGrupo.Columns.Add("ID_Grupo", typeof(int));
        _dtGrupo.Columns.Add("Grupo", typeof(string));
        _dtGrupo.Columns.Add("Quantidade", typeof(int));
        _dtGrupo.Columns.Add("aberto", typeof(bool));
        _dtGrupo.Columns["aberto"].DefaultValue = true;

        PopulaGrupo();
        Bind();

      
    }

    private void PopulaGrupo()
    {
        _dtGrupo.Clear();
        if (_tipoAgrupamento == TipoAgrupamento.OnlineOffline)
        {
            VerificaOnline();
            DataRow dr = _dtGrupo.NewRow();
            dr["ID_Grupo"] = 1;
            dr["Grupo"] = "Online";
            dr["Quantidade"] = Convert.ToInt32(_dtServidor.Compute("COUNT(ID_Servidor)", "online=1") ?? 0);
            dr["aberto"] = true;
            _dtGrupo.Rows.Add(dr);

            dr = _dtGrupo.NewRow();
            dr["ID_Grupo"] = 0;
            dr["Grupo"] = "Offline";
            dr["Quantidade"] = Convert.ToInt32(_dtServidor.Compute("COUNT(ID_Servidor)", "online=0") ?? 0);
            dr["aberto"] = true;
            _dtGrupo.Rows.Add(dr);
        }
        else if (_tipoAgrupamento == TipoAgrupamento.Celula)
        {
            Dictionary<int, string> list = Celula.List();
            foreach (KeyValuePair<int, string> item in list)
            {
                DataRow dr = _dtGrupo.NewRow();
                dr["ID_Grupo"] = item.Key;
                dr["Grupo"] = item.Value;
                dr["Quantidade"] = Convert.ToInt32(_dtServidor.Compute("COUNT(ID_Servidor)", "ID_Celula=" + item.Key) ?? 0);
                dr["aberto"] = true;
                _dtGrupo.Rows.Add(dr);
            }
        }        
    }

    [Anthem.Method]
    public void AbreFechaGrupo(bool aberto, int id_grupo)
    {
        DataRow[] rows = _dtGrupo.Select("ID_Grupo = " + id_grupo.ToString());
        rows[0]["aberto"] = aberto;
    }

    private void Bind()
    {
        VerificaOnline();

        dlGrupo.DataSource = _dtGrupo;
        dlGrupo.DataBind();
        dlGrupo.UpdateAfterCallBack = true;
    }

    private void VerificaOnline()
    {
        foreach (DataRow row in _dtServidor.Rows)
        {
            row["online"] = ChatManager.Contains(Convert.ToInt32(row["ID_Servidor"]));
        }
    }

    protected void dlGrupo_ItemDataBound(object source, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            try
            {
                BindServidor(e);
                DataRowView row = (DataRowView)e.Item.DataItem;

                if (row["Quantidade"].ToString() == "0")
                    return;

                Anthem.DataList dl = (Anthem.DataList)e.Item.FindControl("dlServidor");
                //Anthem.HiddenField hidden = (Anthem.HiddenField)e.Item.FindControl("hiddenGrupo");

                System.Web.UI.WebControls.Image img = (System.Web.UI.WebControls.Image)e.Item.FindControl("imgGrupo");
                //System.Web.UI.HtmlControls.HtmlGenericControl div = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("dlServidor");
                System.Web.UI.HtmlControls.HtmlTableRow tr = (System.Web.UI.HtmlControls.HtmlTableRow)e.Item.FindControl("trGrupo");
                tr.Attributes.Add("onclick", " Mostrar('" + dl.ClientID + "', '" + img.ClientID + "', '" + row["ID_Grupo"].ToString() + "');");

                if (Convert.ToBoolean(row["aberto"]))
                {
                    img.ImageUrl = "../images/minus.gif";
                    dl.Style.Add("display", "");
                }
                else
                {
                    img.ImageUrl = "../images/plus.gif";
                    dl.Style.Add("display", "none");
                }
            }
            catch (Exception ex)
            {
                Anthem.AnthemClientMethods.Alert(ex.Message, this);
            }
        }
    }

    private void BindServidor(DataListItemEventArgs e)
    {
        DataRowView row = (DataRowView)e.Item.DataItem;
        DataView dv = new DataView(_dtServidor);

        if (_tipoAgrupamento == TipoAgrupamento.OnlineOffline)
            dv.RowFilter = "online = " + row["ID_Grupo"].ToString();
        else if (_tipoAgrupamento == TipoAgrupamento.Celula)
            dv.RowFilter = "ID_Celula= " + row["ID_Grupo"].ToString();      

        dv.Sort = "online DESC";
        Anthem.DataList dl = (Anthem.DataList)e.Item.FindControl("dlServidor");
        dl.DataSource = dv;
        dl.DataBind();
    }
}
