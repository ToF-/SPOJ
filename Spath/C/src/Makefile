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
