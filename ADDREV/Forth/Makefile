spoj : addrev.fs
	cat addrev.fs >spoj.fs
	echo "\nMAIN BYE\n" >>spoj.fs

test : spoj
	sed -n -e 's/\(<- \)\(.*\)/\2/pw input.dat'     test.txt >/dev/null
	sed -n -e 's/\(-> \)\(.*\)/\2/pw expected.dat' test.txt >/dev/null
	gforth spoj.fs <input.dat >result.dat
	diff expected.dat result.dat

unit : addrev.fs tests.fs
	gforth-itc tests.fs

clean : 
	rm *.dat
