using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using gitlist.core;
using gitlist.data.Repositories;
using gitlist.data.Services;
using gitlist.domain;
using gitlist.domain.Models;
using gitlist.views.Base;
using System.Collections.ObjectModel;

namespace gitlist.views.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {
        #region Properties

        [ObservableProperty]
        private string? _username;

        public IRelayCommand SearchCommand { get; }

        [ObservableProperty]
        private bool _visibleEmptyMessage = false;

        [ObservableProperty]
        private ObservableCollection<RepositoryModel> _items;

        [ObservableProperty]
        private bool _visibleProgress = false;

        [ObservableProperty]
        private bool _isProgressActive = false;

        [ObservableProperty]
        private bool _dataVisible = true;

        public IAsyncRelayCommand LoadMoreCommand { get; }

        #endregion

        private readonly IUserService _userService;
        private readonly IPublicUserRepositoryService _publicRepositoryService;

        public MainViewModel(IUserService userService, IPublicUserRepositoryService publicRepositoryService)
        {
            _userService = userService;
            _publicRepositoryService = publicRepositoryService;

            LoadMoreCommand = new AsyncRelayCommand(LoadMore);
            Items = new ObservableCollection<RepositoryModel>();
        }

        private UserModel _userModel { get; set; }
        public override async void Active()
        {
            Username = await _userService.GetCurrentUserAccount() ?? ServiceContainer.Current.CurrentUser;
            VisibleEmptyMessage = false;
            DataVisible = true;

            await GetUser();

            if (Items != null && Items.Count == 0)
                await LoadData(0, 10);
        }

        private async Task GetUser()
        {
            if (_userModel == null)
            {
                DataVisible = false;
                VisibleProgress = true;
                IsProgressActive = true;

                var user = await _userService.GetUserInfo(Username);
                if (user == null)
                {
                    VisibleEmptyMessage = true;
                    VisibleProgress = false;
                    IsProgressActive = false;
                    return;
                }

                _userModel = user;
            }
        }

        public override void Deactive()
        {

        }

        public async Task<int> LoadMore()
        {
            if (_userModel != null && Items != null && Items.Count > 0)
                await LoadData(Items.Count, 10);

            return 0;
        }

        private async Task LoadData(int skip, int count)
        {
            if (_userModel != null)
            {
                var result = await _publicRepositoryService.GetRepositoryByPage(_userModel.UserId, _userModel.FullUserName ?? "", skip, count);
                if (result == null) return;

                foreach (var item in result)
                    Items.Add(item);
            }
        }
    }
}
