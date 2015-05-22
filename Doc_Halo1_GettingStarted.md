

# Introduction #

It is assumed that you have downloaded and are using the latest version of `OpenSauce`. _**Note that OS only supports v1.09 of HaloCE.**_

# Common Notes #

Debug and various other files (eg, sapien\_model\_index.dat) that use to be created in the same directory as the .exe which created them are now all found in "My Documents\My Games\Halo CE\OpenSauce\Reports\". This obviously only applied to cases where you're using an OS program. If you're using a stock (ie, non-OS) version of a tool or game, it's the same shit, different day, in terms of functionality.


# `OpenSauce` for the HEK #

The installer for OS uses OpenSauceIDE to apply the "CheApe" modifications to copies of the HEK exes.

## How it all plugs in ##
When the "CheApe" modifications are applied, OSIDE first makes copies of the HEK exes. Eg, it makes a copy of tool.exe and names it OS\_tool.exe. All copies use the "OS_" prefix. The copy is then modified to load a specific build of the CheApe dll. Eg, OS\_tool.exe will load CheApeDLLT.dll, where the letter before the extension corresponds to the first letter of the tool (so, G for Guerilla, etc)._

We now have the "OS HEK". There's one more, dynamic, component to this setup though: CheApe.map. _**This is not a map file like bloodgulch.map**_. It's a memory cache of prebaked binary data which the CheApe DLLs all load and then use to fixup their owning exe (eg, OS\_tool.exe) with the various definitions. Definitions found in CheApe.map include custom tag groups, script functions, etc.

When it comes to new GFX, scripting, etc features, none of them are actually implemented in the HEK. They're only stubbed out enough to allow the tools to interface or make use of them. So you won't see detail maps in Sapien or be able to use physics\_set\_gravity in the console. **Such things can only be fully used in game.**

## Setting up your .scenario ##

OS makes a couple of modifications to the scenario. One, it changes the very first field, a tag\_reference, become a project\_yellow tag reference. This is how we link in OS-specific data.

Another modification is to the hs-syntax-data. OS-made scenario tags by default have an upgraded script node count. **This count makes the scenario tag incompatible with the stock HEK.** _But_, you can still use the stock build-cache-file command in OS\_tool.exe to build .map files compatible with the stock game. With that said, you should only be using to the OS HEK to create OS-bound maps. Use the stock HEK to create regular maps and tags.

## Setting up your .project\_yellow definition ##

The .project\_yellow tag group can be considered OS's equivalent to a "scenario" tag. This tag is referenced by .scenario tags. So you must first create a new .project\_yellow tag, just like you would any other new tag. By convention, it's best to save this in the same directory as the owner .scenario tag.

Let's take levels\test\tutorial\ for example. We would first copy levels\test\tutorial\tutorial.scenario and rename it to tutorial\_os.scenario (remember, scenario made/edited by the OS HEK can't be read by the stock HEK). We would then create levels\test\tutorial\tutorial\_os.project\_yellow. With tutorial\_os.scenario open in OS\_Guerilla, we would then set the "project yellow definitions" field to reference our tutorial\_os.project\_yellow tag.

Now our scenario is setup to to use custom project\_yellow data.

## Setting up your .project\_yellow\_globals definition ##

Setting up a project\_yellow\_globals tag is pretty simple. By convention, this should be stored in your globals\ folder. For example, let's say we created a globals\globals\_os.project\_yellow\_globals tag. In our tutorial\_os.project\_yellow we created earlier, we would then set the field named "yelo globals" to reference our globals\globals\_os.project\_yellow\_globals tag.

Note that you can also setup your project\_yellow tag to use a **different** stock .globals tag as well, via the "globals override" field.


# `OpenSauce` for the Game #

The installer for OS should take care of pretty much everything needed to use the extensions with the game (eg, putting the DLLs in your selected HCE install path, patching the game/HEK, etc). There are a couple of cases where user intervention is required (eg, when installing a mod's Mod-Set data files), but let's first go over how OS hooks into the game exes.

## How it all plugs in ##
For the client (haloce.exe), we use a proxy d3d9.dll file (aka a 'dll hook'). What this means is that we compiled all of our code into a dll, renamed it to a dll the game looks for and uses, then we ourselves load the **real** dll and run off that, and finally, we place our dll into the same directory as the exe file. Essentially we wrap the DirectX 9 APIs.

For the dedi (haloceded.exe), we actually copy the exe, rename the copy to OS\_haloceded.exe, and then modify that exe to load our "OpenSauceDedi.dll" (which is based on the same code used to build our proxy d3d9.dll, except it's made just for and has features specific to the dedi server).

## Playing the game with OS ##
There's nothing you need to do to run haloce.exe with OS, it's all taken care of behind the scenes. If, however, you want to play haloce.exe **without** running the OS extensions, you'll need to move the d3d9.dll out of your haloce.exe's directory (and then put it back when you want to use it again).

For the dedicated server, you need to run OS\_haloceded.exe instead of just haloceded.exe. This setup also makes it easier for server hosters to run both stock and OS servers at the same time, from the same directory.

## .yelo maps ##
.yelo map files are map files that are specially made for features/settings in OS that are incompatible with the stock game. Things like increased memory, mod-sets, new script support, etc all fall into this category.

How it .yelo support implemented? We override the normal map loading process so that if a .map file isn't found, it tries looking for a .yelo file instead. Eg, if the game says it wants to run "zanzibar", it will still try to load zanzibar.map first. If it exists, everything goes about as normal. If it doesn't, it tries to load zanzibar.yelo instead. If neither exist, then the game does the default handling of a non-existent map file.

If both zanzibar.map and zanzibar.yelo exist at the same time, zanzibar.map will **always** be loaded first. zanzibar.yelo will never be loaded until zanzibar.map is removed.

## Installing Mod-sets ##
**TBD**