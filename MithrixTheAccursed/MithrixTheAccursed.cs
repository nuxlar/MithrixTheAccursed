using BepInEx;
using RoR2;
using RoR2.Projectile;
using EntityStates.BrotherMonster;
using EntityStates.BrotherMonster.Weapon;
using UnityEngine;
using RoR2.Skills;
using System;
using UnityEngine.Networking;
using UnityEngine.AddressableAssets;

namespace MithrixTheAccursed
{
    [BepInPlugin("com.zorp.MithrixTheAccursed", "MithrixTheAccursed", "1.1.0")]
    [BepInDependency("com.rune580.riskofoptions")]
    public class MithrixTheAccursed : BaseUnityPlugin
    {
        private bool hasfired;
        int phaseCounter = 0;
        GameObject Mithrix = LegacyResourcesAPI.Load<GameObject>("Prefabs/Characterbodies/BrotherBody");
        public void Awake()
        {
            ModConfig.InitConfig(Config);
            if (ModConfig.accurse.Value)
            {
                On.RoR2.Run.Start += OnRunStart;
                On.RoR2.CharacterMaster.OnBodyStart += CharacterMasterOnBodyStart;
                On.RoR2.CharacterModel.UpdateOverlays += UpdateOverlays;
                On.EntityStates.Missions.BrotherEncounter.BrotherEncounterPhaseBaseState.OnEnter += BrotherEncounterPhaseBaseStateOnEnter;
                On.EntityStates.Missions.BrotherEncounter.Phase2.OnEnter += Phase2OnEnter;
                On.EntityStates.Missions.BrotherEncounter.Phase3.OnEnter += Phase3OnEnter;
                On.EntityStates.Missions.BrotherEncounter.Phase4.OnEnter += Phase4OnEnter;
                if (ModConfig.debuffImmune.Value)
                    On.RoR2.Items.ImmuneToDebuffBehavior.TryApplyOverride += TryApplyOverride;
                CharacterMaster.onStartGlobal += new Action<CharacterMaster>(MasterChanges);
                On.EntityStates.BrotherMonster.EnterSkyLeap.OnEnter += EnterSkyLeapOnEnter;
                On.EntityStates.BrotherMonster.ExitSkyLeap.OnEnter += ExitSkyLeapOnEnter;
                if (ModConfig.debuffResistance.Value)
                {
                    On.EntityStates.FrozenState.OnEnter += FrozenStateOnEnter;
                    On.RoR2.CharacterBody.AddTimedBuff_BuffDef_float += AddTimedBuff_BuffDef_float;
                }
                On.EntityStates.BrotherMonster.SlideIntroState.OnEnter += SlideIntroStateOnEnter;
                On.EntityStates.BrotherMonster.SprintBash.OnEnter += SprintBashOnEnter;
                On.EntityStates.BrotherMonster.WeaponSlam.OnEnter += WeaponSlamOnEnter;
                On.EntityStates.BrotherMonster.WeaponSlam.FixedUpdate += WeaponSlamFixedUpdate;
                On.EntityStates.BrotherMonster.Weapon.FireLunarShards.OnEnter += FireLunarShardsOnEnter;
                On.EntityStates.BrotherMonster.FistSlam.OnEnter += FistSlamOnEnter;
                On.EntityStates.BrotherMonster.SpellChannelEnterState.OnEnter += SpellChannelEnterStateOnEnter;
                On.EntityStates.BrotherMonster.SpellChannelState.OnEnter += SpellChannelStateOnEnter;
                On.EntityStates.BrotherMonster.SpellChannelExitState.OnEnter += SpellChannelExitStateOnEnter;
                On.EntityStates.BrotherMonster.StaggerEnter.OnEnter += StaggerEnterOnEnter;
                On.EntityStates.BrotherMonster.StaggerExit.OnEnter += StaggerExitOnEnter;
                On.EntityStates.BrotherMonster.StaggerLoop.OnEnter += StaggerLoopOnEnter;
                On.EntityStates.BrotherMonster.TrueDeathState.OnEnter += TrueDeathStateOnEnter;
                On.EntityStates.BrotherMonster.WeaponSlam.OnEnter -= CleanupPillar;
            }
        }

