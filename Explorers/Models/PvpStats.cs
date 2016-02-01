using System;
using System.Runtime.Serialization;

namespace WowDotNetAPI.Models
{
    [DataContract]
    public class PvpStats
    {
        [DataMember(Name = "ranking")]
        public int Ranking { get; set; }

        [DataMember(Name = "rating")]
        public int Rating { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "realmId")]
        public int RealmId { get; set; }

        [DataMember(Name = "realmName")]
        public string RealmName { get; set; }

        [DataMember(Name = "realmSlug")]
        public string RealmSlug { get; set; }

        [DataMember(Name = "raceId")]
        public int RaceId { get; set; }

        [DataMember(Name = "classId")]
        public int ClassId { get; set; }

        [DataMember(Name = "specId")]
        public int SpecId { get; set; }

        [DataMember(Name = "factionId")]
        public int FactionId { get; set; }

        [DataMember(Name = "genderId")]
        public int GenderId { get; set; }

        [DataMember(Name = "seasonWins")]
        public int SeasonWins { get; set; }

        [DataMember(Name = "seasonLosses")]
        public int SeasonLosses { get; set; }

        [DataMember(Name = "weeklyWins")]
        public int WeeklyWins { get; set; }

        [DataMember(Name = "weeklyLosses")]
        public int WeeklyLosses { get; set; }

        public string Class => (string)Enum.Parse(typeof(CharacterClass), Enum.GetName(typeof(CharacterClass), ClassId).Replace(' ', '_'));
        public string Race => (string)Enum.Parse(typeof(CharacterRace), Enum.GetName(typeof(CharacterRace), RaceId).Replace(' ', '_'));
        public string Gender => (string)Enum.Parse(typeof(CharacterGender), Enum.GetName(typeof(CharacterGender), GenderId).Replace(' ', '_'));
        public string Spec => (string)Enum.Parse(typeof(CharacterSpec), Enum.GetName(typeof(CharacterSpec), SpecId).Replace(' ', '_'));
    }
}
