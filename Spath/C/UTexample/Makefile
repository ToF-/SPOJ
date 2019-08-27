all: sut runner test

runner: runner.o sut.o
	${CC} -o $@ runner.o sut.o ${LDLFLAGS}

test: runner
	./runner

sut.o: sut.c sut.h
	@echo CC $<
	@${CC} -c ${CFLAGS} $<

prog.o: prog.c sut.c
	@echo CC $<
	@${CC} -c ${CFLAGS} $<

prog: prog.o sut.o 
	${CC} -o $@ prog.o sut.o ${LDLFLAGS}
