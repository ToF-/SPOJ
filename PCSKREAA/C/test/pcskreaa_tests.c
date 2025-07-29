#include <assert.h>
#include "unity.h"
#include "unity_fixture.h"
#include "unity_memory.h"
#include "pcskreaa.h"
#include <stdlib.h>

TEST_GROUP(pcskreaa);

TEST_SETUP(pcskreaa) {
}

TEST_TEAR_DOWN(pcskreaa) {
}

TEST(pcskreaa, cycle_length) {
    TEST_ASSERT_EQUAL(16, cycle_length(22));
    TEST_ASSERT_EQUAL(15, cycle_length(11));
}

TEST(pcskreaa, max_cycle_length) {
    TEST_ASSERT_EQUAL(20, max_cycle_length(1, 10));
}

TEST(pcskreaa, scan_input) {
    int a,b;
    scan_input("4807 2317", &a, &b);
    TEST_ASSERT_EQUAL(4807, a);
    TEST_ASSERT_EQUAL(2317, b);
}
