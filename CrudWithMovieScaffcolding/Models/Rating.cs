using System;
using System.Collections.Generic;

namespace CrudWithMovieScaffcolding.Models;

public partial class Rating
{
    public int? MovId { get; set; }

    public int? RevId { get; set; }

    public decimal? RevStars { get; set; }

    public int? NumORatings { get; set; }

    public int RatingId { get; set; }

    public virtual Movie? Mov { get; set; }

    public virtual Reviewer? Rev { get; set; }
}
