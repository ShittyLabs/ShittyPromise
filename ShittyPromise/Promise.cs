using System;
using System.Linq;
using System.Collections.Generic;

namespace ShittyPromise
{
    enum PromiseStatus
    {
        Pending = 0,
        Fulfilled = 1,
        Rejected = 1
    }
    public class Promise<T>
    {
        private PromiseStatus _status = PromiseStatus.Pending;
        private T _result;
        private object _error;
        private List<Func<T, object>> onFulfilled = new List<Func<T, object>>();
        private List<Func<object, object>> onRejected = new List<Func<object, object>>();

        private Action<object> _notifyNewPromiseResolved;
        private Action<object> _notifyNewPromiseRejected;

        private Action<Action<U>, Action<object>> newPromiseExecutor<U>()
        {
            return (res, rej) =>
            {
                // when resolved, resolve _result
                _notifyNewPromiseResolved = res as Action<object>;
                // when error, reject with _error
                _notifyNewPromiseRejected = rej;

                if (_status == PromiseStatus.Fulfilled)
                {
                    _notifyNewPromiseResolved(_result);
                }
                else if (_status == PromiseStatus.Rejected)
                {
                    _notifyNewPromiseRejected(_error);
                }
            };
        }

        public Promise(Action<Action<T>, Action<object>> executor)
        {
            executor(_aggregateOnFulfilled, _aggregateOnRejected);
        }

        public Promise<U> Then<U>(Func<T, U> onFulfilled)
        {
            this.onFulfilled.Add(onFulfilled as Func<T, object>);
            return new Promise<U>(newPromiseExecutor<U>());
        }
        public Promise<U> Then<U>(Action<T> onFulfilled)
        {
            this.onFulfilled.Add(onFulfilled as Func<T, object>);
            return new Promise<U>(newPromiseExecutor<U>());
        }
        public Promise<U> Then<U>(Func<T, Promise<T>> onFulfilled)
        {
            throw new NotImplementedException();
        }

        private void _aggregateOnFulfilled(T result)
        {
            _result = result;
            onFulfilled.ForEach(fn => fn(_result));
            if(_notifyNewPromiseResolved != null) _notifyNewPromiseResolved(_result);
        }

        private void _aggregateOnRejected(object error)
        {
            _error = error;
            onRejected.ForEach(fn => fn(_error));
            if (_notifyNewPromiseRejected != null) _notifyNewPromiseRejected(_error);
        }
    }
}
