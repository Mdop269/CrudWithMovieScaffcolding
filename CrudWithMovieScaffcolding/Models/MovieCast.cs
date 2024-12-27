using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CrudWithMovieScaffcolding.Models;

public partial class MovieCast
{
    public int? MovId { get; set; }

    public int? ActId { get; set; }

    public string? Role { get; set; }

    public int CastId { get; set; }

    public virtual Actor? Act { get; set; }

    public virtual Movie? Mov { get; set; }
}
