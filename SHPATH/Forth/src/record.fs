\ -------- record.fs --------

: SIZE-MASK ( size -- mask )
    1 SWAP LSHIFT 1- ;

: FIELD-MASK ( offset,size -- mask )
    SIZE-MASK SWAP LSHIFT INVERT ;

: FIELD-OFFSET ( value,offset,size -- value' )
    SIZE-MASK ROT AND SWAP LSHIFT ;

: <FIELD! ( record,value,offset,size -- record' )
    2DUP FIELD-MASK >R
    FIELD-OFFSET R>
    ROT AND OR ;

: >FIELD@ ( record,offset,size -- value )
    SIZE-MASK OVER LSHIFT
    ROT AND SWAP RSHIFT ;
