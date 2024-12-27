using System;
using System.Collections.Generic;

namespace CrudWithMovieScaffcolding.Models;

public partial class MovieGenre
{
    public int? MovId { get; set; }

    public int? GenId { get; set; }

    public int GenresId { get; set; }

    public virtual Genre? Gen { get; set; }

    public virtual Movie? Mov { get; set; }
}
