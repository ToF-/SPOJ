CLEANUP = rm -f

C_COMPILER=gcc
ifeq ($(shell uname -s), Darwin)
C_COMPILER=clang
endif

UNITY_ROOT=unity

TEST_TARGET=bin/run_test

APP_TARGET=bin/spoj

TEST_FILES=test/*.c test/runner/*.c

APP_FILES=src/shpath.c

SPOJ_FILES=build/spoj.c

UNITY_FILES=$(UNITY_ROOT)/src/unity.c $(UNITY_ROOT)/extras/memory/src/unity_memory.c $(UNITY_ROOT)/extras/fixture/src/unity_fixture.c  $(TEST_FILES)

INC_DIRS=-Isrc -I$(UNITY_ROOT)/src -I$(UNITY_ROOT)/extras/fixture/src -I$(UNITY_ROOT)/extras/memory/src

unit:
	mkdir -p test/runner
	./gen_test_runner.awk test/shpath_tests.c >test/runner/test_runner.c
	./gen_all_tests.awk test/shpath_tests.c >test/runner/all_tests.c
	$(C_COMPILER) $(CFLAGS) $(INC_DIRS) $(SYMBOLS) $(APP_FILES) $(UNITY_FILES) -o $(TEST_TARGET)
	- ./$(TEST_TARGET)

spoj: src/shpath.c src/shpath.h
	mkdir -p build
	mkdir -p bin
	cat src/shpath.h         >build/SPOJ.c
	tail -n +2 src/shpath.c >>build/SPOJ.c
	cat src/main.c          >>build/SPOJ.c
	$(C_COMPILER) $(CFLAGS) $(INC_DIRS) $(SYMBOLS) $(SPOJ_FILES) -o bin/spoj

tests: spoj
	bin/spoj


clean:
	rm bin/*

