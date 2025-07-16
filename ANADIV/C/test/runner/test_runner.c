#include "unity_fixture.h"

TEST_GROUP_RUNNER(anadiv) {
    RUN_TEST_CASE(anadiv, scan_number_and_k);
    RUN_TEST_CASE(anadiv, max_anagram);
    RUN_TEST_CASE(anadiv, next_anagram);
}
