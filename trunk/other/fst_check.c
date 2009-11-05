#include <stdio.h>
#include <stdlib.h>
#include <string.h>

// http://en.wikipedia.org/wiki/64-bit
typedef unsigned char u8;
typedef unsigned short u16;
typedef unsigned int u32;
typedef unsigned long long u64;

void init_check_table(u32 files);
void add_to_check_table(u32 offset, u32 size, const char *name, u8 is_dir, u32 self, u32 parent);
void print_check_table(u8 sort_table);

u32 _be32(u8 *addr)
{
    int i;
    u32 retval = 0;
    for(i = 0; i < 4; i ++)
    {
        retval = (retval << 8) | ((u32)(*(addr + i)) & 0xff);
    }
    return retval;
}

u32 do_fst(u8 *fst, const char *names, u32 i, u32 parent)
{
    u32 offset;
    u32 size;
    const char *name;
    u32 j;

    name = names + (_be32(fst + 12*i) & 0x00ffffff);
    size = _be32(fst + 12*i + 8);
    offset = _be32(fst + 12*i + 4);

    if (i == 0) {
		add_to_check_table(offset, size, name, fst[12*i], i, parent);

        for (j = 1; j < size; ){
            j = do_fst(fst, names, j, i);
                }
        return size;
    }
        //printf("name    %s\n",name);

    if (fst[12*i]) {

		add_to_check_table(offset, size, name, fst[12*i], i, parent);

        for (j = i + 1; j < size; )
            j = do_fst(fst, names, j, i);

        return size;
    } else {
		add_to_check_table(offset, size, name, fst[12*i], i, parent);
        return i + 1;
    }
}

void do_files(const char *filename, u8 sort_table)
{
    FILE *fp;
    size_t fsize;
    u8 *fst;
    u32 n_files;

    if((fp = fopen(filename, "rb")) == NULL)
        return;
    fseek(fp, 0, SEEK_END);
    fsize = ftell(fp);
    fseek(fp, 0, SEEK_SET);
    if ((fst = malloc(fsize)) == NULL)
    {
        fclose(fp);
        return;
    }
    fread(fst, 1, fsize, fp);
    fclose(fp);

    n_files = _be32(fst + 8);

	init_check_table(n_files);

    if (n_files > 1)
        do_fst(fst, (char *)fst + 12*n_files, 0, 0);

	print_check_table(sort_table);
    free(fst);
}

typedef struct FST_Entry{
	u32 offset;
	u32 size;
	u8 is_dir;
	const char *name;
	struct FST_Entry *parent;
} FSTentry;

FSTentry **check_table = NULL;

u32 proc_count = 0;

u32 proc_num = 0;

void init_check_table(u32 files)
{
	if (check_table != NULL)
	{
		fprintf(stderr, "Duplicate initialization.\n");
		free(check_table);
	}
	check_table = (FSTentry **) malloc(sizeof(FSTentry *) * files);
	memset(check_table, 0, sizeof(FSTentry *) * files);
	proc_count = files;
}

void add_to_check_table(u32 offset, u32 size, const char *name, u8 is_dir, u32 self, u32 parent)
{
	if (check_table == NULL)
	{
		fprintf(stderr, "Not initialized.\n");
		return;
	}
	if (proc_num != self)
		fprintf(stderr, "Wrong order (ASSUME: %u, ACTUAL: %u).\n", proc_num, self);
	if (check_table[self] != NULL)
		fprintf(stderr, "Entry %u is already full.\n", self);
	else
		check_table[self] = (FSTentry *) malloc(sizeof(FSTentry));
	check_table[self]->offset = offset;
	check_table[self]->size = size;
	check_table[self]->is_dir = is_dir;
	check_table[self]->name = name;
	if (parent > self || check_table[parent] == NULL)
		fprintf(stderr, "Invalid parent (%u) for node (%u).\n", parent, self);
	check_table[self]->parent = check_table[parent];
	proc_num ++;
}

int compare_table_entry(const void *a, const void *b)
{
	const FSTentry *fa = *((FSTentry **)a);
	const FSTentry *fb = *((FSTentry **)b);
	if (fa == fb)
		return 0;
	if (fa == NULL)
		return -1;
	if (fb == NULL)
		return 1;
	if (fa->offset == fb->offset)
		return 0;
	if (fa->offset > fb->offset)
		return 1;
	else
		return -1;
}

void print_name(FSTentry *ent)
{
	if (ent->parent == ent)
	{
		printf("ROOT");
		return;
	}
	print_name(ent->parent);
	printf("/");
	printf("%s", ent->name);
}

void print_check_table(u8 sort_table)
{
	u32 i;
	u32 prev_off, data_from, data_to;
	if (check_table == NULL)
	{
		fprintf(stderr, "Not initialized.\n");
		return;
	}
	if (proc_num != proc_count)
		fprintf(stderr, "Wrong count (ASSUME: %u, ACTUAL: %u).\n", proc_count, proc_num);
	for (i = 0; i < proc_count; i ++)
		if (check_table[i] == NULL)
			fprintf(stderr, "Empty node (%u).\n", i);
	if (sort_table)
		qsort(check_table, proc_count, sizeof(FSTentry *), compare_table_entry);
	prev_off = 0;
	for (i = 0; i < proc_count; i ++)
	{
		if (check_table[i] == NULL)
			continue;
		if (check_table[i]->is_dir)
			continue;
		data_from = check_table[i]->offset << 2;
		if (check_table[i]->size == 0)
			data_to = data_from + 32;
		else
			data_to = data_from + ((((check_table[i]->size + 31) >> 5)) << 5);
		if (sort_table)
		{
			if (data_from > prev_off)
				printf("Blank [%x - %x] Size: %u\n", prev_off, data_from, data_from - prev_off);
			if (data_from < prev_off)
				fprintf(stderr, "Conflict Area: [%x - %x]\n", data_from, prev_off);
		}
        printf("Offset: [%x - %x], Size: %u, Name: ", data_from, data_to, check_table[i]->size);
		print_name(check_table[i]);
		printf("\n");
		if (sort_table || data_to > prev_off)
			prev_off = data_to;
	}
	printf("Total size: %u\n", prev_off);
	for (i = 0; i < proc_count; i ++)
		free(check_table[i]);
	free(check_table);
}

int main()
{
    do_files("fst.bin", 1);
    return 0;
}
