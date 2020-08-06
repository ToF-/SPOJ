spoj: Cyatq.fs
	cat Cyatq.fs Launch.fs >Spoj.fs


test : gss1.fs data/tests.txt
	sed -n -e 's/\(<- \)\(.*\)/\2/pw input.dat'     data/tests.txt >/dev/null
	sed -n -e 's/\(-> \)\(.*\)/\2/pw expected.dat' data/tests.txt >/dev/null
	gforth gss1.fs <input.dat >result.dat
	diff expected.dat result.dat

random : gss1.fs
	../Haskell/maketestdata 50000 300 >data/random.txt
	gforth echo.fs <data/random.txt >data/temp.txt
	sed -n -e 's/\(<- \)\(.*\)/\2/pw input.dat'     data/temp.txt >/dev/null
	sed -n -e 's/\(-> \)\(.*\)/\2/pw expected.dat' data/temp.txt >/dev/null
	gforth gss1.fs <input.dat >result.dat
	diff expected.dat result.dat

clean :
	rm *.dat
