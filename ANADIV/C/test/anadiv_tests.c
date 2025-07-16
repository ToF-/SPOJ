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
    TEST_ASSERT_TRUE(next_anagram(Number)); TEST_ASSERT_EQUAL(8704, number_value(Number));
    TEST_ASSERT_TRUE(next_anagram(Number)); TEST_ASSERT_EQUAL(8470, number_value(Number));
    TEST_ASSERT_TRUE(next_anagram(Number)); TEST_ASSERT_EQUAL(8407, number_value(Number));
    TEST_ASSERT_TRUE(next_anagram(Number)); TEST_ASSERT_EQUAL(8074, number_value(Number));
    TEST_ASSERT_TRUE(next_anagram(Number)); TEST_ASSERT_EQUAL(8047, number_value(Number));
    TEST_ASSERT_TRUE(next_anagram(Number)); TEST_ASSERT_EQUAL(7840, number_value(Number));
    TEST_ASSERT_TRUE(next_anagram(Number)); TEST_ASSERT_EQUAL(7804, number_value(Number));
    TEST_ASSERT_TRUE(next_anagram(Number)); TEST_ASSERT_EQUAL(7480, number_value(Number));
    TEST_ASSERT_TRUE(next_anagram(Number)); TEST_ASSERT_EQUAL(7408, number_value(Number));
    TEST_ASSERT_TRUE(next_anagram(Number)); TEST_ASSERT_EQUAL(7084, number_value(Number));
    TEST_ASSERT_TRUE(next_anagram(Number)); TEST_ASSERT_EQUAL(7048, number_value(Number));
    TEST_ASSERT_TRUE(next_anagram(Number)); TEST_ASSERT_EQUAL(4870, number_value(Number));
    TEST_ASSERT_TRUE(next_anagram(Number)); TEST_ASSERT_EQUAL(4807, number_value(Number));
    TEST_ASSERT_TRUE(next_anagram(Number)); TEST_ASSERT_EQUAL(4780, number_value(Number));
    TEST_ASSERT_TRUE(next_anagram(Number)); TEST_ASSERT_EQUAL(4708, number_value(Number));
    TEST_ASSERT_TRUE(next_anagram(Number)); TEST_ASSERT_EQUAL(4087, number_value(Number));
    TEST_ASSERT_TRUE(next_anagram(Number)); TEST_ASSERT_EQUAL(4078, number_value(Number));
    TEST_ASSERT_TRUE(next_anagram(Number)); TEST_ASSERT_EQUAL( 874, number_value(Number));
    TEST_ASSERT_TRUE(next_anagram(Number)); TEST_ASSERT_EQUAL( 847, number_value(Number));
    TEST_ASSERT_TRUE(next_anagram(Number)); TEST_ASSERT_EQUAL( 784, number_value(Number));
    TEST_ASSERT_TRUE(next_anagram(Number)); TEST_ASSERT_EQUAL( 748, number_value(Number));
    TEST_ASSERT_TRUE(next_anagram(Number)); TEST_ASSERT_EQUAL( 487, number_value(Number));
    TEST_ASSERT_TRUE(next_anagram(Number)); TEST_ASSERT_EQUAL( 478, number_value(Number));
    TEST_ASSERT_FALSE(next_anagram(Number));
}

TEST(anadiv, anagram_with_repeated_digits) {
    int k = scan_number_and_divisor("95059 10", Number);
    max_anagram(Number);TEST_ASSERT_EQUAL(99550, number_value(Number));
    TEST_ASSERT_TRUE(next_anagram(Number)); TEST_ASSERT_EQUAL(99505, number_value(Number));
    TEST_ASSERT_TRUE(next_anagram(Number)); TEST_ASSERT_EQUAL(99055, number_value(Number));
    TEST_ASSERT_TRUE(next_anagram(Number)); TEST_ASSERT_EQUAL(95950, number_value(Number));
    TEST_ASSERT_TRUE(next_anagram(Number)); TEST_ASSERT_EQUAL(95905, number_value(Number));
    TEST_ASSERT_TRUE(next_anagram(Number)); TEST_ASSERT_EQUAL(95590, number_value(Number));
    TEST_ASSERT_TRUE(next_anagram(Number)); TEST_ASSERT_EQUAL(95509, number_value(Number));
    TEST_ASSERT_TRUE(next_anagram(Number)); TEST_ASSERT_EQUAL(95095, number_value(Number));
    TEST_ASSERT_TRUE(next_anagram(Number)); TEST_ASSERT_EQUAL(95059, number_value(Number));
    TEST_ASSERT_TRUE(next_anagram(Number)); TEST_ASSERT_EQUAL(90955, number_value(Number));
    TEST_ASSERT_TRUE(next_anagram(Number)); TEST_ASSERT_EQUAL(90595, number_value(Number));
    TEST_ASSERT_TRUE(next_anagram(Number)); TEST_ASSERT_EQUAL(90559, number_value(Number));
    TEST_ASSERT_TRUE(next_anagram(Number)); TEST_ASSERT_EQUAL(59950, number_value(Number));
}

TEST(anadiv, compare_number) {
    int k = scan_number_and_divisor("4807 10", Number);
    struct number other;
    int l = scan_number_and_divisor("4870 10", &other);
    TEST_ASSERT_FALSE(equal_numbers(Number, &other));
    next_anagram(&other);
    TEST_ASSERT_TRUE(equal_numbers(Number, &other));
}

TEST(anadiv, largest_multiple_of_1_second_to_top) {
    int k = scan_number_and_divisor("8740 1", Number);
    TEST_ASSERT_EQUAL(1, k);
    TEST_ASSERT_TRUE(largest_multiple(Number, k));
    TEST_ASSERT_EQUAL(8704, number_value(Number));
}

TEST(anadiv, largest_multiple_of_1_only_one_digit) {
    int k = scan_number_and_divisor("11 1", Number);
    struct number expected;
    TEST_ASSERT_FALSE(largest_multiple(Number, k));
}

TEST(anadiv, largest_multiple_of_2_no_solution_fast) {
    int k = scan_number_and_divisor("5913777795733331111177777999955555533337 2", Number);
    TEST_ASSERT_FALSE(largest_multiple(Number, k));
}

TEST(anadiv, largest_multiple_of_2) {
    int k = scan_number_and_divisor("4807 2", Number);
    TEST_ASSERT_TRUE(largest_multiple(Number, k));
    TEST_ASSERT_EQUAL(8740, number_value(Number));
}

TEST(anadiv, largest_multiple_of_2_second_to_top) {
    int k = scan_number_and_divisor("8740 2", Number);
    TEST_ASSERT_TRUE(largest_multiple(Number, k));
    TEST_ASSERT_EQUAL(8704, number_value(Number));
}

TEST(anadiv, largest_multiple_of_2_very_large_number) {
    int k = scan_number_and_divisor("81357135713571357135713571357135713571357135713571357135713571357135713571357135713571357135713571357135713571357135713571357135713571357135713571357 2", Number);
    TEST_ASSERT_TRUE(largest_multiple(Number, k));
    print_number(Number);
}
