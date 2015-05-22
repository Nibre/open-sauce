

# Parameter Types #
  * String: a string value
  * `TagName`: Path to a specific tag definition (without the group extension)
  * `DataName`: Path to a specific file in the data sub-folder
  * Switch: can be a boolean value (true/false) or switch (0/1)
  * Directory: Path to a folder in the tags or data sub-folder

# Commands #

## build-cache-file-ex ##
Builds a cache file with extra Open Sauce arguments. Output cache file will be in .yelo format instead of .map if use-memory-updates is on.
| **Parameter**         |  **Type**   | **Help** |
|:----------------------|:------------|:---------|
| mod-name              |  String     | The name of the mod this scenario belongs to. This will also be used when naming the new data-files |
| create-anew           |  Switch     | Should new data-files be created before building the cache? |
| store-resources       |  Switch     | Should the scenario's bitmaps/sounds/locale data be stored in the data-files? |
| use-memory-upgrades   |  Switch     | Does the scenario require Open Sauce's memory upgrades to run? **Produces .yelo file instead of .map** |
| scenario-name         | `TagName`   | Name of the .scenario to use. Just like the stock build-cache-file |

You'll need to turn use-memory-upgrades on if:
  * You're using mod-sets to store-resources externally in those data files
  * You're using OS-specific script functions or globals
  * Your scenario's scripts need more than the standard amount of script limits to compile.
  * Your map needs more than the standard amount of tag memory.
  * Your map needs more than the standard amount of tag instances.


## compile-scripts ##
_Not Implemented_
| **Parameter**   |  **Type**   | **Help** |
|:----------------|:------------|:---------|
| scenario-name   | `TagName`   |          |


## compile-shader-postprocess ##
Creates shader\_postprocess\_generic tags from HLSL .fx shaders in the data directory
| **Parameter**      | **Type**    | **Help** |
|:-------------------|:------------|:---------|
| shader-directory   | Directory   | Path that contains the shaders to compile |


## help ##

| **Parameter**       | **Type** | **Help** |
|:--------------------|:---------|:---------|
| yelo-tool-command   | String   |          |


## import-structure-lightmap-uvs ##
Replaces the lightmap UVs of a bsp with custom ones loaded from an obj file. The obj file must have lightmaps grouped by object and in ascending order (lightmap\_0 to lightmap\_n). Best way to ensure this is to export a fresh lightmaps obj using Aether, import it into your modelling program then **ONLY** edit the UV's to ensure the model matches the bsp.
| **Parameter**   |  **Type**    | **Help** |
|:----------------|:-------------|:---------|
| structure-bsp   | `TagName`    | location of the target bsp, relative to the tags directory |
| obj-file        | `DataName`   | location of the source obj, relative to the data directory |


## build-cpp-definition ##
Creates a c++ definition header file mapping out the tag structure of a specified tag.
The resulting header file is saved to the working directory of OS\_Tool.
| **Parameter**       | **Type** | **Help** |
|:--------------------|:---------|:---------|
| tag-group           | String   | 4 character identifier for the tag type type you want a definition of. E.g., bitm |
| add-boost-asserts   | Switch   | Controls whether to add BOOST\_STATIC\_ASSERT code to the end of struct to ensure the struct sizes are correct |


## runtime-cache-view ##
Reads the tag cache and tag instances from a running instance of haloce, displaying the addresses and values of tag fields to easier reverse engineering. Also has basic memory writing for runtime editing of tag values.
| **Parameter** | **Type** | **Help** |
|:--------------|:---------|:---------|
| _no params_   | _none_   | _none_   |