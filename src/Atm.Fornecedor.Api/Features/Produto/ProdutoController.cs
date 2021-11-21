using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Atm.Fornecedor.Api.Features.Produto
{
    [Route("produto")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProdutoController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
    }
}
