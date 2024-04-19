namespace gitlist.domain;

public class UserModel
{
    private string? _fullUserName;
    private string? _avatarUrl;
    
    public string? FullUserName => _fullUserName;
    public string? AvatarUrl => _avatarUrl;

    public UserModel(string? fullUserName, string? avatarUrl)
    {
        _fullUserName = fullUserName;
        _avatarUrl = avatarUrl;
    }
}