        private void AdjustBaseStats()
        {
            // increases most stats by 5% per loop and 1.25% per player
            Logger.LogMessage("Adjusting Phase 1 Stats");
            int playerCount = PlayerCharacterMasterController.instances.Count;
            float multiplier = (ModConfig.phase1LoopScaling.Value * Run.instance.loopClearCount) + (ModConfig.phase1PlayerScaling.Value * playerCount);
            CharacterBody MithrixBody = Mithrix.GetComponent<CharacterBody>();
            CharacterDirection MithrixDirection = Mithrix.GetComponent<CharacterDirection>();
            CharacterMotor MithrixMotor = Mithrix.GetComponent<CharacterMotor>();

            MithrixMotor.mass = ModConfig.mass.Value;
            MithrixMotor.airControl = ModConfig.aircontrol.Value;
            MithrixMotor.jumpCount = ModConfig.jumpcount.Value;

            if (ModConfig.umbralMithrix.Value)
            {
                MithrixBody.baseMaxHealth = (ModConfig.basehealth.Value + (ModConfig.basehealth.Value * multiplier)) / 10;
                MithrixBody.levelMaxHealth = (ModConfig.levelhealth.Value + (ModConfig.levelhealth.Value * multiplier)) / 10;
                MithrixBody.baseDamage = (ModConfig.basedamage.Value + (ModConfig.basedamage.Value * multiplier)) * 100 / 4;
                MithrixBody.levelDamage = (ModConfig.leveldamage.Value + (ModConfig.leveldamage.Value * multiplier)) * 100 / 4;

            } else
            {
                MithrixBody.baseMaxHealth = ModConfig.basehealth.Value + (ModConfig.basehealth.Value * multiplier);
                MithrixBody.levelMaxHealth = ModConfig.levelhealth.Value + (ModConfig.levelhealth.Value * multiplier);
                MithrixBody.baseDamage = ModConfig.basedamage.Value + (ModConfig.basedamage.Value * multiplier);
                MithrixBody.levelDamage = ModConfig.leveldamage.Value + (ModConfig.leveldamage.Value * multiplier);
            }

            MithrixBody.baseAttackSpeed = ModConfig.baseattackspeed.Value + (ModConfig.baseattackspeed.Value * multiplier);

            MithrixBody.baseMoveSpeed = ModConfig.basespeed.Value + (ModConfig.basespeed.Value * multiplier);
            MithrixBody.baseAcceleration = ModConfig.acceleration.Value + (ModConfig.acceleration.Value * multiplier);
            MithrixBody.baseJumpPower = ModConfig.jumpingpower.Value + (ModConfig.jumpingpower.Value * multiplier);
            MithrixDirection.turnSpeed = ModConfig.turningspeed.Value + (ModConfig.turningspeed.Value * multiplier);

            MithrixBody.baseArmor = ModConfig.basearmor.Value + (ModConfig.basearmor.Value * multiplier);

            ProjectileSteerTowardTarget component = FireLunarShards.projectilePrefab.GetComponent<ProjectileSteerTowardTarget>();
            component.rotationSpeed = ModConfig.ShardHoming.Value + (ModConfig.ShardHoming.Value * multiplier);
            ProjectileDirectionalTargetFinder component2 = FireLunarShards.projectilePrefab.GetComponent<ProjectileDirectionalTargetFinder>();
            component2.lookRange = ModConfig.ShardRange.Value + (ModConfig.ShardRange.Value * multiplier);
            component2.lookCone = ModConfig.ShardCone.Value + (ModConfig.ShardCone.Value * multiplier);
            component2.allowTargetLoss = true;

            WeaponSlam.duration = (3.5f / ModConfig.baseattackspeed.Value);
            HoldSkyLeap.duration = ModConfig.JumpPause.Value - (ModConfig.JumpPause.Value * multiplier);
            ExitSkyLeap.waveProjectileCount = ModConfig.JumpWaveCount.Value;
            ExitSkyLeap.cloneDuration = ModConfig.cloneduration.Value;
            ExitSkyLeap.recastChance = ModConfig.JumpRecast.Value;
            UltChannelState.waveProjectileCount = ModConfig.UltimateWaves.Value;
            UltChannelState.maxDuration = ModConfig.UltimateDuration.Value;
            UltChannelState.totalWaves = ModConfig.UltimateCount.Value;
        }

        private void AdjustBaseSkills()
        {
            SkillLocator SklLocate = Mithrix.GetComponent<SkillLocator>();
            SkillFamily Hammer = SklLocate.primary.skillFamily;
            SkillDef HammerChange = Hammer.variants[0].skillDef;
            HammerChange.baseRechargeInterval = ModConfig.PrimCD.Value;
            HammerChange.baseMaxStock = ModConfig.PrimStocks.Value;

            SkillFamily Bash = SklLocate.secondary.skillFamily;
            SkillDef BashChange = Bash.variants[0].skillDef;
            BashChange.baseRechargeInterval = ModConfig.SecCD.Value;
            BashChange.baseMaxStock = ModConfig.SecStocks.Value;

            SkillFamily Dash = SklLocate.utility.skillFamily;
            SkillDef DashChange = Dash.variants[0].skillDef;
            DashChange.baseRechargeInterval = ModConfig.UtilCD.Value;
            DashChange.baseMaxStock = ModConfig.UtilStocks.Value;

            SkillFamily Ult = SklLocate.special.skillFamily;
            SkillDef UltChange = Ult.variants[0].skillDef;
            UltChange.baseRechargeInterval = ModConfig.SpecialCD.Value;
            UltChange.baseMaxStock = 5;
        }

