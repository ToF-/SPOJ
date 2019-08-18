module PSQ where

data PSQ k p = Void
             | Winner (k,p) (LTree k p) k
    deriving (Eq, Show)

data LTree k p = Start
               | Loser (k,p) (LTree k p) k (LTree k p)
    deriving (Eq, Show)
               

key :: (k,p) -> k
key (k,p) = k

empty :: PSQ k p
empty = Void

singleton :: (k,p) -> PSQ k p
singleton b = Winner b Start (key b)

size :: PSQ k p -> Int
size Void = 0
size (Winner _ Start _) = 1

infixl 5 ><

(><) :: PSQ k p -> PSQ k p -> PSQ k p
Void >< t = t
t >< Void = t

