unit:
	sbcl --load ~/.sbclrc --script test/crypto1-test.lisp

spike:
	sbcl --load ~/.sbclrc --script src/spike.lisp

spoj:
	mkdir -p build
	cat src/crypto1.lisp >build/spoj.lisp
	echo "(process)" >>build/spoj.lisp

tests:
	sbcl --script build/spoj.lisp
	
timestamp:
	sbcl --script spike/timestamp.lisp
	cc spike/timestamp.c -o build/timestamp &&  build/timestamp
