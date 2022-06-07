using BepInEx.Configuration;

namespace MithrixTheAccursed
{
    internal class ModConfig
    {
        public static ConfigEntry<bool> accurse;
        public static ConfigEntry<bool> skipPhase2;
        public static ConfigEntry<int> coatCount;
        public static ConfigEntry<float> coatRegen;
        public static ConfigEntry<bool> debuffImmune;
        public static ConfigEntry<bool> malachiteMithrix;

        public static ConfigEntry<float> basehealth;
        public static ConfigEntry<float> levelhealth;
        public static ConfigEntry<float> basedamage;
        public static ConfigEntry<float> leveldamage;
        public static ConfigEntry<float> basearmor;
        public static ConfigEntry<float> baseattackspeed;

        public static ConfigEntry<float> basespeed;
        public static ConfigEntry<float> mass;
        public static ConfigEntry<float> turningspeed;
        public static ConfigEntry<float> jumpingpower;
        public static ConfigEntry<float> acceleration;
        public static ConfigEntry<int> jumpcount;
        public static ConfigEntry<float> aircontrol;

        public static ConfigEntry<int> PrimStocks;
        public static ConfigEntry<int> SecStocks;
        public static ConfigEntry<int> UtilStocks;
        public static ConfigEntry<float> PrimCD;
        public static ConfigEntry<float> SecCD;
        public static ConfigEntry<float> UtilCD;
        public static ConfigEntry<float> SpecialCD;

        public static ConfigEntry<float> SlamOrbCount;
        public static ConfigEntry<int> SecondaryFan;
        public static ConfigEntry<int> UtilityShotgun;
        public static ConfigEntry<int> LunarShardAdd;
        public static ConfigEntry<int> UltimateWaves;
        public static ConfigEntry<int> UltimateCount;
        public static ConfigEntry<float> UltimateDuration;
        public static ConfigEntry<int> clonecount;
        public static ConfigEntry<int> cloneduration;
        public static ConfigEntry<float> JumpRecast;
        public static ConfigEntry<float> JumpPause;
        public static ConfigEntry<float> ShardHoming;
        public static ConfigEntry<float> ShardRange;
        public static ConfigEntry<float> ShardCone;


        public static void InitConfig(ConfigFile config)
        {
            accurse = config.Bind("General", "Accurse", true, "Accurse the King of Nothing");
            skipPhase2 = config.Bind("General", "Skip Phase 2", true, "Skips the phase where only Lunar Chimera spawn");
            coatCount = config.Bind("General", "Coat Count", 10, "Instead of being completely immune add raincoats (without the barrier)");
            coatRegen = config.Bind("General", "Coat Regen", 1f, "How much time before a stack of raincoat block regens");
            debuffImmune = config.Bind("General", "Debuff Immune", false, "The curse renders the King immune to debuffs");
            malachiteMithrix = config.Bind("General", "Malachite Mithrix", false, "The curse corrupts the King's form (might make this appearance only or just keep it as an extra challenge)");

            basehealth = config.Bind("Stats", "BaseHealth", 1400f, "base health");
            levelhealth = config.Bind("Stats", "LevelHealth", 420f, "level health");
            basedamage = config.Bind("Stats", "BaseDamage", 16f, "base damage");
            leveldamage = config.Bind("Stats", "LevelDamage", 3.2f, "level damage");
            basearmor = config.Bind("Stats", "BaseArmor", 20f, "base armor");
            baseattackspeed = config.Bind("Stats", "BaseAttackSpeed", 1f, "base attack speed");

            basespeed = config.Bind("Movement", "BaseSpeed", 18f, "Mithrix's base movement speed");
            mass = config.Bind("Movement", "Mass", 1200f, "mass, recommended to increase if you increase his movement speed");
            turningspeed = config.Bind("Movement", "TurnSpeed", 900f, "how fast mithrix turns");
            jumpingpower = config.Bind("Movement", "MoonShoes", 75f, "how hard mithrix jumps, vanilla is 25 for context");
            acceleration = config.Bind("Movement", "Acceleration", 180f, "acceleration");
            jumpcount = config.Bind("Movement", "JumpCount", 3, "jump count, probably doesn't do anything");
            aircontrol = config.Bind("Movement", "Aircontrol", 1f, "air control");

            PrimStocks = config.Bind("Skills", "PrimStocks", 2, "Max Stocks for Mithrix's Weapon Slam");
            SecStocks = config.Bind("Skills", "SecondaryStocks", 2, "Max Stocks for Mithrix's Dash Attack");
            UtilStocks = config.Bind("Skills", "UtilStocks", 4, "Max Stocks for Mithrix's Dash");
            PrimCD = config.Bind("Skills", "PrimCD", 3f, "Cooldown for Mithrix's Weapon Slam");
            SecCD = config.Bind("Skills", "SecCD", 3f, "Cooldown for Mithrix's Dash Attack");
            UtilCD = config.Bind("Skills", "UtilCD", 1f, "Cooldown for Mithrix's Dash");
            SpecialCD = config.Bind("Skills", "SpecialCD", 30f, "Cooldown for Mithrix's Jump Attack");

            SlamOrbCount = config.Bind("Skillmods", "OrbCount", 16f, "Orbs fired by weapon slam in a circle, note, set this to an integer");
            SecondaryFan = config.Bind("Skillmods", "FanCount", 5, "half the shards fired in a fan by the secondary skill");
            UtilityShotgun = config.Bind("Skillmods", "ShotgunCount", 5, "shots fired in a shotgun by utility");
            LunarShardAdd = config.Bind("Skillmods", "ShardAddCount", 5, "Bonus shards added to each shot of lunar shards");
            UltimateWaves = config.Bind("Skillmods", "WavePerShot", 16, "waves fired by ultimate per shot");
            UltimateCount = config.Bind("Skillmods", "WaveShots", 6, "Total shots of ultimate");
            UltimateDuration = config.Bind("Skillmods", "WaveDuration", 5.5f, "how long ultimate lasts");
            clonecount = config.Bind("Skillmods", "CloneCount", 2, "clones spawned in phase 3 by jump attack");
            cloneduration = config.Bind("Skillmods", "CloneDuration", 30, "how long clones take to despawn (like happiest mask)");
            JumpRecast = config.Bind("Skillmods", "RecastChance", 0f, "chance mithrix has to recast his jump skill. USE WITH CAUTION.");
            JumpPause = config.Bind("Skillmods", "JumpDelay", 0.2f, "How long Mithrix spends in the air when using his jump special");
            ShardHoming = config.Bind("Skillmods", "ShardHoming", 30f, "How strongly lunar shards home in to targets. Vanilla is 20f");
            ShardRange = config.Bind("Skillmods", "ShardRange", 60f, "Range (distance) in which shards look for targets");
            ShardCone = config.Bind("Skillmods", "ShardCone", 180f, "Cone (Angle) in which shards look for targets");
        }
    }
}
