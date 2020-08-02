addrev: addrev.c
	cc addrev.c -o addrev
test : addrev
	sed -n -e 's/\(<- \)\(.*\)/\2/pw input.dat'     test.txt >/dev/null
	sed -n -e 's/\(-> \)\(.*\)/\2/pw expected.dat' test.txt >/dev/null
	./addrev <input.dat >result.dat
	diff expected.dat result.dat

clean : 
	rm *.dat; rm addrev
