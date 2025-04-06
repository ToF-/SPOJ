module LonerSpec
    where

import Test.Hspec
import Loner

spec :: SpecWith ()
spec = do
    describe "capture" $ do 
        it "changes a board by removing a pawn and moving another" $ do
            capture "110" `shouldBe` Just "001"
            capture "011" `shouldBe` Just "100"
        it "is only possible for pairs of pawn before or after a free square" $ do
            capture "000" `shouldBe` Nothing
            capture "0" `shouldBe` Nothing

    describe "captures" $ do
        it "yields all possible captures on a board" $ do
            captures "110" `shouldBe` ["001"]
            captures "011" `shouldBe` ["100"]
            captures "01"  `shouldBe` []
            captures "0110" `shouldBe` ["1000","0001"]
            captures "01110" `shouldBe` ["10010"]

    describe "Loner" $ do
        it "wins if the board has only one pawn" $ do
            loner "001" `shouldBe` True
            loner "000010000" `shouldBe` True

        it "looses if the board has isolated pawns" $ do
            loner "10001" `shouldBe` False

        it "can transform sequences of 2 pawns followed by a free square" $ do
            loner "110" `shouldBe` True
            loner "11000" `shouldBe` True

        it "can transform sequences of a free square and 2 pawns" $ do
            loner "011" `shouldBe` True
            loner "00011" `shouldBe` True
