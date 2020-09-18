using System;

namespace Domain.Contracts
{
    public interface IBaseEntity<TKey> where TKey : struct
    {
        TKey Id { get; set; }
    }
}