#include "unity.h"
#include "unity_fixture.h"
#include "unity_memory.h"
#include "anadiv.h"
#include <stdlib.h>

TEST_GROUP(anadiv);

struct number *n;
int k;

TEST_SETUP(anadiv) {
    n = (struct number *)malloc(sizeof(struct number));
}

TEST_TEAR_DOWN(anadiv) {
    free(n);
}

void check_largest_anagram(long long input, int factor, long long expected) {
    char line[MAX_DIGITS+3];
    sprintf(line, "%lld %d", input, factor);
    scan_input(line, n, &k);
    TEST_ASSERT_TRUE(largest_anagram(n, k));
    TEST_ASSERT_EQUAL(expected, number_value(n));
}

void check_no_solution(long long input, int factor) {
    char line[MAX_DIGITS+3];
    sprintf(line, "%lld %d", input, factor);
    scan_input(line, n, &k);
    TEST_ASSERT_FALSE(largest_anagram(n, k));
}

TEST(anadiv, scan_number_and_factor) {
    TEST_ASSERT_TRUE(scan_input("4807 7", n, &k));
    TEST_ASSERT_EQUAL(7, k);
    TEST_ASSERT_EQUAL(4807, number_value(n));
}

TEST(anadiv, greatest_permutation) {
    scan_input("4807 7", n, &k);
    greatest_permutation(n);
    TEST_ASSERT_EQUAL(8740, number_value(n));
}

TEST(anadiv, comparing_numbers) {
    struct number *m = (struct number *)malloc(sizeof(struct number));
    scan_input("4807 7", n, &k);
    scan_input("4807 7", m, &k);
    TEST_ASSERT_EQUAL(0, cmp_numbers(n, m));
    scan_input("4806 7", m, &k);
    TEST_ASSERT_TRUE(cmp_numbers(n, m) > 0);
    TEST_ASSERT_TRUE(cmp_numbers(m, n) < 0);
    free(m);
}

TEST(anadiv, largest_multiple_of_1) {
    check_largest_anagram(4807, 1, 8740);
}

TEST(anadiv, largest_multiple_of_1_different_from_N) {
    check_largest_anagram(8740, 1, 8704);
    check_largest_anagram(7321, 1, 7312);
    check_largest_anagram(7311, 1, 7131);
    check_largest_anagram(7531111, 1, 7513111);
}

TEST(anadiv, largest_multiple_of_1_impossible) {
    check_no_solution(7777, 1);
    check_no_solution(1,1);
}

TEST(anadiv, largest_multiple_of_2_obvious_solution) {
    check_largest_anagram(4082, 2, 8420);
}

TEST(anadiv, largest_multiple_of_2_no_even_digit) {
    check_no_solution(97531, 2);
}

TEST(anadiv, largest_multiple_of_2_look_for_first_even) {
    check_largest_anagram(32791, 2, 97312);
    check_largest_anagram(32741, 2, 74312);
}

TEST(anadiv, largest_multiple_of_2_different_from_n) {
    check_largest_anagram(975210, 2, 975102);
    check_largest_anagram(975214, 2, 975412);
}

TEST(anadiv, largest_multiple_of_2_no_solution) {
    check_no_solution(97510, 2);
    check_no_solution(9758, 2);
}
