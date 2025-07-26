CLEANUP = rm -f

C_COMPILER=gcc
ifeq ($(shell uname -s), Darwin)
C_COMPILER=clang
endif

UNITY_ROOT=unity

TEST_TARGET=bin/run_test

APP_TARGET=bin/spoj

TEST_FILES=test/*.c test/runner/*.c

APP_FILES=src/anadiv.c

SPOJ_FILES=build/spoj.c
SPOJ_STRICT_FILES=build/spoj-strict.c

UNITY_FILES=$(UNITY_ROOT)/src/unity.c $(UNITY_ROOT)/extras/memory/src/unity_memory.c $(UNITY_ROOT)/extras/fixture/src/unity_fixture.c  $(TEST_FILES)

INC_DIRS=-Isrc -I$(UNITY_ROOT)/src -I$(UNITY_ROOT)/extras/fixture/src -I$(UNITY_ROOT)/extras/memory/src

unit:
	mkdir -p test/runner
	mkdir -p bin
	./gen_test_runner.awk test/anadiv_tests.c >test/runner/test_runner.c
	./gen_all_tests.awk test/anadiv_tests.c >test/runner/all_tests.c
	$(C_COMPILER) $(CFLAGS) $(INC_DIRS) $(SYMBOLS) $(APP_FILES) $(UNITY_FILES) -o $(TEST_TARGET)
	- ./$(TEST_TARGET)

spoj: src/anadiv.c src/anadiv.h
	mkdir -p build
	mkdir -p bin
	cat src/anadiv.h         >build/spoj.c
	tail -n +2 src/anadiv.c >>build/spoj.c
	echo "int main(int argc, char* argv[]) { process(false); return 0; }" >>build/spoj.c
	$(C_COMPILER) $(CFLAGS) $(INC_DIRS) $(SYMBOLS) $(SPOJ_FILES) -o bin/spoj

spoj-strict: src/anadiv.c src/anadiv.h
	mkdir -p build
	mkdir -p bin
	cat src/anadiv.h         >build/spoj-strict.c
	tail -n +2 src/anadiv.c >>build/spoj-strict.c
	echo "int main(int argc, char* argv[]) { process(true); return 0; }" >>build/spoj-strict.c
	$(C_COMPILER) $(CFLAGS) $(INC_DIRS) $(SYMBOLS) $(SPOJ_STRICT_FILES) -o bin/spoj-strict

tests: spoj
	bin/spoj


spike: src/spike.c
	$(C_COMPILER) $(CFLAGS) $(INC_DIRS) $(SYMBOLS) src/spike.c -o bin/spike
	bin/spike

clean:
	rm build/
	rm bin/*

tags: src/anadiv.c src/anadiv.h test/anadiv_tests.c
	`brew --prefix`/bin/ctags --recurse

random: spoj
	bin/spoj <../test/random-cases.txt >result.txt
	diff ../test/random-expected.txt result.txt

series: spoj
	bin/spoj <../test/series-cases.txt >result.txt
	diff ../test/series-expected.txt result.txt

series-strict: spoj-strict
	bin/spoj-strict <../test/series-strict-cases.txt >result.txt
	diff ../test/series-strict-expected.txt result.txt
