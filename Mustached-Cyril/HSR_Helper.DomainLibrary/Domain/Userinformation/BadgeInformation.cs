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
    }
}
