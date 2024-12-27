using System;
using System.Collections.Generic;

namespace CrudWithMovieScaffcolding.Models;

public partial class MovieDirection
{
    public int? MovId { get; set; }

    public int? DirId { get; set; }

    public int DirectionId { get; set; }

    public virtual Director? Dir { get; set; }

    public virtual Movie? Mov { get; set; }
}
