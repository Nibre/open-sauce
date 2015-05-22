

# Introduction #

For Halo1\_CE, we've added some new external script functions and globals. By external, I mean stuff which isn't defined in your scenario. Eg, object\_create is an external script function.

# Compatibility with non-OS games #

The stock game is not compatible with any maps which use any functions or globals that are not already defined within its code. Thus, custom script definitions require the use enabling of use-memory-upgrades in the build-cache-file-ex tool command. Maps which use incompatible features (ie, features which will crash the game when not running OS) use the .yelo extension instead of the normal .map.

We've setup OS to expose its version in the **real** global **ai\_debug\_path\_maximum\_radius**. For all OS-specific functions/globals which you access, you need to wrap them in a block of code which first checks that this global isn't zero, or below the supported OS version of what you're dealing with

```lisp

; 3.0 is the value of K_OPENSAUCE_VERSION at the time of this writing
(global boolean is_os_enabled (>= ai_debug_path_maximum_radius 3.0) ) ```

Alternatively, you should be also able to do the following
```lisp

(script static boolean is_os_enabled
(>= ai_debug_path_maximum_radius 3.0)
)```
However, it is recommended you **use the script global method** (this can enable OS, in the future, to kill problem scripts due to unrecoverable changes, etc)

Example of using these checks
```lisp

(script startup main
(if is_os_enabled ; if using the global method
; Then
(print "Running OpenSauce!")

; Else
(print "Not running OpenSauce :|")
)


(if (is_os_enabled) ; if using the script method
; Then
(print "Running OpenSauce!")

; Else
(print "Not running OpenSauce :|")
)
)```


# External Script Functions/Globals #

## PLAYER SCRIPTING INTERFACE ##

#### volume\_test\_player\_team ####
returns true if any players of the specified team are within the specified volume
| **Field**        | **Name**         | **Help** |
|:-----------------|:-----------------|:---------|
| return-type      | boolean          |          |
| trigger\_volume  | trigger\_volume  |          |
| short            | team\_index      |          |

#### volume\_test\_player\_team ####
returns true if all players of the specified team are within the specified volume
| **Field**        | **Name**         | **Help** |
|:-----------------|:-----------------|:---------|
| return-type      | boolean          |          |
| trigger\_volume  | trigger\_volume  |          |
| short            | team\_index      |          |

#### player\_team\_teleport ####
moves the specified team to the specified flag
| **Field**        | **Name**         | **Help** |
|:-----------------|:-----------------|:---------|
| return-type      | void             |          |
| short            | team\_index      |          |
| cutscene\_flag   | cutscene\_flag   |          |

#### player\_team\_players ####
returns a list of players on the specified team
| **Field**        | **Name**         | **Help** |
|:-----------------|:-----------------|:---------|
| return-type      | object\_list     |          |
| short            | team\_index      |          |

#### player\_data\_get\_integer ####
returns an integer value property of the player
| **Field**        | **Name**            | **Help** |
|:-----------------|:--------------------|:---------|
| return-type      | long                | **-1** if this fails       |
| short            | player\_list\_index |          |
| string           | data\_name          | kills\_this\_lifetime, friendly\_fire\_kills, deaths, suicides, team\_index, current\_spree\_count, last\_kill\_time, ping |

#### player\_team\_data\_get\_integer ####
returns an integer value of all the players on the team
| **Field**        | **Name**     | **Help** |
|:-----------------|:-------------|:---------|
| return-type      | long         | **-1** if this fails       |
| short            | team\_index  |          |
| string           | data\_name   | kills\_this\_lifetime, friendly\_fire\_kills, deaths, suicides |

#### player\_data\_get\_object ####
returns an object value property of the player
| **Field**        | **Name**            | **Help** |
|:-----------------|:--------------------|:---------|
| return-type      | object              | **none** if this fails       |
| short            | player\_list\_index |          |
| string           | data\_name          | nearest\_object, slave\_unit, last\_slave\_unit, target\_player, weapon0 to 3 |

#### player\_data\_get\_real ####
returns a real value property of the player
| **Field**        | **Name**            | **Help** |
|:-----------------|:--------------------|:---------|
| return-type      | real                | **-1.0** if this fails       |
| short            | player\_list\_index |          |
| string           | data\_name          | speed    |

