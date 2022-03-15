namespace SeedRoad.Common.Application.Contracts
{
    public interface IDispatcher<in TMessage>
    {
        public void Dispatch(TMessage message);
    }
}