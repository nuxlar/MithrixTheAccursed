using BepInEx.Configuration;
using RiskOfOptions;
using RiskOfOptions.Options;
using RiskOfOptions.OptionConfigs;

namespace MithrixTheAccursed
{
    internal class ModConfig
    {
        public static ConfigEntry<bool> phase1Changes;
        public static ConfigEntry<bool> phase2Mithrix;
        public static ConfigEntry<bool> phase2Doppel;
        public static ConfigEntry<bool> phase3Changes;
        //public static ConfigEntry<bool> phase4Changes;
        public static ConfigEntry<int> phase2Elite;
        public static ConfigEntry<bool> debuffImmune;
        public static ConfigEntry<bool> debuffResistance;

        public static ConfigEntry<float> phase1LoopScaling;
        public static ConfigEntry<float> phase2LoopScaling;
        public static ConfigEntry<float> phase3LoopScaling;
        public static ConfigEntry<float> phase4LoopScaling;
        public static ConfigEntry<float> phase1PlayerScaling;
        public static ConfigEntry<float> phase2PlayerScaling;
        public static ConfigEntry<float> phase3PlayerScaling;
        public static ConfigEntry<float> phase4PlayerScaling;

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
        public static ConfigEntry<int> cloneduration;
        public static ConfigEntry<float> JumpRecast;
        public static ConfigEntry<float> JumpPause;
        public static ConfigEntry<int> JumpWaveCount;
        public static ConfigEntry<float> ShardHoming;
        public static ConfigEntry<float> ShardRange;
        public static ConfigEntry<float> ShardCone;


