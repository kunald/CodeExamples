using Insight123.Contract;
using Insight123.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Insight123.Reporting
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
