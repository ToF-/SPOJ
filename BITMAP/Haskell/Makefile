spoj : Bitmap.hs
	tail -n +2 Bitmap.hs > SPOJBitmap.hs
	echo "main = interact process" >> SPOJBitmap.hs

test: spoj
	runhaskell SPOJBitmap.hs <test.txt >result.txt
	diff expected.txt result.txt
