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
    TEST_ASSERT(next_anagram(Number)); TEST_ASSERT_EQUAL(8704, number_value(Number));
    TEST_ASSERT(next_anagram(Number)); TEST_ASSERT_EQUAL(8470, number_value(Number));
    TEST_ASSERT(next_anagram(Number)); TEST_ASSERT_EQUAL(8407, number_value(Number));
    TEST_ASSERT(next_anagram(Number)); TEST_ASSERT_EQUAL(8074, number_value(Number));
    TEST_ASSERT(next_anagram(Number)); TEST_ASSERT_EQUAL(8047, number_value(Number));
    TEST_ASSERT(next_anagram(Number)); TEST_ASSERT_EQUAL(7840, number_value(Number));
    TEST_ASSERT(next_anagram(Number)); TEST_ASSERT_EQUAL(7804, number_value(Number));
    TEST_ASSERT(next_anagram(Number)); TEST_ASSERT_EQUAL(7480, number_value(Number));
    TEST_ASSERT(next_anagram(Number)); TEST_ASSERT_EQUAL(7408, number_value(Number));
    TEST_ASSERT(next_anagram(Number)); TEST_ASSERT_EQUAL(7084, number_value(Number));
    TEST_ASSERT(next_anagram(Number)); TEST_ASSERT_EQUAL(7048, number_value(Number));
    TEST_ASSERT(next_anagram(Number)); TEST_ASSERT_EQUAL(4870, number_value(Number));
    TEST_ASSERT(next_anagram(Number)); TEST_ASSERT_EQUAL(4807, number_value(Number));
    TEST_ASSERT(next_anagram(Number)); TEST_ASSERT_EQUAL(4780, number_value(Number));
    TEST_ASSERT(next_anagram(Number)); TEST_ASSERT_EQUAL(4708, number_value(Number));
    TEST_ASSERT(next_anagram(Number)); TEST_ASSERT_EQUAL(4087, number_value(Number));
    TEST_ASSERT(next_anagram(Number)); TEST_ASSERT_EQUAL(4078, number_value(Number));
    TEST_ASSERT(next_anagram(Number)); TEST_ASSERT_EQUAL( 874, number_value(Number));
    TEST_ASSERT(next_anagram(Number)); TEST_ASSERT_EQUAL( 847, number_value(Number));
    TEST_ASSERT(next_anagram(Number)); TEST_ASSERT_EQUAL( 784, number_value(Number));
    TEST_ASSERT(next_anagram(Number)); TEST_ASSERT_EQUAL( 748, number_value(Number));
    TEST_ASSERT(next_anagram(Number)); TEST_ASSERT_EQUAL( 487, number_value(Number));
    TEST_ASSERT(next_anagram(Number)); TEST_ASSERT_EQUAL( 478, number_value(Number));
    TEST_ASSERT(! next_anagram(Number));
}

TEST(anadiv, anagram_with_repeated_digits) {
    int k = scan_number_and_divisor("95059 10", Number);
    max_anagram(Number);TEST_ASSERT_EQUAL(99550, number_value(Number));
    TEST_ASSERT(next_anagram(Number)); TEST_ASSERT_EQUAL(99505, number_value(Number));
    TEST_ASSERT(next_anagram(Number)); TEST_ASSERT_EQUAL(99055, number_value(Number));
    TEST_ASSERT(next_anagram(Number)); TEST_ASSERT_EQUAL(95950, number_value(Number));
    TEST_ASSERT(next_anagram(Number)); TEST_ASSERT_EQUAL(95905, number_value(Number));
    TEST_ASSERT(next_anagram(Number)); TEST_ASSERT_EQUAL(95590, number_value(Number));
    TEST_ASSERT(next_anagram(Number)); TEST_ASSERT_EQUAL(95509, number_value(Number));
    TEST_ASSERT(next_anagram(Number)); TEST_ASSERT_EQUAL(95095, number_value(Number));
    TEST_ASSERT(next_anagram(Number)); TEST_ASSERT_EQUAL(95059, number_value(Number));
    TEST_ASSERT(next_anagram(Number)); TEST_ASSERT_EQUAL(90955, number_value(Number));
    TEST_ASSERT(next_anagram(Number)); TEST_ASSERT_EQUAL(90595, number_value(Number));
    TEST_ASSERT(next_anagram(Number)); TEST_ASSERT_EQUAL(90559, number_value(Number));
    TEST_ASSERT(next_anagram(Number)); TEST_ASSERT_EQUAL(59950, number_value(Number));
}
