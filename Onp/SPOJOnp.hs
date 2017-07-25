

accept (s,st,l) c | c `elem` ['a'..'z'] = append (s,st,l) c
                  | c == '('            = (s,st,l+1)  
                  | c == ')'            = pop  (s,st,l-1)
                  | c `elem` "+-*/^"    = push (s,st,l) c
                  | otherwise           = (s,st,l)


append (s,st,l) c = (s ++ [c],st,l)

push (s,[],l) op = (s,[op],l) 
push m@(s,(op:st),l) op' | prec op > l*prec op' = push (pop m) op' 
                         | otherwise            = (s,(op':op:st),l)

prec '+' = 1
prec '-' = 1
prec '*' = 2
prec '/' = 2
prec '^' = 3

pop (s,[],l) = (s,[],l)
pop (s,('(':st),l) = (s,st,l)
pop (s,(op:st),l) = (s++[op],st,l)


popAll (s,[],l) = (s,[],l)
popAll m = popAll (pop m)

rpn = (\(s,st,l)->s) . popAll . foldl accept ("","",1) 

solve (n:exps) = map rpn exps

process = unlines . solve . lines


main = interact process
