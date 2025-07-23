unit:
	sbcl --load ~/.sbclrc --script test/anadiv-test.lisp

oracle:
	mkdir -p build
	cat src/anadiv.lisp >build/oracle.lisp
	echo "(process)" >>build/oracle.lisp

series:
	mkdir -p build
	cat src/anadiv.lisp >build/series.lisp
	echo "(series)" >>build/series.lisp

series-large: series
	sbcl --noinform --load build/series.lisp 1 100000


