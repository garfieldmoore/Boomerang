namespace Rainbow.Testing.Boomerang.Host
{
    using System.Collections.Generic;
   
    internal class HostSettings
    {
        public HostSettings()
        {
            Prefixes = new List<string>();
        }

        public IList<string> Prefixes { get; set; }
    }
}