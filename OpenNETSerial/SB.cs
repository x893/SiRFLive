﻿namespace OpenNETCF.IO.Serial
{
    using System;

    [Flags]
    internal enum SB
    {
        PARITY_EVEN = 0x4000000,
        PARITY_MARK = 0x8000000,
        PARITY_NONE = 0x1000000,
        PARITY_ODD = 0x2000000,
        PARITY_SPACE = 0x10000000,
        STOPBITS_10 = 0x10000,
        STOPBITS_15 = 0x20000,
        STOPBITS_20 = 0x40000
    }
}

