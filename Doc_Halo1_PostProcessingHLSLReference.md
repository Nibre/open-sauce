# Post Processing HLSL Reference #
## Rules ##
The post processing system uses shaders written in HLSL and has some rules that must be followed to create a compatible shader.

  1. The shader MUST have at least 1 technique.
  1. All techniques MUST have BOTH a pixel and vertex shader.
  1. The shader MUST have a matrix variable using the ORTHOWORLDVIEWPROJECTION semantic.
  1. The shader MUST NOT mix shader model 3.0 with a model lower than itself.
  1. Technique, Pass and variables names must not exceed 31 characters in length.

If you have the OpenSauce HEK extensions installed then two vertex shaders have already been provided to you in "data\postprocess\shaders\include\vertex\_shaders.fx". To use the vertex shaders in your shaders simply include the above file in your shader by putting "#include "includes\vertex\_shaders.fx"" at the top of the file and use the PassthroughVS function as your vertex shader. There is also a scaling vertex shader which you can use by putting "#define VERTEX\_SCALE\_SHADER" prior to the file include, and using ScaleVS as your vertex shader. Including the vertex\_shaders.fx file also covers you for the ORTHOWORLDVIEWPROJECTION usage rule.

Note that the supplied scaling shader scales from the top left corner of the screen, not the top left of the quad, so if you wish to do scaling with a quad that is not attached to the top left of the screen you will need to write your own shader to do so.

## Vertex Format ##
The vertex format for the post processing quad is as follows:

  * **POSITION**   _float3_
    * `x` - vertex's x position in world space (0.0 to screen x resolution)
    * `y` - vertex's y position in world space (0.0 to negative screen y resolution)
    * `z` - vertex's z position in world space
  * **TEXCOORD0**  _float2_
    * `x` - quad space U coordinate (0.0 to 1.0 from left of quad to right)
    * `y` - quad space V coordinate (0.0 to 1.0 from top of quad to bottom)
  * **TEXCOORD1**  _float2_
    * `x` - screen space U coordinate (0.0 to 1.0 from left of screen to right)
    * `y` - screen space V coordinate (0.0 to 1.0 from top of screen to bottom)
  * **TEXCOORD2**  _float4_
    * `x` - quads x axis lower bound (0.0 to 1.0 from left of quad to right)
    * `y` - quads x axis upper bound (0.0 to 1.0 from left of quad to right)
    * `z` - quads y axis lower bound (0.0 to 1.0 from top of screen to bottom)
    * `w` - quads y axis upper bound (0.0 to 1.0 from top of screen to bottom)

## Semantics ##
The post processing system uses variable semantics to provide certain information to shaders without forcing the shader designer into using specific variable names.

The following format is used for semantics:
```
	VariableType VariableName : SEMANTIC;
```
For instance to get the far clip distance you could use the following:
```
	float f_FarClipDistance : FARCLIPDISTANCE;
```

Basic semantics:
| **Semantic**               | **Type**  | **Usage** |
|:---------------------------|:----------|:----------|
| ORTHOWORLDVIEWPROJECTION   | matrix    | The projection matrix used to transform the post processing quads vertices into screen space. |
| SCENESIZE                  | float2    | The current dimensions of the screen. |
| HUDSCALE                   | float2    | The current X and Y scale factors of the HUD. |
| PIXELSIZE                  | float2    | The current size of a single pixel in screen space. |
| NEARCLIPDISTANCE           | float     | The near clipping distance of the render frustum. |
| FARCLIPDISTANCE            | float     | The far clipping distance of the render frustum. |
| TEXSCENE                   | texture   | A texture containing the scene prior to the current effect being applied. |
| TEXSOURCE                  | texture   | A texture containing the scene prior to the current pass. If this is the first pass, this contains the same texture as TEXSCENE. |
| TEXBUFFER                  | texture   | A texture containing the secondary buffer prior to the current pass. If this is the first pass, this textures contents are undetermined. |

External system semantics:

As the external system has no tag data to define variables that are controlled by the engine, semantics are used instead.

