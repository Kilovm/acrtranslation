## Directory Structure ##

MESS/: texts and fonts, japanese version

LANG/ENG/MESS: texts and fonts, english version

Files inside MESS/ are almost the same, between PAL and NTSC-J version of the game, except one file "GPMESS\_DATA.STR".

## .BRFNT file ##

see http://github.com/Tempus/Brfnt-Creator

### RFNT Header ###

10h bytes

0000: 'RFNT'

0004: FE FF 01 04

0008: DWORD length of whole file

000C: WORD ??

000E: WORD ??  (language flag?)

### FINF Header ###
20H bytes:

0000: 'FINF'

0004: DWORD finf size

0008: BYTE ??

0009: BYTE font height - 1

000A: WORD ??

000C: BYTE ??

000D: BYTE character width or height + 1 (?)

000E: BYTE character height or width + 1 (?)

000F: BYTE 00 (?)

0010: DWORD TGLP section position

0014: DWORD CWDH section position

0018: DWORD CMAP section position

001C: BYTE font height - 1 (excluding left line?)

001D: BYTE font width - 1 (excluding bottom line?)

001E: BYTE character width or height - 1 (?)

001F: BYTE 00 (?)

### TGLP Header ###

30h bytes

0000: 'TGLP'

0004: DWORD length of 'TGLP' section

0008: BYTE font width - 1

0009: BYTE font height - 1

000A: BYTE character width - 1

000B: BYTE character height - 1

000C: DWORD length of 1 image

0010: WORD images count

0012: WORD ??

0014: WORD characters per row

0016: WORD characters per column

0018: WORD width of image

001A: WORD height of image

001C: DWORD position of data

### TGLP Data ###

Bitmap is formed in a Zig-zag way.


A single block of bitmap looks like this:

00 01 02 03 04 05 06 07

08 09 0A 0B 0C 0D 0E 0F

10 11 12 13 14 15 16 17

18 19 1A 1B 1C 1D 1E 1F


Multiple blocks:

B01 B02 B03 ... B08 (assume width=64)

B09 B0A B0B ...

...


Multiple images:

I01

I02

I03

...

### CWDH Section ###

0000: 'CWDH'

0004: DWORD length of this section

0008: DWORD last character ? (= character count - 1)

000C: DWORD first character ? (= 0)

0010: character count `*` (

> BYTE bearing\_x

> BYTE glyph width

> BYTE advance\_x )

????: (0~3) `*` 00 (align to 4 bytes)

### CMAP Section ###

Type 1 :

0000: ‘CMAP’

0004: DWORD length of this section

0008: WORD UTF-16 code of first char

000A: WORD UTF-16 code of last char

000C: DWORD 0 ?

0010: DWORD position of next CMAP + 8

0014: WORD offset of first char

0016: WORD ?

Type 2 :

0000: ‘CMAP’

0004: DWORD length of this section

0008: WORD 0

000A: WORD -1

000C: 00 02 00 00  ?

0010: DWORD position of next CMAP + 8 (0 if no next)

0014: WORD characters in this table (tbl\_char count)

0016: tbl\_char count `*` (

> WORD UTF-16 code of char

> WORD offset of char )

????: WORD 0

## .BMG file ##

http://wiibrew.org/wiki/BMG_files

Texts are stored in UTF-8 encoding.

Commands are inserted into texts.

### Command ###

0000: BYTE 1A

0001: BYTE length

0002: BYTE `*` (length - 1)

### Seperator ###

FIXME: Is it a seperator? I think it's the end of a command, or \n inside texts.

0000: BYTE 0A

### Null ###

0000: BYTE 00

## .MESS file ##

Texts are stored in UTF-8 encoding.

## .TPL file ##

Deprecated. see http://wiibrew.org/wiki/Banner

0000: 00 20 AF 30 00 00 00 01 00 00 00 0C 00 00 00 14

0010: DWORD 0

0014: WORD width or height

0016: WORD height or width

0018: DWORD format

001C: DWORD 0x40

0020: 00 00 00 00 00 00 00 00 00 00 00 01 00 00 00 01

0030: 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00

0040: image data, (width align with 4) `*` (height align with 4) `*` getBytesPerPixel(format)

## .ISO file ##

http://wiibrew.org/wiki/Wii_Disc

## .THP file ##

Thanks shuilixiang.

http://www.cngba.com/thread-17229994-1-1.html

http://www.amnoid.de/gc/

http://www.hcs64.com/in_cube.html

## .BRRES file ##

http://www.zapotlanejo.info/Nintendopapercraftforum/viewtopic.php?f=19&t=3283