using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Marinha.Business;
using Shared.NHibernateDAL;

/// <summary>
/// Summary description for CadastroSimples
/// </summary>
public abstract class CadastroSimples<T> : SortingPageBase where T : IDescricao, new()
{
    private Anthem.DataGrid _dataGrid;
    private Anthem.Button _btnNovo;

    public delegate void BeforeSaveEventHandler(object sender, BeforeSaveEventArgs<T> e);
    public event BeforeSaveEventHandler BeforeSave;
    
    
    protected void RegistraControlesCadastro(Anthem.DataGrid dagaGrid, Anthem.Button btnNovo)
    {
        _dataGrid = dagaGrid;
        _btnNovo = btnNovo;
        this.RegisterSortingControl(_dataGrid);
        this._btnNovo.Click += _btnNovo_Click;
        this._dataGrid.EditCommand += _dataGrid_EditCommand;
        this._dataGrid.CancelCommand += _dataGrid_CancelCommand;
        this._dataGrid.UpdateCommand += _dataGrid_UpdateCommand;
        this._dataGrid.ItemCommand += _dataGrid_ItemCommand;
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (!this.IsPostBack)
        {
            Bind();
            RegisterDeleteScript();
        }
    }

    protected override abstract void Bind();
    
    protected void BindToGrid(List<T> list)
    {
        this.Sort(list);

        _dataGrid.DataSource = list;
        _dataGrid.DataKeyField = "ID";
        _dataGrid.DataBind();
        _dataGrid.UpdateAfterCallBack = true;
        Anthem.AnthemClientMethods.ResizeIFrame();
    }


    #region DataGrid

    void _dataGrid_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Insert")
        {
            try
            {
                Anthem.TextBox txtDescricao = (Anthem.TextBox)e.Item.FindControl("txtDescricaoNovo");
                Anthem.CheckBox chkAtivo = (Anthem.CheckBox)e.Item.FindControl("chkAtivoNovo");

                T obj = new T();
                obj.Descricao = txtDescricao.Text;
                obj.FlagAtivo = chkAtivo.Checked;

                if (BeforeSave != null)
                    BeforeSave(_dataGrid, new BeforeSaveEventArgs<T>(e.Item, true, obj));
                
                obj.Save();

                Bind();
                _dataGrid.ShowFooter = false;
            }
            catch (Exception ex)
            {
                Anthem.AnthemClientMethods.Alert(ex.Message, this);
            }
        }
    }

    void _dataGrid_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            Anthem.TextBox txtDescricao = (Anthem.TextBox)e.Item.FindControl("txtDescricao");
            Anthem.CheckBox chkAtivo = (Anthem.CheckBox)e.Item.FindControl("chkAtivo");

            int id = Convert.ToInt32(_dataGrid.DataKeys[e.Item.ItemIndex]);

            T obj = BusinessObject<T>.Get(id, typeof(T));
            obj.Descricao = txtDescricao.Text;
            obj.FlagAtivo = chkAtivo.Checked;
            
            if(BeforeSave != null)
                BeforeSave(_dataGrid, new BeforeSaveEventArgs<T>(e.Item, false, obj));
            
            obj.Save();
            _dataGrid.EditItemIndex = -1;
            Bind();
        }
        catch (Exception ex)
        {
            Anthem.AnthemClientMethods.Alert(ex.Message, this);
        }
    }

    void _dataGrid_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        _dataGrid.EditItemIndex = -1;
        _dataGrid.ShowFooter = false;
        Bind();
    }

    void _dataGrid_EditCommand(object source, DataGridCommandEventArgs e)
    {
        _dataGrid.EditItemIndex = e.Item.ItemIndex;
        Bind();
    }

    [Anthem.Method]
    public void Excluir(int id)
    {
        T obj = BusinessObject<T>.Get(id, typeof(T));
        obj.Delete();
        Bind();
    }

    void _btnNovo_Click(object sender, EventArgs e)
    {
        _dataGrid.ShowFooter = true;
        Bind();
        _dataGrid.UpdateAfterCallBack = true;
    }
    #endregion
}

public class BeforeSaveEventArgs<T> : EventArgs where T : IDescricao, new()
{
    private DataGridItem _dataGridItem;
    private bool _isNew;
    private T _object;

    public virtual T Object
    {
        get { return _object; }
        set { _object = value; }
    }
    public virtual bool IsNew
    {
        get { return _isNew; }
        set { _isNew = value; }
    }
    public virtual DataGridItem DataGridItem
    {
        get { return _dataGridItem; }
        set { _dataGridItem = value; }
    }


    public BeforeSaveEventArgs(DataGridItem _dataGridItem, bool _isNew, T _object)
    {
        this._dataGridItem = _dataGridItem;
        this._isNew = _isNew;
        this._object = _object;
    }
}
