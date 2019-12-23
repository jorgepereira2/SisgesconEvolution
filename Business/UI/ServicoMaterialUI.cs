namespace Marinha.Business
{
    public class ServicoMaterialUI
    {
        private readonly int _id;
        private readonly string _descricao;
        private readonly string _classe;
        private readonly string _subClasse;
        private readonly string _unidade;

        public virtual string Unidade
        {
            get { return _unidade; }
        }
        
        public virtual string SubClasse
        {
            get { return _subClasse; }
        }
	    
        public virtual string Classe
        {
            get { return _classe; }
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
            get{ return string.Format("{0} - {1} - {2}", _classe, _subClasse, _descricao);}
        }

        public ServicoMaterialUI(int _id, string _descricao, string _classe, string _subClasse, string _unidade)
        {
            this._id = _id;
            this._descricao = _descricao;
            this._classe = _classe;
            this._subClasse = _subClasse;
            this._unidade = _unidade;
        }
    }
}