using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Insight.Cqrs.ReadOnlyStorage
{
    public class PartReadRepo : IReadRepository
    {

        static List<PartDto> items = new List<PartDto>();

        public PartDto GetById(Guid id)
        {
            return items.FirstOrDefault(a => a.Id == id);
        }

        public void Add(PartDto item)
        {
            items.Add(item);
        }

        public void Delete(Guid id)
        {
            items.RemoveAll(i => i.Id == id);
        }

        public List<PartDto> GetItems()
        {
            return items;
        } 
    }
}
