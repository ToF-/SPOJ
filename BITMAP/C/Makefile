
all: bitmap runner test

runner: runner.o bitmap.o
	${CC} -o $@ runner.o bitmap.o 

test: runner
	./runner

bitmap.o: bitmap.c bitmap.h
	@echo CC $<
	@${CC} -c $<

spath.o: spath.c bitmap.c
	@echo CC $<
	@${CC} -c  $<

spath: spath.o bitmap.o 
	${CC} -o $@ spath.o bitmap.o 

main: bitmap.c
	cat bitmap.h > SPOJ.c
	tail -n +2 bitmap.c >>SPOJ.c	
	cat main.c >>SPOJ.c
	${CC} SPOJ.c
