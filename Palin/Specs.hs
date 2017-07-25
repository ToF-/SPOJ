import Test.Hspec
import Palin

main = hspec $ do
    describe "solve" $ do
        it "solve several problems" $ do
            solve ["2","11","141"] `shouldBe` ["22","151"]

    describe "process" $ do
        it "processes input and show output" $ do
            process "2\n11\n141\n" `shouldBe` "22\n151\n"

    describe "palin" $ do
        let check a b = palindrome a `shouldBe` b 
        describe "finds the smallest next palindrome" $ do
            it "with 1 digit numbers" $ do
                check "1" "2"
                check "9" "11"
            it "with 2 digits numbers" $ do
                check "10" "11"
                check "42" "44"
                check "24" "33"
                check "19" "22"
                check "1991" "2002"
                check "99" "101"
            it "with 3 digits numbers" $ do
                check "321" "323"
                check "123" "131"
                check "999" "1001"
                check "231" "232"
                check "132" "141"
            it "with 5 digits numbers" $ do
                check "12345" "12421"
            
