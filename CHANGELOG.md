Changelog
=========

0.4.2
-----

* A new build system running on the .NET 8 SDK
* Better support for trimmed deployment
* Better support for debugging and SourceLink
* Documentation improvements


0.4.1
-----

* Multitargeting fixes


0.3.0
-----

* Added `WithFixedSizeBuffer` helper methods, which stack-allocate a fixed size buffer of a given size and then pass it as a `Span` to a func.
* A new build system based on GitHub Actions
* API docs are now available at http://www.benjamin.pizza/FixedSizeBuffers
