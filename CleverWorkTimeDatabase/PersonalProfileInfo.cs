using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace testyBD.CleverWorkTimeDatabase
{
    public class PersonalProfileInfo
    {
        //zdublowalem numery telefonow i adresy stron stron, nwm czy jest sens robic osobne tabele na wypadetgdyby ktos mial dwa
        [Key]
        public int id { get; set; }
        public int PersonalInfo_id { get; set; }
        public string nickName { get; set; }
        public string firtsName { get; set; }
        public string secondName { get; set; }
        public string lastName { get; set; }
        public string phoneNumber_1 { get; set; }
        public string phoneNumber_2 { get; set; }
        public string email { get; set; }
        public string website_1 { get; set; } 
        public string website_2 { get; set; }
        public string InfoBox { get; set; }
        //ustawienia profilu
        public bool Display_Realname { get; set; } //true wyswietl imie nazwisko, false wyswietl nickname
        public bool Display_phoneNumber { get; set; }
        public bool Display_email { get; set; }
        public bool Display_ProfileSetPrivate { get; set; }
        public bool Display_Website { get; set; }

        public PersonalProfileInfo(int personalInfo_id, string nickName, string firtsName, string secondName, string lastName, string phoneNumber_1, string phoneNumber_2, string email, string website_1, string website_2, string infoBox, bool display_Realname, bool display_phoneNumber, bool display_email, bool display_ProfileSetPrivate, bool display_Website)
        {
            PersonalInfo_id = personalInfo_id;
            this.nickName = nickName;
            this.firtsName = firtsName;
            this.secondName = secondName;
            this.lastName = lastName;
            this.phoneNumber_1 = phoneNumber_1;
            this.phoneNumber_2 = phoneNumber_2;
            this.email = email;
            this.website_1 = website_1;
            this.website_2 = website_2;
            InfoBox = infoBox;
            Display_Realname = display_Realname;
            Display_phoneNumber = display_phoneNumber;
            Display_email = display_email;
            Display_ProfileSetPrivate = display_ProfileSetPrivate;
            Display_Website = display_Website;
        }
        [Obsolete]
        public PersonalProfileInfo()
        {
        }
    }
}
