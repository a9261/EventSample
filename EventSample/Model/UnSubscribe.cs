using System;
using System.Collections.Generic;

namespace EventSample.Model
{
    public class UnSubscribe<T> : IDisposable

    {
        private IObserver<T> observer;
        private List<IObserver<T>> observers;

        public UnSubscribe(IObserver<T> observer)
        {
            this.observer = observer;
        }

        public UnSubscribe(List<IObserver<T>> observers)
        {
            this.observers = observers;
        }

        public void Dispose()
        {
            this.observer = null;
            if (observers.Count > 0)
            {
                this.observers.Clear();
            }
        }
    }
}