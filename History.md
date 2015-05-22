

# History? #

> While cleaning out some draft documents, I found a .doc containing the following notes. I figured I might as well include it in the project's wiki in case there was any curious minds about the history of Project Yellow -> Yelo -> `OpenSauce`. For the most part it's a straight copy-paste from the original document, but there were a few edits (eg, for discussions on problems which are no longer actually problems, etc)

# Introduction #

> Project Yellow (later abbreviated to just “Yelo” for user simplicity sakes) originally started off as a very small venture into using DLL hooks (both by force and by a DirectX proxy) to perform custom operations in Halo PC. I partnered up with Shaun W. (moniker: ‘bitterbanana’) on this project as he was originally the one who brought up the methods. For the curious: the name was derived from the fact that our monikers were both food related, whose colors are also yellow. Also because of the Coldplay song by the same name, 'Yellow'. Don’t judge us.


# Halo PC #

> We started off with rather simple stuff. Shaun mostly dazzled with camera magic, putting in his Third Person modification (originally an external application written in VB6) along with a custom reticule. I spent my time transitioning my research of the engine into usable code in our Yelo project. I also had to bring Shaun up to speed on how the game state was designed and functioned.

> Months drove on with the codebase starting to become a somewhat manageable mess and more nifty little ditty modifications came about. Such things included custom external scripting components, which allowed us to make the global gravity scriptable, and _multi-team vehicles_ (or MTV; ie, different team members can ride in same vehicle). Halo 2 had it, so we figured Halo 1 MP wouldn’t hurt to have it. Snazzier constructs started come about as well.

