#include "unity_fixture.h"

TEST_GROUP_RUNNER(labyrinth) {
    RUN_TEST_CASE(labyrinth, trivial_case);
    RUN_TEST_CASE(labyrinth, simple_case);
    RUN_TEST_CASE(labyrinth, larger_case);
    RUN_TEST_CASE(labyrinth, process_test_case);
}
