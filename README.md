# FontFixer

Replaces the in-game font with an expanded version with support for more characters.

Specifically, this mod replaces all instances of the IBM-3270 font texture with a recompiled one which includes the Extended ASCII space. This directly provides support for all diacritics for Latin characters.

It additionally injects a new set of fallback font textures, built using [Unifont](https://unifoundry.com/unifont/index.html). This provides support for an additional ~65,000 characters spanning the full Unicode Basic Multilingual Plane.

## Demonstration

Before:

![](https://raw.githubusercontent.com/EliteMasterEric/FontFixer/master/Art/brokenCN.png)

After:

![](https://raw.githubusercontent.com/EliteMasterEric/FontFixer/master/Art/fixedCN.png)

## Issues
Report any issues on the Lethal Company Modding Discord.

## Credits
- EliteMasterEric: Programming
- FoguDragon: Playtesting
