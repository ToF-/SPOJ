unit:
	gforth-itc test/tests.fs

spoj:
	mkdir -p build
	cat src/heap.fs            >build/spoj.fs
	cat src/record.fs          >>build/spoj.fs
	cat src/linked-list.fs     >>build/spoj.fs
	cat src/vertex.fs          >>build/spoj.fs
	cat src/hash-table.fs      >>build/spoj.fs
	cat src/priority-queue.fs  >>build/spoj.fs
	cat src/graph.fs           >>build/spoj.fs
	cat src/parse.fs           >>build/spoj.fs
	cat src/request.fs         >>build/spoj.fs
	cat src/input.fs           >>build/spoj.fs
	cat src/process.fs         >>build/spoj.fs


spike:
	gforth-itc src/spike.fs

clean:
	rm build/*
