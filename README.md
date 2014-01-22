Compilable Graph Format (CGF)
===============

CGF is a visual coding/scripting system, used to draw your code in a visual graph format and then compile it to a file.
My first project will be compiling .hlsl files, which I'm already moving steadily forward with.

To begin with, I'm gonna make it work for PostFX's. That said, I am making it completely modular, so we might have a "PostFX" branch and a "Material" branch and an "AI" branch which each does seperate things.

**Here is the current solution structure:**

*Source/Modules*
In here, all the nodes are written in class libraries which will be compiled to .dll's that can be added and removed without recompiling the code.

*Source/CGF*
This library contains all the interfaces and general classes the CGF uses for compiling, saving and loading etc..

*Source/Graph*
This is a modified version of the Graph library by LogicKing

*Source/PostFxUI*
The Windows Form UI for PostFX's

*Source/T3DHLSLAPI*
The API used for .hlsl files in T3D.
This includes some interfaces for identifying what type of nodes you deal with, a DependencyParserStrategy, a HLSLCompiler and a T3DPostFxCompiler.

If you want to add support for something else, you will just have to create a custom UI file, and write your own compiler.
And ofc, the different nodes you need.

It's a bit of work, but something like an AI behaviour tree is definetly possible (but also very much depending on the implementation of the AI).
