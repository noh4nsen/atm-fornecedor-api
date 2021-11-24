using Atm.Fornecedor.Api.Features.Produto.Commands;
using System;

namespace Atm.Fornecedor.Api.Extensions.Entities
{
    public static class ProdutoExtensions
    {
        public static Domain.Produto ToDomain(this InserirProdutoCommand request)
        {
            return new Domain.Produto()
            {
                Nome = request.Nome,
                Tipo = request.Tipo,
                Descricao = request.Descricao,
                QuantidadeEstoque = request.QuantidadeEstoque,
                ValorUnitario = request.ValorUnitario,
                ValorCobrado = request.ValorCobrado,
                Fornecedor = request.Fornecedor,
                FornecedorId = request.Fornecedor.Id
            };
        }

        public static InserirProdutoCommandResponse ToInsertResponse(this Domain.Produto entity)
        {
            return new InserirProdutoCommandResponse()
            {
                Id = entity.Id,
                DataCadastro = entity.DataCadastro
            };
        }

        public static void Update(this AtualizarProdutoCommand request, Domain.Produto entity)
        {
            entity.Nome = request.Nome;
            entity.Tipo = request.Tipo;
            entity.Descricao = request.Descricao;
            entity.ValorUnitario = request.ValorUnitario;
            entity.ValorCobrado = request.ValorCobrado;
            entity.Fornecedor = request.Fornecedor;
            entity.FornecedorId = request.Fornecedor.Id;
        }

        public static AtualizarProdutoCommandResponse ToUpdateResponse(this Domain.Produto entity)
        {
            return new AtualizarProdutoCommandResponse()
            {
                DataAtualizacao = (DateTime)(entity.DataAtualizacao)
            };
        }
    }
}