        private void AdjustPhase2Stats()
        {
            // increases most stats by 10% per loop and 2.5% per player
            Logger.LogMessage("Adjusting Phase 2 Stats");
            int playerCount = PlayerCharacterMasterController.instances.Count;
            float multiplier = (ModConfig.phase2LoopScaling.Value * Run.instance.loopClearCount) + (ModConfig.phase2PlayerScaling.Value * playerCount);
            CharacterBody MithrixBody = Mithrix.GetComponent<CharacterBody>();
            CharacterDirection MithrixDirection = Mithrix.GetComponent<CharacterDirection>();

            if (ModConfig.umbralMithrix.Value)
            {
                MithrixBody.baseMaxHealth = (ModConfig.basehealth.Value + (ModConfig.basehealth.Value * multiplier)) / 10;
                MithrixBody.levelMaxHealth = (ModConfig.levelhealth.Value + (ModConfig.levelhealth.Value * multiplier)) / 10;
                MithrixBody.baseDamage = (ModConfig.basedamage.Value + (ModConfig.basedamage.Value * multiplier)) * 100 / 4;
                MithrixBody.levelDamage = (ModConfig.leveldamage.Value + (ModConfig.leveldamage.Value * multiplier)) * 100 / 4;

            }
            else
            {
                MithrixBody.baseMaxHealth = ModConfig.basehealth.Value + (ModConfig.basehealth.Value * multiplier);
                MithrixBody.levelMaxHealth = ModConfig.levelhealth.Value + (ModConfig.levelhealth.Value * multiplier);
                MithrixBody.baseDamage = ModConfig.basedamage.Value + (ModConfig.basedamage.Value * multiplier);
                MithrixBody.levelDamage = ModConfig.leveldamage.Value + (ModConfig.leveldamage.Value * multiplier);
            }

            MithrixBody.baseAttackSpeed = ModConfig.baseattackspeed.Value + (ModConfig.baseattackspeed.Value * multiplier);

            MithrixBody.baseMoveSpeed = ModConfig.basespeed.Value + (ModConfig.basespeed.Value * multiplier);
            MithrixBody.baseAcceleration = ModConfig.acceleration.Value + (ModConfig.acceleration.Value * multiplier);
            MithrixBody.baseJumpPower = ModConfig.jumpingpower.Value + (ModConfig.jumpingpower.Value * multiplier);
            MithrixDirection.turnSpeed = ModConfig.turningspeed.Value + (ModConfig.turningspeed.Value * multiplier);

            MithrixBody.baseArmor = ModConfig.basearmor.Value + (ModConfig.basearmor.Value * multiplier);

            ProjectileSteerTowardTarget component = FireLunarShards.projectilePrefab.GetComponent<ProjectileSteerTowardTarget>();
            component.rotationSpeed = ModConfig.ShardHoming.Value + (ModConfig.ShardHoming.Value * multiplier);
            ProjectileDirectionalTargetFinder component2 = FireLunarShards.projectilePrefab.GetComponent<ProjectileDirectionalTargetFinder>();
            component2.lookRange = ModConfig.ShardRange.Value + (ModConfig.ShardRange.Value * multiplier);
            component2.lookCone = ModConfig.ShardCone.Value + (ModConfig.ShardCone.Value * multiplier);

            WeaponSlam.duration = (3.5f / (ModConfig.baseattackspeed.Value + (ModConfig.baseattackspeed.Value * multiplier)));
        }

        private void AdjustPhase3Stats()
        {
            // increases most stats by 20% per loop and 5% per player
            Logger.LogMessage("Adjusting Phase 3 Stats");
            int playerCount = PlayerCharacterMasterController.instances.Count;
            float multiplier = (ModConfig.phase3LoopScaling.Value * Run.instance.loopClearCount) + (ModConfig.phase3PlayerScaling.Value * playerCount);
            CharacterBody MithrixBody = Mithrix.GetComponent<CharacterBody>();
            CharacterDirection MithrixDirection = Mithrix.GetComponent<CharacterDirection>();

            if (ModConfig.umbralMithrix.Value)
            {
                MithrixBody.baseMaxHealth = (ModConfig.basehealth.Value + (ModConfig.basehealth.Value * multiplier)) / 10;
                MithrixBody.levelMaxHealth = (ModConfig.levelhealth.Value + (ModConfig.levelhealth.Value * multiplier)) / 10;
                MithrixBody.baseDamage = (ModConfig.basedamage.Value + (ModConfig.basedamage.Value * multiplier)) * 100 / 4;
                MithrixBody.levelDamage = (ModConfig.leveldamage.Value + (ModConfig.leveldamage.Value * multiplier)) * 100 / 4;

            }
            else
            {
                MithrixBody.baseMaxHealth = ModConfig.basehealth.Value + (ModConfig.basehealth.Value * multiplier);
                MithrixBody.levelMaxHealth = ModConfig.levelhealth.Value + (ModConfig.levelhealth.Value * multiplier);
                MithrixBody.baseDamage = ModConfig.basedamage.Value + (ModConfig.basedamage.Value * multiplier);
                MithrixBody.levelDamage = ModConfig.leveldamage.Value + (ModConfig.leveldamage.Value * multiplier);
            }

            MithrixBody.baseAttackSpeed = ModConfig.baseattackspeed.Value + (ModConfig.baseattackspeed.Value * multiplier);

            MithrixBody.baseMoveSpeed = ModConfig.basespeed.Value + (ModConfig.basespeed.Value * multiplier);
            MithrixBody.baseAcceleration = ModConfig.acceleration.Value + (ModConfig.acceleration.Value * multiplier);
            MithrixBody.baseJumpPower = ModConfig.jumpingpower.Value + (ModConfig.jumpingpower.Value * multiplier);
            MithrixDirection.turnSpeed = ModConfig.turningspeed.Value + (ModConfig.turningspeed.Value * multiplier);

            MithrixBody.baseArmor = ModConfig.basearmor.Value + (ModConfig.basearmor.Value * multiplier);

            ProjectileSteerTowardTarget component = FireLunarShards.projectilePrefab.GetComponent<ProjectileSteerTowardTarget>();
            component.rotationSpeed = ModConfig.ShardHoming.Value + (ModConfig.ShardHoming.Value * multiplier);
            ProjectileDirectionalTargetFinder component2 = FireLunarShards.projectilePrefab.GetComponent<ProjectileDirectionalTargetFinder>();
            component2.lookRange = ModConfig.ShardRange.Value + (ModConfig.ShardRange.Value * multiplier);
            component2.lookCone = ModConfig.ShardCone.Value + (ModConfig.ShardCone.Value * multiplier);

            WeaponSlam.duration = (3.5f / (ModConfig.baseattackspeed.Value + (ModConfig.baseattackspeed.Value * multiplier)));
            if (playerCount > 4)
                ExitSkyLeap.cloneCount = 4;
            else
                ExitSkyLeap.cloneCount = 2 + (playerCount - 2);
        }

