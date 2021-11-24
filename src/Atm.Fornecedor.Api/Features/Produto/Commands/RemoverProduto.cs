using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Atm.Fornecedor.Api.Features.Produto.Commands
{
    public class RemoverProdutoCommand : IRequest<RemoverProdutoCommandResponse>
    {
        public Guid Id { get; set; }
    }

    public class RemoverProdutoCommandResponse
    {
        public Guid Id { get; set; }
    }

    public class RemoverProdutoCommandHandler : IRequestHandler<RemoverProdutoCommand, RemoverProdutoCommandResponse>
    {
        public async Task<RemoverProdutoCommandResponse> Handle(RemoverProdutoCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
