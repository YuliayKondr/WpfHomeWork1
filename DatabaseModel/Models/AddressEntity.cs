﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseModel.Models
{
    public class AddressEntity
    {
        public AddressEntity(string city, string streetName, string streetAddress, string zipCode, string state, string country, Coordinates coordinates)
        {
            City = city;
            StreetName = streetName;
            StreetAddress = streetAddress;
            ZipCode = zipCode;
            State = state;
            Country = country;
            Coordinates = coordinates;
        }

        public AddressEntity()
        {
            
        }

        public int Id { get; protected set; }        

        public string City { get; protected set; }

        public string StreetName { get; protected set; }
       
        public string StreetAddress { get; protected set; }
        
        public string ZipCode { get; protected set; }
        
        public string State { get; protected set; }
        
        public string Country { get; protected set; }

        public string CoordinatesJson { get; protected set; }

        public Coordinates Coordinates 
        {
            get => !string.IsNullOrEmpty(CoordinatesJson) ? JsonConvert.DeserializeObject<Coordinates>(CoordinatesJson) : new Coordinates();
            set => CoordinatesJson = value != null ? JsonConvert.SerializeObject(value) : string.Empty;
        }

        public EmployeeEntity Employee { get; protected set; }
    }
}
