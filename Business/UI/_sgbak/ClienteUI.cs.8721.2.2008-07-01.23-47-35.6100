using System;

namespace Marinha.Business
{
    [Serializable]
    public class ClienteUI
    {
        private readonly string _descricao;
        private readonly string _codigo;
        private readonly int _id;
        private readonly string _indicativoNaval;

        public string IndicativoNaval
        {
            get { return _indicativoNaval; }
        }
        public string Codigo
        {
            get { return _codigo; }
        }
        public string Descricao
        {
            get { return _descricao; }
        }

        public int ID
        {
            get { return _id; }
        }
	    
        public string DescricaoCompleta
        {
            get
            {
                if(string.IsNullOrEmpty(_indicativoNaval))
                    return string.Format("{0} - {1}", _codigo, _descricao);
                else
                    return string.Format("{2} - {0} - {1}", _codigo, _descricao, _indicativoNaval);
            }
        }

        public ClienteUI(int id, string descricao, string codigo, string indicativoNaval)
        {
            this._descricao = descricao;
            this._codigo = codigo;
            this._id = id;
            this._indicativoNaval = indicativoNaval;
        }
    }
}