﻿using System;

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
        public Guid FornecedorId { get; set; }
        public Fornecedor Fornecedor { get; set; }
    }
}