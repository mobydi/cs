# README #

This is C# coding test app.

### What I have done ###

* Common application architecture in Reactive Style
* Basic simple WPF UI
* 3 simple plugins for csv, txt and xml parsing
* Basic Unit tests
* Simple config in App.config

### Project notes ###

* Processing files is moved to "Processed" folder to avoid double processing. Requirements doesn't cover this case.
* I batch(buffer) input file content per 100 lines, to IO performance and reduce thread overhead

### What I haven't done ###

* File can be open due to processing, need to skip it
* Logging. Because logging depends on you local preferences and styles. I maked critical section for logging as TODO
* Plugins can be in Reactive Style too, with async reading. I skipped this for simplicity reason
* File content parsing as datetime, int and so on. Requirements doesn't cover this case and for simplicity reason
* Nice UI
* 99.9% unit test coverage and input with errors and mistakes 
* Unit tests for directory watcher
* Coding style, method cases and other things let's leave for haters
