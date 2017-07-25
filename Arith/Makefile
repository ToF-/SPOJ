spoj : Arith.hs
	tail -n +2 Arith.hs > SPOJArith.hs
	echo "main = interact process" >> SPOJArith.hs

test: spoj
	runhaskell SPOJArith.hs <test.txt >result.txt
	diff expected.txt result.txt
