
using System;

namespace ArcFaceService.Entity
{
    public class PersonResult
    {
        public string personId { get; set; }
        public string personCode { get; set; }
        public string personName { get; set; }
        public string facePath { get; set; }
        public float scoreMatching { get; set; }
        public string passpord { get; set; }
        public string phonenumber { get; set; }
        public DateTime birthday { get; set; }

        public PersonResult(string personId, string personCode, string personName, string facePath, float scoreMatching, string passpord, string phonenumber, DateTime birthday)
        {
            this.personId = personId;
            this.personCode = personCode;
            this.personName = personName;
            this.facePath = facePath;
            this.scoreMatching = scoreMatching;
            this.passpord = passpord;
            this.phonenumber = phonenumber;
            this.birthday = birthday;
        }
    }
}
