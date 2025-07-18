#include "unity_fixture.h"

TEST_GROUP_RUNNER(anadiv) {
    RUN_TEST_CASE(anadiv, scan_number_and_factor);
    RUN_TEST_CASE(anadiv, greatest_permutation);
    RUN_TEST_CASE(anadiv, comparing_numbers);
    RUN_TEST_CASE(anadiv, largest_multiple_of_1);
    RUN_TEST_CASE(anadiv, largest_multiple_of_1_different_from_N);
    RUN_TEST_CASE(anadiv, largest_multiple_of_1_impossible);
}
