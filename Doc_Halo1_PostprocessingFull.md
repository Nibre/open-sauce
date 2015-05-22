# OpenSauce Post Processing #
## What is post processing? ##
Generally a post process is something that happens after something else. In the context of computer graphics it can refer to a technique used to change how a rendered frame looks by taking a frames colour data and manipulating it with pixel and vertex shaders before presenting it to the viewer.

Using the post processing system of OpenSauce you can enhance your maps by adding post processing effects that change the visual style of your map or add new gameplay functionality.

## The Post Processing system ##
In OpenSauce there are two applications of the post process system available to the general user:
  * The "Internal" post process system is the system that map makers use to add their own unique visual effects to their maps through the use of custom tags added to the OpenSauce HEK.
  * The "External" post process system can be used by anybody with some HLSL knowledge to customise their own personal installation, adding effects that are globally applied to the game. It also has a major use for map makers, as this system has nearly all of the capabilities of the Internal system, but without the hassle of having to repeatedly compile a map and reload the game, allowing map makers to rapidly prototype their effects.

## HLSL Shaders ##
Please see the full [HLSL reference](Doc_Halo1_PostProcessingHLSLReference.md).

## The Internal System ##
The internal post process system adds per-map effects through the use of four new tags:
  * shader\_postprocess\_generic - Stores your shader and provides an interface to your shaders variables.
  * effect\_postprocess\_generic - Brings your shaders together into a single effect.
  * effect\_postprocess\_collection - Defines which effects to include and how and where they are applied.
  * shader\_postprocess\_globals - Provides basic control over the built in subsystems, on a per-map basis.

### shader\_postprocess\_generic ###
This tag stores your shaders compiled binary code and provides a way for you to set your shaders variables without having to recompile it.

**Techniques**

The techniques block is populated when the shader is compiled in os\_tool. It's purpose is to list up to 3 techniques in the shader, which can be targeted at specific shader models. The block count and technique name cannot be modified manually, but the targeted shader models can be changed post-compile.

| **Field**      | **Type**     | **Usage** |
|:---------------|:-------------|:----------|
| name           | tag string   | The name of the technique. This is automatically set when compiling the shader and cannot be edited manually. The technique name must not exceed 31 characters. |
| shader model   | word flags   | Defines which shader model(s) the technique is intended for. The highest supported shader model is always used. |

**Passes**

The passes block within the technique block is also populated automatically when compiling a shader. The pass block defines how a pass and it's resources need to be applied.

| **Field**      | **Type**     | **Usage** |
|:---------------|:-------------|:----------|
| name           | tag string   | The name of the pass. This is automatically set when compiling the shader and cannot be edited manually. The pass name must not exceed 31 characters. |
| flags          | word flags   | See below. |
| render chain   | enum         | Sets which render chain the pass needs to draw to. |

Render chain values:

| **Value** | **Result** |
|:----------|:-----------|
| main chain | Causes the passes output to be drawn to the main render chain. This must occur on the last pass of an effect for it to be applied correctly. |
| buffer chain | Causes the passes output to be drawn to the secondary render chain, for passing per pixel data between passes and shaders without changing the main chain. |

**Variables**

The tag has a number of variable blocks that all have the same basic functionality, with some exceptions.

All blocks have the following fields:
| **Field**            | **Type**     | **Usage** |
|:---------------------|:-------------|:----------|
| value name           | tag string   | This corresponds to the variable name in your shader that you want to set. |
| value lower bound    | _varies_     | When animated this is the value used as the lowest possible value, not used by default. |
| value upper bound    | _varies_     | Used by default. When animated this is the value used as the highest possible value. |
| runtime value        | enum         | When set, the variable is overriden with a variable taken from the engine. |
| animation function   | enum         | Defines the function used to interpolate between the upper and lower value bounds. Defaults to "one", resulting in the upper bound being used by default. |
| animation flags      | word flags   | See below. |
| animation duration   | real         | Controls how long a single animation cycle takes. |
| animation rate       | real         | Controls how often the variable is updated. |

Animation flags:
| **Flag**               | **Usage** |
|:-----------------------|:----------|
| _invert_               | Flips the animation functions value, essentially swapping the upper and lower bounds |
| _multichannel noise_   | Only applies to variables with 2 or more channels. When the "noise" animation function is used, a different noise value is applied to each channel.|
| _ignore alpha_         | Only applies to colour variables. Variable animation has no affect on the alpha channel, the upper bound alpha value is always used. |

