#include <stdio.h>
#include <allegro.h>

#define SPLIT_V 4
#define SPLIT_H 8
#define GRID 0
#define MARGIN 2
#define SCALE 1

int from_big(unsigned char *c, int bytes)
{
	int ret_val = 0, i;
	for (i = 0; i < bytes; i ++)
	{
		ret_val = (ret_val << 8);
		ret_val |= ((int) c[i]) & 0xff;
	}
	return ret_val;
}

#define from_big_dword(x) from_big(x, 4)
#define from_big_word(x) from_big(x, 2)

void draw_point(int x, int y, unsigned char color)
{
	if (color == 3)
		rect(screen, x*SCALE, y*SCALE, (x+1)*SCALE-1, (y+1)*SCALE-1, makecol(0, 0, 0x7f));
	else
		rect(screen, x*SCALE, y*SCALE, (x+1)*SCALE-1, (y+1)*SCALE-1, makecol(color, color, color));
}

void draw_buf(unsigned char *buf, long fsize)
{
	long pos = 0x60;
	long end_pos = 0x30 + from_big_dword(buf + 0x34);
	long bmp_w = from_big_word(buf + 0x48);
	long bmp_h = from_big_word(buf + 0x4a);
	int blocks = bmp_w / SPLIT_H;
	int x, y, i, j, k;
	x = y = 0;
	while (pos < end_pos)
	{
		for (i = 0; i < blocks; i ++)
		{
			for (j = 0; j < SPLIT_V; j ++)
			{
				for(k = 0; k < SPLIT_H; k ++)
				{
					draw_point(x, y, buf[pos]);
					pos ++;
					x ++;
				}
				x -= SPLIT_H;
				y ++;
			}
			x += SPLIT_H + GRID;
			y -= SPLIT_V;
		}
		x -= blocks * (SPLIT_H + GRID);
		y += SPLIT_V + GRID;
		if (y >= bmp_h)
		{
			x += blocks * (SPLIT_H + GRID) + MARGIN;
			y = 0;
		}
	}
}

void draw_something(const char *fn)
{
	FILE *fp;
	unsigned char *buf;
	long fsize;
	if ((fp = fopen(fn, "rb")) == NULL)
		return;
	fseek(fp, 0, SEEK_END);
	fsize = ftell(fp);
	fseek(fp, 0, SEEK_SET);
	if ((buf = malloc(fsize)) == NULL)
	{
		fclose(fp);
		return;
	}
	fread(buf, 1, fsize, fp);
	fclose(fp);

	draw_buf(buf, fsize);
	free(buf);
}

int main()
{
	allegro_init();
	install_keyboard();
	set_window_title("Test");
	set_color_depth(24);
	set_gfx_mode(GFX_AUTODETECT_WINDOWED, 800, 600, 0, 0);
	clear_to_color(screen, makecol(0, 0x7f, 0));
	//draw_something("utf8_J.brfnt");
	//draw_something("FONT_A.BRFNT");
	//draw_something("FONT_A_19.BRFNT");
	//draw_something("FONT_A_21.BRFNT");
	//draw_something("FONT_B.BRFNT");
	draw_something("FONT_B_16.BRFNT");
	readkey();
	return 0;
}
END_OF_MAIN()