#### player\_data\_set\_real ####
sets a real value property of the player
| **Field**        | **Name**            | **Help** |
|:-----------------|:--------------------|:---------|
| return-type      | void                |          |
| short            | player\_list\_index |          |
| string           | data\_name          | speed    |
| real             | data\_value         |          |

#### player\_local\_get ####
returns the local player's player-list-index
| **Field**        | **Name** | **Help** |
|:-----------------|:---------|:---------|
| return-type      | short    | **-1** if this fails (eg, no active local players) |


## OBJECT SCRIPTING INTERFACES ##

#### objects\_distance\_to\_object ####
returns minimum distance from any of the specified objects to the specified destination object
| **Field**     | **Name**     | **Help** |
|:--------------|:-------------|:---------|
| return-type   | real         | **-1** if there are no objects to check       |
| object\_list  | objects      |          |
| object        | dst\_object  | destination object       |

#### object\_data\_get\_real ####
returns a real value property of the object
| **Field**     | **Name**       | **Help** |
|:--------------|:---------------|:---------|
| return-type   | real           | **-1.0** if this fails       |
| object        | object         |          |
| string        | data\_name     | position, transitional\_velocity, forward, up, angular\_velocity, center, function\_out, radius, scale, maximum\_vitality, current\_vitality |
| string        | subdata\_name  | x/i, y/j, z/k or a, b, c, d |

#### object\_data\_set\_real ####
sets a real value property of the object **DOES NOTHING IN NETWORKED GAMES IF REQUESTED PROPERTY DOESN'T SYNC**
| **Field**     | **Name**       | **Help** |
|:--------------|:---------------|:---------|
| return-type   | void           |          |
| object        | object         |          |
| string        | data\_name     | position, transitional\_velocity, forward, up, angular\_velocity, maximum\_vitality, current\_vitality |
| string        | subdata\_name  | x/i, y/j, z/k |
| real          | data\_value    |          |

#### weapon\_data\_get\_real ####
returns a real value property of the weapon
| **Field**     | **Name**       | **Help** |
|:--------------|:---------------|:---------|
| return-type   | real           | **-1.0** if this fails       |
| object        | weapon         |          |
| string        | data\_name     | heat, age, light\_power |

#### weapon\_data\_set\_real ####
sets a real value property of the weapon. **DOES NOTHING IN NETWORKED GAMES IF REQUESTED PROPERTY DOESN'T SYNC**
| **Field**     | **Name**       | **Help** |
|:--------------|:---------------|:---------|
| return-type   | void           |          |
| object        | weapon         |          |
| string        | data\_name     | heat, age, light\_power |
| real          | data\_value    |          |

#### weapon\_data\_trigger\_set\_real ####
sets a real value property of a weapon's trigger. **DOES NOTHING IN NETWORKED GAMES**
| **Field**     | **Name**       | **Help** |
|:--------------|:---------------|:---------|
| return-type   | void           |          |
| object        | weapon         |          |
| long          | trigger\_index |          |
| string        | data\_name     | spew\_time, rounds\_per\_second |
| string        | subdata\_name  | lower, upper |
| real          | data\_value    |          |

#### unit\_data\_get\_object ####
returns an object value property of the unit
| **Field**     | **Name**       | **Help** |
|:--------------|:---------------|:---------|
| return-type   | object         | **none** if this fails       |
| object        | unit           |          |
| string        | data\_name     | weapon0 to 3, recent\_damage.unit0 to 3, thrown\_grenade\_projectile, equipment, damage\_responsible\_flamer\_object |

#### unit\_data\_get\_integer ####
returns an integer value property of the unit
| **Field**     | **Name**       | **Help** |
|:--------------|:---------------|:---------|
| return-type   | long           | **-1** if this fails       |
| unit          | unit           |          |
| string        | data\_name     | total\_grenade\_count[`plasma`], total\_grenade\_count[`frag`], total\_grenade\_count[`custom2`], total\_grenade\_count[`custom3`], current\_grenade\_index, vehicle\_seat\_index, current\_weapon\_index, feign\_death\_timer, zoom\_level, ticks\_until\_flame\_to\_death, killing\_spree\_count |

#### unit\_data\_set\_integer ####
sets an integer value property of the unit. **DOES NOTHING IN NETWORKED GAMES IF REQUESTED PROPERTY DOESN'T SYNC**
| **Field**     | **Name**       | **Help** |
|:--------------|:---------------|:---------|
| return-type   | void           |          |
| unit          | unit           |          |
| string        | data\_name     | total\_grenade\_count[`plasma`], total\_grenade\_count[`frag`], total\_grenade\_count[`custom2`], total\_grenade\_count[`custom3`], desired\_zoom\_level |
| long          | data\_value    |          |

