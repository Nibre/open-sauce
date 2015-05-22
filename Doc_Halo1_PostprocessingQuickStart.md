# OpenSauce Post Processing Quick Start #

This guide aims to instruct you on the basic work flow for putting a simple post process effect into your map. It by no means explains everything that the system can handle but should get you started for more fun things down the line.

Basic work flow overview:
  1. Create a post process shader in HLSL
  1. Compile the shader using os\_tool
  1. Set the shader variables in OS Guerilla
  1. Create a generic postprocess effect tag
  1. Create an postprocess effect collection tag
  1. Add the effect collection to a scenario
  1. Compile your map

## 1. Create a post process shader in HLSL ##
For the sake of this being a quick start guide we will be using a simple tint shader. Copy the following HLSL to a text file and save it as my\_tint.fx in your "data\postprocess\shaders" folder.

```
#include "includes\vertex_shaders.fx"

texture tex_Source	: TEXSOURCE;

sampler2D sam_Source =
sampler_state
{
	Texture = <tex_Source>;
	AddressU = Clamp;
	AddressV = Clamp;
	MinFilter = Point;
	MagFilter = Linear;
	MipFilter = Linear;
};

float 	blend_amount = 0.8f;
float4	tint_color = { 0.7f, 0.7f, 1.0f, 1.0f };

float4 TintPS( float2 Tex0 : TEXCOORD0 ) : COLOR0{
	float4 OUT = tex2D(sam_Source, Tex0);
	OUT.rgb = lerp(OUT.rgb, OUT.rgb * tint_color.rgb, blend_amount);
	return OUT;
}	   

technique PostProcess
{
	pass Tint
	{
		VertexShader = compile vs_1_1 PassThroughVS();
		PixelShader = compile ps_2_0 TintPS();
	}
}
```

With your my\_tint.fx file in the correct location we can compile the shader.

## 2. Compile the shader using os\_tool ##
Open a command prompt in your HEK installation directory (typically your Halo CE directory) and run OS\_Tool. You will see that OpenSauce extends Tool by including some new commands. The command we will be using is "compile-shader-postprocess". This command compiles all of the .fx files in a directory and either creates new _shader\_postprocess\_generic_ tags for them, or updates existing tags with the new shader code. In our case the post processes reside in the "data\postprocess\shaders" directory so the command will be "os\_tool compile-shader-postprocess postprocess\shaders". When the command is complete you will have a _my\_tint.shader\_postprocess\_generic_ tag in your "tags\postprocess\shaders" directory.

