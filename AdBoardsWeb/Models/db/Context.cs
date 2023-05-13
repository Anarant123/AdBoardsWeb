using System;
using System.Collections.Generic;

namespace AdBoardsWeb.Models.db;

public partial class Context 
{
    public static List<Ad>? Ads { get; set; }

    public static Person? UserNow { get; set; }
}
