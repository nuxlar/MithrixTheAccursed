
# Mithrix the Accursed

The Accursed King of Nothing is faster, stronger, and has some tricks up his sleeve to make the battle more intense. Built off of Artifact of the King with some Inferno sprinkles.
The Default flow:

	- Phase 1 The Accursed King drops in from the sky to greet you in battle (stats +5% per loop +1.25% per player)
	- Phase 2 He returns to test out his creations (stats +10% per loop +2.5% per player)
	- Phase 3 The Accursed King's form becomes corrupted, releasing clones to aid him in battle (stats +20% per loop +5% per player)
	- Phase 4 He absorbs items quickly and releases a deadly slam attack (stats +5% per loop +1.25% per player)

Most stuff is configurable (IF YOURE UPDATING FROM 1.0.0 DELETE YOUR MITHRIXTHEACCURSED CONFIG FILE):

	- Stats
	- Skills
	- Full debuff immunity (scorpions still work)
	- Debuff Resistance (immune to freeze/tentabauble)
	- Umbral Mithrix (Mithrix becomes a shadow of himself for all phases)


## Plans
* Make the flow configurable (preset difficulties mayhaps, Lunar - AotK + Inferno with some accursed sprinkles, Accursed - My vision for the fight, Commando Must Die - For the masochistic risk player)
* Make Mithrix more adaptive rather than flat scaling - based on debuffs, how fast his health drops, etc...

## Contact
Reach out on discord with bugs/feedback **nuxlar#0235**

## Credits
**Icon**: Found this awesome illustration from MushyAvocado on ArtStation, fit the vibe perfectly. (https://www.artstation.com/artwork/PoWkG3)

**Artifact of the King**: Base code is refactored from Blobface (https://thunderstore.io/package/Blobface/Artifact_of_the_King/)

**Inferno**: Added some behaviors from HIFU's Mithrix fight (https://thunderstore.io/package/HIFU/Inferno/)

## Donate
If you like what I'm doing contribute to my caffeine addiction! https://ko-fi.com/nuxlar

## Changelog
**1.1.1**
* Fixed umbra Mithrix doing 4% damage and having 10x HP (Didn't think it was THAT bad but DAMN)
* Removed the clones from getting the umbra item

**1.1.0**
* IF YOURE UPDATING DELETE YOUR MITHRIXTHEACCURSED CONFIG FILE
* Added Risk of Options support
* Added Umbral Mithrix (configurable on/off)
* Added flat loop scaling
* Added vanilla values to config
* Added scaling config
* Added Phase 3 elite to config (disable/enable + elite type)
* Tweaked default config values
* Fixed flow not working when fighting Mithrix multiple times (Looper mod)
* Added Phase 1 scaling (5% per loop, 1.25% per player)
* Reduced Phase 2 scaling (10% per loop, 2.5% per player)
* Reduced Phase 3 scaling (20% per loop, 5% per player)
* Added Phase 4 scaling (5% per loop, 1.25% per player)
* README edits

**1.0.0**
* v1 release!
* Implemented a custom flow for the fight
* Fixed slow item steal (0.9.6 goof)
* Reduced clone scaling (1 per player - maxes out at 4 cuz lag)
* Removed some config (constant Malachite Mithrix, clone count, phase 2 skip)
* Removed the scuffed weaken
* Mithrix gains a 15% stat buff phase 2 (scales 15% per player)
* Mithrix gains a 30% stat buff phase 3 (scales 30% per player)
* Mithrix becomes corrupted in phase 3
* fixed placement of phase 4 pizza slam
* README edits

**0.9.6**
* done goofed and left perfected mithrix instead of malachite

**0.9.5**
* Adds config for debuff resistance (freeze/nullify)
* Makes Phase 1 Mithrix 15% weaker with some stats
* Halves Phase 1 skyleap wave and hammer slam wave/orb
* Phase 3 Mithrix is no longer weakened and gets a 10% buff per player to most stats
* Shadow clones spawn per player now
* README edit

**0.9.2**
* Removed raincoats except for full immunity config
* Made Mithrix immune to freezing and the tentabauble
* Added shrapnel wave to hammer slam
* Doubled shrapnel wave when he exits his sky leap
* Beefed up default stats
* Added config for sky leap wave
* README edit

**0.8.8**
* Added config for Phase 2 skip
* Added config for debuff resistance as the default instead of full immunity - raincoats without barriers and lower regen
* Added config for Malachite Mithrix for a more dangerous arena (off by default)
* refactored code
* README edit

**0.8.4**
* Added debuff immunity to Mithrix (configurable)
* refactored some more
* README edit

**0.8.0**
* Should work fine I want to refactor/add more or catch any bugs before making it a 1.0.0 release.
