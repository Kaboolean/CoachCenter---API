﻿using Dhoojol.Domain.Entities.Base;
using Dhoojol.Domain.Entities.Clients;
using Dhoojol.Domain.Entities.Users;

namespace Dhoojol.Domain.Entities.Coaches
{
    public class Coach : Entity
    {
        public User User { get; set; } = null!;
        public Guid UserId { get; set; }
        public string? Grades { get; set; }
        public string? Description { get; set; }
        public int HourlyRate { get; set; }
        public List<Client>? Clients { get; set; }
    }
}
