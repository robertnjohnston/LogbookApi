using System.Collections.Generic;
using System.Linq;
using CuttingEdge.Conditions;

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
           return _context.Airfield.First(airfield => airfield.Id == id);
        }

        public Airfield Get(string name)
        {
            var airfieldExists = _context.Airfield.First(airfield => airfield.Name == name) ?? Save(new Airfield() { Name = name });

            return airfieldExists;
        }

        public Airfield Save(Airfield entity)
        {
            var airfieldExists = _context.Airfield.First(airfield => airfield.Name == entity.Name);

            if(airfieldExists == null)
            {
                airfieldExists = _context.Airfield.Add(entity);
                _context.SaveChanges();
            }

            return airfieldExists;
        }
    }
}