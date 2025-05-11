module LonerSpec (spec)
    where

import Test.Hspec
import Loner

checkLoner :: String -> Expectation
checkLoner s = do
    case loner s of
        True -> return ()
        False -> putStrLn $ "loner \"" ++ s ++ "\" `shouldBe` True"
    loner s `shouldBe` True

checkNotLoner :: String -> Expectation
checkNotLoner s = do
    case loner s of
        False -> return ()
        True -> putStrLn $ "loner \"" ++ s ++ "\" `shouldBe` False"
    loner s `shouldBe` False

spec :: SpecWith ()
spec = do
    describe "loner" $ do
        it "evaluate trivial cases" $ do
            checkLoner "1"
            checkNotLoner "0"
            checkNotLoner ""

        it "evaluate simple cases" $ do
            checkLoner "10"
            checkNotLoner "11"
            checkLoner "100"
            checkNotLoner "101"
            checkLoner "001"

        it "evaluate 3 sized boards" $ do
            checkLoner "110"
            checkNotLoner "111"
            checkLoner "0000110000"

        it "evaluate complex C boards" $ do
            checkLoner "1101" 
            checkLoner "110101"
            checkLoner "111101" 
            checkLoner "110101111101"

        it "evaluate complex D boards" $ do
            checkLoner "110011" 
            checkLoner "1101010011"
            checkLoner "1100111111"
            checkLoner "110101001111"
            checkLoner "110010101011" 
            checkLoner "11010100101011" 
            checkLoner "11001111101011"
            checkLoner "110101001111101011"

        it "evaluate complex E boards" $ do
            checkLoner "111101110"
            checkLoner "110101110111"
            checkLoner "1111011111"
            checkLoner "11010111011111"
            checkLoner "111101101011"
            checkLoner "1101011101101011"
            checkLoner "1111011111101011"
            checkLoner "11010111011111101011"

        it "evaluate boards with trailings" $ do
            checkLoner "00000000011010111011111101011000000000000000"

        it "evaluate boards in both directions" $ do
            let board = reverse "11010111011111101011"
            checkLoner board
