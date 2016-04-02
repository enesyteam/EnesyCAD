using System;

namespace Enesy.EnesyCAD
{
    /// <summary>
    /// Type of entity
    /// </summary>
    [Flags]
    public enum EntityType : byte
    {
        None = 0,
        Block = 1,
        Polyline = 2,
        Line = 3
    }

    /// <summary>
    /// Type of charactericstic of Block/Xref (Name, FileName or Path (absolute))
    /// </summary>
    [Flags]
    public enum BlockName : byte
    {
        Null,
        Name,
        FileName,
        Path
    }
}
