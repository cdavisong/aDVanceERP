using Newtonsoft.Json;

namespace aDVanceERP.Actualizador.Modelos;

public class GitHubRelease {
    [JsonProperty("tag_name")]
    public string TagName { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("body")]
    public string Body { get; set; }

    [JsonProperty("published_at")]
    public DateTime PublishedAt { get; set; }

    [JsonProperty("prerelease")]
    public bool PreRelease { get; set; }

    [JsonProperty("assets")]
    public List<GitHubAsset> Assets { get; set; }
}
