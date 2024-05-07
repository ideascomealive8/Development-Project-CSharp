using System;

namespace Sparcpoint
{
    public interface IHasTimestamp
    {
        DateTime CreatedTimestamp { get; set; }
    }
}