using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Contracts
{
    public class BaseEntity : IBaseEntity<int>
    {
        public int Id { get; set; }
    }
}