GENERICOBJECTS = main.o endian.o
MAINOBJECTS = tpl.o
OBJECTS = $(GENERICOBJECTS) $(MAINOBJECTS)
CFLAGS = -g
LIBS = -lgd
OUTPUT = zetsubou
main: $(OBJECTS)
	gcc -g -o $(OUTPUT) $(OBJECTS) $(LIBS)
clean:
	rm -f $(OUTPUT) $(OBJECTS)
