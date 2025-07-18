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
