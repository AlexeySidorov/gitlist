using System.Reflection;
using gitlist.core;
using gitlist.domain;

namespace gitlistmobile;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }

    protected async override void OnStart()
    {
 
        //Создание Базы данных
        await Task.Run(() => new DataBase().
                  CreateTables(ServiceContainer.Current.DbPath, [typeof(UserEntity).GetTypeInfo().Assembly]));

        base.OnStart();
    }
}