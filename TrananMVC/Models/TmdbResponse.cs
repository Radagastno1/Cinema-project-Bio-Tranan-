public class TmdbVideoResult
{
    public string Id { get; set; }
    public string Key { get; set; }
    public string Name { get; set; }
    public string Site { get; set; }
    public int Size { get; set; }
    public string Type { get; set; }
}

public class TmdbResponse
{
    public List<TmdbVideoResult> Results { get; set; }
}
