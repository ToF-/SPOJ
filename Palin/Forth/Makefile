test : palin.fs spoj
	sed -n -e 's/\(<- \)\(.*\)/\2/pw input.dat'     test.txt >/dev/null
	sed -n -e 's/\(-> \)\(.*\)/\2/pw expected.dat'  test.txt >/dev/null
	gforth spoj.fs <input.dat >result.dat
	diff expected.dat result.dat

unit: palin.fs testpalin.fs
	gforth-itc testpalin.fs

spoj: palin.fs
	echo "MAIN BYE" >main.fs
	cat palin.fs main.fs >spoj.fs

clean : 
	rm *.tmp ; rm *.dat
