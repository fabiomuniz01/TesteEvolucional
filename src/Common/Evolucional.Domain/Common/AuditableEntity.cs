using System;

namespace Evolucional.Domain.Common
{
    public abstract class AuditableEntity
    {
        public string UsuarioCriacao { get; set; }
        public DateTime DataCriacao { get; set; }
        public string UsuarioAlteracao { get; set; }
        public DateTime? DataAlteracao { get; set; }

    }
}
