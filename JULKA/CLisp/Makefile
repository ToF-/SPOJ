unit:
	sbcl --load ~/.sbclrc --script test/julka-test.lisp

spoj:
	mkdir -p build
	cat src/julka.lisp >build/spoj.lisp
	echo "(process)" >>build/spoj.lisp

tests:
	sbcl --script build/spoj.lisp
