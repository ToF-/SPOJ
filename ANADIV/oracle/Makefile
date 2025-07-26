unit:
	sbcl --load ~/.sbclrc --script test/anadiv-test.lisp

oracle:
	mkdir -p build
	cat src/anadiv.lisp >build/oracle.lisp
	echo "(process)" >>build/oracle.lisp

oracle-strict:
	mkdir -p build
	cat src/anadiv.lisp >build/oracle-strict.lisp
	echo "(process t)" >>build/oracle-strict.lisp

series:
	mkdir -p build
	cat src/anadiv.lisp >build/series.lisp
	echo "(series)" >>build/series.lisp

series-strict:
	mkdir -p build
	cat src/anadiv.lisp >build/series-strict.lisp
	echo "(series t)" >>build/series-strict.lisp

random:
	mkdir -p build
	cat src/anadiv.lisp >build/random.lisp
	echo "(random-)" >>build/random.lisp

random-strict:
	mkdir -p build
	cat src/anadiv.lisp >build/random-strict.lisp
	echo "(random- t)" >>build/random-strict.lisp

series-large: series
	sbcl --noinform --load build/series.lisp 100000 200000 >result.txt
	sed -E 's/^([0-9]+) ([0-9]+):.*/\1 \2/' result.txt > ../test/series-cases.txt
	sed -E 's/^[0-9]+ [0-9]+://' result.txt > ../test/series-expected.txt

series-large-strict: series-strict
	sbcl --noinform --load build/series-strict.lisp 100000 200000 >result.txt
	sed -E 's/^([0-9]+) ([0-9]+):.*/\1 \2/' result.txt > ../test/series-strict-cases.txt
	sed -E 's/^[0-9]+ [0-9]+://' result.txt > ../test/series-strict-expected.txt

random-large: random
	sbcl --noinform --load build/random-strict.lisp 1000 >result.txt
	sed -E 's/^([0-9]+) ([0-9]+):.*/\1 \2/' result.txt > ../test/random-cases.txt
	sed -E 's/^[0-9]+ [0-9]+://' result.txt > ../test/random-expected.txt

random-large-strict: random-strict
	sbcl --noinform --load build/random-strict.lisp 1000 >result.txt
	sed -E 's/^([0-9]+) ([0-9]+):.*/\1 \2/' result.txt > ../test/random-strict-cases.txt
	sed -E 's/^[0-9]+ [0-9]+://' result.txt > ../test/random-strict-expected.txt

tags: src/anadiv.lisp
	ctags --recurse



