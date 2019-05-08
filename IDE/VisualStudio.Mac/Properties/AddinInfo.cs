using Mono.Addins;

[assembly: Addin(
    "VisualStudio.Mac",
    Namespace = "VisualStudio.Mac",
    Version = "0.3.2"
)]

[assembly: AddinName("HotReloading")]
[assembly: AddinCategory("IDE extensions")]
[assembly: AddinDescription(".Net HotReloading tool")]
[assembly: AddinAuthor("Pranshu Aggarwal")]