using RestoHub.Api.Modules.RestaurantesModule.Domain.Entities;

namespace RestoHub.Api.Modules.RestaurantesModule.Application.Mediators.RestaurantesOperations.Dtos
{
    public class RestauranteDto
    {
        public Guid ID { get; set; } = Guid.Empty;
        public string RazaoSocial { get; set; } = string.Empty;
        public string NomeFantasia { get; set; } = string.Empty;
        public string CNPJ {  get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string DDD { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Logradouro { get; set; } = string.Empty;
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
        public DateTime AdicionadoDataHora { get; set; } = DateTime.Now;
        public DateTime? ModificadoDataHora { get; set; } = DateTime.Now;
        public DateTime? ExcluidoDataHora { get; set; } = DateTime.Now;

        public static explicit operator RestauranteDto(Restaurante restaurantes)
        {
            var restauranteDto = new RestauranteDto
            {
                ID = restaurantes.ID,
                RazaoSocial = restaurantes.RazaoSocial,
                NomeFantasia = restaurantes.NomeFantasia,
                CNPJ = restaurantes.CNPJ,
                Email = restaurantes.Email,
                DDD = restaurantes.DDD,
                Telefone = restaurantes.Telefone,
                Logradouro = restaurantes.Logradouro,
                Numero = restaurantes.Numero,
                Bairro = restaurantes.Bairro,
                Cidade = restaurantes.Cidade,
                Estado = restaurantes.Estado,
                CEP = restaurantes.CEP,
                Complemento = restaurantes.Complemento,
                NomeUsuario = restaurantes.NomeUsuario,
                Senha = restaurantes.Senha,
                QRCode = restaurantes.QRCode,
                IsMatriz = restaurantes.IsMatriz,
                AdicionadoDataHora = restaurantes.AdicionadoDataHora,
                ModificadoDataHora = restaurantes.ModificadoDataHora,
                ExcluidoDataHora = restaurantes.ExcluidoDataHora
            };

            return restauranteDto;
        }
    }
}
