using gitlist.views.Base;
using gitlist.views.ViewModels;

namespace gitlist.views.Views;

public partial class MainView : BaseView<MainViewModel>
{
    public MainView(MainViewModel viewModel) : base(viewModel) => InitializeComponent();
}

