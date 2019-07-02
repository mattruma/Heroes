using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Heroes.Data
{
    public abstract class BaseEntity : IEntity
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("created_on")]
        public DateTime CreatedOn { get; set; }

        public BaseEntity()
        {
            this.Id = Guid.NewGuid();
            this.CreatedOn = DateTime.UtcNow;
        }
    }
}