        private void AdjustPhase4Stats()
        {
            // increases most stats by 5% per loop and 1.25% per player
            Logger.LogMessage("Adjusting Phase 4 Stats");
            GameObject MithrixHurt = LegacyResourcesAPI.Load<GameObject>("Prefabs/Characterbodies/BrotherHurtBody");
            int playerCount = PlayerCharacterMasterController.instances.Count;
            float multiplier = (ModConfig.phase4LoopScaling.Value * Run.instance.loopClearCount) + (ModConfig.phase4PlayerScaling.Value * playerCount);
            CharacterBody MithrixHurtBody = MithrixHurt.GetComponent<CharacterBody>();
            CharacterDirection MithrixHurtDirection = MithrixHurt.GetComponent<CharacterDirection>();

            if (ModConfig.umbralMithrix.Value)
            {
                MithrixHurtBody.baseMaxHealth = (ModConfig.basehealth.Value + (ModConfig.basehealth.Value * multiplier)) / 10;
                MithrixHurtBody.levelMaxHealth = (ModConfig.levelhealth.Value + (ModConfig.levelhealth.Value * multiplier)) / 10;
                MithrixHurtBody.baseDamage = (ModConfig.basedamage.Value + (ModConfig.basedamage.Value * multiplier)) * 100 / 4;
                MithrixHurtBody.levelDamage = (ModConfig.leveldamage.Value + (ModConfig.leveldamage.Value * multiplier)) * 100 / 4;

            }
            else
            {
                MithrixHurtBody.baseMaxHealth = ModConfig.basehealth.Value + (ModConfig.basehealth.Value * multiplier);
                MithrixHurtBody.levelMaxHealth = ModConfig.levelhealth.Value + (ModConfig.levelhealth.Value * multiplier);
            }

            MithrixHurtBody.baseMoveSpeed = ModConfig.basespeed.Value + (ModConfig.basespeed.Value * multiplier);
            MithrixHurtBody.baseAcceleration = ModConfig.acceleration.Value + (ModConfig.acceleration.Value * multiplier);
            MithrixHurtDirection.turnSpeed = ModConfig.turningspeed.Value + (ModConfig.turningspeed.Value * multiplier);

            MithrixHurtBody.baseArmor = ModConfig.basearmor.Value + (ModConfig.basearmor.Value * multiplier);

            ProjectileSteerTowardTarget component = FireLunarShards.projectilePrefab.GetComponent<ProjectileSteerTowardTarget>();
            component.rotationSpeed = ModConfig.ShardHoming.Value + (ModConfig.ShardHoming.Value * multiplier);
            ProjectileDirectionalTargetFinder component2 = FireLunarShards.projectilePrefab.GetComponent<ProjectileDirectionalTargetFinder>();
            component2.lookRange = ModConfig.ShardRange.Value + (ModConfig.ShardRange.Value * multiplier);
            component2.lookCone = ModConfig.ShardCone.Value + (ModConfig.ShardCone.Value * multiplier);
            component2.allowTargetLoss = true;
        }

        private void OnRunStart(On.RoR2.Run.orig_Start orig, Run self)
        {
            Logger.LogMessage("Accursing the King of Nothing");
            AdjustBaseSkills();
            AdjustBaseStats();
            orig(self);
        }

        private void FrozenStateOnEnter(On.EntityStates.FrozenState.orig_OnEnter orig, EntityStates.FrozenState self)
        {
            if (self.characterBody.name == "BrotherBody(Clone)")
                return;
            orig(self);
        }


        private void AddTimedBuff_BuffDef_float(On.RoR2.CharacterBody.orig_AddTimedBuff_BuffDef_float orig, CharacterBody self, BuffDef buffDef, float duration)
        {
            if (self.name == "BrotherBody(Clone)" && buffDef == RoR2Content.Buffs.Nullified)
                return;
            orig(self, buffDef, duration);
        }

        private static bool TryApplyOverride(On.RoR2.Items.ImmuneToDebuffBehavior.orig_TryApplyOverride orig, CharacterBody body)
        {
            RoR2.Items.ImmuneToDebuffBehavior component = body.GetComponent<RoR2.Items.ImmuneToDebuffBehavior>();
            if ((bool)component)
            {
                if (component.isProtected)
                    return true;
                if (body.name == "BrotherBody(Clone)")
                {
                    if (ModConfig.debuffImmune.Value)
                        return true;
                }
                else
                {
                    if (body.HasBuff(DLC1Content.Buffs.ImmuneToDebuffReady) && (bool)component.healthComponent)
                    {
                        component.healthComponent.AddBarrier(0.1f * component.healthComponent.fullCombinedHealth);
                        body.RemoveBuff(DLC1Content.Buffs.ImmuneToDebuffReady);
                        EffectManager.SimpleImpactEffect(Addressables.LoadAssetAsync<GameObject>((object)"RoR2/DLC1/ImmuneToDebuff/ImmuneToDebuffEffect.prefab").WaitForCompletion(), body.corePosition, Vector3.up, true);
                        if (!body.HasBuff(DLC1Content.Buffs.ImmuneToDebuffReady))
                            body.AddTimedBuff(DLC1Content.Buffs.ImmuneToDebuffCooldown, 5f);
                        component.isProtected = true;
                        return true;
                    }
                }
            }
            return false;
        }

