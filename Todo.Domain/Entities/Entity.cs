using System;

namespace Todo.Domain.Entities
{
    public abstract class Entity : IEquatable<Entity>
    {
       public Guid Id { get; private set; }

       public Entity()
       {
           this.Id = Guid.NewGuid();
       }

        public bool Equals(Entity other) => this.Id == other.Id;
    }
}