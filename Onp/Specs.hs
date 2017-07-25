import Test.Hspec
import Onp

main = hspec $ do
        
    describe "rpn" $ do
        it "transform an infix expression into a rpn expression" $ do
            rpn "a" `shouldBe` "a"
            rpn "a+b" `shouldBe` "ab+"
            rpn "c+z" `shouldBe` "cz+"
            rpn "a-b" `shouldBe` "ab-"

        it "takes precedence into account" $ do
            rpn "a+b*c" `shouldBe` "abc*+"
            rpn "a*b+c" `shouldBe` "ab*c+"
            rpn "a^b*c+d" `shouldBe` "ab^c*d+"

        it "takes parentheses into account" $ do
            rpn "(a+b)" `shouldBe` "ab+"
            rpn "(a)"  `shouldBe` "a"
            rpn "((a+b))" `shouldBe` "ab+"
            rpn "(a+b)*c)" `shouldBe` "ab+c*"
            rpn "a*(b+c)" `shouldBe` "abc+*"

    describe "solve" $ do
        it "solve several problems" $ do
            solve ["2","a","a+b"] `shouldBe` ["a","ab+"]

    describe "process" $ do
        it "process input and show the output" $ do
            process "2\na\na+b\n" `shouldBe` "a\nab+\n"
