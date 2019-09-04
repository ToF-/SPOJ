

    0001   3210
    0011   2100
    0110   1001

have a min_heap for couples (coords :- distance)
traverse the bitmap, adding to the heap each (i,j :- 0) where bit at i,j is white

while heap is not empty
    pop next (i,j :- d)
    for (x,y) in [(i+1,j),(i-1,j),(i,j+1),(i,j-1) | 0 <= x < max, 0 <= y < max, !visited(x,y)]
        result(x,y) = d+1
        visited(x,y) = true
 
    (0,3 :- 0) (1,2 :- 0) (1,3 :- 0) (2,1 :- 0) (2,2 :- 0)
    (1,2 :- 0) (1,3 :- 0) (2,1 :- 0) (2,2 :- 0) (0,2 :- 1) 
    (1,3 :- 0) (2,1 :- 0) (2,2 :- 0) (0,2 :- 1) (1,1 :- 1) 
    
    
    
    
    
 
