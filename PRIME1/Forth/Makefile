
test : src/prime1.fs spoj
	sed -n -e 's/\(<- \)\(.*\)/\2/pw test/input.dat'     test/test.txt >/dev/null
	sed -n -e 's/\(-> \)\(.*\)/\2/pw test/temp.dat'      test/test.txt >/dev/null
	sed 's/\(.*\)/\1 /pw test/expected.dat'              test/temp.dat >/dev/null
	gforth build/spoj.fs <test/input.dat >test/result.dat
	diff -w test/expected.dat test/result.dat

unit: src/prime1.fs test/testprime1.fs
	gforth-itc test/testprime1.fs

spoj: src/prime1.fs
	cat src/input.fs src/prime1.fs src/main.fs >build/spoj.fs

clean : 
	rm *.tmp ; rm *.dat
