using EventSample.Model;

namespace EventSample.PubSubInterface
{
    public interface IPublisher
    {
        void Subscribe(ISubscriber subscriber);
    }
}