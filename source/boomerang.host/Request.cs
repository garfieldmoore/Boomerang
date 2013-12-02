namespace Rainbow.Testing.Boomerang.Host
{
    using System;

    /// <summary>
    /// A service request
    /// </summary>
    public class Request : IEquatable<Request>
    {
        /// <summary>
        /// The method used in the request (i.e. GET, POST, PUT, DELETE)
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// The relative address of the service
        /// </summary>
        public string Address { get; set; }

        public bool Equals(Request other)
        {
            return string.Compare(other.Address, this.Address, StringComparison.OrdinalIgnoreCase) == 0 && string.Compare(other.Method, this.Method, StringComparison.InvariantCultureIgnoreCase) == 0;
        }

        public override string ToString()
        {
            return this.Method + this.Address;
        }

        public override int GetHashCode()
        {
            return (this.Method + this.Address).ToLower().GetHashCode();
        }
    }
}