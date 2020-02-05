namespace EventSample.PubSubInterface
{
    public interface IChangeManager
    {
        void OnPodcastMessage(string topic, object message, object sender);
    }
}