using Atm.Fornecedor.Api.Features.Produto.Commands;
using Atm.Fornecedor.Api.Features.Produto.Queries;
using Atm.Fornecedor.Domain;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Atm.Fornecedor.Api.Extensions.Entities
{
    public class ProdutoProfile : Profile
    {
        public ProdutoProfile()
        {
            CreateMap<Produto, SelecionarProdutoQueryResponse>();
            CreateMap<InserirProdutoCommand, Produto>();
        }
    }

    public static class ProdutoExtensions
    {
        public static SelecionarProdutoQueryResponse ToQueryResponse(this Domain.Produto entity, Domain.Fornecedor fornecedor)
        {
            return new SelecionarProdutoQueryResponse()
            {
                Id = entity.Id,
                Nome = entity.Nome,
                Tipo = entity.Tipo,
                Descricao = entity.Descricao,
                QuantidadeEstoque = entity.QuantidadeEstoque,
                ValorUnitario = entity.ValorUnitario,
                ValorCobrado = entity.ValorCobrado,
                Fornecedor = fornecedor
            };
        }
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
            entity.QuantidadeEstoque = request.QuantidadeEstoque;
            entity.ValorUnitario = request.ValorUnitario;
            entity.ValorCobrado = request.ValorCobrado;
            entity.FornecedorId = request.Fornecedor.Id;
        }

        public static AtualizarProdutoCommandResponse ToUpdateResponse(this Domain.Produto entity)
        {
            return new AtualizarProdutoCommandResponse()
            {
                DataAtualizacao = (DateTime)(entity.DataAtualizacao)
            };
        }

        public static RemoverProdutoCommandResponse ToRemoveResponse(this Domain.Produto entity)
        {
            return new RemoverProdutoCommandResponse()
            {
                Id = entity.Id
            };
        }

        public static IEnumerable<SelecionarProdutoQueryResponse> ToQueryResponse(this IEnumerable<Domain.Produto> listaProdutos, IEnumerable<Domain.Fornecedor> listaFornecedores)
        {
            IList<SelecionarProdutoQueryResponse> response = new List<SelecionarProdutoQueryResponse>();
            foreach (var entity in listaProdutos.Zip(listaFornecedores, Tuple.Create))
                response.Add(entity.Item1.ToQueryResponse(entity.Item2));
            return response;
        }
    }
}
