using BepInEx;
using RoR2;
using RoR2.Projectile;
using EntityStates.BrotherMonster;
using EntityStates.BrotherMonster.Weapon;
using UnityEngine;
using RoR2.Skills;
using System;
using UnityEngine.AddressableAssets;

namespace MithrixTheAccursed
{
    [BepInPlugin("com.zorp.MithrixTheAccursed", "MithrixTheAccursed", "0.8.4")]

    public class MithrixTheAccursed : BaseUnityPlugin
    {
        private bool hasfired;

        GameObject Mithrix = LegacyResourcesAPI.Load<GameObject>("Prefabs/Characterbodies/BrotherBody");
        public void Awake()
        {
            ModConfig.InitConfig(Config);
            if (ModConfig.accurse.Value)
            {
                On.RoR2.Run.Start += OnRunStart;
                if (ModConfig.debuffImmune.Value)
                    On.RoR2.Items.ImmuneToDebuffBehavior.TryApplyOverride += TryApplyOverride;
                CharacterMaster.onStartGlobal += new Action<CharacterMaster>(MasterChanges);
                On.EntityStates.BrotherMonster.ExitSkyLeap.OnEnter += ExitSkyLeapOnEnter;
                On.EntityStates.BrotherMonster.SlideIntroState.OnEnter += SlideIntroStateOnEnter;
                On.EntityStates.BrotherMonster.SprintBash.OnEnter += SprintBashOnEnter;
                On.EntityStates.BrotherMonster.WeaponSlam.OnEnter += WeaponSlamOnEnter;
                On.EntityStates.BrotherMonster.WeaponSlam.FixedUpdate += WeaponSlamFixedUpdate;
                On.EntityStates.BrotherMonster.Weapon.FireLunarShards.OnEnter += FireLunarShardsOnEnter;
                On.EntityStates.Missions.BrotherEncounter.Phase2.OnEnter += Phase2OnEnter;
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

        private void AdjustStats()
        {
            Logger.LogMessage("Adjusting Stats");
            CharacterBody MithrixBody = Mithrix.GetComponent<CharacterBody>();
            CharacterDirection MithrixDirection = Mithrix.GetComponent<CharacterDirection>();
            CharacterMotor MithrixMotor = Mithrix.GetComponent<CharacterMotor>();
            MithrixMotor.mass = ModConfig.mass.Value;
            MithrixMotor.airControl = ModConfig.aircontrol.Value;
            MithrixMotor.jumpCount = ModConfig.jumpcount.Value;

            MithrixBody.baseMaxHealth = ModConfig.basehealth.Value;
            MithrixBody.levelMaxHealth = ModConfig.levelhealth.Value;

            MithrixBody.baseAttackSpeed = ModConfig.baseattackspeed.Value;

            MithrixBody.baseMoveSpeed = ModConfig.basespeed.Value;
            MithrixBody.baseAcceleration = ModConfig.acceleration.Value;
            MithrixBody.baseJumpPower = ModConfig.jumpingpower.Value;
            MithrixDirection.turnSpeed = ModConfig.turningspeed.Value;

            MithrixBody.baseArmor = ModConfig.basearmor.Value;

            MithrixBody.baseDamage = ModConfig.basedamage.Value;
            MithrixBody.levelDamage = ModConfig.leveldamage.Value;

            ProjectileSteerTowardTarget component = FireLunarShards.projectilePrefab.GetComponent<ProjectileSteerTowardTarget>();
            component.rotationSpeed = ModConfig.ShardHoming.Value;
            ProjectileDirectionalTargetFinder component2 = FireLunarShards.projectilePrefab.GetComponent<ProjectileDirectionalTargetFinder>();
            component2.lookRange = ModConfig.ShardRange.Value;
            component2.lookCone = ModConfig.ShardCone.Value;
            component2.allowTargetLoss = true;

            WeaponSlam.duration = (3.5f / ModConfig.baseattackspeed.Value);
            HoldSkyLeap.duration = ModConfig.JumpPause.Value;
            ExitSkyLeap.cloneCount = ModConfig.clonecount.Value;
            ExitSkyLeap.cloneDuration = ModConfig.cloneduration.Value;
            ExitSkyLeap.recastChance = ModConfig.JumpRecast.Value;
            UltChannelState.waveProjectileCount = ModConfig.UltimateWaves.Value;
            UltChannelState.maxDuration = ModConfig.UltimateDuration.Value;
            UltChannelState.totalWaves = ModConfig.UltimateCount.Value;
        }

        private void AdjustSkills()
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

        private void OnRunStart(On.RoR2.Run.orig_Start orig, Run self)
        {
            Logger.LogMessage("Accursing the King of Nothing");
            AdjustSkills();
            AdjustStats();
            orig(self);
        }
        public void MasterChanges(CharacterMaster master)
        {
            string name = master.name;
            if (name == "BrotherMaster(Clone)")
                master.inventory.GiveItem(DLC1Content.Items.ImmuneToDebuff);
        }

        private static bool TryApplyOverride(On.RoR2.Items.ImmuneToDebuffBehavior.orig_TryApplyOverride orig, CharacterBody body)
        {
            RoR2.Items.ImmuneToDebuffBehavior component = body.GetComponent<RoR2.Items.ImmuneToDebuffBehavior>();
            if ((bool)component)
            {
                if (body.name == "BrotherBody(Clone)" || component.isProtected)
                    return true;
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
            return false;
        }

        private void ExitSkyLeapOnEnter(On.EntityStates.BrotherMonster.ExitSkyLeap.orig_OnEnter orig, ExitSkyLeap self)
            {
                if (self.isAuthority)
                {
                    if (self.fixedAge == 0.45f * self.duration)
                    {
                        self.FireRingAuthority();
                    }

                    if (self.fixedAge == 0.9f * self.duration)
                {
                    self.FireRingAuthority();
                }
                    }
                orig(self);
            }

        private void SlideIntroStateOnEnter(On.EntityStates.BrotherMonster.SlideIntroState.orig_OnEnter orig, SlideIntroState self)
            {
                Ray aimRay = self.GetAimRay();
                if (self.isAuthority)
                {
                    for (int i = 0; i< ModConfig.UtilityShotgun.Value; i++)
                    {
                        ProjectileManager.instance.FireProjectile(FireLunarShards.projectilePrefab, aimRay.origin, Quaternion.LookRotation(aimRay.direction), self.gameObject, self.characterBody.damage* 0.05f / 12f, 0f, Util.CheckRoll(self.characterBody.crit, self.characterBody.master), DamageColorIndex.Default, null, -1f);
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
                            float num = 360f / ModConfig.SlamOrbCount.Value;
                            Vector3 point = Vector3.ProjectOnPlane(self.inputBank.aimDirection, Vector3.up);
                            Transform transform2 = self.FindModelChild(WeaponSlam.muzzleString);
                            Vector3 Position = transform2.position;
                            for (int i = 0; i < ModConfig.SlamOrbCount.Value; i++)
                            {
                                Vector3 forward = Quaternion.AngleAxis(num * (float)i, Vector3.up) * point;
                                ProjectileManager.instance.FireProjectile(FistSlam.waveProjectilePrefab, Position, Util.QuaternionSafeLookRotation(forward), self.gameObject, self.characterBody.damage * FistSlam.waveProjectileDamageCoefficient, FistSlam.waveProjectileForce, Util.CheckRoll(self.characterBody.crit, self.characterBody.master), DamageColorIndex.Default, null, -1f);
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

        private static void Phase2OnEnter(On.EntityStates.Missions.BrotherEncounter.Phase2.orig_OnEnter orig, EntityStates.Missions.BrotherEncounter.Phase2 self)
        {
            orig.Invoke(self);
            self.PreEncounterBegin();
            self.outer.SetNextState(new EntityStates.Missions.BrotherEncounter.Phase3());
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
                Vector3 position = self.FindModelChild(WeaponSlam.muzzleString).position + new Vector3(UnityEngine.Random.Range(-50f, 50f), 0.0f, UnityEngine.Random.Range(-50f, 50f));
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

            SpellChannelState.stealInterval = 0.05f;
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