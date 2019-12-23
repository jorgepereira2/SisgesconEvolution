using System;

namespace Marinha.Business
{
    public class PedidoServicoUI
    {
        private readonly int _id;
        private readonly string _descricao;
      
        public virtual string Descricao
        {
            get { return _descricao; }
        }
        public virtual int ID
        {
            get { return _id; }
        }

        public PedidoServicoUI(int id, int codigo, DateTime data)
        {
            this._id = id;
            this._descricao = string.Format("{0}/{1}", codigo, data.Year);
            
        }
    }
}