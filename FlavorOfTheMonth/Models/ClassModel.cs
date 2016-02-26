using System;
using System.Collections.Generic;

namespace FlavorOfTheMonth.Models
{
    /// <summary>
    /// A list of all the WoW classes to be used in the filter dropdowns.
    /// </summary>
    public class ClassModel
    {
        #region Properties

        /// <summary>
        /// A list of strings to hold all of the names of the WoW classes.
        /// </summary>
        public List<string> ClassesList;

        /// <summary>
        /// A list of strings to hold all of the Death Knight specializations.
        /// </summary>
        public List<string> DeathKnightSpecList;

        /// <summary>
        /// A list of strings to hold all of the Druid specializations.
        /// </summary>
        public List<string> DruidSpecList;

        /// <summary>
        /// A list of strings to hold all of the Hunter specializations.
        /// </summary>
        public List<string> HunterSpecList;

        /// <summary>
        /// A list of strings to hold all of the Mage specializations.
        /// </summary>
        public List<string> MageSpecList;

        /// <summary>
        /// A list of strings to hold all of the Monk specializations.
        /// </summary>
        public List<string> MonkSpecList;

        /// <summary>
        /// A list of strings to hold all of the Paladin specializations.
        /// </summary>
        public List<string> PaladinSpecList;

        /// <summary>
        /// A list of strings to hold all of the Priest specializations.
        /// </summary>
        public List<string> PriestSpecList;

        /// <summary>
        /// A list of strings to hold all of the Rogue specializations.
        /// </summary>
        public List<string> RogueSpecList;

        /// <summary>
        /// A list of strings to hold all of the Shaman specializations.
        /// </summary>
        public List<string> ShamanSpecList;

        /// <summary>
        /// A list of strings to hold all of the Warlock specializations.
        /// </summary>
        public List<string> WarlockSpecList;

        /// <summary>
        /// A list of strings to hold all of the Warrior specializations.
        /// </summary>
        public List<string> WarriorSpecList;

        public Dictionary<string, string> SpecDicDK;
        public Dictionary<string, string> SpecDicDruid;
        public Dictionary<string, string> SpecDicHunter;
        public Dictionary<string, string> SpecDicMage;
        public Dictionary<string, string> SpecDicMonk;
        public Dictionary<string, string> SpecDicPaladin;
        public Dictionary<string, string> SpecDicPriest;
        public Dictionary<string, string> SpecDicRogue;
        public Dictionary<string, string> SpecDicShaman;
        public Dictionary<string, string> SpecDicWarlock;
        public Dictionary<string, string> SpecDicWarrior;

        #endregion Properties

