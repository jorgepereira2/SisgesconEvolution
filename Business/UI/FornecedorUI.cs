using System;

namespace Marinha.Business
{
    [Serializable]
    public class FornecedorUI
    {
        private readonly string _razaoSocial;
        private readonly int _id;
        public string RazaoSocial
        {
            get { return _razaoSocial; }
        }

        public int ID
        {
            get { return _id; }
        }

        public FornecedorUI(int id, string razaoSocial)
        {
            this._razaoSocial = razaoSocial;
            this._id = id;
        }
    }
}