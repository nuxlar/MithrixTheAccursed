# Mithrix the Accursed

Every phase has been enhanced (except 4, sadge) to provide an intense and challenging experience no matter how long your run is. This mod is completely configurable, you can change which phases are enhanced, scaling, stats, etc... though you can't turn off phase 4 skip prevention and debuff resistance/immunity after a certain amount of loops. The game defines a loop as ~5 stages so when I say 1 loop that means going to the moon "without looping".

The Default flow:

- Phase 1 Mithrix's hammer attack is more deadly. A decaying shadow clone splits from him after his jump special.
- Phase 2 Mithrix becomes corrupted and spawns shadows of the survivors.
- Phase 3 Mithrix is consumed by his shadow, splitting into 2 with reduced HP.
- Phase 4 Mithrix quickly steals your items and releases a deadly fist slam when damaged.

Most stuff is configurable (IF YOURE UPDATING FROM 1.0.0 DELETE YOUR MITHRIXTHEACCURSED CONFIG FILE):

- Phase 1 change (umbral clone)
- Phase 2 changes (Mithrix spawning and doppelganger event)
- Phase 2 Elite (turn off or change the elite)
- Phase 3 changes (Dual shadow Mithrix)
- HP scaling per loop/player
- Mobility (movementspd, acceleration, turningspd) scaling per loop/player
- Skills (stocks/cooldowns)
- Debuff Immunity (scorpions still work) (applies after 3 loops - 15 stages even with the config off)
- Debuff Resistance (immune to freeze/tentabauble) (applies after 2 loops - 10 stages even with the config off)

- Reworking Phase 4 once I know what I'm doing

## Contact Me

Join my Discord!

[<img src="https://www.seekpng.com/png/detail/19-192311_discord-color-logo-discord-banner-for-twitch.png">](https://discord.gg/VNB3wqy242)

## Support Me

If you like what I'm doing contribute to my caffeine addiction!

[<img src="https://uploads-ssl.webflow.com/5c14e387dab576fe667689cf/61e11d430afb112ea33c3aa5_Button-1.png">](https://ko-fi.com/nuxlar)

## Credits

**Icon**: Found this awesome illustration from MushyAvocado on ArtStation, fit the vibe perfectly. (https://www.artstation.com/artwork/PoWkG3)

**Artifact of the King**: Base code is refactored from Blobface (https://thunderstore.io/package/Blobface/Artifact_of_the_King/)

**Inferno**: Added "phase 5" from HIFU's Inferno (faster pizza slices after death) (https://thunderstore.io/package/HIFU/Inferno/)

## Changelog

**2.0.1**

- Removes unused config value (P4 mobility)
- Fixes doppel event not triggering sometimes
- Fixes Phase 4 infinite immune after dios
- Fixes enhanced hammer attack sometimes not working
- Fixes "vanilla" phase 3 not having glass clones
- Fixes "vanilla" phase 3 not having pizza attack

**2.0.0**

- IF YOURE UPDATING DELETE YOURE CONFIG (this is the price of configurability and satisfying almost everyone)
- Debuff resistance applies after 2 loops (10 stages)
- Debuff immunity applies after 3 loops (15 stages)
- Debuff resistance/immunity can be configured to start from loop 1 (5 stages)
- First loop scaling is now separate from other loop scaling
- Removed damage, armor, and shards from stat scaling
- Split scaling into HP and Mobility (movespd, acceleration, turnspd)
- Removed Mobility scaling from Phase 4
- Edited Risk Of Options values so the sliders arent nuts
- Added random elite option
- Made elite Mithrix phase 2 instead of 3
- Made 99% of the mod configurable
- Changed A LOT of default values (so the fight is more forgiving and less bullshit)
- Removed Umbral Mithrix to differentiate since the enhanced fight uses umbras
- Removed larger flame pillar because of enhanced phase 3
- Added enhanced Phase 1
  - Decaying umbral clone spawns after skyleap (2 for groups with 3+ survivors)
- Added enhanced Phase 2
  - Mithrix becomes an elite (default malachite)
  - Decaying umbral clone spawns after skyleap (2 for groups with 3+ survivors)
- Added enhanced Phase 3
  - Mithrix splits into 2 enemies with half HP (3 for groups of 3+)
- Added enhanced Phase 4
  - This phase is now unskippable
  - Items are quickly stolen
  - Mithrix's fist slam is deadlier with a fan of shards and a pizza

## Changelog

**1.1.1**

- Fixed umbra Mithrix doing 4% damage and having 10x HP (Didn't think it was THAT bad but DAMN)
- Removed the clones from getting the umbra item

**1.1.0**

- IF YOURE UPDATING DELETE YOUR MITHRIXTHEACCURSED CONFIG FILE
- Added Risk of Options support
- Added Umbral Mithrix (configurable on/off)
- Added flat loop scaling
- Added vanilla values to config
- Added scaling config
- Added Phase 3 elite to config (disable/enable + elite type)
- Tweaked default config values
- Fixed flow not working when fighting Mithrix multiple times (Looper mod)
- Added Phase 1 scaling (5% per loop, 1.25% per player)
- Reduced Phase 2 scaling (10% per loop, 2.5% per player)
- Reduced Phase 3 scaling (20% per loop, 5% per player)
- Added Phase 4 scaling (5% per loop, 1.25% per player)
- README edits

**1.0.0**

- v1 release!
- Implemented a custom flow for the fight
- Fixed slow item steal (0.9.6 goof)
- Reduced clone scaling (1 per player - maxes out at 4 cuz lag)
- Removed some config (constant Malachite Mithrix, clone count, phase 2 skip)
- Removed the scuffed weaken
- Mithrix gains a 15% stat buff phase 2 (scales 15% per player)
- Mithrix gains a 30% stat buff phase 3 (scales 30% per player)
- Mithrix becomes corrupted in phase 3
- fixed placement of phase 4 pizza slam
- README edits

**0.9.6**

- done goofed and left perfected mithrix instead of malachite

**0.9.5**

- Adds config for debuff resistance (freeze/nullify)
- Makes Phase 1 Mithrix 15% weaker with some stats
- Halves Phase 1 skyleap wave and hammer slam wave/orb
- Phase 3 Mithrix is no longer weakened and gets a 10% buff per player to most stats
- Shadow clones spawn per player now
- README edit

**0.9.2**

- Removed raincoats except for full immunity config
- Made Mithrix immune to freezing and the tentabauble
- Added shrapnel wave to hammer slam
- Doubled shrapnel wave when he exits his sky leap
- Beefed up default stats
- Added config for sky leap wave
- README edit

**0.8.8**

- Added config for Phase 2 skip
- Added config for debuff resistance as the default instead of full immunity - raincoats without barriers and lower regen
- Added config for Malachite Mithrix for a more dangerous arena (off by default)
- refactored code
- README edit

**0.8.4**

- Added debuff immunity to Mithrix (configurable)
- refactored some more
- README edit

**0.8.0**

- Should work fine I want to refactor/add more or catch any bugs before making it a 1.0.0 release.
