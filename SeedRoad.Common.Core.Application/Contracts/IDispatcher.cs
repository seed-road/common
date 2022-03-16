namespace SeedRoad.Common.Core.Application.Contracts
{
    public interface IDispatcher<in TMessage>
    {
        public void Dispatch(TMessage message);
    }
}