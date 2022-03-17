namespace SeedRoad.Common.Core.Domain.Events
{
    public interface IEventDispatcher<in TMessage>
    {
        public void Dispatch(TMessage message);
    }
}