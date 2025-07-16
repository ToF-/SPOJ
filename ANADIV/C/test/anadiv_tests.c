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

int number_value(struct number *number) {
    int result = 0;
    for (int i=number->length-1; i >= 0; i--) {
        result = result * 10 + number->digits[i];
    }
    return result;
}

TEST(anadiv, scan_number_and_k) {
    int k = scan_number_and_divisor("4807 10", Number);
    TEST_ASSERT_EQUAL(10, k);
    TEST_ASSERT_EQUAL(4807, number_value(Number));
}

TEST(anadiv, max_anagram) {
    int k = scan_number_and_divisor("4807 10", Number);
    max_anagram(Number);
    TEST_ASSERT_EQUAL(8740, number_value(Number));
}

TEST(anadiv, next_anagram) {
    int k = scan_number_and_divisor("4807 10", Number);
    max_anagram(Number);
    next_anagram(Number);
    TEST_ASSERT_EQUAL(8704, number_value(Number));

}