![http://wiki.open-sauce.googlecode.com/hg/images/Doc_Halo1_PostprocessingQuickStart/ShaderCompile.jpg](http://wiki.open-sauce.googlecode.com/hg/images/Doc_Halo1_PostprocessingQuickStart/ShaderCompile.jpg)

## 3. Set the shader variables in OS Guerilla ##
The _shader\_postprocess\_generic_ tag provides an interface between the shader variables and the designer, allowing you to change the variable values from OS Guerilla. Run OS Guerilla now and open the _my\_tint.shader\_postprocess\_generic_ tag. We are going to add variable entries for the 2 variables in our tint shader:

```
float 	blend_amount = 0.8f;
float4	tint_color = { 0.7f, 0.7f, 1.0f, 1.0f };
```

Add an entry to the FLOATS block and set the name to "blend\_amount". Then set the upper bound field to 0.8. At runtime, the game will look for a variable in the shader named "blend\_amount" and will set it's value to 0.8. For the "tint\_color" variable you could add a FLOAT4S block, however since this is a colour it is more easily set by a colour picker, so add a COLORS block entry instead. As with with the float variable, set the name and upper bound value.

![http://wiki.open-sauce.googlecode.com/hg/images/Doc_Halo1_PostprocessingQuickStart/ShaderVariables.jpg](http://wiki.open-sauce.googlecode.com/hg/images/Doc_Halo1_PostprocessingQuickStart/ShaderVariables.jpg)

When copying colour values from HLSL it is important to note that in HLSL a float4 stores a colour as RGBA, while in guerilla it is displayed as ARGB.

The reason we change the upper bound value is because, as is with other areas of the HEK, shader variables can be animated and by default the animation function is set to "one" meaning the upper bound is used.

Once you have added your variables save the shader tag.

## 4. Create a generic postprocess effect tag ##
While still in OS Guerilla create a new _effect\_postprocess\_generic_ tag. This tag brings your shaders together into a single reusable effect, by applying your shaders in whatever order you wish them to be applied. First you must add a reference your shader by adding an entry to the SHADERS block and setting the shader tag reference to your my\_tint shader.

Next you need to add an entry to the SHADER INDICES block, changing the shader field to references your shader from the SHADERS block. The SHADER INDICES block is what defines the order your shaders are applied in. In this quick start guide you will only have one entry, but for more complicated effects you could have many more, even with some duplicate entries.

![http://wiki.open-sauce.googlecode.com/hg/images/Doc_Halo1_PostprocessingQuickStart/Effect.jpg](http://wiki.open-sauce.googlecode.com/hg/images/Doc_Halo1_PostprocessingQuickStart/Effect.jpg)

With your shader added to both the SHADERS and SHADER INDICES blocks save your effect to "tags\postprocess\effects\my\_tint.effect\_postprocess\_generic".

## 5. Create an postprocess effect collection tag ##
Next create a new _effect\_postprocess\_collection_ tag. This tag brings your effects together and defines where and when to apply them.

First you need to add your effect to the EFFECTS block so add a new block entry and set the effect tag reference.

Next you need to add an entry to the EFFECT INSTANCES block. An effect instance defines where and when an effect is applied. You can have multiple effect instances referencing the same effect to apply it to different areas on the screen and/or at different times in the render cycle. For this guide you will only need one effect instance.

To set up your effect instance change the effect field to reference your my\_tint entry in the EFFECTS block.
The render stage field sets where in Halos render cycle to draw the effect instance; for your tint effect we want to affect the HUD so set the field to "after hud - before menu".
The quad tesselation field controls how may quads make up the quad used for the instance, which is important for vertex animation but for your effect 4x4 will be fine.
The X and Y screen bounds controls the area of the screen that your effect is applied to. For your effect set both to 0 to 1.
The activation operation field and ACTIVATION CONTROLS block controls when the effect is turned on and off according to specific events in the engine. If no activation control is added the effect instance will be applied by default so you can leave those options unchanged.

![http://wiki.open-sauce.googlecode.com/hg/images/Doc_Halo1_PostprocessingQuickStart/EffectCollection.jpg](http://wiki.open-sauce.googlecode.com/hg/images/Doc_Halo1_PostprocessingQuickStart/EffectCollection.jpg)

Finally, save your effect collection tag to your maps scenario directory, such as "tags\levels\test\tutorial\tutorial.effect\_postprocess\_collection"

## 6. Add the effect collection to a scenario ##
To add OS specific tags to a map you have to first create two tags, a _project\_yellow_ tag and a _tag\_collection_ tag. First create a _tag\_collection_ tag and add a reference to your _effect\_postprocess\_collection_ and save it in your scenarios directory to keep per map tags with their respective scenario.

![http://wiki.open-sauce.googlecode.com/hg/images/Doc_Halo1_PostprocessingQuickStart/TagCollection.jpg](http://wiki.open-sauce.googlecode.com/hg/images/Doc_Halo1_PostprocessingQuickStart/TagCollection.jpg)

Next create a _project\_yellow_ tag and set the "scenario explicit references" field to the _tag\_collection_ you just saved, then save the _project\_yellow_ tag along side your scenario.

![http://wiki.open-sauce.googlecode.com/hg/images/Doc_Halo1_PostprocessingQuickStart/ProjectYellow.jpg](http://wiki.open-sauce.googlecode.com/hg/images/Doc_Halo1_PostprocessingQuickStart/ProjectYellow.jpg)

Then open your maps scenario tag. At the very top you will see that the DON'T USE field is now a "project yellow definitions" field. That field takes a reference to the _project\_yellow_ tag you just created. Save everything and you are ready to compile your map.

![http://wiki.open-sauce.googlecode.com/hg/images/Doc_Halo1_PostprocessingQuickStart/Scenario.jpg](http://wiki.open-sauce.googlecode.com/hg/images/Doc_Halo1_PostprocessingQuickStart/Scenario.jpg)

## 7. Compile your map ##
Whenever you create a map that uses OpenSauce you should compile it using the "build-cache-file-ex" command of os\_tool. This is because there are some behind the scenes processes that only occur when running build-cache-file-ex that are required for smooth running of the OpenSauce extensions. This does not however mean you have to use all of the features of build-cache-file-ex. Use 'build-cache-file-ex "" false false false <your maps scenario>' and the map will not use the additional OpenSauce memory features.

![http://wiki.open-sauce.googlecode.com/hg/images/Doc_Halo1_PostprocessingQuickStart/BuildMap.jpg](http://wiki.open-sauce.googlecode.com/hg/images/Doc_Halo1_PostprocessingQuickStart/BuildMap.jpg)

## Finally ##
Run your map in Halo CE with OpenSauce installed and your effect will be applied as soon as the map is loaded. An important point to make is that because the only things added to the map are some extra tags, it will still function just fine on a Halo setup that doesn't have OpenSauce installed!

![http://wiki.open-sauce.googlecode.com/hg/images/Doc_Halo1_PostprocessingQuickStart/Result.jpg](http://wiki.open-sauce.googlecode.com/hg/images/Doc_Halo1_PostprocessingQuickStart/Result.jpg)