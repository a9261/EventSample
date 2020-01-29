using System;

namespace EventSample.Model
{
    public class UnSubscribe<T> : IDisposable

    {
        private IObserver<T> observer;

        public UnSubscribe(IObserver<T> observer)
        {
            this.observer = observer;
        }

        public void Dispose()
        {
            this.observer = null;
        }
    }
}