
unit: src/Loner.hs test/LonerSpec.hs
	stack test

spoj: src/Loner.hs
	mkdir -p build
	tail -n +2 src/Loner.hs >build/SPOJ.hs
	tail -n +3 app/Main.hs >>build/SPOJ.hs

test: src/Loner.hs
	stack run <../test/sample.txt

