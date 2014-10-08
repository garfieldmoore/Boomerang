namespace Rainbow.Testing.Boomerang.Host
{
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    internal class HostSettings
    {
        public HostSettings()
        {
            Prefixes = new List<string>();
        }

        public IList<string> Prefixes { get; set; }
    }
}