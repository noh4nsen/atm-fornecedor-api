using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Atm.Fornecedor.Api.Features.Fornecedor.Queries
{
    public class SelecionarFornecedorFiltersQuery : IRequest<SelecionarFornecedorQueryResponse>
    {
    }

    public class SelecionarFornecedorFiltersQueryHanbdler : IRequestHandler<SelecionarFornecedorFiltersQuery, SelecionarFornecedorQueryResponse>
    {
        public Task<SelecionarFornecedorQueryResponse> Handle(SelecionarFornecedorFiltersQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
