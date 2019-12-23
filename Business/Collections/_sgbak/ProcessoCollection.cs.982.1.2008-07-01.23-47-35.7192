using System;
using System.Collections.Generic;
using System.Text;



namespace Marinha.Business
{
    public class ProcessoCollection : List<Processo>
    {
        public Processo Find(int id)
        {
            Processo processo = null;
            foreach (Processo p in this)
            {
                if (p.ID == id)
                    return p;
                else
                {
                    processo = Find(p, id);
                    if (processo != null)
                        return processo;
                }
            }
            return null;
        }

        private Processo Find(Processo pai, int id)
        {
            Processo processo = null;
            foreach (Processo p in pai.Processos)
            {
                if (p.ID == id)
                    return p;
                {
                    processo = Find(p, id);
                    if (processo != null)
                        return processo;
                }
            }

            return null;
        }
           
        /// <summary>
        /// Remove o processo, na própria coleção ou nas coleções dentro desta coleção
        /// </summary>        
        public new void Remove(Processo processo)
        {
            if (processo.ProcessoPai == null)
                base.Remove(processo);
            else
                processo.ProcessoPai.Processos.Remove(processo);
        }        
    }
}
