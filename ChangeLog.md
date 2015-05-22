

# `Halo1` #
## `Halo1_CE` ##
### Version 3.1 ###
| PATCH | project\_yellow\_globals tag features new flag for forcing the game to use 'stun jumping penalty' instead of 'turning penalty' in the engine's biped jump code |
|:------|:---------------------------------------------------------------------------------------------------------------------------------------------------------------|
| PATCH | non-multiplayer maps will no longer be selectable from the MP map list                                                                                         |
| CHANGE | Game hooking is now done via dinput8.dll instead of d3d9.dll                                                                                                   |
| CHANGE | Removed XFire restriction now that we use dinput8.dll                                                                                                          |
| CHANGE | Changed the server list to list all servers regardless of their version                                                                                        |
| CHANGE | Made OpensauceIDE an optional installation                                                                                                                     |
| CHANGE | Made XP SP3 a minimum requirement when installing                                                                                                              |
| CHANGE | Installation now requires .NET 3.5                                                                                                                             |
| CHANGE | Increased hardcoded 'k\_game\_state\_allocation\_maximum\_size\_for\_yelo' by 0x970 bytes                                                                      |
| CHANGE | Increased hardcoded 'k\_runtime\_data\_max\_values\_count' to a total of 64 (up from 32)                                                                       |
| CHANGE | Made the GBuffer require SM 3.0                                                                                                                                |
| CHANGE | Clients now use "initc.txt" for their init.txt file                                                                                                            |
| ADD   | Added flag to use specular colour map alpha as a specular lighitng exponent mask                                                                               |
| ADD   | Make screenshots get saved to unique directorys to stop them from being overwritten                                                                            |
| ADD   | Upgraded the max number of particles active/rendered at once                                                                                                   |
| ADD   | Upgraded the max number of objects rendered at once                                                                                                            |
| ADD   | Ability for mainmenu maps to have animating objects                                                                                                            |
| ADD   | Added an xml setting for completely turning off game update checks                                                                                             |
| ADD   | Added an in-game map download system to the client                                                                                                             |
| ADD   | Added a HTTP server to the OS dedi                                                                                                                             |
| ADD   | Added a map server component to the OS dedi                                                                                                                    |
| ADD   | Added an installation validation component to the installer                                                                                                    |
| ADD   | Added file patching component to the installer                                                                                                                 |
| ADD   | Added new setting for forcing OS to use .yelo files first when searching for .map files (off by default)                                                       |
| ADD   | Script functions for bit operations (AND, OR, etc) and converting hex string to an integer                                                                     |
| ADD   | Script function for playing a .bik movie just like how the engine plays credits.bik, etc (local games only)                                                    |
| ADD   | New tag/script system for displaying ui widgets to the user                                                                                                    |
| ADD   | Added system for adding custom dedicated server logging events                                                                                                 |
| ADD   | Runtime vector3d values (part of the game state)                                                                                                               |
| ADD   | Added a mini crashdump creator for easier debugging                                                                                                            |
| ADD   | Added command line argument to disable all OS graphics (-no\_os\_gfx)                                                                                          |
| ADD   | Added command line argument to disable special OS game state allocations (-no\_os\_gamestate)                                                                  |
| ADD   | Added Visual C++ 9.0 runtime to the installer                                                                                                                  |
| FIX   | Fixed Halo not loading when using -use|
| FIX   | Fixed crash in shader model settings when extension element is missing                                                                                         |
| FIX [39](http://code.google.com/p/open-sauce/issues/detail?id=39) | Fixed GBuffer issues with stretched models and incorrect velocity when using custom models with high bone counts                                               |
| FIX   | Fixed shader models' specular lighting being dulled by detail maps when using detail before reflection                                                         |
| FIX   | Fixed an exception on load when the device doesn't support hardware rendering (Intel integrated)                                                               |
| FIX   | Fixed a bug where a non-existant map (of any kind) would cause the 'missing -mapname-.yelo' exception, when the stock game already had code to handle such cases |
| FIX   | project\_yellow tag's "prohibit\_multiteam\_vehicles" flag should work as expected now                                                                         |
| FIX   | Multi-team vehicles should no longer allow 'double loading'                                                                                                    |
| FIX   | Fix for instant respawns when using the OS dedi                                                                                                                |
| FIX   | Fixed the -path argument being ignored by OS                                                                                                                   |
| FIX   | Fixed the roaming/appdata folders made by OS not being removed on uninstall                                                                                    |
| FIX [56](http://code.google.com/p/open-sauce/issues/detail?id=56) | Fixed the team index offset for MP teams for the GBuffer. MP teams now start at 10, not 9. This is a breaking fix, but I'm not aware of any major MP map releases that used this specific part of postprocessing |

### Version 3.0.2 ###
| Type | Description |
|:-----|:------------|
| PATCH | Made a work-around for the HaloPC engine bug where model nodes over 43 were rendering wrong. |
| ADD  | Verbose script definition mismatch messages when developer\_mode is >= 4 |
| FIX  | Script definition mismatch error message erroneously prompting |
| FIX  | Fixed tag-based gravity settings problem |

### Version 3.0.1 ###
| Type | Description | Misc |
|:-----|:------------|:-----|
| REMOVED | Vehicle Views from F7 menu (it's unimplemented) |      |
| ADD  | Implemented project\_yellow's gravity\_scale override field |      |
| ADD  | Prompts user with a MessageBox when a map contains mismatching yelo script definitions with the current build of OS |      |
| ADD  | Bloom toggle added to the users settings |      |
| ADD  | Shader extension toggle added to the user settings to completely disable it if needed |      |
| CHANGE | Motion blur is now disabled by default |      |
| FIX  | Fixed potential issue when a proxy calling OS stores the device pointer in a local variable |      |
| FIX  | Fixed possible issue where _global\_yelo could reference invalid memory_|      |
| FIX [19](http://code.google.com/p/open-sauce/issues/detail?id=19) | Unregistered mod-sets crash game w/o warning |      |
| FIX [17](http://code.google.com/p/open-sauce/issues/detail?id=17) | External PP effects problem w/ single model mask |      |
| FIX [14](http://code.google.com/p/open-sauce/issues/detail?id=14) | Weapon Views not saving correctly | Thanks jellymasterer! |
| FIX [13](http://code.google.com/p/open-sauce/issues/detail?id=13) | OS\_haloceded crashes on startup |      |
| FIX [11](http://code.google.com/p/open-sauce/issues/detail?id=11) | Black screen when `DisableAlphaRenderTargets` is set | Thanks as7raios! |
| FIX [8](http://code.google.com/p/open-sauce/issues/detail?id=8) | HUD scaling causes problems with UI | Thanks `SeanAero`! |
| FIX [5](http://code.google.com/p/open-sauce/issues/detail?id=5) | HUD scaling has chatbox problem w/ hi-res | Thanks `SeanAero`! |


## `Halo1_CheApe` ##
### Version 3.1 ###
| PATCH | Weapon's magazine's magazine-objects block will no longer crash when you try to add more than 2 elements. Uses the equipment field for the block name now too. |
|:------|:---------------------------------------------------------------------------------------------------------------------------------------------------------------|
| PATCH | "The %s shader %s cannot be used in the environment" will now output the correct shader type in all cases                                                      |
| PATCH | project\_yellow\_globals tag features new flag for forcing the game to use 'stun jumping penalty' instead of 'turning penalty' in the engine's biped jump code |
| PATCH | project\_yellow tag features new flag for forcing game update logic to ignore if players can see various game entities (flag only acknowledged in mainmenu scenarios) |
| ADD   | project\_yellow\_globals\_cv tag; unit\_infections data were also moved to here                                                                                |
| ADD   | Mini-dump support for tool and sapien builds (no guerilla support)                                                                                             |
| ADD   | Increased the number of grenade types to 4                                                                                                                     |
| FIX   | Increased the maximum buffer size for model vertex and index data. Tool crashes (buffer overflow) if you actually hit this limit (Gearbox's fault, not ours!)  |

### Version 3.0.2 ###
| Type | Description |
|:-----|:------------|
| PATCH | Made a work-around for the HaloPC engine bug where model nodes over 43 were rendering wrong. |
| ADD  | Increased the maximum amount of files tool will process during an import (this means tool won't stop importing after 500 animations) |
| CHANGE | Increased maximum animation seat elements from 32 to 64 in light of the increased max animations |
| CHANGE | Increased maximum animations from 500 to 2048. Crazy shit. I dare someone to use all 2048. Can I get a Challenge Accepted? |
| FIX  | Corrected the project\_yellow\_scripting.xml data to match the code (should no longer get script mismatch messages with new builds) |
| FIX  | Patched some tool code that is ran when reimporting an animation tag to use the increased limit (wasn't patched before, resulting in buffer overflows) |

### Version 3.0.1 ###
| Type | Description |
|:-----|:------------|
| ADD  | More verbose debug output to compile-shader-postprocess |
| ADD  | More sanity checks and messages to build-cache-fil-ex |
| CHANGE | Moved autogeneration of yelo script definitions to when building tags for runtime |
| FIX	[18](http://code.google.com/p/open-sauce/issues/detail?id=18) | Yelo scripting blocks add **all** definitions |