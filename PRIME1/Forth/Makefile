
test : prime1.fs
	sed -n -e 's/\(<- \)\(.*\)/\2/pw input.dat'     test.txt >/dev/null
	sed -n -e 's/\(-> \)\(.*\)/\2/pw expected.dat' test.txt >/dev/null
	gforth prime1.fs <input.dat >result.dat
	diff expected.dat result.dat

unit: prime1.fs testprime1.fs
	gforth-itc testprime1.fs

spoj: prime1.fs
	echo "MAIN BYE" >main.fs
	cat prime1.fs main.fs >spoj.fs

clean : 
	rm *.dat
