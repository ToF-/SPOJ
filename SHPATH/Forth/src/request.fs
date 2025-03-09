\ -------- request.fs --------

100 CONSTANT MAX-REQUEST
CREATE REQUESTS
    0 ,
    MAX-REQUEST CELLS 2* ALLOT

: REQUEST^ ( n -- addr )
    CELLS 2* REQUESTS + CELL+ ;

: REQUEST# ( n -- vertex^,vertex^ )
    REQUEST^ 2@ ;

: ADD-REQUEST ( vertex^,vertex^ -- )
    REQUESTS @ REQUEST^ 2!
    1 REQUESTS +! ;
