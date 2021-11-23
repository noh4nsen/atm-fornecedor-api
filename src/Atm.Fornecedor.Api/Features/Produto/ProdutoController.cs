using Atm.Fornecedor.Api.Features.Produto.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] InserirProdutoCommand request)
        {
            return Ok(await _mediator.Send(request));
        }
    }
}
