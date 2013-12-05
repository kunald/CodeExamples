using Domain.Model.Part;
using Insight123.Contract;

namespace Domain.EventHandlers
{
    public class PartDescriptionChangedEventHandler : IEventHandler<PartDescriptionChanged>
    {
        private readonly IReadRepository _reportDatabase;
        public PartDescriptionChangedEventHandler(IReadRepository reportDatabase)
        {
            _reportDatabase = reportDatabase;
        }
        public void Handle(PartDescriptionChanged handle)
        {
            //ToDo: Call the update() for the repository
            var item = _reportDatabase.GetById(handle.AggregateId);
            item.PartDescription = handle.PartDescription;
        }
    }
}