        /// <summary>
        /// Constructor which initializes all of the string class and spec lists.
        /// </summary>
        public ClassModel()
        {
            ClassesList = new List<string>()
            {
                "Select a class...",
                "Death Knight",
                "Druid",
                "Hunter",
                "Mage",
                "Monk",
                "Paladin",
                "Priest",
                "Rogue",
                "Shaman",
                "Warlock",
                "Warrior"
            };

            // Dictionaries
            SpecDicDK = new Dictionary<string, string>()
            {
                { "Select a spec...", "Select a spec..." },
                { "Blood", "DK_BLOOD" },
                { "Frost", "DK_FROST" },
                { "Unholy", "DK_UNHOLY" }
            };

            SpecDicDruid = new Dictionary<string, string>()
            {
                { "Select a spec...", "Select a spec..." },
                { "Balance", "DRUID_BALANCE" },
                { "Feral", "DRUID_FERAL" },
                { "Guardian", "DRUID_GUARDIAN" },
                { "Restoration", "DRUID_RESTOR" }
            };

            SpecDicHunter = new Dictionary<string, string>()
            {
                { "Select a spec...", "Select a spec..." },
                { "Beast Mastery", "HUNTER_BEASTMASTER" },
                { "Marksmanship", "HUNTER_MARKSMAN" },
                { "Survival", "HUNTER_SURVIVAL" }
            };

            SpecDicMage = new Dictionary<string, string>()
            {
                { "Select a spec...", "Select a spec..." },
                { "Arcane", "MAGE_ARCANE" },
                { "Fire", "MAGE_FIRE" },
                { "Frost", "MAKE_FROST" }
            };

            SpecDicMonk = new Dictionary<string, string>()
            {
                { "Select a spec...", "Select a spec..." },
                { "Brewmaster", "MONK_BREWMASTER" },
                { "Mistweaver", "MONK_MISTWEAVER" },
                { "Windwalker", "MONK_WINDDANCER" }
            };

            SpecDicPaladin = new Dictionary<string, string>()
            {
                { "Select a spec...", "Select a spec..." },
                { "Holy", "PALADIN_HOLY" },
                { "Protection", "PALADIN_PROTECTION" },
                { "Retribution", "PALADIN_RETRIBUTION" }
            };

            SpecDicPriest = new Dictionary<string, string>()
            {
                { "Select a spec...", "Select a spec..." },
                { "Discipline", "PRIEST_DISCIPLINE" },
                { "Holy", "PRIEST_HOLY" },
                { "Shadow", "PRIEST_SHADOW" }
            };

            SpecDicRogue = new Dictionary<string, string>()
            {
                { "Select a spec...", "Select a spec..." },
                { "Assassination", "ROGUE_ASSASSINATION" },
                { "Combat", "ROGUE_COMBAT" },
                { "Subtlety", "ROGUE_SUBTLETY" }
            };

            SpecDicShaman = new Dictionary<string, string>()
            {
                { "Select a spec...", "Select a spec..." },
                { "Elemental", "SHAMAN_ELEMENTAL" },
                { "Enhancement", "SHAMAN_ENHANCEMENT" },
                { "Restoration", "SHAMAN_RESTORATION" }
            };

            SpecDicWarlock = new Dictionary<string, string>()
            {
                { "Select a spec...", "Select a spec..." },
                { "Affliction", "WARLOCK_AFFLICTION" },
                { "Demonology", "WARLOCK_DEMONOLOGY" },
                { "Destruction", "WARLOCK_DESTRUCTION" }
            };

            SpecDicWarrior = new Dictionary<string, string>()
            {
                { "Select a spec...", "Select a spec..." },
                { "Arms", "WARRIOR_ARMS" },
                { "Fury", "WARRIOR_FURY" },
                { "Protection", "WARRIOR_PROTECTION" }
            };

            DeathKnightSpecList = new List<string>()
            {
                "Select a spec...",
                "Blood",
                "Frost",
                "Unholy"
            };

            DruidSpecList = new List<string>()
            {
                "Select a spec...",
                "Balance",
                "Feral",
                "Guardian",
                "Restoration"
            };

            HunterSpecList = new List<string>()
            {
                "Select a spec...",
                "Beast Mastery",
                "Marksmanship",
                "Survival"
            };

            MageSpecList = new List<string>()
            {
                "Select a spec...",
                "Arcane",
                "Fire",
                "Frost"
            };

            MonkSpecList = new List<string>()
            {
                "Select a spec...",
                "Brewmaster",
                "Mistweaver",
                "Windwalker"
            };

            PaladinSpecList = new List<string>()
            {
                "Select a spec...",
                "Holy",
                "Protection",
                "Retribution"
            };

            PriestSpecList = new List<string>()
            {
                "Select a spec...",
                "Discipline",
                "Holy",
                "Shadow"
            };

            RogueSpecList = new List<string>()
            {
                "Select a spec...",
                "Assassination",
                "Combat",
                "Subtlety"
            };

            ShamanSpecList = new List<string>()
            {
                "Select a spec...",
                "Elemental",
                "Enhancement",
                "Restoration"
            };

            WarlockSpecList = new List<string>()
            {
                "Select a spec...",
                "Affliction",
                "Demonology",
                "Destruction"
            };

            WarriorSpecList = new List<string>()
            {
                "Select a spec...",
                "Arms",
                "Fury",
                "Protection"
            };
        }
    }
}