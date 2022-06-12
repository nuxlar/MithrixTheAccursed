using BepInEx.Configuration;

namespace MithrixTheAccursed
{
    internal class ModConfig
    {
        public static ConfigEntry<bool> accurse;
        public static ConfigEntry<bool> skipPhase2;
        public static ConfigEntry<bool> debuffImmune;
        public static ConfigEntry<bool> debuffResistance;
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

        public static ConfigEntry<int> SlamWaveProjectileCount;
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
        public static ConfigEntry<int> JumpWaveCount;
        public static ConfigEntry<float> ShardHoming;
        public static ConfigEntry<float> ShardRange;
        public static ConfigEntry<float> ShardCone;


        public static void InitConfig(ConfigFile config)
        {
            accurse = config.Bind("General", "Accurse", true, "Accurse the King of Nothing");
            debuffImmune = config.Bind("General", "Debuff Immune", false, "The curse renders the King immune to debuffs");
            debuffResistance = config.Bind("General", "Freeze/Nullify Immune", true, "Toggle the debuff resistance");

            basehealth = config.Bind("Stats", "BaseHealth", 1750f, "base health");
            levelhealth = config.Bind("Stats", "LevelHealth", 350f, "level health");
            basedamage = config.Bind("Stats", "BaseDamage", 18f, "base damage");
            leveldamage = config.Bind("Stats", "LevelDamage", 4f, "level damage");
            basearmor = config.Bind("Stats", "BaseArmor", 35f, "base armor");
            baseattackspeed = config.Bind("Stats", "BaseAttackSpeed", 1.25f, "base attack speed");

            basespeed = config.Bind("Movement", "BaseSpeed", 18f, "Mithrix's base movement speed");
            mass = config.Bind("Movement", "Mass", 5000f, "mass, recommended to increase if you increase his movement speed");
            turningspeed = config.Bind("Movement", "TurnSpeed", 1000f, "how fast mithrix turns");
            jumpingpower = config.Bind("Movement", "MoonShoes", 80f, "how hard mithrix jumps, vanilla is 25 for context");
            acceleration = config.Bind("Movement", "Acceleration", 350f, "acceleration");
            jumpcount = config.Bind("Movement", "JumpCount", 3, "jump count, probably doesn't do anything");
            aircontrol = config.Bind("Movement", "Aircontrol", 1.5f, "air control");

            PrimStocks = config.Bind("Skills", "PrimStocks", 2, "Max Stocks for Mithrix's Weapon Slam");
            SecStocks = config.Bind("Skills", "SecondaryStocks", 2, "Max Stocks for Mithrix's Dash Attack");
            UtilStocks = config.Bind("Skills", "UtilStocks", 4, "Max Stocks for Mithrix's Dash");
            PrimCD = config.Bind("Skills", "PrimCD", 3f, "Cooldown for Mithrix's Weapon Slam");
            SecCD = config.Bind("Skills", "SecCD", 3f, "Cooldown for Mithrix's Dash Attack");
            UtilCD = config.Bind("Skills", "UtilCD", 1f, "Cooldown for Mithrix's Dash");
            SpecialCD = config.Bind("Skills", "SpecialCD", 30f, "Cooldown for Mithrix's Jump Attack");

            SlamWaveProjectileCount = config.Bind("Skillmods", "Wave Projectile Count", 20, "Orbs and Waves fired by weapon slam in a circle");
            SecondaryFan = config.Bind("Skillmods", "FanCount", 5, "half the shards fired in a fan by the secondary skill");
            UtilityShotgun = config.Bind("Skillmods", "ShotgunCount", 5, "shots fired in a shotgun by utility");
            LunarShardAdd = config.Bind("Skillmods", "ShardAddCount", 5, "Bonus shards added to each shot of lunar shards");
            UltimateWaves = config.Bind("Skillmods", "WavePerShot", 18, "waves fired by ultimate per shot");
            UltimateCount = config.Bind("Skillmods", "WaveShots", 9, "Total shots of ultimate");
            UltimateDuration = config.Bind("Skillmods", "WaveDuration", 7.5f, "how long ultimate lasts");
            cloneduration = config.Bind("Skillmods", "CloneDuration", 30, "how long clones take to despawn (like happiest mask)");
            JumpRecast = config.Bind("Skillmods", "RecastChance", 0f, "chance mithrix has to recast his jump skill. USE WITH CAUTION.");
            JumpPause = config.Bind("Skillmods", "JumpDelay", 0.2f, "How long Mithrix spends in the air when using his jump special");
            JumpWaveCount = config.Bind("Skillmods", "Jump Wave Count", 24, "Shockwave count when Mithrix lands after a jump. Vanilla is 12");
            ShardHoming = config.Bind("Skillmods", "ShardHoming", 40f, "How strongly lunar shards home in to targets. Vanilla is 20f");
            ShardRange = config.Bind("Skillmods", "ShardRange", 100f, "Range (distance) in which shards look for targets");
            ShardCone = config.Bind("Skillmods", "ShardCone", 260f, "Cone (Angle) in which shards look for targets");
        }
    }
}
