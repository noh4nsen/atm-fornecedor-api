using Atm.Fornecedor.Api.Extensions.Entities;
using Atm.Fornecedor.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Atm.Fornecedor.Api.Features.Produto.Queries
{
    public class SelecionarProdutoFiltersQuery : IRequest<IEnumerable<SelecionarProdutoQueryResponse>>
    {
        public string Nome { get; set; }
    }

    public class SelecionarProdutoFiltersQueryHandler : IRequestHandler<SelecionarProdutoFiltersQuery, IEnumerable<SelecionarProdutoQueryResponse>>
    {
        private readonly IRepository<Domain.Produto> _repositoryProduto;
        private readonly IRepository<Domain.Fornecedor> _repositoryFornecedor;

        public SelecionarProdutoFiltersQueryHandler(IRepository<Domain.Produto> repositoryProduto, IRepository<Domain.Fornecedor> repositoryFornecedor)
        {
            _repositoryProduto = repositoryProduto;
            _repositoryFornecedor = repositoryFornecedor;
        }

        public async Task<IEnumerable<SelecionarProdutoQueryResponse>> Handle(SelecionarProdutoFiltersQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException("Erro ao processar requisição");

            IEnumerable<Domain.Produto> result = string.IsNullOrEmpty(request.Nome)
                                                ? await _repositoryProduto.GetAsync()
                                                : await _repositoryProduto.GetAsync(p => p.Nome.ToUpper().Contains(request.Nome.ToUpper()));

            IEnumerable<Domain.Fornecedor> listaFornecedores = await PreencheFornecedores(result);

            return result.ToQueryResponse(listaFornecedores);
        }

        private async Task<IEnumerable<Domain.Fornecedor>> PreencheFornecedores(IEnumerable<Domain.Produto> produtosList)
        {
            IList<Domain.Fornecedor> listaFornecedores = new List<Domain.Fornecedor>();
            foreach (var produto in produtosList)
            {
                listaFornecedores.Add(await _repositoryFornecedor.GetFirstAsync(f => f.Id.Equals(produto.FornecedorId)));
            }

            return listaFornecedores;
        }
    }
}
