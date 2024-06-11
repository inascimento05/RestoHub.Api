using System.Diagnostics.CodeAnalysis;

namespace RestoHub.Api.Modules.RestaurantesModule.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class Restaurante
    {
        public Guid ID { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string CNPJ { get; set; }
        public string Email { get; set; }
        public string DDD { get; set; }
        public string Telefone { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string CEP { get; set; }
        public string? Complemento { get; set; }
        public string NomeUsuario { get; set; }
        public string Senha { get; set; }
        public string QRCode { get; set; }
        public bool IsMatriz { get; set; }
        public DateTime AdicionadoDataHora { get; set; }
        public DateTime? ModificadoDataHora { get; set; }
        public DateTime? ExcluidoDataHora { get; set; }
    }
}