**Exceptions**

_Bitmaps_

Only uses the value name field.

| **Field**      | **Type**        | **Usage** |
|:---------------|:----------------|:----------|
| bitmap index   | short           | When the referenced bitmap contains more than one texture, this is the index of the texture you wish to use. |
| bitmap         | tag reference   | The bitmap tag to use. |

_Bools_

Does not use the upper and lower bounds fields.

| **Field**      | **Type**        | **Usage** |
|:---------------|:----------------|:----------|
| value          | word flags      | enabled - Controls the initial value of the boolean variable. |
| flags          | word flags      | Has one flag, Invert, which inverts the runtime value when it is used. |

**Creating a shader\_postprocess\_generic tag**

To create this tag you use the os\_tool command 'compile-shader-postprocess' to compile a directory of HLSL .fx shaders which will be saved as _shader\_postprocess\_generic_ tags in your tags directory. If a _shader\_postprocess\_generic_ tag already exists for a shader, it's shader binary code is updated with the newly compiled code.

**Child shaders**

The internal post process system allows you to override the variables in a _shader\_postprocess\_generic_ tag by referencing it in a child _shader\_postprocess\_generic_ tag. This allows you to have a single shader tag which contains the shader code and initial variable values, and any number of child shader tags that override the initial variables to create a different variant of the shader. For instance, if you have a shader that tints the scene blue and the blue colour variable is controlled in the tag, you can use that shader as a base for another shader tag that overrides the colour variable to create a child shader that tints the scene green. This is done simply by referencing the original shader in the "base shader" field and adding a variable of the same type with the same value name as a variable in the base shader.

### effect\_postprocess\_generic ###
This tag controls how your shaders are put together to create your final effect.

The _effect\_postprocess\_generic_ tag starts with a quad definition, however the internal system does not currently use it. It is there in case a future application of the post processing system requires a quad definition but does not go through the _effect\_postprocess\_collection_ tag.

| **Field**           | **Type**      | **Usage** |
|:--------------------|:--------------|:----------|
| quad tessellation   | point2d       | Sets how many segments the quad has on the x and y axes. |
| x screen bounds     | real\_bounds  | Defines the start and end points of the quad on the x axis. |
| y screen bounds     | real\_bounds  | Defines the start and end points of the quad on the y axis. |

**Shaders**

All of the post process shaders you want to use in your effect must be added to this block.

| **Field** | **Type**        | **Usage** |
|:----------|:----------------|:----------|
| shader    | tag reference   | Points to a single _shader\_postprocess\_generic_ tag. |

**Shader Indices**

Controls what shaders are used and the order in which they are applied. When you add an entry to the shader indices block you are adding an instance of that shader to the list. Meaning that you can add multiple instances of a shader to the list to create a complex effect from separate shaders. For instance if you wanted to create an effect that blurs the scene, but your blur shader does not create as large a blur as you want, you can simply add multiple instances of the blur shader to the shader indices block to multiply its effect.

| **Field** | **Type**      | **Usage** |
|:----------|:--------------|:----------|
| shader    | block index   | The index of the shader to use for this instance. |

**Exposed Parameters**

The purpose of exposed parameters is to define which variables in the shader instances (if any) are available to be overriden by a post processing subsystem. For the internal system the exposed parameters are connected to through the _effect\_postprocess\_collection_ tag to be changed using Halo script.
Exposed parameters only access a single parameter in a single shader instance. This means that if you used a shader multiple times in an effect the exposed parameter will only affect a single instance of the shader. For instance, suppose you have an effect with two instances of a blue tint shader. The effect will end up tinting the scene blue, twice. However, if you expose the colour parameter of the first shader instance and it is set to green by a post processing subsystem, the effect will end up first tinting the scene green, and then tinting it blue.  So even though the two shader instances use the same shader you can control the parameters of each instance separately via the exposed parameters.

| **Field** | **Type**        | **Usage** |
|:----------|:----------------|:----------|
| exposed name | string          | The name the parameter will be referenced by outside of the effect. |
| parameter name | string          | The name of the shader parameter to expose. |
| shader index | block index     | The shader instance the exposed parameter belongs to. |

### effect\_postprocess\_collection ###

This tag is used to bring the effects together that you want to use in a map and set up where and when they are applied.

