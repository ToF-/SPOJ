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
    scan_input("4807 1", n, &k);
    TEST_ASSERT_TRUE(largest_anagram(n, k));
    TEST_ASSERT_EQUAL(8740, number_value(n));
}

TEST(anadiv, largest_multiple_of_1_different_from_N) {
    scan_input("8740 1", n, &k);
    TEST_ASSERT_TRUE(largest_anagram(n, k));
    TEST_ASSERT_EQUAL(8704, number_value(n));

    scan_input("7321 1", n, &k);
    TEST_ASSERT_TRUE(largest_anagram(n, k));
    TEST_ASSERT_EQUAL(7312, number_value(n));

    scan_input("7311 1", n, &k);
    TEST_ASSERT_TRUE(largest_anagram(n, k));
    TEST_ASSERT_EQUAL(7131, number_value(n));

    scan_input("7531111 1", n, &k);
    TEST_ASSERT_TRUE(largest_anagram(n, k));
    TEST_ASSERT_EQUAL(7513111, number_value(n));
}

TEST(anadiv, largest_multiple_of_1_impossible) {
    scan_input("7777 1", n, &k);
    TEST_ASSERT_FALSE(largest_anagram(n, k));

    scan_input("1 1", n, &k);
    TEST_ASSERT_FALSE(largest_anagram(n, k));
}

