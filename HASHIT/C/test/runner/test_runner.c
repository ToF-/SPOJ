#include "unity_fixture.h"

TEST_GROUP_RUNNER(hashit) {
    RUN_TEST_CASE(hashit, initially_empty);
    RUN_TEST_CASE(hashit, initially_not_finding_any_key);
    RUN_TEST_CASE(hashit, adding_and_finding_a_key);
    RUN_TEST_CASE(hashit, adding_and_finding_any_key);
    RUN_TEST_CASE(hashit, deleting_a_key);
    RUN_TEST_CASE(hashit, deleting_a_key_and_preseving_others);
    RUN_TEST_CASE(hashit, adding_distinct_keys_with_same_hash);
}
