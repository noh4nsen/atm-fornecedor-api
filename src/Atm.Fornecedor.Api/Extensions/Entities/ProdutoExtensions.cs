using Atm.Fornecedor.Api.Features.Produto.Commands;

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
                FornecedorId = request.FornecedorId
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
    }
}
