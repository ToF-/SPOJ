module LonerSpec
    where

import Test.Hspec
import Loner

spec :: SpecWith ()
spec = do
    describe "Loner" $ do
        it "wins if the board has only one pawn" $ do
            loner "001" `shouldBe` True
            loner "000010000" `shouldBe` True

        it "looses if the board has isolated pawns" $ do
            loner "10001" `shouldBe` False

        it "can transform sequences of 2 pawns followed by a free cell" $ do
            loner "110" `shouldBe` True
            loner "11000" `shouldBe` True

        it "can transform sequences of a free cell and 2 pawns" $ do
            loner "011" `shouldBe` True
            loner "00011" `shouldBe` True
    
        it "can transform sequences until finding a win or a fail" $ do
            loner "000011000" `shouldBe` True
            loner "000110011" `shouldBe` True
