using System;

namespace Atm.Fornecedor.Domain
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public Guid IdExterno { get; set; }
    }
}
