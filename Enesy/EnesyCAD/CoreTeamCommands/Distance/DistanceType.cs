using System;
using System.ComponentModel;

namespace Enesy.EnesyCAD.CoreTeamCommands.Distance
{
    public enum DistanceType
    {
        [Description("Horizontal")]
        Horizontal = 0,
        [Description("Vertical")]
        Vertical = 1,
        [Description("Curve")]
        Curve = 2
    }
}
