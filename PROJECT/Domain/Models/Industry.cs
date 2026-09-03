using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Industry
{
    public Guid IndustryId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Company> Companies { get; set; } = new List<Company>();
}
