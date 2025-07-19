#include "unity_fixture.h"

TEST_GROUP_RUNNER(anadiv) {
    RUN_TEST_CASE(anadiv, scan_number_and_factor);
    RUN_TEST_CASE(anadiv, greatest_permutation);
    RUN_TEST_CASE(anadiv, comparing_numbers);
    RUN_TEST_CASE(anadiv, largest_multiple_of_1);
    RUN_TEST_CASE(anadiv, largest_multiple_of_1_different_from_N);
    RUN_TEST_CASE(anadiv, largest_multiple_of_1_impossible);
    RUN_TEST_CASE(anadiv, largest_multiple_of_2_obvious_solution);
    RUN_TEST_CASE(anadiv, largest_multiple_of_2_no_even_digit);
    RUN_TEST_CASE(anadiv, largest_multiple_of_2_look_for_first_even);
    RUN_TEST_CASE(anadiv, largest_multiple_of_2_different_from_n);
    RUN_TEST_CASE(anadiv, largest_multiple_of_2_no_solution);
    RUN_TEST_CASE(anadiv, largest_multiple_of_2_no_solution_different_from_n);
    RUN_TEST_CASE(anadiv, largest_multiple_of_3_no_solution);
    RUN_TEST_CASE(anadiv, largest_multiple_of_3_obvious_solution);
    RUN_TEST_CASE(anadiv, largest_multiple_of_3_no_solution_different_from_n);
    RUN_TEST_CASE(anadiv, largest_anagram_multiple_of_4_no_solution);
    RUN_TEST_CASE(anadiv, largest_anagram_multiple_of_4_obvious_solution);
    RUN_TEST_CASE(anadiv, largest_anagram_multiple_of_4_different_from_n);
}
