-- from FingerTrees: a simple general-purpose data structure, Hinze & Paterson

data Tree a = Zero a | Succ (Tree (Node a))
    deriving (Eq,Show)

data Node a = Node2 a a | Node3 a a a
    deriving (Eq,Show)

treeExample = 
    Node2 
        (Node3 
            (Node2 't' 'h') 
            (Node2 'i' 's') 
            (Node2 'i' 's'))
        (Node3
            (Node3 'n' 'o' 't')
            (Node2 'a' 't')
            (Node3 'r' 'e' 'e'))


data FingerTree a = Empty
                  | Single a
                  | Deep (Digit a) (FingerTree (Node a)) (Digit a)
    deriving (Eq,Show)

depth :: FingerTree a -> Int
depth Empty      = 0
depth (Single _) = 1
depth (Deep pr m sf) = max 1 (max (1+depth m) 1)

data Digit a = NoDigit
             | One a
             | Two a a
             | Three a a a 
             | Four a a a a
    deriving (Eq,Show)


fingerTreeExample = 
    Deep 
    (Four 't' 'h' 'i' 's')
    (Deep 
        (Two (Node3 'i' 's' 'n') (Node3 'o' 't' 'a'))
        Empty 
        (One (Node3 't' 'r' 'e'))
    ) 
    (One 'e')

class Reduce f where
    reducer :: (a -> b -> b) -> (f a -> b -> b)
    reducel :: (b -> a -> b) -> (b -> f a -> b)

instance Reduce [] where
    reducer (<|) x z = foldr (<|) z x 
    reducel (|>) x z = foldl (|>) x z

instance Reduce Digit where
    reducer (<|) (One a) z = a <| z
    reducer (<|) (Two a b) z = a <| (b <| z)
    reducer (<|) (Three a b c) z = a <| (b <| (c <| z))
    reducer (<|) (Four a b c d) z = a <| (b <| (c <| (d <| z)))
    reducel (|>) z (One a) = z |> a
    reducel (|>) z (Two a b) = (z |> b) |> a
    reducel (|>) z (Three a b c) = ((z |> c) |> b) |> a
    reducel (|>) z (Four a b c d) = (((z |> d) |> c) |> b) |> a

digitToList :: Digit a -> [a]
digitToList (One a)        = [a]
digitToList (Two a b)      = [a,b]
digitToList (Three a b c)  = [a,b,c]
digitToList (Four a b c d) = [a,b,c,d]

listToDigit :: [a] -> Digit a
listToDigit [a]       = One a
listToDigit [a,b]     = Two a b 
listToDigit [a,b,c]   = Three a b c
listToDigit [a,b,c,d] = Four a b d c


toList :: (Reduce f) => f a -> [a]
toList s = s |: [] where (|:) = reducer (:)

instance Reduce Node where
    reducer (<|) (Node2 a b) z   = a <| (b <| z)
    reducer (<|) (Node3 a b c) z = a <| (b <| (c <| z))
    reducel (|>) z (Node2 b a)   = (z |> b) |> a
    reducel (|>) z (Node3 c b a) = ((z |> c) |> b) |> a

instance Reduce FingerTree where
    reducer (<|) Empty          z = z
    reducer (<|) (Single x)     z = x <| z
    reducer (<|) (Deep pr m sf) z = pr <|| (m <||| (sf <|| z))
        where
        (<||)  = reducer (<|)
        (<|||) = reducer (reducer (<|))
    reducel (|>) z Empty          = z
    reducel (|>) z (Single x)     = z |> x
    reducel (|>) z (Deep pr m sf) = ((z ||> pr) |||> m) ||> sf 
        where
        (||>)  = reducel (|>)
        (|||>) = reducel (reducel (|>))

infixr 5 <|
(<|) :: a -> FingerTree a -> FingerTree a
a <| Empty               = Single a
a <| Single b            = Deep (One a) Empty (One b)
a <| Deep (Four b c d e) m sf = Deep (Two a b) (Node3 c d e <| m) sf
a <| Deep (One b) m sf        = Deep (Two a b) m sf
a <| Deep (Two b c) m sf      = Deep (Three a b c) m sf
a <| Deep (Three b c d ) m sf = Deep (Four a b c d) m sf


infixl 5 |>
(|>) :: FingerTree a -> a -> FingerTree a
Empty               |> a = Single a
Single b            |> a = Deep (One b) Empty (One a)
Deep pr m (Four e d c b) |> a = Deep pr (m |> Node3 e d c) (Two b a)
Deep pr m (One b)        |> a = Deep pr m (Two b a)
Deep pr m (Two c b)      |> a = Deep pr m (Three c b a)
Deep pr m (Three d c b)  |> a = Deep pr m (Four d c b a)


(<||) :: (Reduce f) => f a -> FingerTree a -> FingerTree a
(<||) = reducer (<|)

(||>) :: (Reduce f) => FingerTree a -> f a -> FingerTree a
(||>) = reducel (|>)

toTree :: (Reduce f) => f a -> FingerTree a
toTree s = s <|| Empty

data ViewL s a = NilL | ConsL a (s a)
    deriving (Eq,Show)

viewL :: FingerTree a -> ViewL FingerTree a
viewL Empty          = NilL
viewL (Single x)     = ConsL x Empty
viewL (Deep (One a) m sf) = ConsL a (deepL NoDigit m sf)
viewL (Deep (Two a b) m sf) = ConsL a (deepL (One b) m sf)
viewL (Deep (Three a b c) m sf) = ConsL a (deepL (Two b c) m sf)
viewL (Deep (Four a b c d) m sf) = ConsL a (deepL (Three b c d) m sf)

deepL :: Digit a -> FingerTree (Node a) -> Digit a -> FingerTree a
deepL NoDigit m sf = case viewL m of
    NilL       -> toTree sf
    ConsL a m' -> Deep (toDigit a) m' sf
deepL pr m sf = Deep pr m sf

toDigit :: Node a -> Digit a
toDigit (Node2 a b) = Two a b
toDigit (Node3 a b c) = Three a b c

isEmpty :: FingerTree a -> Bool
isEmpty x = case viewL x of 
    NilL      -> True
    ConsL _ _ -> False
                           
headL :: FingerTree a -> a 
headL x = case viewL x of
    ConsL a _ -> a

tailL :: FingerTree a -> FingerTree a
tailL x = case viewL x of
    ConsL _ x' -> x'

app3 :: FingerTree a -> [a] -> FingerTree a -> FingerTree a
app3 Empty ts xs = ts <|| xs
app3 xs ts Empty = xs ||> ts
app3 (Single x) ts xs = x <| (ts <|| xs)
app3 xs ts (Single x) = (xs ||> ts) |> x
app3 (Deep pr1 m1 sf1) ts (Deep pr2 m2 sf2) =
    Deep pr1 (app3 m1 (nodes ((digitToList sf1) ++ ts ++ (digitToList pr2))) m2) sf2

nodes :: [a] -> [Node a]
nodes [a,b]      = [Node2 a b]
nodes [a,b,c]    = [Node3 a b c]
nodes [a,b,c,d]  = [Node2 a b, Node2 c d]
nodes (a:b:c:xs) = Node3 a b c : nodes xs

(><) :: FingerTree a -> FingerTree a -> FingerTree a
xs >< ys = app3 xs [] ys
