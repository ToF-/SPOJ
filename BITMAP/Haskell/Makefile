spoj : Bitmap.hs
	tail -n +2 Bitmap.hs > SPOJBitmap.hs
	echo "main = interact process" >> SPOJBitmap.hs

test: spoj
	runhaskell SPOJBitmap.hs <test.txt >result.txt
	diff expected.txt result.txt

profile: spoj
	ghc --make -prof -fprof-auto -rtsopts SPOJBitmap.hs -o bin/spoj
	bin/spoj +RTS -p <medium.txt
	cat spoj.prof
	cat spoj.prof