> One of them well worth mentioning was our Gravity Gun. It wasn’t a first class object itself (like the Physics Gun in the popular [GMod](http://en.wikipedia.org/wiki/Garry%27s_Mod#Gameplay)) but rather a set of code which ran based on an object’s view and a unit’s input (so it wasn’t fixed to a player datum...hell, an AI could have potentially used it). The Gravity Gun was a pretty bad hack in terms of engine interaction: all it did was modify the basic `object_datum`’s world position and velocities. While we could filter the object types it could interact with, this still didn’t always solve the issue that I believe stems from “connecting” objects to the bsp concept. I originally noticed this when I had the bright idea to grab a rocket which I had just shot. I increased its speed and then finally let it fly off into a wall. Bam. Crash. I shrugged it off at first; I didn’t expect to allow projectile types to be mimicked in a public release anyway, better to spend my time frying bigger fish (coming up next). However, whilst on a map, I had grabbed a vehicle and started moving very far across the map. Somewhere in between letting go of it to maybe trying to get into it, the game crashed. I then set the feature aside to later try to find a more proper implementation (possibly a function which disconnected the object for editing then reconnecting when we let go?) to see if the problem persisted. I never had the time to do this sadly.


# Halo 1 & 2 Xbox #

> The project would later evolve to consume a project another friend, Mike D. (moniker: xbox7887), and I were working on for the Xbox version of Halo 1 & 2. Initially, we were using a custom Xbox Dashboard to launch our “trainers” (or as I prefer to call them: game modules). Due to this method of code injection, we were limited to about ~11KB of x86 assembly code and data. Other games didn’t push the RAM to its limits (which H1 & H2 do) so I’ve heard of larger module usage.

> We were doing things like allowing users to dump screenshots (both normal, faked hi-res, and panorama), custom debug camera, Skull enabling\disabling, allowing AI in MP modes, etc. As you can probably imagine, these features and their implementations in pure assembly code started to rack up in size after a while. Even with some of more ambitious assembly hacks we were able to conjure up. It was time to think outside the box.

> I forget which box I found myself wondering around, dazed and confused, when I thought of this solution but it must have been black and monolithic. The solution to our problems was based on compiled C (with some usable C++ language constructs) code, linked into a very bare DLL file. From this DLL and optionally a .MAP (Linker Address Map) file we would then process the code and data so that the addresses were relocated to a base address which we were going to inject the module at in the game memory. The .MAP option was for finding any implicit compiler/linker clashes (eg, any at-start or at-exit routines) or unneeded/garbage code/data which we could exclude in our Module Linking process to further reduce the required memory needed to run a module.

> With the power of a high-level language like C++ I was able to create a custom Xbox SDK which Module codebases could link to. Then those modules could also call Xbox API functions and run their own C code. This reduced code maintenance ten-fold. Not only could we now have an abstract Xbox SDK (abstract to an extent and in terms of the game we were targeting) we could link to, but we were able to expand on the things we could target due to struct/class definitions and truly reusable code.

> However, this new technology didn’t get pushed very far. Mike himself was growing busy in his personal life and was really only fluent in x86 and some C# at the time. I stopped any major development for the system around the release of Halo 3 (was also when I got my 360 XDK which distracted me from any current Xbox 1 works)


# `OpenSauce` SDK #

> Eventually in 2008 I would come to realize that this codebase we had created would be better off in an open source environment. We weren’t doing any real work on the Yelo system (however, Shaun did release a module for both PC and Custom Edition named Yelo: _Battery_ earlier) and both of us were distracted by real life so I didn’t want to neglect the community of something we had been showing off to them; it didn’t settle well with me. Seriously, a tear just ran down my cheek. I should get that leak fixed.

> So I spent a few months creating a somewhat new codebase for `OpenSauce`. I wanted to clean up the dump which Yelo’s code came to be. Along with crafting a more recent codebase, I set out to quash any bugs like any good developer would and to also finish something that had, until then, been a personal project: `CheApe`.


## `CheApe` ##

> CheApe was thought up after I reviewed a GDC [presentation](http://halo.bungie.net/Inside/publications.aspx#pub15064) by Chris Butcher ([archived link](http://web.archive.org/web/*/http://www.bungie.net/images/Inside/publications/presentations/publicationsdes/engineering/butcher_gametech04.pdf)). One of the concerns which really sparked my attention was the fact that designers couldn’t edit or annotate tag fields. I wanted to prototype a system which would not only work for defining tag definitions, but allowed for post-compile editing of the editor specific values for those definitions so designers can make comments (e.g. for a ‘real’ field, “1.2 to 2.4 are good values”).

> I had already done extensive research with the Halo 1 toolkit so I decided it would just be better to create a tool which processed XML data into a memory mappable data file, then use a DLL with my own code to hook in functionality. The data file would act just like a game cache file: pre-baked data located at a fixed base address, load said data from file to that base, perform fixup operations, and continue normal toolkit code flow. Initially I ended up programming support for statically built byte-swap definitions in the data file but I eventually figured out how to setup the toolkit hook so that the engine did all that for me. I figured it was good practice implementing it anyway so nothing meaningful was lost in adding the support.

> So here it was, `CheApe`: an extension to the Halo 1 toolkit which could allow new tag definitions. Since this was just a prototype and an extension to alien software I didn’t see a need to add support for changing existing Blam Engine definitions. However, later I did implement custom script definition and data fixup support.

> At the end of the day, `CheApe` isn't a real viable solution for providing developer hooks for things like custom comments or annotations. A more ideal solution would be to separate the struct definitions from the editor definitions (which is what I believe Bungie has done since at least Reach). However, since Halo1 and Halo2 are released and no longer updated, `CheApe` was great for extending the game's tools.

The name "`CheApe`" is based on the logo used by Guerilla. They used a Planet-of-the-Apes-style logo of Che Guevara ([before](http://2.bp.blogspot.com/_uGeb2QOar5U/SUt-rXq3zGI/AAAAAAAAAWU/XVU5MX5KLNE/s1600/ORIGINALE.jpg), [after](http://lh5.ggpht.com/_xM6S1O620QE/StF8JgvVQOI/AAAAAAAAUyA/QMiyySHXLCg/Ape%20As%20Teen%20Nazi%20Youth%20Member%20Skinhead%20Orangutan%201.jpg)). Hence, the 'Che' and 'Ape', as `CheApe` was originally suppose to be an extension for Guerilla only.


## `Halo1_CE` (Yelo) Codebase ##

> The meat of the SDK; this is where the game changing code for Halo Custom Edition can be found. The project is named 'Halo1\_CE' is due to the Halo2 (Xbox) code being added to the codebase and thus needing to have some kind of unique naming scheme for each game Yelo is implementated for. Refer to the [naming conventions for more information](http://code.google.com/p/open-sauce/wiki/SourceTree)

> This codebase is really just a refactored and cleaned up Project Yellow. Notable changes from Project Yellow to `OpenSauce` include using boost C++ libraries, switched to a specialized include files for all compile-time game engine addresses (ie, addresses within the game exe), polished Networking code, removed statistics collection from public release in favor of the “Synapse” extension (which would be closed source), started working on game simulation/film support for Synapse’s development (that too, closed source).


### Synapse Extension ###

> Cerebrum and Synapse were never finished. Synapse was/is a specialized build of OS for the HaloCE dedi that aggregates game events and such into a game "film" (much like Halo3+ game films) which would then later be parsed for player and game stats (plus additional automated analysis like detecting cheaters).


## `Halo1_CheApe` Codebase ##

> This codebase is where the toolkit hook code resides. Tool, Guerilla, Sapien; we use specialized build configurations to produce a DLL which is loaded by each respective modified `OpenSauce` tool EXE (eg, OS\_Guerilla).