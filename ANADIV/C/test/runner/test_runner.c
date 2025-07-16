#include "unity_fixture.h"

TEST_GROUP_RUNNER(anadiv) {
    RUN_TEST_CASE(anadiv, scan_number_and_k);
    RUN_TEST_CASE(anadiv, max_anagram);
    RUN_TEST_CASE(anadiv, next_anagram);
    RUN_TEST_CASE(anadiv, anagram_with_repeated_digits);
    RUN_TEST_CASE(anadiv, compare_number);
    RUN_TEST_CASE(anadiv, largest_multiple_of_1_second_to_top);
    RUN_TEST_CASE(anadiv, largest_multiple_of_1_only_one_digit);
    RUN_TEST_CASE(anadiv, largest_multiple_of_2_no_solution_fast);
    RUN_TEST_CASE(anadiv, largest_multiple_of_2);
    RUN_TEST_CASE(anadiv, largest_multiple_of_2_second_to_top);
    RUN_TEST_CASE(anadiv, largest_multiple_of_2_very_large_number);
}
