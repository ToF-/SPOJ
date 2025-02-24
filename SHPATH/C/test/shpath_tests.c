#include "unity.h"
#include "unity_fixture.h"
#include "unity_memory.h"
#include "shpath.h"

TEST_GROUP(shpath);

struct graph *g;

TEST_SETUP(shpath) {
    *g = create_graph();

    add_vertex(g, 1);
    add_edge(g, 1, 2, 100);
    add_edge(g, 1, 3, 300);

    add_vertex(g, 2);
    add_edge(g, 2, 1, 100);
    add_edge(g, 2, 3, 100);
    add_edge(g, 2, 4, 400);

    add_vertex(g, 3);
    add_edge(g, 3, 1, 3);
    add_edge(g, 3, 2, 1);
    add_edge(g, 3, 4, 1);

    add_vertex(g, 4);
    add_edge(g, 4, 2, 4);
    add_edge(g, 4, 3, 1);
}

TEST_TEAR_DOWN(shpath) {
    destroy_graph(g);
}

TEST(shpath, a_simple_graph) {

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
