using System.Reflection;
using Cstieg.StringHelper;

namespace Cstieg.Geography
{
    /// <summary>
    /// A base for all address objects
    /// </summary>
    public class AddressBase
    {
        public virtual string Recipient { get; set; }

        public virtual string Address1 { get; set; }

        public virtual string Address2 { get; set; }

        public virtual string City { get; set; }

        public virtual string State { get; set; }

        public virtual string PostalCode { get; set; }

        public virtual string Country { get; set; }

        public virtual string Phone { get; set; }

        public override string ToString()
        {
            return Address1 + " " + Address2 + ", " + City + ", " + State + " " + PostalCode;
        }

        /// <summary>
        /// Determines whether an address is equivalent to this (does not consider phone).
        /// Is case sensitive, but does not consider differences in leading or trailing spaces
        /// </summary>
        /// <param name="address">The address to compare to the present address</param>
        /// <returns>True if the two addresses are equivalent, false if not</returns>
        public bool AddressIsSame(AddressBase address)
        {
            return Address1.Trim() == address.Address1.Trim() &&
                Address2.Trim() == address.Address2.Trim() &&
                City.Trim() == address.City.Trim() &&
                State.Trim() == address.State.Trim() &&
                PostalCode.Trim() == address.PostalCode.Trim();
        }

        /// <summary>
        /// Determines whether the phone number in an address is equivalent to this
        /// </summary>
        /// <param name="address">The address whose phone number to compare to the phone number of the present address</param>
        /// <returns>True if the phone numbers are equivalent, false if not</returns>
        public bool PhoneIsSame(AddressBase address)
        {
            return Phone.Digits() == address.Phone.Digits();
        }

        /// <summary>
        /// Determines whether an address (including phone number) is equivalent to this
        /// </summary>
        /// <param name="address">The address to compare to the present address</param>
        /// <returns>True if the two addresses are equivalent, false if not</returns>
        public bool FullAddressIsSame(AddressBase address)
        {
            return AddressIsSame(address) && PhoneIsSame(address);
        }

        /// <summary>
        /// Copies this addres to another address object
        /// </summary>
        /// <param name="address">The destination object</param>
        public void CopyTo(AddressBase address)
        {
            PropertyInfo[] properties = GetType().GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                PropertyInfo copyFromProperty = properties[i];
                PropertyInfo copyToProperty = address.GetType().GetProperty(copyFromProperty.Name);
                if (copyToProperty != null)
                {
                    copyToProperty.SetValue(address, copyFromProperty.GetValue(this));
                }
            }
        }

        /// <summary>
        /// Replaces all null strings with the empty string ("")
        /// </summary>
        public void SetNullStringsToEmpty()
        {
            PropertyInfo[] properties = GetType().GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                if (properties[i].PropertyType == typeof(string) && properties[i].GetValue(this) == null)
                {
                    properties[i].SetValue(this, "");
                }
            }
        }
    }
}