#### unit\_data\_get\_real ####
returns a real value property of the unit
| **Field**     | **Name**       | **Help** |
|:--------------|:---------------|:---------|
| return-type   | real           | **-1.0** if this fails       |
| unit          | unit           |          |
| string        | data\_name     | camo\_power, driver\_power, gunner\_power, integrated\_light\_power, integrated\_light\_toggle\_power, integrated\_night\_vision\_toggle\_power, full\_spectrum\_vision\_power |

#### unit\_data\_set\_real ####
sets a real value property of the unit. **DOES NOTHING IN NETWORKED GAMES**
| **Field**     | **Name**       | **Help** |
|:--------------|:---------------|:---------|
| return-type   | void           |          |
| unit          | unit           |          |
| string        | data\_name     | camo\_power, driver\_power, gunner\_power, integrated\_light\_power, integrated\_light\_toggle\_power, integrated\_night\_vision\_toggle\_power, full\_spectrum\_vision\_power |
| real          | data\_value    |          |


## PHYSICS ##

#### physics\_get\_gravity ####
get the current global gravity acceleration relative to halo standard gravity
| **Field**     | **Name** | **Help** |
|:--------------|:---------|:---------|
| return-type   | real     |          |

#### physics\_set\_gravity ####
set global gravity acceleration relative to halo standard gravity
| **Field**     | **Name**           | **Help** |
|:--------------|:-------------------|:---------|
| return-type   | void               |          |
| real          | gravity\_fraction  |          |

#### physics\_constants\_reset ####
resets all physics constants to earthly values
| **Field**     | **Name** | **Help** |
|:--------------|:---------|:---------|
| return-type   | void     |          |


## RUNTIME DATA SCRIPTING INTERFACE ##
As of this writing, OS supports up to 64 'runtime integers/vectors'. These are stored within the game state, so they persist when the game is saved

#### runtime\_integers\_reset ####
reset ALL runtime integers back to zero
| **Field**     | **Name** | **Help** |
|:--------------|:---------|:---------|
| return-type   | void     |          |

#### runtime\_integer\_get ####
get the runtime integer's current value
| **Field**     | **Name** | **Help** |
|:--------------|:---------|:---------|
| return-type   | long     |          |
| value\_index  | short    |          |

#### runtime\_integer\_set ####
set the runtime integer's current value, returning its previous value
| **Field**     | **Name** | **Help** |
|:--------------|:---------|:---------|
| return-type   | void     |          |
| value\_index  | short    |          |
| value         | long     |          |

#### runtime\_integer\_inc ####
return the runtime integer's current value, then increments the value (aka post-increment)
| **Field**     | **Name** | **Help** |
|:--------------|:---------|:---------|
| return-type   | void     |          |
| value\_index  | short    |          |

#### runtime\_integer\_dec ####
return the runtime integer's current value, then decrements the value (aka post-decrement)
| **Field**     | **Name** | **Help** |
|:--------------|:---------|:---------|
| return-type   | void     |          |
| value\_index  | short    |          |

#### runtime\_integer\_operation ####
perform an operation on a runtime integer
| **Field**         | **Name**  | **Help** |
|:------------------|:----------|:---------|
| return-type       | long      | result of operation |
| value\_index      | short     |          |
| operation\_name   | string    | =, +=, -=, **=, /=, ++, --, &=, |=, <sup>=, <<=, >>=, +, -, *, /, &, |, </sup>, <<, >>**|
| operation\_value  | short     | not required for ++ or -- |

#### runtime\_vectors\_reset ####
reset ALL runtime vectors back to zero
| **Field**     | **Name** | **Help** |
|:--------------|:---------|:---------|
| return-type   | void     |          |

#### runtime\_vector\_get\_element ####
get the runtime vectors's current value
| **Field**       | **Name** | **Help** |
|:----------------|:---------|:---------|
| return-type     | real     |          |
| value\_index    | short    |          |
| element\_index  | short    | 0,1,2    |

#### runtime\_vector\_set\_element ####
set the runtime vector's element value
| **Field**        | **Name**  | **Help** |
|:-----------------|:----------|:---------|
| return-type      | boolean   | true if successful |
| value\_index     | short     |          |
| element\_index   | short     | 0, 1, 2  |
| operation\_name  | string    | =, +=, -=, **=, /=**|
| value            | real      |          |