        public void MasterChanges(CharacterMaster master)
        {
            string name = master.name;
            if (name == "BrotherMaster(Clone)")
            {
                if (ModConfig.debuffImmune.Value)
                    master.inventory.GiveItem(DLC1Content.Items.ImmuneToDebuff);
            }
        }

        private void CharacterMasterOnBodyStart(On.RoR2.CharacterMaster.orig_OnBodyStart orig, CharacterMaster self, CharacterBody body)
        {
            orig(self, body);
            if (self.name == "BrotherMaster(Clone)" && phaseCounter == 2 && ModConfig.phase3Elite.Value != 0)
            {
                switch (ModConfig.phase3Elite.Value)
                {
                    case 1:
                        // Malachite
                        self.inventory.GiveEquipmentString(RoR2Content.Equipment.AffixPoison.name);
                        break;
                    case 2:
                        // Perfected
                        self.inventory.GiveEquipmentString(RoR2Content.Equipment.AffixLunar.name);
                        break;
                    case 3:
                        // VoidTouched
                        self.inventory.GiveEquipmentString(DLC1Content.Equipment.EliteVoidEquipment.name);
                        break;
                    case 4:
                        // Blazing
                        self.inventory.GiveEquipmentString(RoR2Content.Equipment.AffixRed.name);
                        break;
                    case 5:
                        // Overloading
                        self.inventory.GiveEquipmentString(RoR2Content.Equipment.AffixBlue.name);
                        break;
                    case 6:
                        // Glacial
                        self.inventory.GiveEquipmentString(RoR2Content.Equipment.AffixWhite.name);
                        break;
                    case 7:
                        // Celestine
                        self.inventory.GiveEquipmentString(RoR2Content.Equipment.AffixHaunted.name);
                        break;
                }
            }
        }

        private void UpdateOverlays(On.RoR2.CharacterModel.orig_UpdateOverlays orig, CharacterModel self)
        {
            if (self.body)
            {
                // Make sure config value is enabled, then give umbra item to Mithrix, his glass clones, and phase 4 Mithrix
                if (ModConfig.umbralMithrix.Value && (self.body.name == "BrotherBody(Clone)" || self.body.name == "BrotherGlassBody(Clone)" || self.body.name == "BrotherHurtBody(Clone)"))
                    self.body.inventory.GiveItem(RoR2Content.Items.InvadingDoppelganger);
            }
            orig(self);
        }

        private void BrotherEncounterPhaseBaseStateOnEnter(On.EntityStates.Missions.BrotherEncounter.BrotherEncounterPhaseBaseState.orig_OnEnter orig, EntityStates.Missions.BrotherEncounter.BrotherEncounterPhaseBaseState self)
        {
            phaseCounter++;
            // BrotherEncounterBaseState OnEnter
            self.childLocator = self.GetComponent<ChildLocator>();
            Transform child1 = self.childLocator.FindChild("ArenaWalls");
            Transform child2 = self.childLocator.FindChild("ArenaNodes");
            if ((bool)child1)
                child1.gameObject.SetActive(self.shouldEnableArenaWalls);
            if (!(bool)child2)
                return;
            child2.gameObject.SetActive(self.shouldEnableArenaNodes);
            // BrotherEncounterBaseState OnEnter
            if ((bool)PhaseCounter.instance)
            {
                phaseCounter = PhaseCounter.instance.phase;
                PhaseCounter.instance.GoToNextPhase();
            }
            if ((bool)self.childLocator)
            {
                self.phaseControllerObject = self.childLocator.FindChild(self.phaseControllerChildString).gameObject;
                if ((bool)self.phaseControllerObject)
                {
                    self.phaseScriptedCombatEncounter = self.phaseControllerObject.GetComponent<ScriptedCombatEncounter>();
                    self.phaseBossGroup = self.phaseControllerObject.GetComponent<BossGroup>();
                    self.phaseControllerSubObjectContainer = self.phaseControllerObject.transform.Find("PhaseObjects").gameObject;
                    self.phaseControllerSubObjectContainer.SetActive(true);
                }
                GameObject gameObject = self.childLocator.FindChild("AllPhases").gameObject;
                if ((bool)gameObject)
                    gameObject.SetActive(true);
            }
            self.healthBarShowTime = Run.FixedTimeStamp.now + self.healthBarShowDelay;
            if ((bool)DirectorCore.instance)
            {
                foreach (Behaviour component in DirectorCore.instance.GetComponents<CombatDirector>())
                    component.enabled = false;
            }
            if (!NetworkServer.active || self.phaseScriptedCombatEncounter == null)
                return;
            if (phaseCounter == 1)
            {
                SpawnCard MithrixCard = LegacyResourcesAPI.Load<SpawnCard>("SpawnCards/CharacterSpawnCards/cscBrother");
                Mithrix.transform.position = new Vector3(-88.5f, 491.5f, -0.3f);
                Mithrix.transform.rotation = Quaternion.identity;
                Transform explicitSpawnPosition = Mithrix.transform;
                ScriptedCombatEncounter.SpawnInfo spawnInfo = new ScriptedCombatEncounter.SpawnInfo
                {
                    explicitSpawnPosition = explicitSpawnPosition,
                    spawnCard = MithrixCard,
                };
                ScriptedCombatEncounter.SpawnInfo[] spawnInfoList = new ScriptedCombatEncounter.SpawnInfo[] { spawnInfo };
                self.phaseScriptedCombatEncounter.spawns.SetValue(spawnInfo, 0);
            }
            self.phaseScriptedCombatEncounter.combatSquad.onMemberAddedServer += new Action<CharacterMaster>(self.OnMemberAddedServer);
        }

