namespace Marinha.Business
{
    public class EquipamentoUI
    {
        private readonly int _id;
        private readonly string _descricao;
        private readonly string _tipo;
        private readonly string _subTipo;

        public virtual string SubTipo
        {
            get { return _subTipo; }
        }
	    
        public virtual string Tipo
        {
            get { return _tipo; }
        }
        public virtual string Descricao
        {
            get { return _descricao; }
        }
        public virtual int ID
        {
            get { return _id; }
        }

        public virtual string DescricaoCompleta
        {
            get{ return string.Format("{0} - {1} - {2}", _tipo, _subTipo, _descricao);}
        }

        public EquipamentoUI(int _id, string _descricao, string _tipo, string _subTipo)
        {
            this._id = _id;
            this._descricao = _descricao;
            this._tipo = _tipo;
            this._subTipo = _subTipo;
        }
    }
}