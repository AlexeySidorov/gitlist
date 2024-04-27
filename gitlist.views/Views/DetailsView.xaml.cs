using gitlist.views.Base;
using gitlist.views.ViewModels;

namespace gitlist.views.Views;

public partial class DetailsView : BaseView<DetailsViewModel>
{
    public DetailsView(DetailsViewModel viewModel) : base(viewModel) => InitializeComponent();
}