        private void Phase2OnEnter(On.EntityStates.Missions.BrotherEncounter.Phase2.orig_OnEnter orig, EntityStates.Missions.BrotherEncounter.Phase2 self)
        {
            AdjustPhase2Stats();
            orig(self);
        }

        private void Phase3OnEnter(On.EntityStates.Missions.BrotherEncounter.Phase3.orig_OnEnter orig, EntityStates.Missions.BrotherEncounter.Phase3 self)
        {
            AdjustPhase3Stats();
            switch (ModConfig.phase3Elite.Value)
            {
                case 0:
                    break;
                case 1:
                    // Malachite
                    Chat.SendBroadcastChat(new Chat.SimpleChatMessage()
                    {
                        baseToken = $"<color=#3f6f39>The King's form becomes corrupted</color>"
                    });
                    break;
                case 2:
                    // Perfected
                    Chat.SendBroadcastChat(new Chat.SimpleChatMessage()
                    {
                        baseToken = $"<color=#7fadd4>The King perfects his form</color>"
                    });
                    break;
                case 3:
                    // VoidTouched
                    Chat.SendBroadcastChat(new Chat.SimpleChatMessage()
                    {
                        baseToken = $"<color=#8826dd>The King gazes into the void</color>"
                    });
                    break;
                case 4:
                    // Blazing
                    Chat.SendBroadcastChat(new Chat.SimpleChatMessage()
                    {
                        baseToken = $"<color=#e78c08>The Infernal King emerges</color>"
                    });
                    break;
                case 5:
                    // Overloading
                    Chat.SendBroadcastChat(new Chat.SimpleChatMessage()
                    {
                        baseToken = $"<color=#4e8ada>The King's power overloads</color>"
                    });
                    break;
                case 6:
                    // Glacial
                    Chat.SendBroadcastChat(new Chat.SimpleChatMessage()
                    {
                        baseToken = $"<color=#e6f3fc>The Glacial King rises</color>"
                    });
                    break;
                case 7:
                    // Celestine
                    Chat.SendBroadcastChat(new Chat.SimpleChatMessage()
                    {
                        baseToken = $"<color=#29a5be>The King's form becomes haunting</color>"
                    });
                    break;
            }
            orig(self);
        }

        private void Phase4OnEnter(On.EntityStates.Missions.BrotherEncounter.Phase4.orig_OnEnter orig, EntityStates.Missions.BrotherEncounter.Phase4 self)
        {
            AdjustPhase4Stats();
            orig(self);
        }

        private void EnterSkyLeapOnEnter(On.EntityStates.BrotherMonster.EnterSkyLeap.orig_OnEnter orig, EnterSkyLeap self)
        {
            if (self.characterBody.name == "BrotherBodyIT(Clone)")
                return;
            orig(self);
        }

        private void ExitSkyLeapOnEnter(On.EntityStates.BrotherMonster.ExitSkyLeap.orig_OnEnter orig, ExitSkyLeap self)
        {
            // EntityStates BaseState OnEnter
            if (!(bool)self.characterBody)
                return;
            self.attackSpeedStat = self.characterBody.attackSpeed;
            self.damageStat = self.characterBody.damage;
            self.critStat = self.characterBody.crit;
            self.moveSpeedStat = self.characterBody.moveSpeed;
            // EntityStates BaseState OnEnter
            self.duration = ExitSkyLeap.baseDuration / self.attackSpeedStat;
            int num = (int)Util.PlaySound(ExitSkyLeap.soundString, self.gameObject);
            self.PlayAnimation("Body", nameof(ExitSkyLeap), "SkyLeap.playbackRate", self.duration);
            self.PlayAnimation("FullBody Override", "BufferEmpty");
            self.characterBody.AddTimedBuff(RoR2Content.Buffs.ArmorBoost, ExitSkyLeap.baseDuration);
            AimAnimator aimAnimator = self.GetAimAnimator();
            if ((bool)aimAnimator)
                aimAnimator.enabled = true;
            if (self.isAuthority)
            {
                if (self.fixedAge == 0.45f * self.duration)
                    self.FireRingAuthority();
                self.FireRingAuthority();
            }
            if (!(bool)PhaseCounter.instance || PhaseCounter.instance.phase != 3)
                return;
            if ((double)UnityEngine.Random.value < ExitSkyLeap.recastChance)
                self.recast = true;
            for (int index = 0; index < ExitSkyLeap.cloneCount; ++index)
            {
                SpawnCard spawnCard = LegacyResourcesAPI.Load<SpawnCard>("SpawnCards/CharacterSpawnCards/cscBrotherGlass");
                DirectorPlacementRule placementRule = new DirectorPlacementRule();
                placementRule.placementMode = DirectorPlacementRule.PlacementMode.Approximate;
                placementRule.minDistance = 3f;
                placementRule.maxDistance = 20f;
                placementRule.spawnOnTarget = self.gameObject.transform;
                Xoroshiro128Plus rng = RoR2Application.rng;
                DirectorSpawnRequest directorSpawnRequest = new DirectorSpawnRequest(spawnCard, placementRule, rng);
                directorSpawnRequest.summonerBodyObject = self.gameObject;
                directorSpawnRequest.onSpawnedServer += spawnResult =>
                {
                    Inventory cloneInventory = spawnResult.spawnedInstance.GetComponent<Inventory>();
                    cloneInventory.GiveItem(RoR2Content.Items.HealthDecay, ExitSkyLeap.cloneDuration);
                };
                DirectorCore.instance.TrySpawnObject(directorSpawnRequest);
            }
            GenericSkill genericSkill = (bool)self.skillLocator ? self.skillLocator.special : null;
            if (!(bool)genericSkill)
                return;
            genericSkill.SetSkillOverride(self.outer, UltChannelState.replacementSkillDef, GenericSkill.SkillOverridePriority.Contextual);
        }

