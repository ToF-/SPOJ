module LonerSpec
    where

import Test.Hspec
import Loner

spec :: SpecWith ()
spec = do
    describe "Loner" $ do
        it "wins if the board has only one pawn" $ do
            loner "001" `shouldBe` True
