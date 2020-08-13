unit: testjednakos.fs jednakos.fs
	gforth-itc -r 32k testjednakos.fs

test: spoj test.txt jednakos.fs
	sed -n -e 's/\(<- \)\(.*\)/\2/pw input.dat'    test.txt >/dev/null
	sed -n -e 's/\(-> \)\(.*\)/\2/pw expected.dat' test.txt >/dev/null
	gforth spoj.fs <input.dat >output.dat
	diff expected.dat output.dat

spoj: jednakos.fs
	echo "MAIN BYE" >main.fs
	cat jednakos.fs main.fs >spoj.fs

clean: 
	rm *.dat
