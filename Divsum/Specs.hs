import Test.Hspec
import DivSum

main = hspec $ do
    describe "divSum" $ do
        it "sums the proper divisors of a number" $ do
            divSum 1 `shouldBe` 0
            divSum 2 `shouldBe` 1
            divSum 3 `shouldBe` 1
            divSum 10 `shouldBe` 8

    describe "solve" $ do
        it "solves a number of DivSum problems" $ do
            solve [5,2,10,20,40] `shouldBe` [1,8,22,50]

    describe "process" $ do
        it "processes input and display output" $ do
            process "5\n2\n10\n20\n40\n" `shouldBe` "1\n8\n22\n50\n"

    describe "primeFactors" $ do
        it "gives the prime factors of a number" $ do
            primeFactors 1 `shouldBe` []            
            primeFactors 6 `shouldBe` [2,3]
            primeFactors 20 `shouldBe` [2,2,5]
            primeFactors 34 `shouldBe` [2,17]

    describe "primes" $ do
        it "gives primes numbers" $ do
            primes !! 11 `shouldBe` 37
            primes !! 12 `shouldBe` 41

    describe "minus on infinite ordered lists" $ do
        it "returns the list minus the other list" $ do
            take 3 ([1..]`minus`[2,4..]) `shouldBe` [1,3,5]
            take 3 ([2..]`minus`[2,4..]) `shouldBe` [3,5,7]
            take 3 ([3..]`minus`[2,4..]) `shouldBe` [3,5,7]

    describe "multiples" $ do
        it "gives an infinite list of the multiples of a number starting with n*n" $ do
            take 3 (multiples 3) `shouldBe` [9,12,15]

    describe "merge two infinite ordered lists" $ do
        it "returns the list of all the values in the lists" $ do
            take 5 (merge [1,3..] [2,4..]) `shouldBe` [1,2,3,4,5]

    describe "union of several infinite ordered lists" $ do
        it "merges all the lists together" $ do
            take 15 (union [[4,6..],[9,12..],[25,30..]]) `shouldBe` [4,6,8,9,10,12,14,15,16,18,20,21,22,24,25]