| **Semantic**       | **Type**  | **Usage** |
|:-------------------|:----------|:----------|
| VARTEXTURE\_1-4    | texture   | Declares a texture variable that will have an externally loaded bitmap assigned to it. |
| VARBOOL\_1-16      | bool      | Declares that this boolean will be set dynamically by the engine. |
| VARINT\_1-16       | int       | Declares that this integer will be set dynamically by the engine. |
| VARCOLOR\_1-16     | float4    | Declares that this float4 will be set dynamically by the engine. |
| VARFLOAT\_1-16     | float     | Declares that this float will be set dynamically by the engine. |
| VARFLOAT2\_1-16    | float2    | Declares that this float2 will be set dynamically by the engine. |
| VARFLOAT3\_1-16    | float3    | Declares that this float3 will be set dynamically by the engine. |
| VARFLOAT4\_1-16    | float4    | Declares that this float4 will be set dynamically by the engine. |

## Annotations ##

When compiling your shaders using os\_tool, or using shaders with the external system at runtime, annotations are used to set some of the shaders attributes.

### Technique/Pass annotations ###
The following format is used for technique and/or pass annotations:
```
	technique TechniqueName
	<
		AnnotationType AnnotationName = AnnotationValue;
	>
	{
		pass PassName
		<
			AnnotationType AnnotationName = AnnotationValue;
		>
		{

		}
	}
```

For example:
```
	technique Sepia
	<
		int shader_model_mask = shader_model_mask_1_0 | shader_model_mask_2_0 | shader_model_mask_3_0;	
	>
	{
		pass Sepia
		<
			bool clear_target = true;
			int render_chain = 1;
		>
		{
			VertexShader = compile vs_1_1 PassThroughVS();
			PixelShader = compile ps_2_0 SepiaPS();
		}
	}
```

Available annotations:
| **Semantic**           | **Target**  | **Type**  | **Usage** |
|:-----------------------|:------------|:----------|:----------|
| shader\_model\_mask    | technique   | int       | Controls which shader models this shader will be used with. |
| clear\_target          | pass        | bool      | Defines whether to clear the target surface before a pass is rendered. |
| copy\_scene\_to\_target | pass        | bool      | Defines whether to duplicate the scene texture onto the target surface. |
| render\_chain          | pass        | int       | Defines which render chain to draw to. |
| clear\_buffer\_texture | pass        | bool      | Defines whether the buffer source should be cleared before the pass is rendered. |

Available render chain values
| **Value** | **Usage** |
|:----------|:----------|
| 0         | Main render chain. |
| 1         | Secondary buffer chain. |

### Variable Annotations ###

The following format is used for variable annotations:
```
	VariableType VariableName : VARIABLESEMANTIC
		<
			AnnotationType AnnotationName = AnnotationValue;
		> = VariableValue;
```

For example to set up a float that uses noise you could use the following:
```
	float f_Noise : VARFLOAT_1
		<
			float value_lower_bound = 0.0f;
			float value_upper_bound = 1.0f;
			int animation_function = 8;
			float animation_duration = 1.0f;
		> = 0.0f;
```

Note that variable annotations are not used when compiling your postprocess tags, so your variables will have to be set up manually in guerilla.

External system annotations:
To mirror the variable tag blocks of the postprocess tag, the following annotations are used.
| **Annotation**           | **Target** | **Type** | **Usage** |
|:-------------------------|:-----------|:---------|:----------|
| texture\_location        | variable   | string   | Textures only. Contains a path to the bitmap to be loaded into this variable. The path is relative to the user defined texture directory. |
| value\_lower\_bound      | variable   | varies   | Sets the lowest value for this variable when animated. Type is the same as the variable it is attached to. Ignored by booleans. |
| value\_upper\_bound      | variable   | varies   | Sets the highest value for this variable when animated. Type is the same as the variable it is attached to. |
| runtime\_value\_override | variable   | int      | Indicates the value this variable should be set to from the engine. |
| runtime\_value\_invert   | variable   | bool     | Used with bools only, defines whether to invert the runtime value. |
| animation\_function      | variable   | int      | Integer value to define which animation function to use. |
| animation\_flags         | variable   | int      | Controls the output of the animation function. These are flags so should be bitwise OR'd together.	 |
| animation\_duration      | variable   | float    | The time it takes for a single cycle of the animation function. |
| animation\_rate          | variable   | float    | How often the variable is updated. |

Bool overrides:
| **Index** | **Value** |
|:----------|:----------|
| 0         | None      |

