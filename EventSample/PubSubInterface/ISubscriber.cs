namespace EventSample.PubSubInterface
{
    public interface ISubscriber
    {
        void Received(object message);
    }
}