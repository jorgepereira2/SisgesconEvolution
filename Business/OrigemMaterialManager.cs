using System;
using System.Collections.Generic;
using System.Text;
using Shared.Common;

namespace Marinha.Business
{
    public class OrigemMaterialManager
    {
        public static Dictionary<int, string> ListSemSingra()
        {
            Dictionary<int, string> list = new Dictionary<int, string>(3);
            list.Add(Convert.ToInt32(OrigemMaterial.Obtencao), Util.GetDescription(OrigemMaterial.Obtencao));
            list.Add(Convert.ToInt32(OrigemMaterial.PEP), Util.GetDescription(OrigemMaterial.PEP));
            list.Add(Convert.ToInt32(OrigemMaterial.Rodizio), Util.GetDescription(OrigemMaterial.Rodizio));
            return list;
        }

        public static Dictionary<int, string> ListApenasObtencao()
        {
            Dictionary<int, string> list = new Dictionary<int, string>(1);
            list.Add(Convert.ToInt32(OrigemMaterial.Obtencao), Util.GetDescription(OrigemMaterial.Obtencao));
            return list;
        }
    }
}
