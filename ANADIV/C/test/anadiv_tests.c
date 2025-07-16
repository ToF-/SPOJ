#include "unity.h"
#include "unity_fixture.h"
#include "unity_memory.h"
#include "anadiv.h"
#include <stdlib.h>

TEST_GROUP(anadiv);

struct number *Number;

TEST_SETUP(anadiv) {
    Number = (struct number *)malloc(sizeof(struct number));
}

TEST_TEAR_DOWN(anadiv) {
    free(Number);
}

TEST(anadiv, scan_number_and_k) {
    int k = scan_number_and_divisor("4807 10", Number);
    TEST_ASSERT_EQUAL(10, k);
    TEST_ASSERT_EQUAL(4, Number->length);
    TEST_ASSERT_EQUAL(7, Number->digits[0]);
    TEST_ASSERT_EQUAL(0, Number->digits[1]);
    TEST_ASSERT_EQUAL(8, Number->digits[2]);
    TEST_ASSERT_EQUAL(4, Number->digits[3]);
    print_number(Number);
}

TEST(anadiv, max_anagram) {
    int k = scan_number_and_divisor("4807 10", Number);
    max_anagram(Number);
    print_number(Number);
    TEST_ASSERT_EQUAL(0, Number->digits[0]);
    TEST_ASSERT_EQUAL(4, Number->digits[1]);
    TEST_ASSERT_EQUAL(7, Number->digits[2]);
    TEST_ASSERT_EQUAL(8, Number->digits[3]);
}
