using System;

namespace Marinha.Business
{
    public class DelineamentoOrcamentoUI
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

        public DelineamentoOrcamentoUI(int id, int codigo, DateTime data, int numeroOrcamento)
        {
            this._id = id;
            this._descricao = string.Format("{0}/{1} - Orçamento {2}", codigo, data.Year, numeroOrcamento);
        }
    }
}