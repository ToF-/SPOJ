module Loner (loner)
    where

type Parser = String -> (Bool, String)

parseChar :: Char -> Parser
parseChar c (x:xs) | c == x = (True, xs)
parseChar _ xs     = (False,xs)

eos :: Parser
eos "" = (True,"")
eos s  = (False, s)

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
                  (False, t) -> (True, t)
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

c0 :: Parser
c0 = many e <&> pp <&> ep  <&> many e <&> eos

c1 :: Parser
c1 = many e <&> pp <&> some ep <&> many e <&> eos

c2 :: Parser
c2 = many e <&> some pp <&> ep <&> many e <&> eos

c3 :: Parser
c3 = many e <&> pp <&> some ep <&> some pp <&> ep <&> many e <&> eos

loner :: String -> Bool
loner = fst . (a <|> b <|> c0 <|> c1 <|> c2 <|> c3)