        public static void InitConfig(ConfigFile config)
        {
            phase1Changes = config.Bind("General", "Phase 1 Changes", true, "Toggle P1 changes: the umbral clone after skyleap");
            phase2Mithrix = config.Bind("General", "Phase 2 Mithrix", true, "Toggle P2 change: Mithrix spawning in instead of chimera");
            phase2Doppel = config.Bind("General", "Phase 2 Doppel Event", true, "Toggle P2 change: The vengeance event triggering (player doppelgangers)");
            phase3Changes = config.Bind("General", "Phase 3 Changes", true, "Toggle P3 changes: Mithrix splitting into 2 and no clones on skyleap");
            //phase4Changes = config.Bind("General", "Phase 4 Changes", true, "Toggle the changes to Phase 4 (stationary Mithrix + reworked fist slam + pizza slices + glass clone)");
            phase2Elite = config.Bind("General", "Phase 2 Elite", 1, "Set Mithrix's Phase 2 elite (0: Off, 1: Malachite, 2: Perfected, 3: Voidtouched, 4: Blazing, 5: Overloading, 6: Glacial, 7: Random)");
            debuffResistance = config.Bind("General", "Freeze/Nullify Immune", false, "Toggle the debuff resistance for loop 1, will not turn off for loops 2 and up");
            debuffImmune = config.Bind("General", "Debuff Immune", false, "Toggle debuff immunity for loops 1 & 2, will not turn off for loops 3 and up");

            phase1BaseHPScaling = config.Bind("Scaling First Loop", "P1 HP", 0.0125f, "HP boost percentage for Phase 1 FIRST LOOP (5 stages)");
            phase2BaseHPScaling = config.Bind("Scaling First Loop", "P2 HP", 0.05f, "HP boost percentage for Phase 2 FIRST LOOP");
            phase3BaseHPScaling = config.Bind("Scaling First Loop", "P3 HP", 0.075f, "HP boost percentage for Phase 3 FIRST LOOP");
            phase4BaseHPScaling = config.Bind("Scaling First Loop", "P4 HP", 0.1f, "HP boost percentage for Phase 4 FIRST LOOP");
            phase1BaseMobilityScaling = config.Bind("Scaling First Loop", "P1 Mobility", 0.0125f, "Mobility boost percentage for Phase 1 FIRST LOOP");
            phase2BaseMobilityScaling = config.Bind("Scaling First Loop", "P2 Mobility", 0.025f, "Mobility boost percentage for Phase 2 FIRST LOOP");
            phase3BaseMobilityScaling = config.Bind("Scaling First Loop", "P3 Mobility", 0.05f, "Mobility boost percentage for Phase 3 FIRST LOOP");

            phase1LoopHPScaling = config.Bind("Scaling Per Loop", "P1 Loop HP", 0.05f, "HP boost percentage for Phase 1 PER LOOP (every 5 stages)");
            phase2LoopHPScaling = config.Bind("Scaling Per Loop", "P2 Loop HP", 0.075f, "HP boost percentage for Phase 2 PER LOOP");
            phase3LoopHPScaling = config.Bind("Scaling Per Loop", "P3 Loop HP", 0.1f, "HP boost percentage for Phase 3 PER LOOP");
            phase4LoopHPScaling = config.Bind("Scaling Per Loop", "P4 Loop HP", 0.15f, "HP boost percentage for Phase 4 PER LOOP");
            phase1LoopMobilityScaling = config.Bind("Scaling Per Loop", "P1 Loop Mobility", 0.025f, "Mobility (movementspd, acceleration, turningspd) boost percentage for Phase 1 PER LOOP");
            phase2LoopMobilityScaling = config.Bind("Scaling Per Loop", "P2 Loop Mobility", 0.05f, "Mobility boost percentage for Phase 2 PER LOOP");
            phase3LoopMobilityScaling = config.Bind("Scaling Per Loop", "P3 Loop Mobility", 0.075f, "Mobility boost percentage for Phase 3 PER LOOP");

            phase1PlayerScaling = config.Bind("Player Scaling", "P1 Scaling", 0.0125f, "Stat (HP + Mobility) boost percentage for Phase 1 PER PLAYER");
            phase2PlayerScaling = config.Bind("Player Scaling", "P2 Scaling", 0.025f, "Stat (HP + Mobility) boost percentage for Phase 2 PER PLAYER");
            phase3PlayerScaling = config.Bind("Player Scaling", "P3 Scaling", 0.05f, "Stat (HP + Mobility) boost percentage for Phase 3 PER PLAYER");
            phase4PlayerScaling = config.Bind("Player Scaling", "P4 Scaling", 0.075f, "Stat (HP) boost percentage for Phase 4 PER PLAYER");

            basehealth = config.Bind("Stats", "Base Health", 1400f, "Vanilla: 1400");
            levelhealth = config.Bind("Stats", "Level Health", 420f, "Health gained per level. Vanilla: 420");
            basedamage = config.Bind("Stats", "Base Damage", 15f, "Vanilla: 16");
            leveldamage = config.Bind("Stats", "Level Damage", 2.75f, "Damage gained per level. Vanilla: 3.2");
            basearmor = config.Bind("Stats", "Base Armor", 30f, "Vanilla: 20");
            baseattackspeed = config.Bind("Stats", "Base Attack Speed", 1.25f, "Vanilla: 1");

            basespeed = config.Bind("Movement", "Base Move Speed", 15f, "Vanilla: 15");
            mass = config.Bind("Movement", "Mass", 5000f, "Recommended to increase if you increase his movement speed. Vanilla: 900");
            turningspeed = config.Bind("Movement", "Turn Speed", 810f, "Vanilla: 270");
            jumpingpower = config.Bind("Movement", "Moon Shoes", 70f, "How high Mithrix jumps. Vanilla: 25");
            acceleration = config.Bind("Movement", "Acceleration", 350f, "Vanilla: 45");
            jumpcount = config.Bind("Movement", "Jump Count", 3, "Probably doesn't do anything. Vanilla: 0");
            aircontrol = config.Bind("Movement", "Air Control", 1.5f, "Vanilla: 0.25");

            PrimStocks = config.Bind("Skills", "Primary Stocks", 2, "Max Stocks for Mithrix's Weapon Slam. Vanilla: 1");
            SecStocks = config.Bind("Skills", "Secondary Stocks", 2, "Max Stocks for Mithrix's Dash Attack. Vanilla: 1");
            UtilStocks = config.Bind("Skills", "Util Stocks", 3, "Max Stocks for Mithrix's Dash. Vanilla: 2");
            PrimCD = config.Bind("Skills", "Primary Cooldown", 3f, "Cooldown for Mithrix's Weapon Slam. Vanilla: 4");
            SecCD = config.Bind("Skills", "Secondary Cooldown", 3.5f, "Cooldown for Mithrix's Dash Attack. Vanilla: 5");
            UtilCD = config.Bind("Skills", "Util Cooldown", 1.5f, "Cooldown for Mithrix's Dash. Vanilla: 3");
            SpecialCD = config.Bind("Skills", "Special Cooldown", 30f, "Cooldown for Mithrix's Jump Attack. Vanilla: 30");

            SlamWaveProjectileCount = config.Bind("Skill Mods", "Wave Projectile Count", 8, "Orbs and Waves fired by weapon slam in a circle. Vanilla: N/A");
            SecondaryFan = config.Bind("Skill Mods", "Fan Count", 3, "half the shards fired in a fan by the secondary skill. Vanilla: N/A");
            UtilityShotgun = config.Bind("Skill Mods", "Shotgun Count", 3, "shots fired in a shotgun by utility. Vanilla: N/A");
            LunarShardAdd = config.Bind("Skill Mods", "Shard Add Count", 1, "Bonus shards added to each shot of lunar shards. Vanilla: N/A");
            UltimateWaves = config.Bind("Skill Mods", "Wave Per Shot", 10, "Waves fired by ultimate per shot. Vanilla: 4");
            UltimateCount = config.Bind("Skill Mods", "Wave Shots", 8, "Total shots of ultimate. Vanilla: 4");
            UltimateDuration = config.Bind("Skill Mods", "Wave Duration", 8f, "how long ultimate lasts. Vanilla: 8");
            cloneduration = config.Bind("Skill Mods", "Clone Duration", 30, "how long clones take to despawn. Vanilla: 0");
            JumpRecast = config.Bind("Skill Mods", "Recast Chance", 0f, "Chance Mithrix has to recast his jump skill. USE WITH CAUTION. Vanilla: 0");
            JumpPause = config.Bind("Skill Mods", "Jump Delay", 0.3f, "How long Mithrix spends in the air when using his jump special. Vanilla: 3");
            JumpWaveCount = config.Bind("Skill Mods", "Jump Wave Count", 18, "Shockwave count when Mithrix lands after a jump. Vanilla: 12");
            ShardHoming = config.Bind("Skill Mods", "Shard Homing", 25f, "How strongly lunar shards home in to targets. Vanilla: 20");
            ShardRange = config.Bind("Skill Mods", "Shard Range", 100f, "Range (distance) in which shards look for targets. Vanilla: 80");
            ShardCone = config.Bind("Skill Mods", "Shard Cone", 120f, "Cone (Angle) in which shards look for targets. Vanilla: 90");

            // Risk Of Options Setup
            // General
            ModSettingsManager.AddOption(new CheckBoxOption(phase1Changes));
            ModSettingsManager.AddOption(new CheckBoxOption(phase2Mithrix));
            ModSettingsManager.AddOption(new CheckBoxOption(phase2Doppel));
            ModSettingsManager.AddOption(new CheckBoxOption(phase3Changes));
            //ModSettingsManager.AddOption(new CheckBoxOption(phase4Changes));
            ModSettingsManager.AddOption(new CheckBoxOption(debuffImmune));
            ModSettingsManager.AddOption(new CheckBoxOption(debuffResistance));
            ModSettingsManager.AddOption(new IntSliderOption(phase2Elite, new IntSliderConfig() { min = 0, max = 7 }));
            // Scaling
            ModSettingsManager.AddOption(new StepSliderOption(phase1LoopScaling, new StepSliderConfig() { min = 0, max = 1, increment = 0.025f }));
            ModSettingsManager.AddOption(new StepSliderOption(phase2LoopScaling, new StepSliderConfig() { min = 0, max = 1, increment = 0.025f }));
            ModSettingsManager.AddOption(new StepSliderOption(phase3LoopScaling, new StepSliderConfig() { min = 0, max = 1, increment = 0.025f }));
            ModSettingsManager.AddOption(new StepSliderOption(phase4LoopScaling, new StepSliderConfig() { min = 0, max = 1, increment = 0.025f }));
            ModSettingsManager.AddOption(new StepSliderOption(phase1PlayerScaling, new StepSliderConfig() { min = 0, max = 1, increment = 0.0125f }));
            ModSettingsManager.AddOption(new StepSliderOption(phase2PlayerScaling, new StepSliderConfig() { min = 0, max = 1, increment = 0.0125f }));
            ModSettingsManager.AddOption(new StepSliderOption(phase3PlayerScaling, new StepSliderConfig() { min = 0, max = 1, increment = 0.0125f }));
            ModSettingsManager.AddOption(new StepSliderOption(phase4PlayerScaling, new StepSliderConfig() { min = 0, max = 1, increment = 0.0125f }));
            // Stats
            ModSettingsManager.AddOption(new StepSliderOption(basehealth, new StepSliderConfig() { min = 1400, max = 10000, increment = 50f }));
            ModSettingsManager.AddOption(new StepSliderOption(levelhealth, new StepSliderConfig() { min = 100, max = 1000, increment = 25f }));
            ModSettingsManager.AddOption(new StepSliderOption(basedamage, new StepSliderConfig() { min = 10, max = 100, increment = 1f }));
            ModSettingsManager.AddOption(new StepSliderOption(leveldamage, new StepSliderConfig() { min = 1, max = 10, increment = 0.25f }));
            ModSettingsManager.AddOption(new StepSliderOption(basearmor, new StepSliderConfig() { min = 5, max = 100, increment = 5f }));
            ModSettingsManager.AddOption(new StepSliderOption(baseattackspeed, new StepSliderConfig() { min = 0.25f, max = 10, increment = 0.25f }));
            // Movement
            ModSettingsManager.AddOption(new StepSliderOption(basespeed, new StepSliderConfig() { min = 10, max = 50, increment = 1f }));
            ModSettingsManager.AddOption(new StepSliderOption(mass, new StepSliderConfig() { min = 900, max = 10000, increment = 100f }));
            ModSettingsManager.AddOption(new StepSliderOption(turningspeed, new StepSliderConfig() { min = 100, max = 1000, increment = 10f }));
            ModSettingsManager.AddOption(new StepSliderOption(jumpingpower, new StepSliderConfig() { min = 25, max = 100, increment = 5f }));
            ModSettingsManager.AddOption(new StepSliderOption(acceleration, new StepSliderConfig() { min = 45, max = 500, increment = 5f }));
            ModSettingsManager.AddOption(new IntSliderOption(jumpcount, new IntSliderConfig() { min = 1, max = 10}));
            ModSettingsManager.AddOption(new StepSliderOption(aircontrol, new StepSliderConfig() { min = 0.25f, max = 5, increment = 0.25f }));
            // Skills
            ModSettingsManager.AddOption(new IntSliderOption(PrimStocks, new IntSliderConfig() { min = 1, max = 10 }));
            ModSettingsManager.AddOption(new IntSliderOption(SecStocks, new IntSliderConfig() { min = 1, max = 10 }));
            ModSettingsManager.AddOption(new IntSliderOption(UtilStocks, new IntSliderConfig() { min = 1, max = 10 }));
            ModSettingsManager.AddOption(new StepSliderOption(PrimCD, new StepSliderConfig() { min = 1, max = 10, increment = 0.25f }));
            ModSettingsManager.AddOption(new StepSliderOption(SecCD, new StepSliderConfig() { min = 1, max = 10, increment = 0.25f }));
            ModSettingsManager.AddOption(new StepSliderOption(UtilCD, new StepSliderConfig() { min = 1, max = 10, increment = 0.25f }));
            ModSettingsManager.AddOption(new StepSliderOption(SpecialCD, new StepSliderConfig() { min = 10, max = 50, increment = 1f }));
            // Skill Mods
            ModSettingsManager.AddOption(new IntSliderOption(SlamWaveProjectileCount, new IntSliderConfig() { min = 0, max = 24 }));
            ModSettingsManager.AddOption(new IntSliderOption(SecondaryFan, new IntSliderConfig() { min = 1, max = 10 }));
            ModSettingsManager.AddOption(new IntSliderOption(UtilityShotgun, new IntSliderConfig() { min = 1, max = 10 }));
            ModSettingsManager.AddOption(new IntSliderOption(LunarShardAdd, new IntSliderConfig() { min = 1, max = 20 }));
            ModSettingsManager.AddOption(new IntSliderOption(UltimateWaves, new IntSliderConfig() { min = 1, max = 20 }));
            ModSettingsManager.AddOption(new StepSliderOption(UltimateDuration, new StepSliderConfig() { min = 1, max = 10, increment = 0.25f }));
            ModSettingsManager.AddOption(new IntSliderOption(cloneduration, new IntSliderConfig() { min = 10, max = 60 }));
            ModSettingsManager.AddOption(new StepSliderOption(JumpRecast, new StepSliderConfig() { min = 0, max = 1, increment = 0.025f }));
            ModSettingsManager.AddOption(new StepSliderOption(JumpPause, new StepSliderConfig() { min = 0.1f, max = 3, increment = 0.1f }));
            ModSettingsManager.AddOption(new IntSliderOption(JumpWaveCount, new IntSliderConfig() { min = 1, max = 24 }));
            ModSettingsManager.AddOption(new StepSliderOption(ShardHoming, new StepSliderConfig() { min = 10f, max = 100, increment = 5f }));
            ModSettingsManager.AddOption(new StepSliderOption(ShardRange, new StepSliderConfig() { min = 50, max = 200, increment = 10f }));
            ModSettingsManager.AddOption(new StepSliderOption(ShardCone, new StepSliderConfig() { min = 80f, max = 180, increment = 10f }));

            ModSettingsManager.SetModDescription("The Accursed King of Nothing uses shadows and indiscriminate firepower to overwhelm all who oppose him.");
        }
    }
}
