using System;

namespace Marinha.Business
{
    [Serializable]
    public class ServidorUI
    {
        private readonly int _id;
        private readonly string _nome;

        public virtual string Nome
        {
            get { return _nome; }
        }

        public virtual int ID
        {
            get { return _id; }
        }

        public ServidorUI(int id, string nome)
        {
            this._id = id;
            this._nome = nome;
        }
    }
}