        private void SlideIntroStateOnEnter(On.EntityStates.BrotherMonster.SlideIntroState.orig_OnEnter orig, SlideIntroState self)
        {
            Ray aimRay = self.GetAimRay();
            if (self.isAuthority)
            {
                for (int i = 0; i < ModConfig.UtilityShotgun.Value; i++)
                {
                    ProjectileManager.instance.FireProjectile(FireLunarShards.projectilePrefab, aimRay.origin, Quaternion.LookRotation(aimRay.direction), self.gameObject, self.characterBody.damage * 0.05f / 12f, 0f, Util.CheckRoll(self.characterBody.crit, self.characterBody.master), DamageColorIndex.Default, null, -1f);
                    aimRay.direction = Util.ApplySpread(aimRay.direction, 0f, 4f, 4f, 4f, 0f, 0f);
                }
            }
            orig(self);
        }
        private void SprintBashOnEnter(On.EntityStates.BrotherMonster.SprintBash.orig_OnEnter orig, SprintBash self)
        {
            if (self.isAuthority)
            {
                for (int i = 0; i < ModConfig.SecondaryFan.Value; i++)
                {
                    Ray aimRay = self.GetAimRay();
                    Vector3 forward = Util.ApplySpread(aimRay.direction, 0f, 0f, 1f, 0f, i * 5f, 0f);
                    ProjectileManager.instance.FireProjectile(FireLunarShards.projectilePrefab, aimRay.origin, Util.QuaternionSafeLookRotation(forward), self.gameObject, self.characterBody.damage * 0.1f / 12f, 0f, Util.CheckRoll(self.characterBody.crit, self.characterBody.master), DamageColorIndex.Default, null, -1f);
                    Ray aimRay2 = self.GetAimRay();
                    Vector3 forward2 = Util.ApplySpread(aimRay2.direction, 0f, 0f, 1f, 0f, -i * 5f, 0f);
                    ProjectileManager.instance.FireProjectile(FireLunarShards.projectilePrefab, aimRay2.origin, Util.QuaternionSafeLookRotation(forward2), self.gameObject, self.characterBody.damage * 0.1f / 12f, 0f, Util.CheckRoll(self.characterBody.crit, self.characterBody.master), DamageColorIndex.Default, null, -1f);
                }
            }
            orig(self);
        }

        private void WeaponSlamOnEnter(On.EntityStates.BrotherMonster.WeaponSlam.orig_OnEnter orig, WeaponSlam self)
        {
            GameObject projectilePrefab = WeaponSlam.pillarProjectilePrefab;
            projectilePrefab.transform.localScale = new Vector3(4f, 4f, 4f);
            projectilePrefab.GetComponent<ProjectileController>().ghostPrefab.transform.localScale = new Vector3(4f, 4f, 4f);
            hasfired = false;
            orig(self);
        }

        private void WeaponSlamFixedUpdate(On.EntityStates.BrotherMonster.WeaponSlam.orig_FixedUpdate orig, WeaponSlam self)
        {
            if (self.isAuthority)
            {
                Logger.LogDebug("added hammer proj");
                if (self.hasDoneBlastAttack)
                {
                    Logger.LogDebug("blast attack done");
                    if (self.modelTransform)
                    {
                        if (hasfired == false)
                        {
                            hasfired = true;
                            Logger.LogDebug("modeltransformed");
                            float num = 360f / ModConfig.SlamWaveProjectileCount.Value;
                            if (phaseCounter == 0 || phaseCounter == 1)
                                num = 360f / (ModConfig.SlamWaveProjectileCount.Value / 2);
                            Vector3 point = Vector3.ProjectOnPlane(self.inputBank.aimDirection, Vector3.up);
                            Transform transform2 = self.FindModelChild(WeaponSlam.muzzleString);
                            Vector3 Position = transform2.position;
                            for (int i = 0; i < ModConfig.SlamWaveProjectileCount.Value; i++)
                            {
                                Vector3 forward = Quaternion.AngleAxis(num * (float)i, Vector3.up) * point;
                                ProjectileManager.instance.FireProjectile(FistSlam.waveProjectilePrefab, Position, Util.QuaternionSafeLookRotation(forward), self.gameObject, self.characterBody.damage * FistSlam.waveProjectileDamageCoefficient, FistSlam.waveProjectileForce, Util.CheckRoll(self.characterBody.crit, self.characterBody.master), DamageColorIndex.Default, null, -1f);
                                ProjectileManager.instance.FireProjectile(WeaponSlam.waveProjectilePrefab, Position, Util.QuaternionSafeLookRotation(forward), self.gameObject, self.characterBody.damage * WeaponSlam.waveProjectileDamageCoefficient, WeaponSlam.waveProjectileForce, Util.CheckRoll(self.characterBody.crit, self.characterBody.master), DamageColorIndex.Default, null, -1f);
                            }

                        }

                    }
                }
            }
            orig(self);
        }

