using System.Collections.Generic;
using System.Linq;
using CuttingEdge.Conditions;
using LogbookApi.Database;

namespace LogbookApi.Providers.Implementation
{
    public class AirfieldProvider : IEntityProvider<Airfield>
    {
        private readonly jetstrea_LogbookEntities _context;

        public AirfieldProvider(jetstrea_LogbookEntities context)
        {
            Condition.Requires(context, nameof(context)).IsNotNull();
            _context = context;
        }
        public IEnumerable<Airfield> Get()
        {
            return _context.Airfield.ToList();
        }

        public Airfield Get(int id)
        {
           return _context.Airfield.Find(id);
        }

        public Airfield Save(Airfield entity)
        {
            if (entity.Id == 0)
            {
                entity.Id = _context.Airfield.Local.Count + 1;
                var airfield = _context.Airfield.Add(entity);
                _context.SaveChanges();
                return airfield;
            }

            return entity;
        }
    }
}