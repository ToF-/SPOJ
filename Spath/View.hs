data Min_View a = Empty | Min a [a]
    deriving (Eq,Show,Read)

min_view :: Ord(a) => [a] -> Min_View a
min_view [] = Empty
min_view (a1:as) = case min_view as of
    Empty           -> Min a1 []
    Min a2 as 
        | a1 <= a2  -> Min a1 (a2:as)
        | otherwise -> Min a2 (a1:as)

selection_sort :: Ord(a) => [a] -> [a]
selection_sort xs = case min_view xs of
    Empty    -> []
    Min a as -> a : selection_sort as