#### runtime\_vector\_set ####
set the runtime vector's elements
| **Field**        | **Name**  | **Help** |
|:-----------------|:----------|:---------|
| return-type      | boolean   | true if successful |
| value\_index     | short     |          |
| operation\_name  | string    | =, +=, -=, **=, /=**|
| value\_x         | real      |          |
| value\_y         | real      |          |
| value\_z         | real      |          |

#### runtime\_vector\_operation ####
perform an operation on a runtime vector
NOTE: dot\_product and magnitude set the result value to operation\_vector\_index's 'x' (ie, 0th) element
| **Field**                | **Name**  | **Help** |
|:-------------------------|:----------|:---------|
| return-type              | boolean   | true if successful |
| value\_index             | short     |          |
| operation\_name          | string    | +=, -=, **=, /=, normalize, inverse, conjugate, cross\_product, dot\_product, scalar\_mul, scalar\_div, magnitude**|
| operation\_vector\_index | short     | not required for normalize or inverse |

#### runtime\_vector\_to\_string ####
returns the vector in a string as 'x y z', or an empty string if this fails
| **Field**     | **Name** | **Help** |
|:--------------|:---------|:---------|
| return-type   | string   |          |
| value\_index  | short    |          |

#### object\_data\_set\_vector ####
set an object's vector field using an existing runtime-vector. **DOES NOTHING IN NETWORKED GAMES IF REQUESTED PROPERTY DOESN'T SYNC**
| **Field**      | **Name**    | **Help** |
|:---------------|:------------|:---------|
| return-type    | boolean     | true if successful |
| object         | object      |          |
| string         | data\_name  | position, transitional\_velocity, forward, up, angular\_velocity, center |
| vector\_index  | short       | source vector |

#### object\_data\_save\_vector ####
save an object's vector field to an existing runtime-vector
| **Field**      | **Name**    | **Help** |
|:---------------|:------------|:---------|
| return-type    | boolean     | true if successful |
| object         | object      |          |
| string         | data\_name  | position, transitional\_velocity, forward, up, angular\_velocity, center |
| vector\_index  | short       | destintation vector |


## STRUCTURE\_BSP LIGHTMAP EXTENSIONS ##

This system is currently deprecated.


## GAME ENGINE ##

#### game\_change\_version\_id ####

| **Field**                       | **Name**    | **Help**                            |
|:--------------------------------|:------------|:------------------------------------|
| return-type                     | boolean     | true if the change was successful   |
| also-change-game-build-string   | boolean     |                                     |
| version-string                  | string      | "1.00", "1.07", "1.08", "1.09"      |

#### game\_engine\_data\_get\_integer ####
returns an integer value property of the current game engine
| **Field**     | **Name** | **Help**           |
|:--------------|:---------|:-------------------|
| return-type   | long     | -1 if this fails   |
| data\_name    | string   | see table below    |

##### Valid data\_name's #####
| **Engine** | **data\_name**        | **Help** |
|:-----------|:----------------------|:---------|
| All        | type                  | 0=None, 1=CTF, 2=Slayer, 3=Oddball, 4=King, 5=Race |
| All        | is\_teams             | 0=false, 1=true |
| All        | is\_odd\_man\_out     | 0=false, 1=true |
| All        | lives                 |          |
| All        | score\_to\_win        |          |
| Oddball    | oddball:ball\_count   |          |


## MACHINE/SERVER UTILS ##

#### machine\_is\_host ####
returns whether or not the local machine is the host
| **Field**     | **Name**  | **Help** |
|:--------------|:----------|:---------|
| return-type   | boolean   |          |

#### machine\_is\_dedi ####
returns whether or not the local machine is a dedicated server
| **Field**     | **Name**  | **Help** |
|:--------------|:----------|:---------|
| return-type   | boolean   |          |


## BIT OPERATIONS ##

#### bitwise\_and ####
AND 'value' with 'flags'
| **Field**     | **Name** | **Help** |
|:--------------|:---------|:---------|
| return-type   | long     |          |
| value         | long     |          |
| flags         | long     |          |

#### bitwise\_or ####
OR 'value' with 'flags'
| **Field**     | **Name** | **Help** |
|:--------------|:---------|:---------|
| return-type   | long     |          |
| value         | long     |          |
| flags         | long     |          |

