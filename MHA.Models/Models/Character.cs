using MHA.Tools.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace MHA.Models.Models
{
    [CollectionName("characters")]
    public class Character : ModelBase
    {
        public string Affiliation { get; set; }
        public string Birthday { get; set; }
        public string Bloodtype { get; set; }
        public string Description { get; set; }
        public string Eye { get; set; }
        public string Fightstyle { get; set; }
        public string Gender { get; set; }
        public string Hair { get; set; }
        public string Height { get; set; }
        public string Kanji { get; set; }
        public string Occupation { get; set; }
        public string Quirk { get; set; }
        public string Romaji { get; set; }
        public string Status { get; set; }
        public string Teams { get; set; }
        public List<string> Images { get; set; }
        public string Epithet { get; set; }
        public List<string> Ages { get; set; }
        public List<string> Family { get; set; }
        public string UserId { get; set; }
        public Boolean Custom { get; set; }
    }
}
