unit:
	gforth-itc test/tests.fs

spoj:
	mkdir -p build
	cat src/bigint.fs  >build/spoj.fs
	cat src/input.fs  >>build/spoj.fs
	cat src/julka.fs  >>build/spoj.fs
	echo "STDIN INPUT-FILE ! PROCESS BYE" >>build/spoj.fs

clean:
	rm build/*
