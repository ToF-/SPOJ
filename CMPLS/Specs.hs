import Test.Hspec
import CMPLS

main = hspec $ do
    describe "complete" $ do
        it "completes a sequence of natural numbers" $ do
            complete 2 [1,2,4,7,11,16,22,29] `shouldBe` [37,46]
            complete 2 (replicate 9 1 ++ [2]) `shouldBe` [11,56]

    describe "compute" $ do
        it "compute a list of list of integers" $ do
            compute [[1],[8,2],[1,2,4,7,11,16,22,29]] `shouldBe` [[37,46]]
            compute [[2]
                    ,[8,2]
                    ,[1,2,4,7,11,16,22,29]
                    ,[10,2]
                    ,[1,1,1,1,1,1,1,1,1,2]] `shouldBe` [[37,46]
                                                       ,[11,56]]

    describe "process" $ do
        it "processes a string into another string" $ do
            process "2\n8 2\n1 2 4 7 11 16 22 29\n10 2\n1 1 1 1 1 1 1 1 1 2\n"
             `shouldBe` "37 46\n11 56\n"