**Effects**

The effects you wish to use are added to this block. This block also sets up which of the effects exposed parameters (if it has any) you wish to modify with Halo script, and by what name you wish to reference them by.

| **Field** | **Type**        | **Usage** |
|:----------|:----------------|:----------|
| effect    | tag\_reference  | Points to an effect\_postprocess\_generic tag to include in the collection. |
| name      | tag\_string     | A unique for the effect. This is used to reference the effect in Halo script. |

**Script Variables**

The script variables block connects the exposed parameters in an effect to Halo script via a unique identifier.

| **Field**                | **Type**     | **Usage** |
|:-------------------------|:-------------|:----------|
| script variable name     | tag\_string  | A unique identifier for the script variable. This is used to reference the variable in Halo script. |
| exposed parameter name   | tag\_string  | The name of the exposed parameter in the effect that this variable is connected to. |

**Effect Instances**

An effect instance defines when and where an effect is applied. Each effect instance can be treated as a single entity, so multiple effect instances can reference the same effect and apply it at different points in the render cycle, as well as being active at different times with individual levels of fading.

| **Field**              | **Type**      | **Usage** |
|:-----------------------|:--------------|:----------|
| name                   | tag string    | A unique identifier used to reference the effect from Halo script |
| effect                 | block index   | The index of the effect to use when rendering this instance. |
| render stage           | enum          | The point during Halo's render cycle to apply this effect instance. |
| quad tessellation      | point2d       | Defines how many segments the instances quad should consist of on the x and y axes. |
| x screen bounds        | real\_bounds  | The start and end points of the instances quad on the x axis. |
| y screen bounds        | real\_bounds  | The start and end points of the instances quad on the y axis. |
| activation operation   | enum          | When using multiple activation controls, this defines whether to boolean AND or OR the control values. |

Render stages:
| **Value**                           | **Usage** |
|:------------------------------------|:----------|
| after bsp - before blur             | Applies the effect immediately after all opaque geometry has been drawn and prior to the built in motion blur system. |
| after blur - before alpha's faces   | Applies the effect immediately after the motion blur and before alpha tested geometry is drawn. |
| after alpha'd faces - before hud    | Applies the effect immediately after alpha tested geometry has been drawn, and before the HUD. |
| after hud - before menu             | Applies the effect after the HUD has been drawn and before Halo's menus are drawn. |
| after menu                          | Applies the effect after the menus have been drawn. |

Activation Operations:
| **Value** | **Result** |
|:----------|:-----------|
| all       | The effect instance is only applied when all of the activation controls evaluate to true. |
| any       | The effect instance is applied when any of the activation controls evaluate to true. |

**Activation Controls**

Activation controls allow you to tie whether an effect instance is on or not to one or more engine states, without using Halo script.

| **Field** | **Type**     | **Usage** |
|:----------|:-------------|:----------|
| state     | enum         | The engine state the instance will be controlled by. |
| flags     | word flags   | A single flag, _invert_ which controls whether the controls result should be inverted. |

Activation states:
| **Value**                | **Usage** |
|:-------------------------|:----------|
| initially active         | Controls whether the effect instance is initially made active when the map is loaded. Instances are initially active anyway, so this is only of use when inverted. |
| is in cutscene           | This state will return true whenever the cinematic black bars are displayed in the game. |
| player is zoomed         | This state will return true whenever the local player is zoomed in with their current weapon. |
| player using a vehicle   | This state will return true whenever the local player is in any seat of any vehicle. |

### shader\_postprocess\_globals ###
This tag provide basic control over built in subsystems for your map. Currently the only system you can change is the bloom subsystem. To use this tag simply add it to your maps _tag\_collection_ tag.

**Bloom Globals**
| **Field**             | **Type**         | **Usage** |
|:----------------------|:-----------------|:----------|
| flags                 | word flags       | Control the blooms activation and render stage. |
| bloom size            | real             | Defines how far the bloom should extend, 1.0 to 5.0 is the recommended range. Any higher and the effect becomes fragmented. Default value is 3.5. |
| bloom exposure        | real             | Multiplies the bloom to increase or decrease its brightness. Default value is 1.0. |
| bloom mix amount      | real             | An amount between 0.0 and 1.0 that blends between no bloom and full bloom respectively. Default value is 0.5. |
| bloom minimum color   | real rgb color   | When filtering out the colour range for the bloom, colours below this are lost. Default value is { 0.55, 0.55, 0.55 }. |
| bloom maximum color   | real rgb color   | When filtering out the colour range for the bloom, colours above this are kept. Default value is { 1.0, 1.0, 1.0 }. |

