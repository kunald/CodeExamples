
using System;
using System.Collections.Generic;
using Insight123.Dto;

namespace Insight123.Contract
{
    public interface IReadRepository
    {
        PartDto GetById(Guid id);
        void Add(PartDto item);
        void Delete(Guid id);
        List<PartDto> GetItems();
        //TODO: implement crud for readonly models based on events of aggregates
    }
}
