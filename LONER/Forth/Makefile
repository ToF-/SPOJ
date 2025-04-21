unit:
	gforth-itc test/tests.fs

spoj:
	mkdir -p build
	cat src/parser.fs  >build/spoj.fs
	cat src/loner.fs  >>build/spoj.fs
	cat src/parse.fs  >>build/spoj.fs
	cat src/input.fs  >>build/spoj.fs
	cat src/process.fs >>build/spoj.fs
	cat src/main.fs   >>build/spoj.fs

test: spoj
	gforth build/spoj.fs <../test/sample.txt

generate:
	cat test/generated.fs  >test/random-test.fs
	gforth src/generate.fs -e "555 TO GAME-LENGTH 300 TO TAKES : GEN 10 0 DO .WINNING-CASE LOOP 10 0 DO .LOSING-CASE LOOP ; GEN BYE" >>test/random-test.fs
	echo "BYE" >>test/random-test.fs
	gforth test/random-test.fs
	
oracle: spoj
	mkdir -p temp
	gforth src/generate.fs -e "10 .TEST-CASES BYE" >../test/random.txt
	gforth build/spoj.fs <../test/random.txt >temp/random-forth.txt
	python3 ~/tmp/oracle.py <../test/random.txt >temp/random-oracle.txt
	diff temp/random-oracle.txt temp/random-forth.txt
	
clean:
	rm build/*
