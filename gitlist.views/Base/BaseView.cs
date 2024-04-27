

namespace gitlist.views.Base
{
    public abstract class BaseView<TViewModel>(TViewModel viewModel) : BaseView(viewModel) where TViewModel : BaseViewModel
    {
        public new TViewModel BindingContext => (TViewModel)base.BindingContext;
    }

    public abstract class BaseView : ContentPage
    {
        protected BaseView(object? viewModel = null)
        {
            BindingContext = viewModel;
            Padding = 12;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            (BindingContext as BaseViewModel)?.Active();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            (BindingContext as BaseViewModel)?.Deactive();
        }
    }
}