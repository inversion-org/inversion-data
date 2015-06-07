namespace Inversion.Data
{
    public abstract class StoreBase : IStore
    {
        private StoreState _state = StoreState.Unstarted;

        public bool HasStarted
        {
            get { return _state == StoreState.Started; }
        }

        public bool HasStopped
        {
            get { return _state == StoreState.Stopped; }
        }

        public virtual void Start()
        {
            if (this.HasStarted)
            {
                throw new StoreStartedException("The store you are attempting to start has already been started.");
            }

            _state = StoreState.Started;
        }

        public virtual void Stop()
        {
            if (this.HasStarted)
            {
                _state = StoreState.Stopped;
            }
        }

        protected void AssertIsStarted()
        {
            if (!this.HasStarted)
            {
                throw new StoreProcessException("The store must be started to use it.");
            }
        }

        public abstract void Dispose();
    }
}