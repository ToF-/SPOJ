spoj : Spath.hs
	tail -n +2 Spath.hs > SPOJSpath.hs
	echo "main = interact process" >> SPOJSpath.hs

test: spoj
	runhaskell SPOJSpath.hs <test.txt >result.txt
	diff expected.txt result.txt
