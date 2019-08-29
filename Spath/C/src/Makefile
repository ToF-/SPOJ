all: dijkstra runner test

runner: runner.o dijkstra.o
	${CC} -o $@ runner.o dijkstra.o 

test: runner
	./runner

dijkstra.o: dijkstra.c dijkstra.h
	@echo CC $<
	@${CC} -c $<

spath.o: spath.c dijkstra.c
	@echo CC $<
	@${CC} -c  $<

spath: spath.o dijkstra.o 
	${CC} -o $@ spath.o dijkstra.o 

main: dijkstra.c
	cat dijkstra.h > SPOJ.c
	tail -n +2 dijkstra.c >>SPOJ.c	
	cat main.c >>SPOJ.c
	${CC} SPOJ.c
