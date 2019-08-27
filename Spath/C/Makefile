all: sut runner test

runner: runner.o sut.o
	${CC} -o $@ runner.o sut.o ${LDLFLAGS}

test: runner
	./runner

sut.o: sut.c
	@echo CC $<
	@${CC} -c ${CFLAGS} $<
