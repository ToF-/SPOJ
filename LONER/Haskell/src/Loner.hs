module Loner (loner)
    where

type Parser = String -> (Bool, String)

parseChar :: Char -> Parser
parseChar c (x:xs) | c == x = (True, xs)
parseChar _ xs     = (False,xs)

eos :: Parser
eos "" = (True,"")
eos s  = (False, s)

infixl 7 <&>
infixl 6 <|>

(<&>) :: Parser -> Parser -> Parser
(parserA <&> parserB) s =
        case parserA s of
            (True, t) -> parserB t
            r -> r


(<|>) :: Parser -> Parser -> Parser
(parserA <|> parserB) s =
        case parserA s of
            (False,_) -> parserB s
            r -> r

many :: Parser -> Parser
many parser s = case parser s of
                  (False, t) -> (True, s)
                  (True, t) -> many parser t

some :: Parser -> Parser
some parser s = case parser s of
                  (True, t) -> many parser t
                  r -> r
p :: Parser
p = parseChar '1'

e :: Parser
e = parseChar '0'

a :: Parser
a = many e <&> p <&> many e <&> eos

b :: Parser
b = many e <&> p <&> p <&> e <&> many e <&> eos

ep :: Parser
ep = e <&> p

pp :: Parser
pp = p <&> p

ee :: Parser
ee = e <&> e

c0 :: Parser
c0 = many e <&> pp <&> ep  <&> many e <&> eos

c1 :: Parser
c1 = many e <&> pp <&> some ep <&> many e <&> eos

c2 :: Parser
c2 = many e <&> some pp <&> ep <&> many e <&> eos

c3 :: Parser
c3 = many e <&> pp <&> some ep <&> some pp <&> ep <&> many e <&> eos

d0 :: Parser
d0 = many e <&> pp <&> ee <&> pp <&> many e <&> eos

d1 :: Parser
d1 = many e <&> pp <&> some ep <&>  ee <&> pp <&> many e <&> eos

d2 :: Parser
d2 = many e <&> pp <&> ee <&> some pp <&> many e <&> eos

d3 :: Parser
d3 = many e <&> pp <&> many ep <&> ee <&> some pp <&> many e <&> eos

loner :: String -> Bool
loner = fst . (a <|> b <|> c0 <|> c1 <|> c2 <|> c3 <|> d0 <|> d1 <|> d2 <|> d3)

