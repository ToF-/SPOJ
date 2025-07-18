#include "unity.h"
#include "unity_fixture.h"
#include "unity_memory.h"
#include "anadiv.h"
#include <stdlib.h>

TEST_GROUP(anadiv);

struct number *n;

TEST_SETUP(anadiv) {
    n = (struct number *)malloc(sizeof(struct number));
}

TEST_TEAR_DOWN(anadiv) {
    free(n);
}

TEST(anadiv, scan_number_and_factor) {
    int k;
    TEST_ASSERT_TRUE(scan_input("4807 7", n, &k));
    TEST_ASSERT_EQUAL(7, k);
    TEST_ASSERT_EQUAL(4807, number_value(n));
}

