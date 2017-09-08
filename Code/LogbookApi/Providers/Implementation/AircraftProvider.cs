using System.Collections.Generic;
using System.Linq;
using System.Text;
using CuttingEdge.Conditions;

namespace LogbookApi.Providers.Implementation
{
    public class AircraftProvider : IEntityProvider<Aircraft>
    {
        private readonly jetstrea_LogbookEntities _context;

        public AircraftProvider(jetstrea_LogbookEntities context)
        {
            Condition.Requires(context, nameof(context)).IsNotNull();
            _context = context;
        }
        public IEnumerable<Aircraft> Get()
        {
            return _context.Aircraft.ToList();
        }

        public Aircraft Get(int id)
        {
            return _context.Aircraft.Find(id);
        }

        public Aircraft Save(Aircraft entity)
        {
            if (entity.Id == 0)
            {
                entity.Id = _context.Aircraft.Local.Count + 1;
                var aircraft = _context.Aircraft.Add(entity);
                _context.SaveChanges();
                return aircraft;
            }

            return entity;
        }
    }
}