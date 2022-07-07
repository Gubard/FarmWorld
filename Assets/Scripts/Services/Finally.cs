using System;

namespace Services
{
    public class Finally : IDisposable
    {
        private readonly Action _action;

        public Finally(Action action)
        {
            _action = action;
        }

        public void Dispose()
        {
            _action.Invoke();
        }
    }
}