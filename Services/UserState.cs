using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace PoolApp.Services
{
    public class UserState
    {
        private int _userid;
        public string? Name { get; set; }
        public int Points { get; set; }
        public int userid
        {
            get => _userid;
            set
            {
                if (_userid != value)
                {
                    _userid = value;
                    NotifyStateChanged();
                }
            }
            
        }

        public event Action? OnChange;
        public void NotifyStateChanged() => OnChange?.Invoke();
    }
}
