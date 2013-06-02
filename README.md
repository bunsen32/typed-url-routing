typed-url-routing
=================

Library which adds strong typing to the URL-routing/generation in Microsoft's MVC4 (alpha-quality).

You may be able to use this directly, but since it’s not properly tested, or proven in production, you’ll probably 
have to fix a lot of things yourself.

If that doesn’t put you off, have a read of this blog post which explains the idea:
> https://dysphoria.net/2013/03/14/strongly-typed-action-references-in-microsoft-mvc4/

Basically it lets you:
 * Define URL routes as strongly-typed, first-class-objects
 * Bind routes to controller actions, in such a way that the compiler catches parameter mismatches/misspellings
 * Generate links in your Razor code (a) succinctly and (b) fully statically type-checked.

Oh and:
 * It all works at compile time. (You don’t need to run a program to generate code or anything like that.)
