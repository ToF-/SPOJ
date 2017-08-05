import Test.Hspec
import Spath

main = hspec $ do
    describe "PSQueue" $ do
        describe "binding" $ do
            let b = (42, 100)
            it "has a key" $ do
                key b `shouldBe` 42
            it "has a priority" $ do
                prio b `shouldBe` 100
        describe "max key" $ do
            it "is the maximum key" $ do
                let p = Winner (42, 100) (Loser (17,200) Start 42 Start) 42
                max_key p `shouldBe` 42
        describe "play" $ do
            it "takes 2 PSQ with keys in p1 < keys in p2 and return the union of p1 and p2" $ do
                let p = Winner (17, 200) Start 17
                    q = Winner (42, 100) Start 42
                    r = Winner (65, 150) Start 65
                (p `play` q) `shouldBe` Winner (42,100) (Loser (17,200) Start 17 Start) 42
                (q `play` r) `shouldBe` Winner (42,100) (Loser (65,150) Start 42 Start) 65
                (p `play` Void) `shouldBe` p
            
                
