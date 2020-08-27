spoj : Palin.hs
	tail -n +2 Palin.hs > SPOJPalin.hs
	echo "main = interact process" >> SPOJPalin.hs

test: spoj
	runhaskell SPOJPalin.hs <test.txt >result.txt
	diff expected.txt result.txt

data: maketestdata.hs Palin.hs
	runhaskell maketestdata.hs 1000 100 >test.txt

