namespace TrananMVC.ViewModel;

public class MessageViewModel 
{
    public string Message { get; set; }
    public string OriginUrl { get; set; }
    public string RedirectUrl { get; set; }

    public MessageViewModel() { }

    public MessageViewModel(string message, string originUrl, string redirectUrl)
    {
        Message = message;
        OriginUrl = originUrl;
        RedirectUrl = redirectUrl;
    }
}