Flags:
| **Flag**          | **Usage** |
|:------------------|:----------|
| enabled           | Turns the bloom subsystem on or off. Default value is true. |
| apply after hud   | Defines whether the bloom effect should be applied after or before the HUD is drawn. Default value is true. |

### Halo Script ###
A number of new Halo script functions have been added by OpenSauce that allow you to interact with post processing effects through your maps scripting.

The full script function listing can be found in the [script functions page](Doc_Halo1_NewScriptInterfaces.md)

## The External System ##
Using the external system only requires you to have a HLSL shader file (.fx) and an effect set up in your OS\_Settings.PostProcessing.xml file.

**Directory tree**

The location of your external subsystems settings file is under your (My) Documents folder, in your Halo CE game directory:

> (My) Documents\My Games\Halo CE

In that directory you should have the following directory/file tree:
  * OpenSauce
    * OS\_Settings.PostProcessing.xml - mirrors most of the functionality of the tags in the internal post processing system.

**OS\_Settings.PostProcessing.xml**

This xml file mirrors most of the functionality of the tags in the internal post processing system, to define how your shaders are applied and where.

This is a basic example of setting up a single shader and a single effect:

```
<?xml version="1.0" encoding="utf-8"?>
<osYeloSettings engine="Halo1_CE">
	<postprocessing 
		shader_directory=
			"D:\Program Files (x86)\Microsoft Games\Halo Custom Edition\data\postprocess\shaders" 
		texture_directory=
			"D:\Program Files (x86)\Microsoft Games\Halo Custom Edition\data\postprocess\bitmaps">
		<shaders>
			<shader id="tint_shader">my_tint</shader>
		</shaders>
		<effects>
			<effect id="tint_effect">
				<shaders>
					<shader>tint_shader</shader>
				</shaders>
			</effect>
		</effects>
		<effect_instances>
			<effect_instance name="tint_effect_instance" effect="tint_effect" render_stage="3" activation_operation="0">
				<quad>
					<tessellation x="4" y="4"/>
					<x_bounds lower="0.0" upper="1.0"/>
					<y_bounds lower="0.0" upper="1.0"/>
				</quad>
				<activation_controls>
					<activation_control state="1" invert="true"/>
				</activation_controls>
			</effect_instance>
		</effect_instances>
	</postprocessing>
</osYeloSettings>
```

**Initial setup**

Before you can start adding post process effects you have to set the directory attributes in the postprocessing element.

  * shader\_directory - Should contain a path to where your shaders are located. This directory is also used to resolve the relative file includes in your shaders.
  * texture\_directory - Should contain a path to where your shaders textures are located.

```
	<postprocessing 
		shader_directory=
			"D:\Program Files (x86)\Microsoft Games\Halo Custom Edition\data\postprocess\shaders" 
		texture_directory=
			"D:\Program Files (x86)\Microsoft Games\Halo Custom Edition\data\postprocess\bitmaps">
```

**Adding a shader**

The shaders you want to use are added under `<postprocessing>` in the `<shaders>` node, by adding a child `<shader>` node and settings its inner text to the location and file name of your shader, minus the .fx extension. The shaders location will be relative to the  shader\_directory attribute of the postprocessing element. You must then set the id attribute of your shader element as this is what is used in the effect definition to reference the shader.

```
		<shaders>
			<shader id="tint_shader">my_tint</shader>
		</shaders>
```

**Adding an effect**

To add an effect you add a child `<effect>` node to `<effects>` and set its id attribute.

```
		<effects>
			<effect id="tint_effect">
			</effect>
		</effects>
```

Effect attributes:
| **Attribute**        | **Type** | **Usage** |
|:---------------------|:---------|:----------|
| id                   | string   | This must be set as this is used to reference an effect in the effect instance node. |

**Effect shader instances**

Shader instances are added through a child node `<shaders>`, under the `<effect>` node. To add a shader instance you add a `<shader>` node to `<shaders>` and use the shaders id attribute name as it's inner text. This does not load the file directly, but references a shader from the `<shaders>` node under `<postprocessing>`, so a shader must be added to that node before it can be used in an effect.

