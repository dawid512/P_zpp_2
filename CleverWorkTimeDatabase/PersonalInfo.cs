using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace testyBD.CleverWorkTimeDatabase
{
    public class PersonalInfo
    {
        [Key]
        public int id { get; set; }
        public int employmentInfo_id { get; set; }
        public string firtsName { get; set; }
        public string secondName { get; set; }
        public string lastName { get; set; }
        public string phoneNumber_1 { get; set; }
        public string phoneNumber_2 { get; set; }
        public string email{ get; set; }
        public string otherCommunicationMethod_Name { get; set; }
        public string otherCommunicationMethod_Adress { get; set; }
        public string adressCountry { get; set; }
        public string adressCity { get; set; }
        public string adressStreet { get; set; }
        public string adressHouseNumber { get; set; }
        public string adressAppartmentNumber { get; set; }

        public PersonalInfo(int employmentInfo_id, string firtsName, string secondName, string lastName, string phoneNumber_1, string phoneNumber_2, string email, string otherCommunicationMethod_Name, string otherCommunicationMethod_Adress, string adressCountry, string adressCity, string adressStreet, string adressHouseNumber, string adressAppartmentNumber)
        {
            this.employmentInfo_id = employmentInfo_id;
            this.firtsName = firtsName;
            this.secondName = secondName;
            this.lastName = lastName;
            this.phoneNumber_1 = phoneNumber_1;
            this.phoneNumber_2 = phoneNumber_2;
            this.email = email;
            this.otherCommunicationMethod_Name = otherCommunicationMethod_Name;
            this.otherCommunicationMethod_Adress = otherCommunicationMethod_Adress;
            this.adressCountry = adressCountry;
            this.adressCity = adressCity;
            this.adressStreet = adressStreet;
            this.adressHouseNumber = adressHouseNumber;
            this.adressAppartmentNumber = adressAppartmentNumber;
        }
        [Obsolete]
        public PersonalInfo()
        {
        }
    }
}
