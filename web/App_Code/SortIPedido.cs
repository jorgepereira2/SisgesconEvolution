using System;
using System.Collections.Generic;
using Marinha.Business;

public class SortIPedido
{
    public static void Sort(List<IPedido> list, string sortExpression)
    {
        if (!String.IsNullOrEmpty(sortExpression))
        {
            if (sortExpression == "CodigoComAno ASC")
                list.Sort(CompareCodigoComAnoASC);
            else if (sortExpression == "CodigoComAno DESC")
                list.Sort(SortIPedido.CompareCodigoComAnoDESC);
            else if (sortExpression == "Cliente ASC")
                list.Sort(SortIPedido.CompareClienteASC);
            else if (sortExpression == "Cliente DESC")
                list.Sort(SortIPedido.CompareClienteDESC);
            //else if (sortExpression == "Equipamento ASC")
            //    list.Sort(SortIPedido.CompareEquipamentoASC);
            //else if (sortExpression == "Equipamento DESC")
            //    list.Sort(SortIPedido.CompareEquipamentoDESC);
            else if (sortExpression == "DataEmissao ASC")
                list.Sort(SortIPedido.CompareDataASC);
            else if (sortExpression == "DataEmissao DESC")
                list.Sort(SortIPedido.CompareDataDESC);
            else if (sortExpression == "Status ASC")
                list.Sort(SortIPedido.CompareStatusASC);
            else if (sortExpression == "Status DESC")
                list.Sort(SortIPedido.CompareStatusDESC);
            else if (sortExpression == "ServidorGerente ASC")
                list.Sort(SortIPedido.CompareServidorGerenteASC);
            else if (sortExpression == "ServidorGerente DESC")
                list.Sort(SortIPedido.CompareServidorGerenteDESC);
            else if (sortExpression == "Celula ASC")
                list.Sort(SortIPedido.CompareCelulaASC);
            else if (sortExpression == "Celula DESC")
                list.Sort(SortIPedido.CompareCelulaDESC);
        }
    }

    private static int CompareCodigoComAnoASC(IPedido p1, IPedido p2)
    {
        return p1.CodigoComAno.CompareTo(p2.CodigoComAno);
    }

    private static int CompareCodigoComAnoDESC(IPedido p1, IPedido p2)
    {
        return p2.CodigoComAno.CompareTo(p1.CodigoComAno);
    }

    private static int CompareClienteASC(IPedido p1, IPedido p2)
    {
        return p1.Cliente.CompareTo(p2.Cliente);
    }

    private static int CompareClienteDESC(IPedido p1, IPedido p2)
    {
        return p2.Cliente.CompareTo(p1.Cliente);
    }

    //private static int CompareEquipamentoASC(IPedido p1, IPedido p2)
    //{
    //    return p1.Equipamento.CompareTo(p2.Equipamento);
    //}

    //private static int CompareEquipamentoDESC(IPedido p1, IPedido p2)
    //{
    //    return p2.Equipamento.CompareTo(p1.Equipamento);
    //}

    private static int CompareDataASC(IPedido p1, IPedido p2)
    {
        return p1.DataEmissao.CompareTo(p2.DataEmissao);
    }

    private static int CompareDataDESC(IPedido p1, IPedido p2)
    {
        return p2.DataEmissao.CompareTo(p1.DataEmissao);
    }

    private static int CompareStatusASC(IPedido p1, IPedido p2)
    {
        return p1.Status.CompareTo(p2.Status);
    }

    private static int CompareStatusDESC(IPedido p1, IPedido p2)
    {
        return p2.Status.CompareTo(p1.Status);
    }

    private static int CompareServidorGerenteASC(IPedido p1, IPedido p2)
    {
        if (p2.ServidorGerente == null || p1.ServidorGerente == null) return 1;
        return p1.ServidorGerente.NomeGuerra.CompareTo(p2.ServidorGerente.NomeGuerra);
    }

    private static int CompareServidorGerenteDESC(IPedido p1, IPedido p2)
    {
        if (p2.ServidorGerente == null || p1.ServidorGerente == null) return 0;
        return p2.ServidorGerente.NomeGuerra.CompareTo(p1.ServidorGerente.NomeGuerra);
    }

    private static int CompareCelulaASC(IPedido p1, IPedido p2)
    {
        if (p2.Celula == null || p1.Celula == null) return 1;
        return p1.Celula.CompareTo(p2.Celula);
    }

    private static int CompareCelulaDESC(IPedido p1, IPedido p2)
    {
        if (p2.Celula == null || p1.Celula == null) return 0;
        return p2.Celula.CompareTo(p1.Celula);
    }
}