```
		<effects>
			<effect id="tint_effect">
				<shaders>
					<shader>tint_shader</shader>
				</shaders>
			</effect>
		</effects>
```

**Effect instances**

To add an effect instance you add a child `<effect instance>` node to `<effect_instances>` and set its various attributes and child nodes.

```
		<effect_instances>
			<effect_instance name="tint_effect_instance" effect="tint_effect" render_stage="3" activation_operation="0">
				<quad>
					<tessellation x="4" y="4"/>
					<x_bounds lower="0.0" upper="1.0"/>
					<y_bounds lower="0.0" upper="1.0"/>
				</quad>
				<activation_controls>
					<activation_control state="1" invert="true"/>
				</activation_controls>
			</effect_instance>
		</effect_instances>
```

Effect instance attributes:
| **Attribute**          | **Type** | **Usage** |
|:-----------------------|:---------|:----------|
| name                   | string   | Purely for your own reference, is not used in-game. |
| effect                 | string   | The ID of the effect to instance. |
| render\_stage          | int      | Controls when the effect instance should be applied, defaults to pre-HUD. |
| activation\_operation  | string   | Purely for your own reference, is not used in-game. |

Render stages:
| **Value** | **Render Stage**               |
|:----------|:-------------------------------|
| 0         |  Pre motion blur.              |
| 1         |  Pre alpha blended geometry.   |
| 2         |  Pre HUD.                      |
| 3         |  Pre Menus.                    |
| 4         |  Post menus.                   |

Activation operation values:
| **Value** | **Result** |
|:----------|:-----------|
| 0         | The activation controls are AND'd so all must be true for the effect instance to be displayed. |
| 1         | The activation controls are OR'd so if any of the values are true, the effect instance is displayed. |

Quad child node:

The quad node defines the tessellation and bounds of the quad used to render the effect instance.

```
				<quad>
					<tessellation x="4" y="4"/>
					<x_bounds lower="0.0" upper="1.0"/>
					<y_bounds lower="0.0" upper="1.0"/>
				</quad>
```

| **Child Node** | **Usage** |
|:---------------|:----------|
| tesselation    | Controls the number of segments the quad for this effect instance has. |
| x\_bounds      | Controls the x axis dimension of the quad. |
| y\_bounds      | Controls the y axis dimension of the quad. |

Tesselation node attributes:
| **Attribute** | **Type** | **Usage** |
|:--------------|:---------|:----------|
| x             | int      | Defines how many segments the quad should have on the x axis. |
| y             | int      | Defines how many segments the quad should have on the y axis. |

x/y\_bounds node attributes:
| **Attribute** | **Type** | **Usage** |
|:--------------|:---------|:----------|
| lower         | int      | Defines the start point of the quad the respective axis. |
| upper         | int      | Defines the end point of the quad the respective axis. |

**Activation Controls**

An effect instances activation controls are added to the `<activation_controls>` node under `<effect instance>`. To set the control you want to use, set the state attribute to the corresponding integer. If you wish the controls value to be inverted set the invert attribute to true.

```
				<activation_controls>
					<activation_control state="1" invert="true"/>
				</activation_controls>
```

Activation control attributes:
| **Attribute** | **Type** | **Usage** |
|:--------------|:---------|:----------|
| state         | int      | Indicates the engine value to use when determining if the effect instance should be active. |
| invert        | bool     | Inverts the result of the control. |

Available activation states:
| **Value** | **State**                 | **Condition** |
|:----------|:--------------------------|:--------------|
| 0         | initially active          | Controls whether the effect is displayed by default. |
| 1         | is in cutscene            | This value is true whenever the cinematic black bars are displayed. |
| 2         | player is zoomed          | This value is true whenever the player zooms with their current weapon. |
| 3         | player using a vehicle    | This value is true whenever the player is in any vehicle seat. |

### Prototyping ###
The external system can be used by map makers to rapidly prototype their shaders, without having to recompile their map. Doing so requires the game to be running in devmode, with the console available.

First you set up your shader using the external system, as normal. You then run the game and play your map, which should not yet have the shader added to it as a tag. If you decide to change your shader, you simply alt-tab out of the game, edit your shader, and return to the game. Then to reload the shader you open the console and run "pp\_unload" and "pp\_load" in that order. This will force the game to completely unload and completely reload all of the post processes being used, thus reloading the shader being worked on so you can view the affect of your changes.