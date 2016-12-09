using System;

namespace JakeCleary.PocketMongrels.Core
{
    public interface IGloballyUniqueEntity
    {
        Guid Id { get; }
    }
}
