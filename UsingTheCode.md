

# Introduction #

This wiki entry should give you the run down of how the code is configured and what you need (and optionally need) to have in order to compile the code

If you already have a clone of the codebase, you can run the [PrereqChecks.bat](http://code.google.com/p/open-sauce/source/browse/bin/_scripts/PrereqChecks.bat) script to verify your machine is ready for working with the code. There are _some_ libraries it can't check for, however (refer to the bat source for more).

# Base Requirements #

For starters, the only supported IDE is Visual Studio 2008 (and the compilers found in it). [VS2010 has some pretty big changes](http://msdn.microsoft.com/en-us/library/bb531344.aspx) to the VC++ project files. Since there's no real need to upgrade the projects to 2010, I'm not mucking with figuring out the changes needed. If you use VS2010, you're on your own.

Next, you'll need is the latest version of the [DirectX SDK](http://msdn.microsoft.com/en-us/directx/aa937788).

Finally, you'll need the [C++ Boost](http://www.boost.org/) libraries. At the time of this we're writing, we're only using the preprocessor and file system libraries from version 1.43. You may also want to look into the installers [BoostPro](http://www.boostpro.com/download/) makes.

# Specific Requirements #

## `BlamLib` ##

`LowLevel` can optionally build some additional code if you have a copy of the XDK. You will need to remove the LOWLEVEL\_NO\_X360 define from the project config to even begin to enable this code. If you have the XDK, I assume you aren't just some script kiddie and can handle some of the other small compiler hurdles that will follow

`BlamLib` references the [SlimDX SDK](http://slimdx.org/), a .NET wrapper for the DirectX.

## `OpenSauce` PC and HEK ##

HaloPC uses GameSpy for its multiplayer framework. `GameSpy`'s [OpenSDK](http://developer.poweredbygamespy.com/user/register) also has some useful HTTP libraries, which we use in Halo1\_CE. If you don't have a copy of OpenSDK (it's free for indies), then you'll need to configure Halo1\_CE's Precompile.hpp to reflect this and you'll also need to remove the SDK's file references in the vcproj. In VS's Solution Explorer, these files can be found under Halo1->Halo1\_CE->Common->Shared->`GameSpy`

In the lastest OpenSDK they have some code that won't compile under `OpenSauce`'s default settings. We use stdcall conventions where OpenSDK uses (or assumes) cdecl. Because of this, you'll need to change the signature of **gsiCoreAtExitShutdown** in _gsCore.c_ to explicitly be cdecl.

## `OpenSauce` Xbox ##

TBD

## `OpenSauce` Xenon ##

Chances are high that all code found in the Xenon part of the codebase references the XDK. You'll need that to compile the projects.