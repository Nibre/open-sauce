# Introduction #

OS's source tree is laid out in a way to let multiple solutions covering multiple and differing problems co-exist. For example, "`BlamLib`" covers .NET interop with all of the Blam engines. Then there is the actual `OpenSauce` itself which covers extending the engine and its tools.


# Details #

Besides the actual code's tree there is also an explicit code-object tree. All the projects are setup to output to "bin", "lib" and "obj" in the root directory.

Then there are the shared references. For things shared on a global level there is the "shared" folder in the root directory. In here is where shared include files, assemblies, etc are placed. Specific solutions can also have their own shared data, in which case they would have a "shared" folder within their solution directory. For example, `OpenSauce` has it's own "shared" folder where it can reference shared code/data for the game and tool extensions.

# `OpenSauce` project naming convention #

The OS solution has a specific naming convention. It goes as follows: {Game}{#}