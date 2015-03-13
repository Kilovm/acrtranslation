## File extract/import ##

### Trucha ###

This tool can extract the ISO file recursively. But only file which is smaller than or equal to original one can be imported.

Usage:
  * import wiikeyset.reg file to Windows registry
  * select "Select KeySet -> Custom KeySet 1" from menu
  * remember to "Trucha sign" partition after modification, and Close ISO before loading the game from emulator

### WiiScrubber ###

This tool can import files of any size into ISO.

Usage:
  * put key.bin (Wii Common Key File) to the same directory as executable.

## Emulator ##

The only existing wii emulator is [dolphin-emu](http://www.dolphin-emu.com).

  * "Options -> Configure", uncheck "Use Panic Handlers".
  * "Options -> Graphic Settings", check "Use Real XFB".
  * NOTE: debug version must be used with plugins of debug version as well.
  * NOTE: the game doesn't run well with DirectX plugin yet.
  * Add command line argument "-d" to start as debugger mode.
  * NOTE: the game doesn't run well on Linux. :(

# Details #

Add your content here.  Format your content with:
  * Text in **bold** or _italic_
  * Headings, paragraphs, and lists
  * Automatic links to other wiki pages