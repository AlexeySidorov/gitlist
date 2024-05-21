namespace gitlist.domain;

public class UserModel
{
    private string? _fullUserName;
    private string? _avatarUrl;
    public int _userId;

    public string? FullUserName => _fullUserName;
    public string? AvatarUrl => _avatarUrl;

    public int UserId => _userId;

    public UserModel(int userId, string? fullUserName, string? avatarUrl)
    {
        _fullUserName = fullUserName;
        _avatarUrl = avatarUrl;
        _userId = userId;
    }
}