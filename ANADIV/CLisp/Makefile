unit:
	sbcl --load ~/.sbclrc --script test/anadiv-test.lisp

spike:
	sbcl --load ~/.sbclrc --script src/spike.lisp

spoj:
	mkdir -p build
	cat src/anadiv.lisp >build/spoj.lisp
	echo "(process)" >>build/spoj.lisp

tests:
	sbcl --script build/spoj.lisp
