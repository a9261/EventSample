namespace EventSample.PubSubInterface
{
    public interface ISubscriber
    {
        void OnReceived(object message);
    }
}