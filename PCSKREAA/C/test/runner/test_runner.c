#include "unity_fixture.h"

TEST_GROUP_RUNNER(pcskreaa) {
    RUN_TEST_CASE(pcskreaa, cycle_length);
    RUN_TEST_CASE(pcskreaa, max_cycle_length);
    RUN_TEST_CASE(pcskreaa, scan_input);
}
