using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace UP7.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    AppDbContext context;
    UserService userAppService;

    [ObservableProperty]
    private string newUserLogin;

    [ObservableProperty]
    private string newUserPassword;

    public MainViewModel()
    {
        context = new AppDbContext();
        userAppService = new(context);
        // Загрузите пользователей при создании ViewModel
        Task.Run(async () => await LoadUsersAsync());
    }

    [RelayCommand]
    private async Task CreateUser()
    {
        // 1. Дожидаемся завершения создания пользователя
        await userAppService.CreateUserAsync(newUserLogin, newUserPassword);

        // 2. Дожидаемся завершения обновления списка пользователей
        await LoadUsersAsync();
    }

    private ObservableCollection<User> _userList;

    public ObservableCollection<User> UserList
    {
        get => _userList;
        private set => SetProperty(ref _userList, value);
    }

    public async Task LoadUsersAsync()
    {
        var users = await userAppService.GetUsersAsync();
        UserList = new ObservableCollection<User>(users);
    }

    
}
