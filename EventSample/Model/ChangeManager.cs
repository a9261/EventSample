using System;
using System.Collections.Generic;
using EventSample.PubSubInterface;

namespace EventSample.Model
{
    public class ChangeManager : IChangeManager
    {
        public Dictionary<string, List<IPublisher>> _publisher;
        public Dictionary<string, List<ISubscriber>> _subscriber;

        public ChangeManager()
        {
            _publisher = new Dictionary<string, List<IPublisher>>();
            _subscriber = new Dictionary<string, List<ISubscriber>>();
        }

        public void RegisterPublisher(string topic, IPublisher publisher)
        {
            if (_publisher.ContainsKey(topic))
            {
                _publisher[topic].Add(publisher);
            }
            else
            {
                _publisher.Add(topic, new List<IPublisher>() { publisher });
            }
        }

        public void RegisterSubscriber(string topic, ISubscriber subscriber)
        {
            if (_subscriber.ContainsKey(topic))
            {
                _subscriber[topic].Add(subscriber);
            }
            else
            {
                _subscriber.Add(topic, new List<ISubscriber>() { subscriber });
            }
        }

        public void OnPodcastMessage(string topic, object message, object sender)
        {
            if (_subscriber.ContainsKey(topic) && _publisher.ContainsKey(topic))
            {
                if (_publisher[topic].Contains((IPublisher)sender))
                {
                    foreach (var subscriber in _subscriber[topic])
                    {
                        subscriber.OnReceived(message);
                    }
                }
            }
        }
    }
}