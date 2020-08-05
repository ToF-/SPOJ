spoj: Cyatq.fs
	cat Cyatq.fs Launch.fs >Spoj.fs


test : gss1.fs tests.txt
	sed -n -e 's/\(<- \)\(.*\)/\2/pw input.dat'     tests.txt >/dev/null
	sed -n -e 's/\(-> \)\(.*\)/\2/pw expected.dat' tests.txt >/dev/null
	gforth gss1.fs <input.dat >result.dat
	diff expected.dat result.dat
