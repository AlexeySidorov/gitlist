public class Constaint
{
    private static readonly Lazy<Constaint> Instance = new Lazy<Constaint>(() => new Constaint());
    public static Constaint Current => Instance.Value;

    // API url
    public string BaseApiUrl = "https://api.github.com/";

    public string DbName = "gitlist.db";
}
