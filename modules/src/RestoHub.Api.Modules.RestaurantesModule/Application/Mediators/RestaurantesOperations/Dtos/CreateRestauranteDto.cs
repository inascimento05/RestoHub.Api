using FluentValidator;
using FluentValidator.Validation;

namespace RestoHub.Api.Modules.RestaurantesModule.Application.Mediators.RestaurantesOperations.Dtos
{
    public class CreateRestauranteDto : Notifiable
    {
        public string RazaoSocial { get; set; } = string.Empty;
        public string NomeFantasia { get; set; } = string.Empty;
        public string CNPJ { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string DDD { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Logradouro {  get; set; } = string.Empty;
        public string Numero {  get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string CEP { get; set; } = string.Empty;
        public string? Complemento { get; set; } = string.Empty;
        public string NomeUsuario { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public string QRCode { get; set; } = string.Empty;
        public bool IsMatriz { get; set; }

        public void Validate()
        {
            AddNotifications(new ValidationContract()
                .IsNotNullOrEmpty(RazaoSocial, nameof(RazaoSocial), "Razão social is required.")
                .IsNotNullOrEmpty(NomeFantasia, nameof(NomeFantasia), "Nome fantasia is required.")
                .IsNotNullOrEmpty(CNPJ, nameof(CNPJ), "CNPJ is required.")
                .IsNotNullOrEmpty(Email, nameof(Email), "Email is required.")
                .IsNotNullOrEmpty(DDD, nameof(DDD), "DDD is required.")
                .IsNotNullOrEmpty(Telefone, nameof(Telefone), "Telefone is required.")
                .IsNotNullOrEmpty(Logradouro, nameof(Logradouro), "Logradouro is required.")
                .IsNotNullOrEmpty(Numero, nameof(Numero), "Logradouro is required.")
                .IsNotNullOrEmpty(Bairro, nameof(Bairro), "Bairro is required.")
                .IsNotNullOrEmpty(Cidade, nameof(Cidade), "Cidade is required.")
                .IsNotNullOrEmpty(Estado, nameof(Estado), "Estado is required.")
                .IsNotNullOrEmpty(CEP, nameof(CEP), "CEP is required.")
                .IsNotNullOrEmpty(NomeUsuario, nameof(NomeUsuario), "Nome de usuário is required.")
                .IsNotNullOrEmpty(Senha, nameof(Senha), "Senha is required.")
                .IsNotNullOrEmpty(QRCode, nameof(QRCode), "QRCode is required."));
        }
    }
}
