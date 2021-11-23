using Atm.Fornecedor.Api.Extensions.Entities;
using Atm.Fornecedor.Repositories;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Atm.Fornecedor.Api.Features.Produto.Commands
{
    public class InserirProdutoCommand : IRequest<InserirProdutoCommandResponse>
    {
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public string Descricao { get; set; }
        public int QuantidadeEstoque { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorCobrado { get; set; }
        public Guid FornecedorId { get; set; }
    }

    public class InserirProdutoCommandResponse
    {
        public Guid Id { get; set; }
        public DateTime DataCadastro { get; set; }
    }

    public class InserirProdutoCommandHandler : IRequestHandler<InserirProdutoCommand, InserirProdutoCommandResponse>
    {
        private readonly IRepository<Domain.Produto> _repository;
        private readonly IRepository<Domain.Fornecedor> _repositoryFornecedor;
        private readonly InserirProdutoCommandValidator _validator;

        public InserirProdutoCommandHandler(IRepository<Domain.Produto> repository, IRepository<Domain.Fornecedor> repositoryFornecedor, InserirProdutoCommandValidator validator)
        {
            _repository = repository;
            _repositoryFornecedor = repositoryFornecedor;
            _validator = validator;
        }

        public async Task<InserirProdutoCommandResponse> Handle(InserirProdutoCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException("Erro ao processar requisição");

            Domain.Produto entity = request.ToDomain();

            await _validator.ValidateDataAsync(request, await _repositoryFornecedor.GetFirstAsync(f => f.Id.Equals(request.FornecedorId)));


            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();

            return entity.ToInsertResponse();
        }
    }

    public class InserirProdutoCommandValidator : AbstractValidator<InserirProdutoCommand>
    {
        public InserirProdutoCommandValidator()
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
            RuleFor(p => p.FornecedorId).NotEqual(Guid.Empty)
                                        .WithMessage("Id de Fornecedor é obrigatório");
        }

        public async Task ValidateDataAsync(InserirProdutoCommand request, Domain.Fornecedor fornecedor)
        {
            RuleFor(r => r.FornecedorId)
                .Must(f => { return fornecedor != null; })
                .WithMessage($"Fornecedor de id {request.FornecedorId} não encontrado.");
            await this.ValidateAndThrowAsync(request);
        }
    }
}
