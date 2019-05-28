using System;

namespace RobustEngine.Graphics
{
    [FlagsAttribute]
    public enum UsageHint
    {
        Read =1 ,
        Write = 2,
        Copy = 4,
        Dynamic = 8,
        Static = 16,
        Stream = 32,
    }
}