using CommunityToolkit.Mvvm.ComponentModel;

namespace gitlist.domain.Models
{
    public partial class RepositoryModel : ObservableObject
    {
        [ObservableProperty]
        private string? _name;
        public RepositoryModel(string name)
        {
            Name = name;
        }
    }
}