        private void FireLunarShardsOnEnter(On.EntityStates.BrotherMonster.Weapon.FireLunarShards.orig_OnEnter orig, FireLunarShards self)
        {
            if (!(self is FireLunarShardsHurt))
            {
                if (self.isAuthority)
                {
                    Ray aimRay = self.GetAimRay();
                    Transform transform = self.FindModelChild(FireLunarShards.muzzleString);
                    if (transform)
                    {
                        aimRay.origin = transform.position;
                    }
                    FireProjectileInfo fireProjectileInfo = default(FireProjectileInfo);
                    fireProjectileInfo.position = aimRay.origin;
                    fireProjectileInfo.rotation = Quaternion.LookRotation(aimRay.direction);
                    fireProjectileInfo.crit = self.characterBody.RollCrit();
                    fireProjectileInfo.damage = self.characterBody.damage * self.damageCoefficient;
                    fireProjectileInfo.damageColorIndex = DamageColorIndex.Default;
                    fireProjectileInfo.owner = self.gameObject;
                    fireProjectileInfo.procChainMask = default(ProcChainMask);
                    fireProjectileInfo.force = 0f;
                    fireProjectileInfo.useFuseOverride = false;
                    fireProjectileInfo.useSpeedOverride = false;
                    fireProjectileInfo.target = null;
                    fireProjectileInfo.projectilePrefab = FireLunarShards.projectilePrefab;

                    for (int i = 0; i < ModConfig.LunarShardAdd.Value; i++)
                    {
                        ProjectileManager.instance.FireProjectile(fireProjectileInfo);
                        aimRay.direction = Util.ApplySpread(aimRay.direction, 0f, self.maxSpread * (1f + 0.45f * i), self.spreadYawScale * (1f + 0.45f * i), self.spreadPitchScale * (1f + 0.45f * i), 0f, 0f);
                        fireProjectileInfo.rotation = Quaternion.LookRotation(aimRay.direction);
                    }

                }
            }
            orig(self);
        }

        private static void FistSlamOnEnter(On.EntityStates.BrotherMonster.FistSlam.orig_OnEnter orig, FistSlam self)
        {
            FistSlam.waveProjectileDamageCoefficient = 2.3f;
            FistSlam.healthCostFraction = 0.0f;
            FistSlam.waveProjectileCount = 20;
            FistSlam.baseDuration = 3.5f;
            if (self.isAuthority)
            {
                float num1 = 8f;
                float num2 = 360f / num1;
                Vector3 vector3 = Vector3.ProjectOnPlane(self.inputBank.aimDirection, Vector3.up);
                Vector3 position = self.FindModelChild(FistSlam.muzzleString).position;
                for (int index = 0; index < num1; ++index)
                {
                    Vector3 forward = Quaternion.AngleAxis(num2 * index, Vector3.up) * vector3;
                    ProjectileManager.instance.FireProjectile(UltChannelState.waveProjectileLeftPrefab, position, Util.QuaternionSafeLookRotation(forward), self.gameObject, self.characterBody.damage * 2f, FistSlam.waveProjectileForce, Util.CheckRoll(self.characterBody.crit, self.characterBody.master));
                }
            }
            orig.Invoke(self);
        }

        private static void SpellChannelEnterStateOnEnter(
          On.EntityStates.BrotherMonster.SpellChannelEnterState.orig_OnEnter orig,
          SpellChannelEnterState self)
        {
            SpellChannelEnterState.duration = 3f;
            orig.Invoke(self);
        }

        private static void SpellChannelStateOnEnter(
          On.EntityStates.BrotherMonster.SpellChannelState.orig_OnEnter orig,
          SpellChannelState self)
        {

            SpellChannelState.stealInterval = 0.1f;
            SpellChannelState.delayBeforeBeginningSteal = 0.0f;
            SpellChannelState.maxDuration = 15f;
            orig.Invoke(self);
        }

        private static void SpellChannelExitStateOnEnter(
          On.EntityStates.BrotherMonster.SpellChannelExitState.orig_OnEnter orig,
          SpellChannelExitState self)
        {
            SpellChannelExitState.lendInterval = 0.04f;
            SpellChannelExitState.duration = 2.5f;
            orig.Invoke(self);
        }

        private static void StaggerEnterOnEnter(On.EntityStates.BrotherMonster.StaggerEnter.orig_OnEnter orig, StaggerEnter self)
        {
            self.duration = 0.0f;
            orig.Invoke(self);
        }

        private static void StaggerExitOnEnter(On.EntityStates.BrotherMonster.StaggerExit.orig_OnEnter orig, StaggerExit self)
        {
            self.duration = 0.0f;
            orig.Invoke(self);
        }

        private static void StaggerLoopOnEnter(On.EntityStates.BrotherMonster.StaggerLoop.orig_OnEnter orig, StaggerLoop self)
        {
            self.duration = 0.0f;
            orig.Invoke(self);
        }

        private static void TrueDeathStateOnEnter(On.EntityStates.BrotherMonster.TrueDeathState.orig_OnEnter orig, TrueDeathState self)
        {
            TrueDeathState.dissolveDuration = 5f;
            orig.Invoke(self);
        }

        private static void CleanupPillar(On.EntityStates.BrotherMonster.WeaponSlam.orig_OnEnter orig, WeaponSlam self)
        {
            GameObject projectilePrefab = WeaponSlam.pillarProjectilePrefab;
            projectilePrefab.transform.localScale = new Vector3(1f, 1f, 1f);
            projectilePrefab.GetComponent<ProjectileController>().ghostPrefab.transform.localScale = new Vector3(1f, 1f, 1f);
            orig.Invoke(self);
        }
    }
}