namespace Domain.Contracts
{
    public class BaseEntity : IBaseEntity<int>
    {
        public int Id { get; set; }
    }
}