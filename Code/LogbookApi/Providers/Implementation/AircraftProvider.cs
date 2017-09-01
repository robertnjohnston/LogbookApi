using System.Collections.Generic;
using System.Linq;
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

        public Aircraft Get(string name)
        {
            var aircraftExist = _context.Aircraft.FirstOrDefault(aircraft => aircraft.Name == name) ?? Save(new Aircraft {Name = name});

            return aircraftExist;
        }

        public Aircraft Save(Aircraft entity)
        {
            var aircraftExists = _context.Aircraft.First(aircraft => aircraft.Name == entity.Name);

            if (aircraftExists == null)
            {
                aircraftExists = _context.Aircraft.Add(entity);
                _context.SaveChanges();
            }

            return aircraftExists;
        }
    }
}