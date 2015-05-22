

# Introduction #

Both the game and editor extensions use XML files to store user-configurable settings. For the game, most of these are settings are configured using the in-game menu. The rest require editing the actual XML.


# Halo1\_CE #

You can reference the game [template file](http://code.google.com/p/open-sauce/source/browse/OpenSauce/Halo1/Halo1_CE/YeloSettingsTemplate.xml) for a low level break down of what settings are stored and used by the game.

Upon running OS with the game for the first time, two XML files will be created in your "My Documents\My Games\Halo CE\`OpenSauce`\" folder: OS\_Settings.User.xml and OS\_Settings.Server.xml. One file `OS_Settings.PostProcessing.xml` will be present after installing OS as it has no options that are set by the runtime.

**Note: The OS in-game menu can be activated by pressing F7 while in-game**

## OS\_Settings.User.xml ##

The user settings file controls options that change the features available in the client version of the game.

Single elements:
| `<cacheFiles mainmenuScenario="" />` | Set this to the scenario name of the ui map you wish OS to use. Eg, the scenario name for the stock "ui.map" is "levels\ui\ui". If you had a map "ui\_custom.map" and its scenario name was "levels\ui\ui\_custom", you would set the attribute like so: mainmenuScenario="levels\ui\ui" |
|:-------------------------------------|:-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

### dx9 Settings ###

The `<dx9>` element contains all of the settings that relate to the graphical additions and/or changes that Open Sauce contains.

Single elements:
| **Element**                                      | **Usage** |
|:-------------------------------------------------|:----------|
| `<nVidiaActiveCamouflage basic="false" />`       | When using an nVidia card this setting controls whether the active camouflage defaults to the actual camouflage effect or just a basic transparency. |
| `<Upgrades dynamicTriangles="false" />`          | The upgrades element controls whether experimental attempts to increase the rendering limits of the game are enabled. |
| `<GBuffer enabled="true" />`                     | The GBuffer option toggles whether the geometry buffer is drawn to in the game. This can be turned off to increase framerate at the expense of some effects that rely on it. |

`<PostProcessing>` element:

The `<PostProcessing>` element contins settings that control the post processing system and its options.

| **Element**                                      | **Usage** |
|:-------------------------------------------------|:----------|
| `<MotionBlur enabled="true" amount="1" />`       | Controls whether motion blur is enabled, and how much blurring is applied when it is. |
| `<FXAA enabled="false" />`                       | Controls whether the anti aliasing post process is applied. |
| `<Internal enabled="true" />`                    | Controls whether internal, map based post processes are applied. |
| `<External enabled="true" />`                    | Controls whether external, user set post processes are applied. |

`<ShaderExtension>` element:

The `<ShaderExtension>` element contains settings that control whether the rendering additions added by Open Sauce are enabled or not.

**`<Model>`**

| **Element**                                 | **Usage** |
|:--------------------------------------------|:----------|
| `<NormalMap enabled="true" />`              | Toggles whether normal maps on shader\_model's are enabled |
| `<DetailNormalMaps enabled="true" />`       |	Toggles whether detail normal maps on shader\_model's are enabled |
| `<SpecularMap enabled="true" />`            |	Toggles whether specular color maps on shader\_model's are enabled |
| `<SpecularLighting enabled="true" />`       | Toggles whether specular lighting on shader\_model's is enabled |

## OS\_Settings.Server.xml ##

The settings file for the dedicated server currently has no options that the user has to set manually.

## `OS_Settings.PostProcessing.xml` ##

This settings file controls the external post processing system, which allows you to add your own post process effects at runtime. The main bulk of the file is initially commented out so that the system is not needlessly trying to use the settings when they are not in use. Full documentation of the post processing settings can be found in the [post processing system documentation](Doc_Halo1_PostprocessingFull#The_External_System.md).

## `OS_Settings.ModSets.xml` ##

Users may register a maximum of **32** different mod-sets. Each mod-set is registered by defining a `<ModSet>` element inside the `<modSets>` element. When a developer builds a new map with a mod-set name assigned, this data is automatically added to said dev's `ModSets.xml`.

| **Element**                | **Usage** |
|:---------------------------|:----------|
| `<ModSet name="" />`       | 'name' is the mod-set name, eg, "h2ce" |


# `Halo1_CheApe` #

## OS\_Settings.Editor.xml ##

The editor settings XML located in your HEK directory allows you to define different paths and options for different projects without having duplicate directories. You can reference the editor [template file](http://code.google.com/p/open-sauce/source/browse/OpenSauce/Halo1/Halo1_CheApe/EditorSettingsTemplate.xml) for a low level break down of what settings are used by the editor.

The editor options are separated into `<Profile>` elements under `<profiles>`.
Each profile must have a unique name, which is used in a command line argument added to OS Guerilla and OS Sapien '-editorProfile:

&lt;profile-name&gt;

' to make the program use the settings from that named profile. This allows you to set up shortcuts for OS Guerilla and OS Sapien that target different sets of tags without having to duplicate your HEK directory or rename your tags folders to switch projects.
The `<osEditorSettings>` element has a 'defaultProfile' attribute, which defines which of the profiles to use when OS Guerilla or OS Sapien are launched without the '-editorProfile:' argument.

### Profile ###

Attributes:
| **Attribute**       | **Type**  | **Usage** |
|:--------------------|:----------|:----------|
| name                | string    | A unique string identifier for the profile. |
| affinityMask        | integer   | Sets which processor(s) the editor tools are set to use on a multi-processor computer. |
| ignoredByGuerilla   | bool       | When true, OS Guerilla ignores the profile settings, instead using the default paths. |
| ignoredByTool       | bool       | When true, OS Tool ignores the profile settings, instead using the default paths. |
| ignoredBySapien     | bool       | When true, OS Sapien ignores the profile settings, instead using the default paths. |

Child Elements:
| **Element** | **Type** | **Usage** |
|:------------|:---------|:----------|
| root        | string   | Defines the directory to use as the root directory, which affects where compiled maps are saved to. |
| tagsName    | string   | Allows you to set a different directory to use as the tags directory so that multiple tag directories can exist side by side. |
| data        | string   | Sets the absolute path to use as the location of the data directory. |
| tool        | N/A      | TBD       |