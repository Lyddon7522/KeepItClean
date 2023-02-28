﻿using System.Drawing;

namespace KeepItClean.Server.Domain;

public class Location
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required Point Coordinates { get; set; }
}
