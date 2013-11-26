namespace Rainbow.Testing.Boomerang.Host
{
    using System;

    public class Registration : IEquatable<Registration>
    {
        public string Method { get; set; }
        public string Address { get; set; }

        public bool Equals(Registration other)
        {
            return other.Address == this.Address && other.Method == this.Method;
        }

        public override string ToString()
        {
            return this.Method + this.Address;
        }

        public override int GetHashCode()
        {
            return (this.Method + this.Address).GetHashCode();
        }
    }
}