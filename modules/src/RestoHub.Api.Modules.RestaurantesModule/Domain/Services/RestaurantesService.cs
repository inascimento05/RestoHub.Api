using RestoHub.Api.Modules.RestaurantesModule.Application.Mediators.RestaurantesOperations.Dtos;
using RestoHub.Api.Modules.RestaurantesModule.Domain.Entities;
using RestoHub.Api.Modules.RestaurantesModule.Domain.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.ConstrainedExecution;

namespace RestoHub.Api.Modules.RestaurantesModule.Domain.Services
{
    public class RestaurantesService : IRestaurantesService
    {
        private readonly IRestaurantesRepository _repository;

        public RestaurantesService(IRestaurantesRepository repository)
        {
            _repository = repository;
        }

        public async Task<RestauranteDto> CreateRestauranteAsync(RestauranteDto restaurante)
        {
            ValidateCreateRestaurante(restaurante);

            var restauranteToSave = new Restaurante
            {
                ID = restaurante.ID,
                RazaoSocial = restaurante.RazaoSocial,
                NomeFantasia = restaurante.NomeFantasia,
                CNPJ = restaurante.CNPJ,
                Email = restaurante.Email,
                DDD = restaurante.DDD,
                Telefone = restaurante.Telefone,
                Logradouro = restaurante.Logradouro,
                Numero = restaurante.Numero,
                Bairro = restaurante.Bairro,
                Cidade = restaurante.Cidade,
                Estado = restaurante.Estado,
                CEP = restaurante.CEP,
                Complemento = restaurante.Complemento,
                NomeUsuario = restaurante.NomeUsuario,
                Senha = restaurante.Senha,
                QRCode = restaurante.QRCode,
                IsMatriz = restaurante.IsMatriz,
                AdicionadoDataHora = restaurante.AdicionadoDataHora
            };

            var savedRestaurante = await _repository.CreateRestauranteAsync(restauranteToSave);
            var restauranteToReturn = (RestauranteDto)savedRestaurante;

            return restauranteToReturn;
        }

        private static void ValidateCreateRestaurante(RestauranteDto restaurante)
        {
            if (restaurante != null)
            {
                ValidateRestauranteRazaoSocial(restaurante.RazaoSocial);
                ValidaRestauranteNomeFantasia(restaurante.NomeFantasia);
                ValidateRestauranteCNPJ(restaurante.CNPJ);
                ValidateRestauranteEmail(restaurante.Email);
                ValidateRestauranteDDD(restaurante.DDD);
                ValidateRestauranteTelefone(restaurante.Telefone);
                ValidateRestauranteLogradouro(restaurante.Logradouro);
                ValidateRestauranteNumero(restaurante.Numero);
                ValidateRestauranteBairro(restaurante.Bairro);
                ValidateRestauranteCidade(restaurante.Cidade);
                ValidateRestauranteCEP(restaurante.CEP);
                ValidateRestauranteComplemento(restaurante.Complemento);
                ValidateRestauranteNomeUsuario(restaurante.NomeUsuario);
            }
            else
            {
                throw new ArgumentNullException("Restaurante inválido. Restaurante Não pode ser nulo.");
            }
        }

        #region Private Methods
        private static void ValidateRestauranteNomeUsuario(string nomeUsuario)
        {
            if (string.IsNullOrEmpty(nomeUsuario))
            {
                throw new ArgumentException("Nome Usuário inválido. 'Nome Usuário' não pode ser nulo.");
            }
            if (nomeUsuario.Length > 255)
            {
                throw new ArgumentException("Nome Usuário inválido. 'Nome Usuário' deve conter no máximo 255 caracteres.");
            }
        }

        private static void ValidateRestauranteComplemento(string? complemento)
        {
            if (complemento.Length > 100)
            {
                throw new ArgumentException("Complemento inválido. 'Complemento' deve conter no máximo 100 caracteres.");
            }
        }

