: /-test -1 2 / abort" This implementation of SQRT requires that dividing by two makes all numbers closer to zero. Try replacing the '2 /' in sqrt-closer with 'S>D 2 SM/REM NIP'" ; \ /-test

: sqrt-closer ( square guess -- square guess adjustment) 
    2dup / over - s>d 2 sm/rem nip ;
: sqrt ( square -- root ) 1 begin sqrt-closer dup while + repeat drop nip ;

: sqrt-test 2000000000 1 do i dup . sqrt . cr i 100 / 1+ +loop ;
