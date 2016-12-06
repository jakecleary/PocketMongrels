using System;

namespace JakeCleary.PocketMongrels.Core.Entity
{
    public interface IGloballyUniqueEntity
    {
        Guid Guid { get; }
    }
}
