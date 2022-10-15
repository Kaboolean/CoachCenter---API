using System.ComponentModel.DataAnnotations;

namespace Dhoojol.Domain.Entities.Base;

public abstract class Entity
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
}

