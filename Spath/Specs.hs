import Test.Hspec
import Spath

main = hspec $ do
    describe "solve" $ do
        it "solve several problems" $ do
            solve [  "1"
                    ,"4"
                    ,"gdansk" ,"2" ,"2 1" ,"3 3"
                    ,"bydgoszcz" ,"3" ,"1 1" ,"3 1" ,"4 4"
                    ,"torun" ,"3" ,"1 3" ,"2 1" ,"4 1"
                    ,"warszawa" ,"2" ,"2 4" ,"3 1"
                    ,"2"
                    ,"gdansk warszawa"
                    ,"bydgoszcz warszawa"
                    ,""]
                `shouldBe` [3,2]

    describe "process" $ do
        it "process input and show output" $ do
            process   "1\n4\ngdansk\n2\n2 1\n3 3\nbydgoszcz\n3\n1 1\n3 1\n4 4\ntorun\n3\n1 3\n2 1\n4 1\nwarszawa\n2\n2 4\n3 1\n2\ngdansk warszawa\nbydgoszcz warszawa\n"
                `shouldBe` "3\n2\n"
