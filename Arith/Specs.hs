import Data.Text as T
import Prelude as P
import Test.Hspec
import Test.QuickCheck
import Arith

propFormat x s = x>0 && P.length s <= x ==> T.length (format x (pack s)) == x 

shouldBeText a b = a `shouldBe` P.map pack b
main = do
quickCheck propFormat
hspec $ do
    describe "align" $ do
        it "aligns nicely any operation" $ do
            align (P.map pack ["42","+4807","-","4809"])  `shouldBeText` 
                  ["   42","+4807","-----"," 4809"]
    describe "addition" $ do
        it "shows addition of 2 integer" $ do
            addition 42 4807 `shouldBeText` ["   42"
                                        ,"+4807"
                                        ,"-----"
                                        ," 4849"]
            addition 4807 42 `shouldBeText` ["4807"
                                        ," +42"
                                        ,"----"
                                        ,"4849"]
    describe "subtraction" $ do
        it "shows subtraction of 2 integers" $ do
            subtraction 4807 42 `shouldBeText` ["4807"
                                           ," -42"
                                           ,"----"
                                           ,"4765"]
            subtraction 1000 1 `shouldBeText` ["1000"
                                          ,"  -1"
                                          ," ---"
                                          ," 999"]
    describe "multiplication" $ do
        it "shows multiplication of 2 integers" $ do
            multiplication 4807 420 `shouldBeText` ["   4807"
                                               ,"   *420"
                                               ,"   ----"
                                               ,"      0"
                                               ,"  9614"
                                               ,"19228"
                                               ,"-------"
                                               ,"2018940"]
            multiplication 4807 42  `shouldBeText` ["  4807"
                                               ,"   *42"
                                               ,"  ----"
                                               ,"  9614"
                                               ,"19228"
                                               ,"------"
                                               ,"201894"]
            multiplication 1 12345  `shouldBeText` ["     1"
                                               ,"*12345"
                                               ,"------"
                                               ,"     5"
                                               ,"    4"
                                               ,"   3"
                                               ,"  2"
                                               ," 1"
                                               ," -----"
                                               ," 12345"]

            multiplication 123451234567890 9 `shouldBeText`
                [" 123451234567890","              *9","----------------","1111061111111010"]
        it "does not show intermediate computations when only one" $ do
            multiplication 4807 2 `shouldBeText` ["4807"
                                             ,"  *2"
                                             ,"----"
                                             ,"9614"]

    describe "operation" $ do
        it "shows operation given an input" $ do
            operation (pack "4807-42") `shouldBe` subtraction 4807 42 ++ [T.empty]

    describe "solve" $ do
        it "solve several problems" $ do
            solve (P.map pack ["2","4807+42","4807-42"]) `shouldBeText` ["4807"
                                                       ," +42"
                                                       ,"----"
                                                       ,"4849"
                                                       ,""
                                                       ,"4807"
                                                       ," -42"
                                                       ,"----"
                                                       ,"4765"
                                                       ,""]

    describe "process" $ do
        it "process input and show output" $ do
            process "2\n4807+42\n4807-42\n" `shouldBe` 
                "4807\n +42\n----\n4849\n\n4807\n -42\n----\n4765\n\n"
            


