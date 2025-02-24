#include "unity.h"
#include "unity_fixture.h"
#include "unity_memory.h"
#include "shpath.h"

TEST_GROUP(shpath);

TEST_SETUP(shpath) { }

TEST_TEAR_DOWN(shpath) { }

TEST(shpath, dummy) {
  int result;
  result = doit();
  TEST_ASSERT_EQUAL(42, result);
}

TEST_GROUP(foo);

TEST_SETUP(foo) { }

TEST_TEAR_DOWN(foo) { }

TEST(foo,dummy_too) {
    TEST_ASSERT_EQUAL(4, 2+2);
}
