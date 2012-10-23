using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace HSR_Helper.DomainLibrary.Domain.Userinformation
{
    public class BadgeInformation : Persistency.PersistentObject
    {
        [JsonProperty("badgeSaldo")]
        public float CashAmount { get; set; }

        [XmlIgnore]
        public string BadgeSaldoString
        {
            get
            {
                return "Saldo: " + CashAmount.ToString() + " Chf";
            }
        }

        public override bool Equals(object obj)
        {
            var o = obj as BadgeInformation;
            if (o == null)
                return false;
            return CashAmount == o.CashAmount;
        }

        public override int GetHashCode()
        {
            return CashAmount.GetHashCode();
        }
    }
}
