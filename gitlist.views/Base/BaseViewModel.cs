using CommunityToolkit.Mvvm.ComponentModel;

namespace gitlist.views.Base
{
    public abstract partial class BaseViewModel : ObservableObject
    {
        public abstract void Active();
        public abstract void Deactive();
    }
}
