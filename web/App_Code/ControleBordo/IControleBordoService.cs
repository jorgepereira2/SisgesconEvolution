using System.ServiceModel;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IControleBordoService" in both code and config file together.
[ServiceContract]
public interface IControleBordoService
{
	[OperationContract]
    ServidorDTO GetServidor(string nip);

    [OperationContract]
    bool UpdateServidor(string nip, byte[] foto);

    [OperationContract]
    bool RegistrarEntrada(string nip, int id_servidorControle, out string mensagem);

    [OperationContract]
    bool RegistrarSaida(string nip, int id_servidorControle, string justificativaSaida, int id_servidorAutorizacaoSaida, out string mensagem);

    [OperationContract]
    int Login(string usuario, string senha);

    [OperationContract]
    VisitanteDTO GetVisitante(string identidade);

    [OperationContract]
    bool RegistrarEntradaVisitante(VisitanteDTO dto, int id_servidorControle, out string mensagem);

    [OperationContract]
    bool RegistrarSaidaVisitante(string identidade, int id_servidorControle, out string mensagem);
}

