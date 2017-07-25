spoj : DivSum.hs
	tail -n +2 DivSum.hs > SPOJDivSum.hs
	echo "main = interact process" >> SPOJDivSum.hs

test: spoj
	runhaskell SPOJDivSum.hs <test.txt >result.txt
	diff expected.txt result.txt

time: spoj
	time runhaskell SPOJDivSum.hs <test.txt >result.txt
	
