using Dapper;
using RestoHub.Api.Modules.RestaurantesModule.Domain.Entities;
using RestoHub.Api.Modules.RestaurantesModule.Domain.Interfaces;
using System.Data;

namespace RestoHub.Api.Modules.RestaurantesModule.Data.Repositories
{
    public class RestaurantesRepository : IRestaurantesRepository
    {
        private readonly IDbConnection _dbConnection;

        public RestaurantesRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<Restaurante> CreateRestauranteAsync(Restaurante restaurante)
        {
            const string query = @"INSERT INTO
                                    Restaurantes (
                                        ID, 
                                        RazaoSocial, 
                                        NomeFantasia, 
                                        CNPJ, 
                                        Email, 
                                        DDD, 
                                        Telefone, 
                                        Logradouro, 
                                        Numero, 
                                        Bairro, 
                                        Cidade, 
                                        Estado, 
                                        CEP, 
                                        Complemento, 
                                        NomeUsuario, 
                                        Senha, 
                                        QRCode, 
                                        IsMatriz, 
                                        AdicionadoDataHora)
                                   VALUES(
                                        @ID, 
                                        @RazaoSocial, 
                                        @NomeFantasia, 
                                        @CNPJ, 
                                        @Email, 
                                        @DDD, 
                                        @Telefone, 
                                        @Logradouro, 
                                        @Numero, 
                                        @Bairro, 
                                        @Cidade, 
                                        @Estado, 
                                        @CEP, 
                                        @Complemento, 
                                        @NomeUsuario, 
                                        @Senha, 
                                        @QRCode, 
                                        @IsMatriz, 
                                        @AdicionadoDataHora);";
            var param = new
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

            restaurante.ID = await _dbConnection.QuerySingleOrDefaultAsync<Guid>(query, param);
            return restaurante;
        }
    }
}