        private static void ValidateRestauranteCEP(string cep)
        {
            if (string.IsNullOrEmpty(cep))
            {
                throw new ArgumentException("CEP inválida. 'CEP' não pode ser nula.");
            }
            if (cep.Length < 8 || cep.Length > 8)
            {
                throw new ArgumentException("CEP inválido. 'CEP' deve conter 8 dígitos.");
            }
        }

        private static void ValidateRestauranteCidade(string cidade)
        {
            if (string.IsNullOrEmpty(cidade))
            {
                throw new ArgumentException("Cidade inválida. 'Cidade' não pode ser nula.");
            }
            if (cidade.Length > 255)
            {
                throw new ArgumentException("Cidade. 'Cidade' pode conter no máximo 255 caracteres.");
            }
        }

        private static void ValidateRestauranteBairro(string bairro)
        {
            if (string.IsNullOrEmpty(bairro))
            {
                throw new ArgumentException("Bairro inválido. 'Bairro' não pode ser nulo.");
            }
            if (bairro.Length > 255)
            {
                throw new ArgumentException("Bairro. 'Bairro' pode conter no máximo 255 caracteres.");
            }
        }

        private static void ValidateRestauranteNumero(string numero)
        {
            if (string.IsNullOrEmpty(numero))
            {
                throw new ArgumentException("Número inválido. 'Número' não pode ser nulo.");
            }
            if (numero.Length > 20)
            {
                throw new ArgumentException("Número inválido. 'Número' pode conter no máximo 20 caracteres.");
            }
        }

        private static void ValidateRestauranteLogradouro(string logradouro)
        {
            if (string.IsNullOrEmpty(logradouro))
            {
                throw new ArgumentException("Logradouro inválido. 'Logradouro' não pode ser nulo.");
            }
            if (logradouro.Length > 255)
            {
                throw new ArgumentException("Logradouro inválido. 'Logradouro' pode conter no máximo 255 caracteres.");
            }
        }

        private static void ValidateRestauranteEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Email inválido. 'Email' não pode ser nulo.");
            }
        }

        private static void ValidaRestauranteNomeFantasia(string nomeFantasia)
        {
            if (string.IsNullOrEmpty(nomeFantasia))
            {
                throw new ArgumentException("Nome Fantasia inválido. 'Nome Fantasia' não pode ser nula.");
            }
            if (nomeFantasia.Length > 255)
            {
                throw new ArgumentException("Nome Fantasia inválida. 'Nome Fantasia' deve conter no máximo 255 caracteres.");
            }
        }

        private static void ValidateRestauranteRazaoSocial(string razaoSocial)
        {
            if (string.IsNullOrEmpty(razaoSocial))
            {
                throw new ArgumentException("Razão Social inválida. 'Razão Social' não pode ser nula. ");
            }
            if (razaoSocial.Length > 255)
            {
                throw new ArgumentException("Razão Social inválida. 'Razão Social' deve conter no máximo 255 caracteres.");
            }
        }

        private static void ValidateRestauranteDDD(string ddd)
        {
            if (string.IsNullOrEmpty(ddd))
            {
                throw new ArgumentException("DDD inválido. 'DDD' não pode ser vazio");
            }
            if (ddd.Length < 2 || ddd.Length > 2)
            {
                throw new ArgumentException("DDD inválido. 'DDD' deve conter 2 dígitos.");
            }
        }

        private static void ValidateRestauranteTelefone(string telefone)
        {
            if (string.IsNullOrEmpty(telefone))
            {
                throw new ArgumentException("Telefone inválido. 'Telefone' não pode ser vazio");
            }
            if (telefone.Length < 9 || telefone.Length > 9)
            {
                throw new ArgumentException("Telefone inválido. 'Telefone' precisa ter 9 dígitos");
            }
        }

        private static void ValidateRestauranteCNPJ(string cnpj)
        {
            if (string.IsNullOrEmpty(cnpj))
            {
                throw new ArgumentException("CNPJ. CNPJ não pode ser vazio");
            }
            if (cnpj.Length < 14 || cnpj.Length > 14)
            {
                throw new ArgumentException("CNPJ. 'CNPJ' precisa conter 14 dígitos.");
            }
        }
        #endregion
    }
}
