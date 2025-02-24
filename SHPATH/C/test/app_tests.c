#include "unity.h"
#include "unity_fixture.h"
#include "unity_memory.h"
#include "app.h"

TEST_GROUP(app);

TEST_SETUP(app) { }

TEST_TEAR_DOWN(app) { }

TEST(app, dummy) {
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
