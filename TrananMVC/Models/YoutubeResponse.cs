namespace TrananMVC.Model;
public class YoutubeResponse
{
    public string Kind { get; set; }
    public string Etag { get; set; }
    public PageInfo PageInfo { get; set; }
    public List<YouTubeVideoItem> Items { get; set; }
}

public class PageInfo
{
    public int TotalResults { get; set; }
    public int ResultsPerPage { get; set; }
}

public class YouTubeVideoItem
{
    public string Kind { get; set; }
    public string Etag { get; set; }
    public string Id { get; set; }
    public YouTubeVideoPlayer Player { get; set; }
}

public class YouTubeVideoPlayer
{
    public string EmbedHtml { get; set; }
}
