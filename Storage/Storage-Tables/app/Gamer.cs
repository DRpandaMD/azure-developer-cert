using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure;


namespace app
{
    public class Gamer : TableEntity
    {
        public string Region {get {return this.PartitionKey; }}
        public string Email { get {return this.RowKey; }}
        public string Name { get; set; }
        public string Phone { get; set; }

        public Gamer() {}
        public Gamer (string email, string region, string name, string phone = null )
        {
            this.PartitionKey = region;
            this.RowKey = email;
            this.Name = name;
            this.Phone = phone;
        }

        public override string ToString()
        {
            return $"Region(pk):{Region} Email(rk):{Email} Name:{Name} Phone:{Phone} \n";
        }
    }   
}