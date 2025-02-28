
- generate a 10000 vertice strongly connected graph
- create a N-cycle:
    - for i in 1…10000:
        j = i + 1 
        if j > 10000 
            j = 1
        c = random
        add edge i,j,c
        add edge j,i,c
- add some random edges

- use a bitset with edges to check for uniqueness of edges, and then to list all edges sorted by start node
    

declare arrays and all other structures on the heap memory

shorten words for heap memory use 
    ALLOT → H-ALLOT
    HERE → H-HERE
    , → H-,
    C, → H-C,
    CREATE → H-CREATE


constructs

- array : to map an integer index value to a record

    ARRAY
    ARRAY-MAX
    ARRAY-CAPACITY
    ARRAY-ITEM-SIZE
    ARRAY-NEXT
    ARRAY-ITEM

- heap allocation : to use an indefinite amount of memory

    HEAP-ALLOCATE
    HEAP-FREE
    HEAP-HERE
    HEAP-ALLOT
    HEAP,
    HEAPC,

- linked list : to represent sequences of records
    
    LINK-ADD
    LINK-NEXT

- hash table : to map efficiently any key value to a record

    HASH-INIT
    HASH-KEY
    HASH-INSERT
    HASH-FIND

- prioriy queue : to efficiently extract extremum from a list

    QUEUE-INIT
    QUEUE-COUNT
    QUEUE-INSERT
    QUEUE-EXTRACT

- bitset : to map an integer value to boolean

    BITSET-INIT
    BITSET-INCLUDE?
    BITSET-INSERT

- inputs/outputs : to manage input and output of data
    GET-LINE
    GET-NUMBER
    GET-NUMBERS

- dijkstra : to efficiently search for minimal path in a graph
    
    GRAPH-INIT
    GRAPH-ADD-NODE
    GRAPH-ADD-EDGE
    GRAPH-NODE
    GRAPH-PATH-WEIGHT




