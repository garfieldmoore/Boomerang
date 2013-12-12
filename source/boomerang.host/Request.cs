namespace Rainbow.Testing.Boomerang.Host
{
    using System;

    /// <summary>
    /// A service request
    /// </summary>
    public class Request : IEquatable<Request>
    {
        private string address;

        /// <summary>
        /// The method used in the request (i.e. GET, POST, PUT, DELETE)
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// The relative address of the service
        /// </summary>
        public string Address
        {
            get
            {
                return address;
            }

            set
            {
                if (value.StartsWith("/"))
                {
                    address = value;
                }
                else
                {
                    address = "/" + value;
                }
            }
        }

        public bool Equals(Request other)
        {
            return AreAddressEqual(other) && AreMethodSame(other);
        }

        public override string ToString()
        {
            return Method + Address;
        }

        public override int GetHashCode()
        {
            return (this.Method + this.Address).ToLower().GetHashCode();
        }

        private bool AreMethodSame(Request other)
        {
            return string.Compare(other.Method, this.Method, StringComparison.InvariantCultureIgnoreCase) == 0;
        }

        private bool AreAddressEqual(Request other)
        {
            return string.Compare(other.Address, this.Address, StringComparison.OrdinalIgnoreCase) == 0;
        }
    }
}