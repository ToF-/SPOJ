jednakos: jednakos.c
	cc jednakos.c -o jednakos

clean:
	rm jednakos

test: jednakos.c test.txt
	sed -n -e 's/\(<- \)\(.*\)/\2/pw input.dat'    test.txt >/dev/null
	sed -n -e 's/\(-> \)\(.*\)/\2/pw expected.dat' test.txt >/dev/null
	xargs ./jednakos <input.dat >result.dat
	diff expected.dat result.dat
