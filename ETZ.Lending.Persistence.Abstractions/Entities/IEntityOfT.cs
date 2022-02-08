namespace ETZ.Lending.Persistence.Abstractions.Entities
{
    public interface IEntity<TKey> : IEntity
    {
        public new TKey Id { get; set; }
    }
}