GENERICOBJECTS = main.o endian.o
MAINOBJECTS = tpl.o
OBJECTS = $(GENERICOBJECTS) $(MAINOBJECTS)
CFLAGS = -O2
LIBS = -lgd
OUTPUT = zetsubou.exe
main: $(OBJECTS)
	gcc $(CFLAGS) -o $(OUTPUT) $(OBJECTS) $(LIBS)
clean:
	rm -f $(OUTPUT) $(OBJECTS)
