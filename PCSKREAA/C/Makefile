CLEANUP = rm -f

C_COMPILER=gcc
ifeq ($(shell uname -s), Darwin)
C_COMPILER=clang
endif

UNITY_ROOT=unity

TEST_TARGET=bin/run_test

APP_TARGET=bin/spoj

TEST_FILES=test/*.c test/runner/*.c

APP_FILES=src/pcskreaa.c

SPOJ_FILES=build/spoj.c
SPOJ_STRICT_FILES=build/spoj-strict.c

UNITY_FILES=$(UNITY_ROOT)/src/unity.c $(UNITY_ROOT)/extras/memory/src/unity_memory.c $(UNITY_ROOT)/extras/fixture/src/unity_fixture.c  $(TEST_FILES)

INC_DIRS=-Isrc -I$(UNITY_ROOT)/src -I$(UNITY_ROOT)/extras/fixture/src -I$(UNITY_ROOT)/extras/memory/src

unit:
	mkdir -p test/runner
	mkdir -p bin
	./gen_test_runner.awk test/pcskreaa_tests.c >test/runner/test_runner.c
	./gen_all_tests.awk test/pcskreaa_tests.c >test/runner/all_tests.c
	$(C_COMPILER) $(CFLAGS) $(INC_DIRS) $(SYMBOLS) $(APP_FILES) $(UNITY_FILES) -o $(TEST_TARGET)
	- ./$(TEST_TARGET)

spoj: src/pcskreaa.c src/pcskreaa.h
	mkdir -p build
	mkdir -p bin
	cat src/pcskreaa.h         >build/spoj.c
	tail -n +2 src/pcskreaa.c >>build/spoj.c
	echo "int main(int argc, char* argv[]) { process(); return 0; }" >>build/spoj.c
	$(C_COMPILER) $(CFLAGS) $(INC_DIRS) $(SYMBOLS) $(SPOJ_FILES) -o bin/spoj

tests: spoj
	bin/spoj <../test/sample.txt >result.txt
	diff ../test/expected.txt result.txt


spike: src/spike.c
	$(C_COMPILER) $(CFLAGS) $(INC_DIRS) $(SYMBOLS) src/spike.c -o bin/spike
	bin/spike

clean:
	rm build/
	rm bin/*

tags: src/pcskreaa.c src/pcskreaa.h test/pcskreaa_tests.c
	`brew --prefix`/bin/ctags --recurse

