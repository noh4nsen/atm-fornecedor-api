using Atm.Fornecedor.Api.Extensions.Entities;
using Atm.Fornecedor.Repositories;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Atm.Fornecedor.Api.Features.Produto.Commands
{
    public class AtualizarProdutoCommand : IRequest<AtualizarProdutoCommandResponse>
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public string Descricao { get; set; }
        public int QuantidadeEstoque { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorCobrado { get; set; }
        public Domain.Fornecedor Fornecedor { get; set; }
    }

    public class AtualizarProdutoCommandResponse
    {
        public DateTime DataAtualizacao { get; set; }
    }

    public class AtualizarProdutoCommandHandler : IRequestHandler<AtualizarProdutoCommand, AtualizarProdutoCommandResponse>
    {
        private readonly IRepository<Domain.Produto> _repository;
        private readonly IRepository<Domain.Fornecedor> _repositoryFornecedor;
        private readonly AtualizarProdutoCommandValidator _validator;

        public AtualizarProdutoCommandHandler(IRepository<Domain.Produto> repository, IRepository<Domain.Fornecedor> repositoryFornecedor, AtualizarProdutoCommandValidator validator)
        {
            _repository = repository;
            _repositoryFornecedor = repositoryFornecedor;
            _validator = validator;
        }

        public async Task<AtualizarProdutoCommandResponse> Handle(AtualizarProdutoCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException("Erro ao processar requisição");

            Domain.Produto entity = await _repository.GetFirstAsync(p => p.Id.Equals(request.Id));

            await _validator.ValidateDataAsync(request, entity);
            await _validator.ValidateFornecedorAsync(request, await _repositoryFornecedor.GetFirstAsync(f => f.Id.Equals(request.Fornecedor.Id)));

            request.Update(entity);

            await _repository.UpdateAsync(entity);
            await _repository.SaveChangesAsync();

            return entity.ToUpdateResponse();
        }
    }

    public class AtualizarProdutoCommandValidator : AbstractValidator<AtualizarProdutoCommand>
    {
        public AtualizarProdutoCommandValidator()
        {
            RuleFor(p => p.Nome).NotEmpty()
                                .WithMessage("Nome de produto é obrigatório");
            RuleFor(p => p.Tipo).NotNull()
                                .WithMessage("Tipo do produto é obrigatório");
            RuleFor(p => p.ValorUnitario).NotNull()
                                         .WithMessage("Valor unitário é obrigatório")
                                         .GreaterThan(0)
                                         .WithMessage("Valor unitário deve ser maior que zero");
            RuleFor(p => p.ValorCobrado).NotNull()
                                        .WithMessage("Valor cobrado é obrigatório")
                                        .GreaterThan(0)
                                        .WithMessage("Valor cobrado deve ser maior que zero");
            RuleFor(p => p.Fornecedor.Id).NotEqual(Guid.Empty)
                                        .WithMessage("Id de Fornecedor é obrigatório");
        }

        public async Task ValidateDataAsync(AtualizarProdutoCommand request, Domain.Produto entity)
        {
            RuleFor(r => r.Id)
                .Must(p => { return entity != null; })
                .WithMessage($"Produto de id {request.Id} não encontrado.");
            await this.ValidateAndThrowAsync(request);
        }

        public async Task ValidateFornecedorAsync(AtualizarProdutoCommand request, Domain.Fornecedor fornecedor)
        {
            RuleFor(r => r.Id)
                .Must(p => { return fornecedor != null; })
                .WithMessage($"Fornecedor de id {fornecedor.Id} não encontrado.");
            await this.ValidateAndThrowAsync(request);
        }
    }
}
