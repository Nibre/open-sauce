# Introduction #

Recently, someone asked me about the standards used in OS and about the standards used at Bungie. I decided it would probably be best to answer these two questions in a wiki entry.

# Bungie #

When it comes to general C/C++ code, Bungie uses a lower-underscore-case. Eg, "object\_create". All words in it are lowercase, and the underscore is used for separating those words. From Halo2 and onward, they started using a subset of Hungarian notation for types:
  * class:    "c\_this\_is\_a\_class
  * struct:   "s\_this\_is\_a\_struct"
  * enum:     "e\_this\_is\_an\_enum\_typename"
  * constant: "k\_i\_am\_a\_named\_constant"

For non-POD types they prefix members with "m`_`" (eg, "m\_definition\_index"). I've also seen cases where they use the "g`_`" prefix for global variables.

For enumeration types, the typename will be prefixed with "e`_`". Members of the enum wll be prefixed with the typename, sans the initial "e". For members which describe a bit index, they typically postfix the member name with "`_`bit". For members which describe a series of flags, I recall seeing them use "`_`flags" before as a postfix.

Like say, these are conventions used with the start of Halo2. With Halo1, they were still only using C in the engine code (albeit, the tools had some C++ code). Many of these conventions weren't followed at the time because they didn't have fancy pants classes and such.

And of course, they also use SCREAMING\_CAPS for macros definitions.

# Kornner Studios #

When it comes to C++ based code, we typically follow the same conventions, with a few differences. Where Bungie doesn't use namespaces (as far as I've seen in their production code), we do. For the most part, we also use pascal case for our functions and namespace names. Usually when they're not directly related to reverse engineered Bungie code anyway. There are some exceptions (see `EngineFunctions`.hpp).

We also keep enum values tucked away in a nested "Enums" namespace. The same goes for bit/flag enumerations, but in a "Flags" namespace.

# .NET #

Both of us seem to follow the conventions used by the .NET framework itself. However, for newer code (anything made after 2009) we've started to use "m`_`" prefixes for non-public member data (some of the .NET framework's internals do this, other parts don't). We also use a 'k' prefixed for literals as well (still pascal case after the 'k' though)