using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Marinha.Business;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ControleBordoService" in code, svc and config file together.
public class ControleBordoService : IControleBordoService
{
    public ServidorDTO GetServidor(string nip)
	{
	    Servidor servidor = Servidor.GetByNIP(nip);

        if(servidor != null)
        {
            ServidorDTO dto = new ServidorDTO();
            dto.NomeCompleto = servidor.NomeCompleto;
            dto.NomeGuerra = servidor.NomeGuerra;
            dto.UltimaEntrada = "-";
            dto.Imagem = servidor.Foto;


            MovimentoServidor movimentoServidor = MovimentoServidor.GetLast(servidor.ID);
            if(movimentoServidor != null)
            {
                dto.ABordo = !movimentoServidor.HoraSaida.HasValue;
                dto.UltimaEntrada = movimentoServidor.HoraEntrada.ToShortDateString() + " " + movimentoServidor.HoraEntrada.ToShortTimeString();
            }

            return dto;
        }
        return null;
	}

    public bool RegistrarEntrada(string nip, int id_servidorControle, out string mensagem)
    {
        Servidor servidor = Servidor.GetByNIP(nip);
        mensagem = "";
        if (servidor != null)
        {
            
            MovimentoServidor movimentoServidor = new MovimentoServidor();
            movimentoServidor.Servidor = servidor;
            movimentoServidor.HoraEntrada = DateTime.Now;
            movimentoServidor.ServidorControleEntrada = Servidor.Get(id_servidorControle);
            movimentoServidor.Save();
            return true;
        }
        else
        {
            mensagem = "NIP não encontrado.";
            return false;    
        }
        
    }

    public bool RegistrarSaida(string nip, int id_servidorControle, string justificativaSaida, int id_servidorAutorizacaoSaida, out string mensagem)
    {
        Servidor servidor = Servidor.GetByNIP(nip);
        mensagem = "";
        
        if (servidor != null)
        {
            MovimentoServidor movimentoServidor = MovimentoServidor.GetLast(servidor.ID);

            if(movimentoServidor == null)
            {
                mensagem = "Este servidor não está a bordo.";
                return false;
            }
            movimentoServidor.HoraSaida = DateTime.Now;
            movimentoServidor.ServidorControleSaida = Servidor.Get(id_servidorControle);
            movimentoServidor.JustificativaSaida = justificativaSaida;
            if(id_servidorAutorizacaoSaida > 0)
                movimentoServidor.ServidorAutorizacaoSaida = Servidor.Get(id_servidorAutorizacaoSaida);
            movimentoServidor.Save();
            return true;
        }
        else
        {
            mensagem = "NIP não encontrado.";
            return false;
        }

    }

    public bool UpdateServidor(string nip, byte[] foto)
    {
        Servidor servidor = Servidor.GetByNIP(nip);

        if (servidor != null)
        {
            servidor.Foto = foto;
            servidor.Save();
            return true;
        }
        return false;
    }

    public int Login(string usuario, string senha)
    {
        Servidor servidor = Servidor.Get(usuario, senha);
        if (servidor == null)
            return 0;
        else
            return servidor.ID;
    }

    #region Visitante
    public VisitanteDTO GetVisitante(string identidade)
    {
        Visitante visitante = Visitante.GetByIdentidade(identidade);

        if (visitante != null)
        {
            VisitanteDTO dto = new VisitanteDTO();
            dto.Nome = visitante.Nome;
            dto.NomeEmpresa = visitante.NomeEmpresa;
            dto.Telefone = visitante.Telefone;
            dto.Identidade = visitante.Identidade;
            dto.Imagem = visitante.Foto;

            MovimentoVisitante movimentoVisitante = MovimentoVisitante.GetLast(visitante.ID);
            if (movimentoVisitante != null)
            {
                dto.ABordo = !movimentoVisitante.HoraSaida.HasValue;
            }

            return dto;
        }
        return null;
    }

    public bool RegistrarEntradaVisitante(VisitanteDTO dto, int id_servidorControle, out string mensagem)
    {
        Visitante visitante = Visitante.GetByIdentidade(dto.Identidade);
        mensagem = "";
         
        if (visitante == null)
        {   
            visitante = new Visitante();
            visitante.Identidade = dto.Identidade;
            visitante.Nome = dto.Nome;
            visitante.NomeEmpresa = dto.NomeEmpresa;
            visitante.Telefone = dto.Telefone;
            visitante.Foto = dto.Imagem;
            visitante.Save();
        }
        else
        {
            if(dto.Imagem != null && dto.Imagem.Length > 0)
            {
                visitante.Foto = dto.Imagem;
            }
            visitante.Telefone = dto.Telefone;
            visitante.NomeEmpresa = dto.NomeEmpresa;
            visitante.Save();
        }
        
        MovimentoVisitante movimento = new MovimentoVisitante();
        movimento.HoraEntrada = DateTime.Now;
        movimento.ServidorControleEntrada = Servidor.Get(id_servidorControle);
        movimento.Visitante = visitante;
        movimento.Save();
        return true;
    }

    public bool RegistrarSaidaVisitante(string identidade, int id_servidorControle, out string mensagem)
    {
        Visitante visitante = Visitante.GetByIdentidade(identidade);
        mensagem = "";

        if (visitante != null)
        {
            MovimentoVisitante movimento = MovimentoVisitante.GetLast(visitante.ID);

            if (movimento == null)
            {
                mensagem = "Este visitante não está a bordo.";
                return false;
            }
            movimento.HoraSaida = DateTime.Now;
            movimento.ServidorControleSaida = Servidor.Get(id_servidorControle);
            movimento.Save();
            return true;
        }
        else
        {
            mensagem = "Identidade não encontrada.";
            return false;
        }
    }
    #endregion
}

[DataContract]
public class ServidorDTO
{
    [DataMember]
    public string NomeCompleto { get; set; }
    [DataMember]
    public string NomeGuerra { get; set; }
    [DataMember]
    public string UltimaEntrada { get; set; }
    [DataMember]
    public byte[] Imagem { get; set; }
    [DataMember]
    public bool ABordo { get; set; }
}

[DataContract]
public class VisitanteDTO
{
    [DataMember]
    public string Nome { get; set; }
    [DataMember]
    public string NomeEmpresa { get; set; }
    [DataMember]
    public string Identidade { get; set; }
    [DataMember]
    public string Telefone { get; set; }
    [DataMember]
    public byte[] Imagem { get; set; }
    [DataMember]
    public bool ABordo { get; set; }
}

