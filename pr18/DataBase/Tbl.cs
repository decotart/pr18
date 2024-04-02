using System;
using System.Collections.Generic;

namespace pr18;

public partial class Tbl
{
    public int Id { get; set; }

    public string NameOfL { get; set; } = null!;

    public int SumOfL { get; set; }

    public int CountOfL { get; set; }

    public DateOnly DateOfProd { get; set; }

    public DateOnly ExpDate { get; set; }

    public string NameOfFabric { get; set; } = null!;

    public string? AdresOfFabric { get; set; }
}
