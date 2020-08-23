
test : prime1.fs spoj
	sed -n -e 's/\(<- \)\(.*\)/\2/pw input.dat'     test.txt >/dev/null
	sed -n -e 's/\(-> \)\(.*\)/\2/pw temp.dat'      test.txt >/dev/null
	sed 's/\(.*\)/\1 /pw expected.dat'              temp.dat >/dev/null
	gforth spoj.fs <input.dat >result.dat
	diff expected.dat result.dat

unit: prime1.fs testprime1.fs
	gforth-itc testprime1.fs

spoj: prime1.fs
	echo "MAIN BYE" >main.fs
	cat prime1.fs main.fs >spoj.fs

clean : 
	rm *.tmp ; rm *.dat
