#include "unity.h"
#include "unity_fixture.h"
#include "unity_memory.h"
#include "anadiv.h"
TEST_GROUP(anadiv);

TEST_SETUP(anadiv) { }
TEST_TEAR_DOWN(anadiv) { }

TEST(anadiv, scan_number_and_k) {
    int k = scan_number_and_divisor("4807 10");
    TEST_ASSERT_EQUAL(10, k);
    TEST_ASSERT_EQUAL(4, Length);
    TEST_ASSERT_EQUAL(7, Number[0]);
    TEST_ASSERT_EQUAL(0, Number[1]);
    TEST_ASSERT_EQUAL(8, Number[2]);
    TEST_ASSERT_EQUAL(4, Number[3]);
}