Integer overrides:
| **Index** | **Value** |
|:----------|:----------|
| 0         | None      |

Float overrides:
| **Index** | **Value** |
|:----------|:----------|
| 0         | None      |

Float2 overrides:
| **Index** | **Value** |
|:----------|:----------|
| 0         | None      |

Float3 overrides:
| **Index** | **Value** |
|:----------|:----------|
| 0         | None      |

Float4 overrides:
| **Index** | **Value** |
|:----------|:----------|
| 0         | None      |

Color overrides:
| **Index** | **Value** |
|:----------|:----------|
| 0         | None      |
| 1         | Player team color    |

Animation function values:
| **Index** | **Function** |
|:----------|:-------------|
| 0         | One          |
| 1         | Zero         |
| 2         | Cosine       |
| 3         | Cosine Variable |
| 4         | Diagonal Wave |
| 5         | Diagonal Wave Variable |
| 6         | Slide        |
| 7         | Slide Variable |
| 8         | Noise        |
| 9         | Jitter       |
| 10        | Wander       |
| 11        | Spark        |

Animation Flags:

These are flags so should be bitwise OR'd together.
| **Value** | **Flag**             | **Usage** |
|:----------|:---------------------|:----------|
| 1         | Inverted             | Essentially swaps upper and lower bounds. |
| 2         | Multichannel Noise   | When the noise function is used, uses a different noise value for each channel. |
| 4         | Ignore Alpha         | Prevents the animation from changing the alpha channel. The upper bounds alpha channel is always used. |

### #defines ###
There is currently one reserved define "SHADER\_EXTERNAL" which is used by the external post processing system when compiling your shaders at runtime. This allows you to seperate areas of code that only apply when used in the external subsystem. This is very useful for developing an effect that will be compiled in to tag format as you do not need to have one copy of the shaders used for development and another copy that will be compiled into tags.

To use the define for this purpose you would do something like this:
```
	VariableType VariableName
	#ifdef SHADER_EXTERNAL
		 : VARIABLESEMANTIC
		<
			AnnotationType AnnotationName = AnnotationValue;
		>
	#endif
	 = VariableValue;
```

By doing this, you can set up the shader with annotations and semantics that apply only to the external subsystem, without them bleeding over into the binary format stored in your tags.

### Gbuffer usage ###
Another extension to Halo's render engine is the creation of a GBuffer (Geometry-Buffer) which, as a separate render pass, draws information about the scene into a number of full screen textures for use in other extensions, such as post processing.

GBuffer defines:
| **Buffer** | **Define**             |
|:-----------|:-----------------------|
| Depth      | GBUFFER\_USE\_DEPTH    |
| Velocity   | GBUFFER\_USE\_VELOCITY |
| Normals    | GBUFFER\_USE\_NORMALS  |
| Index      | GBUFFER\_USE\_INDEX    |

GBuffer HLSL functions:
| **Function**                     | **Buffer** | **Usage** |
|:---------------------------------|:-----------|:----------|
| float GetDepth(float2 Tex)       | Depth      | Returns the linear depth of a pixel between 0.0 and 1.0 |
| float2 GetVelocity(float2 Tex)   | Velocity   | Returns the velocity at a specific pixel |
| float3 GetNormals(float2 Tex)    | Normals    | Returns the normals of a specific pixel |
| int2 GetIndex(float2 Tex)        | Index      | Returns the contents of the Index buffer in an int2 |
| int GetType(in int2 Index)       | Index      | Extracts mesh type information from a sample of the Index buffer |
| int GetTeam(in int2 Index)       | Index      | Extracts team type information from a sample of the Index buffer |
| int GetIsEnemy(in int2 Index)    | Index      | Extracts whether a unit is dead from a sample of the Index buffer |
| int GetIsDead(in float2 Index)   | Index      | Extracts whether a unit is an enemy from a sample of the Index buffer |

To use the Gbuffer textures you must use the defines at the very top of your shader file, for example:
```
	#define GBUFFER_USE_DEPTH
	#define GBUFFER_USE_NORMALS
	#include "includes\gbuffer_functions.fx"
```
To extract the required information from the index buffer use GetIndex to get the raw packed data, then pass the returned value to one of the other functions to get the specific data you require.