#### bitwise\_xor ####
XOR 'value' with 'flags'
| **Field**     | **Name** | **Help** |
|:--------------|:---------|:---------|
| return-type   | long     |          |
| value         | long     |          |
| flags         | long     |          |

#### bitwise\_lhs ####
Bit-shift (to the left) 'value' by 'bit\_count'
| **Field**     | **Name**    | **Help** |
|:--------------|:------------|:---------|
| return-type   | long        | Returns 0 if count is out of range |
| value         | long        |          |
| flags         | bit\_count  | Any value between 0 and 31, inclusive |

#### bitwise\_rhs ####
Bit-shift (to the right) 'value' by 'bit\_count'
| **Field**     | **Name**    | **Help** |
|:--------------|:------------|:---------|
| return-type   | long        | Returns 0 if count is out of range |
| value         | long        |          |
| flags         | bit\_count  | Any value between 0 and 31, inclusive |

#### bit\_test ####
Test whether 'value' has a certain bit set
| **Field**     | **Name**    | **Help** |
|:--------------|:------------|:---------|
| return-type   | boolean     | Returns false if index is out of range |
| value         | long        |          |
| bit\_index    | short       | Any value between 0 and 31, inclusive |

#### bit\_toggle ####
Turns on or off the bit at 'bit\_index' in 'value'
| **Field**     | **Name**    | **Help** |
|:--------------|:------------|:---------|
| return-type   | false       | Returns false if index is out of range |
| value         | long        |          |
| bit\_index    | short       | Any value between 0 and 31, inclusive |
| on\_or\_off   | boolean     | as in true\_or\_false |

#### bit\_flags\_test ####
Test whether 'value' has a certain bit set
| **Field**     | **Name**    | **Help** |
|:--------------|:------------|:---------|
| return-type   | boolean     | Returns false if index is out of range |
| value         | long        |          |
| flags         | long        |          |

#### bit\_flags\_toggle ####
Adds or removes 'flags' to or from 'value'
| **Field**     | **Name**    | **Help** |
|:--------------|:------------|:---------|
| return-type   | long        | Returns 0 if index is out of range |
| value         | long        |          |
| flags         | long        |          |
| on\_or\_off   | boolean     | as in true\_or\_false |


## MISC ##

#### display\_scripted\_ui\_widget ####
displays the ui widget
| **Field**     | **Name**    | **Help** |
|:--------------|:------------|:---------|
| return-type   | boolean     | returns true if successful |
| widget\_name  | string      | name of the widget as it appears in the yelo tags. searches the scenario's yelo first, then globals |

