using System;
using System.Collections.Generic;

namespace Atm.Fornecedor.Domain
{
    public class Produto : Entity
    {
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public string Descricao { get; set; }
        public int QuantidadeEstoque { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorCobrado { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public List<Guid> Orcamentos { get; set; }
    }
}