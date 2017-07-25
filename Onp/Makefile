spoj : Onp.hs
	tail -n +2 Onp.hs > SPOJOnp.hs
	echo "main = interact process" >> SPOJOnp.hs

test: spoj
	runhaskell SPOJOnp.hs <test.txt >result.txt
	diff expected.txt result.txt
