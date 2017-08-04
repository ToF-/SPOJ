module PSQueue where

data LTree k p = Start
               | Loser (k,p) (LTree k p) k (LTree k p) 
    deriving (Eq,Show)

prio (k,p) = p
key  (k,p) = k

empty :: PSQ k p
empty = Void

singleton :: (k,p) -> PSQ k p
singleton b = Winner b Start (key b)

max_key :: PSQ k p -> k
max_key (Winner b t m) = m

match :: Ord(p) => PSQ k p -> PSQ k p -> PSQ k p
Void `match` q = q
q `match` Void = q
(Winner b t m) `match` (Winner b' t' m')
    | prio b <= prio b' = Winner b (Loser b' t m t') m'
    | otherwise         = Winner b'(Loser b  t m t') m'

from_ord_list :: Ord(p) => Ord(k) => [(k,p)] -> PSQ k p
from_ord_list = foldl match empty . map singleton

del_min :: Ord(k) => Ord(p) => PSQ k p -> PSQ k p
del_min Void = Void
del_min (Winner b Start m) = Void
del_min (Winner b (Loser b' t k u) m) 
    | key b' <= k = Winner b' t k `match` del_min (Winner b u m)
    | otherwise   = del_min (Winner b t k) `match` Winner b' u m

data Min_View k p = Empty | Min (k,p) (PSQ k p)
    deriving (Eq,Show)

min_view :: Ord(k) => Ord(p) => PSQ k p -> Min_View k p  
min_view Void = Empty
min_view (Winner b t m) = Min b (second_best t m)

second_best :: Ord(k) => Ord(p) => LTree k p -> k -> PSQ k p
second_best Start m = Void
second_best (Loser b t k u) m
    | key b <= k = Winner b t k    `match` second_best u m
    | otherwise  = second_best t k `match` Winner b u m 

data PSQ k p = Void
             | Winner (k,p) (LTree k p) k
    deriving (Eq,Show)

data Tour_View k p = Null | Singleton (k,p) | PSQ k p `Match` PSQ k p
    deriving (Eq, Show)

tour_view :: Ord(k) => Ord(p) => PSQ k p -> Tour_View k p
tour_view Void               = Null
tour_view (Winner b Start m) = Singleton b 
tour_view (Winner b (Loser b' tl k tr) m)
    | key b' <= k = (Winner b' tl k) `Match` (Winner b tr m)
    | otherwise   = (Winner b  tl k) `Match` (Winner b' tr m)

to_ord_list :: Ord(k) => Ord(p) => PSQ k p -> [(k,p)]
to_ord_list q = case tour_view q of
    Null -> []
    Singleton b -> [b]
    tl `Match` tr -> to_ord_list tl ++ to_ord_list tr

adjust :: Ord(k) => Ord(p) => (p -> p) -> k -> PSQ k p -> PSQ k p
adjust f k q = case tour_view q of
    Null -> Void
    Singleton b 
        | k == (key b) -> Winner (k, f (prio b)) Start k 
        | otherwise -> Winner b Start k
    tl `Match` tr
        | k <= max_key tl -> (adjust f k tl) `match` tr
        | otherwise       -> tl `match` (adjust f k tr)

