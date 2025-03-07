\ -------- record.fs --------

: SIZE-MASK ( size -- mask )
    1 SWAP LSHIFT 1- ;

: FIELD-AND-MASK ( size,offset -- mask )
    SWAP SIZE-MASK
    SWAP LSHIFT INVERT ;

: <FIELD! ( record,value,size,offset -- record' )
    2DUP FIELD-AND-MASK >R
    -ROT SIZE-MASK AND SWAP LSHIFT
    R> ROT AND OR ;

