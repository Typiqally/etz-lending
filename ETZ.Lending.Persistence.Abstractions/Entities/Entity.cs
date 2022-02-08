using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETZ.Lending.Persistence.Abstractions.Entities
{
    public abstract class Entity<TKey> : IEntity<TKey>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TKey Id { get; set; }

        object IEntity.Id
        {
            get => Id;
            set => Id = value is TKey key ? key : default;
        }
    }
}