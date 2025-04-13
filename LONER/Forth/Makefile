unit:
	gforth-itc test/tests.fs

spoj:
	mkdir -p build
	cat src/parser.fs  >build/spoj.fs
	cat src/loner.fs  >>build/spoj.fs
	cat src/parse.fs  >>build/spoj.fs
	cat src/input.fs  >>build/spoj.fs
	cat src/process.fs >>build/spoj.fs
	cat src/main.fs   >>build/spoj.fs

spike:

clean:
	rm build/*
