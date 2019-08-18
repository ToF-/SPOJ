
key :: (k, p) -> k
key (k, _) = k

prio :: (k, p) -> p
prio (_, p) = p

data PSQ k p = Void | Winner (k,p) (LTree k p) k
    deriving (Eq,Show)

data LTree k p = Start | Loser (k,p) (LTree k p) k (LTree k p)
    deriving (Eq,Show)

max_key :: PSQ k p -> k
max_key (Winner b lt m) = m 
    
play :: Ord(p) => PSQ k p -> PSQ k p -> PSQ k p
Void `play` t = t
t `play`Void = t
(Winner b t m) `play` (Winner b' t' m') 
    | prio b <= prio b' = Winner b (Loser b' t m t') m'
    | otherwise         = Winner b' (Loser b t m t') m'
main = interact process
