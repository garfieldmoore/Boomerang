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
        /// Gets or sets the method used in the request (i.e. GET, POST, PUT, DELETE)
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// Gets or sets the relative address of the service. Will be appended with '/'
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

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(Request other)
        {
            return AreAddressEqual(other) && AreMethodSame(other);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return Method + Address;
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
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