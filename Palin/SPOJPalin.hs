import Data.Char

solve (s:ss) = map palindrome ss

process = unlines . solve . lines

palindrome "9" = "11"
palindrome [c] = [succ c]
palindrome s = p
    where
    size = length s `div` 2
    left  = take size s
    middle = drop size (reverse (drop size s)) 
    right = drop (length s - size) s
    rleft = reverse left
    p = case rleft > right of
        True -> left++middle++rleft
        False -> take size (reverse i) ++ i
                where 
                    i = add1 (middle++rleft)

add1 "9" = "01"
add1 ('9':cs) = '0':add1 cs
add1 (c:cs)   = succ c : cs
 
main = interact process
