import Test.Hspec
import Lib

main :: IO ()
main = hspec $  
    describe "should calculate n!" $ do
        it "for trivial values of n" $ do
            fact 1 `shouldBe` 1
            fact 2 `shouldBe` 2

        it "for big values of n" $ do
            fact 10  `shouldBe` 3628800
            fact 100 `shouldBe` 93326215443944152681699238856266700490715968264381621468592963895217599993229915608941463976156518286253697920827223758251185210916864000000000000000000000000
        
