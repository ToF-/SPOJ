scanner: src/scanner.y
	bison -d src/scanner.y --output src/scanner.c

parser: src/parser.l
	lex -o src/parser.c  src/parser.l 

loner: scanner parser
	mkdir -p bin
	gcc -o bin/loner src/parser.c src/scanner.c
