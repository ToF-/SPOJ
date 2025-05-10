module LonerSpec (spec)
    where

import Test.Hspec
import Loner

spec :: SpecWith ()
spec = do
    describe "loner" $ do
        it "evaluate trivial cases" $ do
            loner "1" `shouldBe` True
            loner "0" `shouldBe` False
            loner ""  `shouldBe` False

        it "evaluate simple cases" $ do
            loner "10" `shouldBe` True
            loner "11" `shouldBe` False
            loner "100" `shouldBe` True
            loner "101" `shouldBe` False
            loner "001" `shouldBe` True

        it "evaluate 3 sized boards" $ do
            loner "110" `shouldBe` True
            loner "111" `shouldBe` False 
            loner "0000110000" `shouldBe` True

        it "evaluate complex C boards" $ do
            loner "1101" `shouldBe` True 
            loner "110101" `shouldBe` True
            loner "111101" `shouldBe` True 
            loner "110101111101" `shouldBe` True