#### play\_bink\_movie ####
players a .bik file (must be in the same folder as the game's bik files)
| **Field**     | **Name** | **Help** |
|:--------------|:---------|:---------|
| return-type   | void     |          |
| filename      | string   |          |

#### hex\_string\_to\_long ####
converts a hex string (eg, 'DEADBEEF') to an integer
| **Field**     | **Name** | **Help** |
|:--------------|:---------|:---------|
| return-type   | long     |          |
| value         | string   |          |

#### abs\_integer ####
return the absolute (non-negative) value of an integer
| **Field**     | **Name** | **Help** |
|:--------------|:---------|:---------|
| return-type   | long     |          |
| value         | long     |          |

#### abs\_real ####
return the absolute (non-negative) value of a real
| **Field**     | **Name** | **Help** |
|:--------------|:---------|:---------|
| return-type   | real     |          |
| value         | real     |          |


## RASTERIZER ##

#### rasterizer\_rt\_display ####
Global variable used to display the contents of the GBuffer. -1 = all buffers, 0 = no display, 1 = depth, 2 = velocity, 3 = normals (-1 to 1),
4 = normals (0 to 1), 5 = index.
| **Field**        | **Name**                | **Help** |
|:-----------------|:------------------------|:---------|
| short            | rasterizer\_rt\_display |          |

#### rasterizer\_gbuffer\_enabled ####
Global variable used to disable/enable the GBuffer. DO NOT use this in your maps scripts. This is only to be used by the end user if they have issues with performance.
| **Field**        | **Name**                     | **Help** |
|:-----------------|:-----------------------------|:---------|
| bool             | rasterizer\_gbuffer\_enabled |          |

## POSTPROCESSING ##

#### pp\_external\_post\_processes\_enabled ####
Global variable used to enable/disable external post processes. DO NOT use this in your maps scripts.
| **Field**        | **Name**                             | **Help** |
|:-----------------|:-------------------------------------|:---------|
| bool             | pp\_external\_post\_processes\_enabled |          |

#### pp\_internal\_post\_processes\_enabled ####
Global variable used to enable/disable internal post processes. DO NOT use this in your maps scripts.
| **Field**        | **Name**                             | **Help** |
|:-----------------|:-------------------------------------|:---------|
| bool             | pp\_internal\_post\_processes\_enabled |          |

#### pp\_fxaa\_enabled ####
Global variable used to enable/disable FXAA. DO NOT use this in your maps scripts.
| **Field**        | **Name**          | **Help** |
|:-----------------|:------------------|:---------|
| bool             | pp\_fxaa\_enabled |          |

#### pp\_motion\_blur\_enabled ####
Global variable used to enable/disable motion blur. DO NOT use this in your maps scripts. This is only to be used by the end user if they have issues with performance.
| **Field**        | **Name**                 | **Help** |
|:-----------------|:-------------------------|:---------|
| bool             | pp\_motion\_blur\_enabled |          |

#### pp\_motion\_blur\_amount ####
Global variable used to control the amount of motion blur. DO NOT use this in your maps scripts.
| **Field**        | **Name**                | **Help** |
|:-----------------|:------------------------|:---------|
| bool             | pp\_motion\_blur\_amount |          |

#### pp\_load ####
Reloads the post processing system after an unload. DO NOT use this in your maps scripts, it is to be used only as a console function!
| **Field**        | **Name**      | **Help** |
|:-----------------|:--------------|:---------|
| returns          | void          |          |

#### pp\_unload ####
Unloads the entire post processing system. DO NOT use this in your maps scripts, it is to be used only as a console function!
| **Field**        | **Name**      | **Help** |
|:-----------------|:--------------|:---------|
| return-type      | void          |          |

#### pp\_get\_effect\_instance\_index\_by\_name ####
Returns the index of the effect instance by name
| **Field**     | **Name**      | **Help** |
|:--------------|:--------------|:---------|
| return-type   | short         | **-1** if there is no matching effect instance |
| string        | instance\_name |          |

#### pp\_set\_effect\_instance\_active ####
Turns an effect instance on or off. If an effect instance is set to be not initially enabled, this is the only way to enable the instance.
| **Field**        | **Name**           | **Help** |
|:-----------------|:-------------------|:---------|
| return-type      | void               |          |
| short            | instance\_index    |          |
| boolean          | instance\_enabled  | True to enable the effect, false to disable |

#### pp\_set\_effect\_instance\_fade ####
This function will fade the result of an effect instance in or out. The function takes the instance index, the fade amount to start at and end at (between 0.0 and 1.0), and the length of time the fade should take. If fade-start and fade-end are equal, or fade-time is zero, the end fade amount will be applied immediately. Fading the effect between 0.0 and 1.0 requires a built in post process to be used and incurs a minor performance hit, however once the fade is at 0.0 or 1.0 the built in post process is no longer used.
| **Field**        | **Name**         | **Help** |
|:-----------------|:-----------------|:---------|
| return-type      | void             |          |
| short            | effect\_index    |          |
| real             | fade\_start      | The fade amount to start fading in/out from. Between 0.0f and 1.0f. |
| real             | fade\_end        | The fade amount to start fading in/out to. Between 0.0f and 1.0f. |
| real             | fade\_time       | The amount of time in seconds (approximate) that the fade should take. |

#### pp\_get\_effect\_instance\_current\_fade ####
Returns the current fade amount of an effect instance.
| **Field**        | **Name**         | **Help** |
|:-----------------|:-----------------|:---------|
| return-type      | real             |          |
| short            | instance\_index  |          |

#### pp\_get\_effect\_instance\_fade\_direction ####
Returns the current fade direction of an effect instance.
| **Field**        | **Name**         | **Help** |
|:-----------------|:-----------------|:---------|
| return-type      | short            | -1 = not fading, 0 = fading out, 1 = fading in |
| short            | instance\_index  |          |

#### pp\_get\_effect\_index\_by\_name ####
Returns the index of a post process effect by name
| **Field**     | **Name**      | **Help** |
|:--------------|:--------------|:---------|
| return-type   | short         | **-1** if there is no matching effect |
| string        | effect\_name  |          |

#### pp\_get\_effect\_is\_valid ####
Returns true if the effect is correct and ready for use. If an effect is broken for some reason the function returns false. For example, this could occur when a shader in the effect uses a shader model higher than the players graphic card can support.
| **Field**        | **Name**         | **Help** |
|:-----------------|:-----------------|:---------|
| return-type      | boolean          |          |
| short            | effect\_index    |          |

#### pp\_get\_effect\_shader\_variable\_index\_by\_name ####
Returns the index of a scripted shader variable in a defined effect by name.
| **Field**        | **Name**         | **Help** |
|:-----------------|:-----------------|:---------|
| return-type      | short            | **-1** if there is no matching scripted variable |
| short            | effect\_index    |          |
| string           | variable\_name   |          |

#### pp\_set\_effect\_shader\_variable\_boolean ####
Sets the value of a scripted boolean variable.
| **Field**        | **Name**         | **Help** |
|:-----------------|:-----------------|:---------|
| return-type      | void             |          |
| short            | effect\_index    |          |
| short            | variable\_index  |          |
| boolean          | value            |          |
| real             | change\_time     | Time in seconds (approx.) to change from the current value to the new value. |

#### pp\_set\_effect\_shader\_variable\_integer ####
Sets the value of a scripted integer variable.
| **Field**        | **Name**         | **Help** |
|:-----------------|:-----------------|:---------|
| return-type      | void             |          |
| short            | effect\_index    |          |
| short            | variable\_index  |          |
| long             | value            |          |
| real             | change\_time     | Time in seconds (approx.) to change from the current value to the new value. |

#### pp\_set\_effect\_shader\_variable\_real ####
Sets the value of a scripted integer variable. This function is used for float, float2, float3, float4 and color variables. Use 0.0 for channels you do not need to set.
| **Field**        | **Name**         | **Help** |
|:-----------------|:-----------------|:---------|
| return-type      | void             |          |
| short            | effect\_index    |          |
| short            | variable\_index  |          |
| real             | float1           |          |
| real             | float2           |          |
| real             | float3           |          |
| real             | float4           |          |
| real             | change\_time     | Time in seconds (approx.) to change from the current value to the new value. |

#### pp\_set\_effect\_shader\_instance\_active ####
Turn a specific shader instance in an effect on or off.
| **Field**        | **Name**           | **Help** |
|:-----------------|:-------------------|:---------|
| return-type      | void               |          |
| short            | effect\_index      |          |
| short            | instance\_index    |          |
| boolean          | instance\_enabled  | True to enable the instance, false to disable. |

#### pp\_bloom\_set\_size ####
Changes the blur size of the bloom post process.
| **Field**        | **Name**      | **Help** |
|:-----------------|:--------------|:---------|
| return-type      | void          |          |
| real             | size          |          |
| real             | change\_time  | Time in seconds (approx.) to change from the current value to the new value. |

#### pp\_bloom\_set\_exposure ####
Changes the exposure value of the bloom post process. The bloom result is multiplied by this.
| **Field**        | **Name**      | **Help** |
|:-----------------|:--------------|:---------|
| return-type      | void          |          |
| real             | exposure      |          |
| real             | change\_time  | Time in seconds (approx.) to change from the current value to the new value. |

#### pp\_bloom\_set\_mix\_amount ####
Changes the mix amount of the bloom post process. This interpolates between no bloom and full bloom.
| **Field**        | **Name**      | **Help** |
|:-----------------|:--------------|:---------|
| return-type      | void          |          |
| real             | mix\_amount   |          |
| real             | change\_time  | Time in seconds (approx.) to change from the current value to the new value. |

#### pp\_bloom\_set\_minimum\_color ####
Changes the minimum color value of the bloom post process. Colors below this value have no bloom.
| **Field**        | **Name**      | **Help** |
|:-----------------|:--------------|:---------|
| return-type      | void          |          |
| real             | red           |          |
| real             | green         |          |
| real             | blue          |          |
| real             | change\_time  | Time in seconds (approx.) to change from the current value to the new value. |

#### pp\_bloom\_set\_maximum\_color ####
Changes the maximum color value of the bloom post process. Colors above this value are fully bloomed.
| **Field**        | **Name**      | **Help** |
|:-----------------|:--------------|:---------|
| return-type      | void          |          |
| real             | red           |          |
| real             | green         |          |
| real             | blue          |          |
| real             | change\_time  | Time in seconds (approx.) to change from the current value to the new value. |