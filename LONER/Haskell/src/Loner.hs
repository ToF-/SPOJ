module Loner (loner) where

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
                  (False, _) -> (True, s)
                  (True, t) -> many parser t

some :: Parser -> Parser
some parser s = case parser s of
                  (True, t) -> many parser t
                  r -> r
p :: Parser
p = parseChar '1'

e :: Parser
e = parseChar '0'


a_patterns :: Parser
a_patterns = es <&> p <&> es <&> eos

b_patterns :: Parser
b_patterns = es <&> pp <&> e <&> es <&> eos

ep :: Parser
ep = e <&> p

pe :: Parser
pe = p <&> e

pp :: Parser
pp = p <&> p

ee :: Parser
ee = e <&> e

es :: Parser
es = many e

c_patterns :: Parser
c_patterns = c1 <|> c2 <|> c3

c1 :: Parser
c1 = es <&> pp <&> some ep <&> es <&> eos

c2 :: Parser
c2 = es <&> some pp <&> ep <&> es <&> eos

c3 :: Parser
c3 = es <&> pp <&> some ep <&> some pp <&> ep <&> es <&> eos

d_patterns :: Parser
d_patterns = d0 <|> d1 <|> d2 <|> d3 <|> d4 <|> d5 <|> d6 <|> d7

d0 :: Parser
d0 = es <&> pp <&> ee <&> pp <&> es <&> eos

d1 :: Parser
d1 = es <&> pp <&> some ep <&> ee <&> pp <&> es <&> eos

d2 :: Parser
d2 = es <&> pp <&> ee <&> some pp <&> es <&> eos

d3 :: Parser
d3 = es <&> pp <&> some ep <&> ee <&> some pp <&> es <&> eos

d4 :: Parser
d4 = es <&> pp <&> ee <&> some pe <&> pp <&> es <&> eos

d5 :: Parser
d5 = es <&> pp <&> some ep <&> ee <&> some pe <&> pp <&> es <&> eos

d6 :: Parser
d6 = es <&> pp <&> ee <&> some pp <&> some pe <&> pp <&> es <&> eos

d7 :: Parser
d7 = es <&> pp <&> some ep <&> ee <&> some pp <&> some pe <&> pp <&> es <&> eos

e_patterns :: Parser
e_patterns = e0 <|> e1 <|> e2 <|> e3 <|> e4 <|> e5 <|> e6 <|> e7

e0 :: Parser
e0 = es <&> pp <&> pp <&> ep <&> pp <&> es <&> eos

e1 :: Parser
e1 = es <&> pp <&> some ep <&> pp <&> ep <&> pp <&> es <&> eos

e2 :: Parser
e2 = es <&> pp <&> pp <&> ep <&> some pp <&> es <&> eos

e3 :: Parser
e3 = es <&> pp <&> some ep <&> pp <&> ep <&> some pp <&> es <&> eos

e4 :: Parser
e4 = es <&> pp <&> pp <&> ep <&> some pe <&> pp <&> es <&> eos

e5 :: Parser
e5 = es <&> pp <&> some ep <&> pp <&> ep <&> some pe <&> pp <&> es <&> eos

e6 :: Parser
e6 = es <&> pp <&> pp <&> ep <&> some pp <&> some pe <&> pp <&> es <&> eos

e7 :: Parser
e7 = es <&> pp <&> some ep <&> pp <&> ep <&> some pp <&> some pe <&> pp <&> es <&> eos

all_patterns :: Parser
all_patterns = a_patterns <|> b_patterns <|> c_patterns <|> d_patterns <|> e_patterns

loner :: String -> Bool
loner s = fst (all_patterns s) || fst (all_patterns (reverse s))

