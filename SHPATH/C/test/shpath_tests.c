#include "unity.h"
#include "unity_fixture.h"
#include "unity_memory.h"
#include "shpath.h"

TEST_GROUP(shpath);

struct graph *graph;

TEST_SETUP(shpath) {
    graph = create_graph();

    add_vertex(graph, "foo");
    add_edge(graph, 0, 1, 100);
    add_edge(graph, 0, 2, 300);

    add_vertex(graph, "bar");
    add_edge(graph, 1, 0, 100);
    add_edge(graph, 1, 2, 100);
    add_edge(graph, 1, 3, 400);

    add_vertex(graph, "qux");
    add_edge(graph, 2, 0, 300);
    add_edge(graph, 2, 1, 100);
    add_edge(graph, 2, 3, 100);

    add_vertex(graph, "law");
    add_edge(graph, 3, 1, 400);
    add_edge(graph, 3, 2, 100);
}

TEST_TEAR_DOWN(shpath) {
    destroy_graph(graph);
}

TEST(shpath, graph_size) {
  TEST_ASSERT_EQUAL(4, graph->size);
}

TEST(shpath, graph_vertex_edges) {
    TEST_ASSERT_EQUAL(2, graph->vertice[0]->size);
    TEST_ASSERT_EQUAL(3, graph->vertice[1]->size);
    TEST_ASSERT_EQUAL(3, graph->vertice[2]->size);
    TEST_ASSERT_EQUAL(2, graph->vertice[3]->size);
    TEST_ASSERT_EQUAL(1, graph->vertice[0]->edges[0]->destination);
    TEST_ASSERT_EQUAL(2, graph->vertice[0]->edges[1]->destination);
    TEST_ASSERT_EQUAL(400, graph->vertice[3]->edges[0]->cost);
}

TEST(shpath, graph_visited) {
    init_visited(graph);
    TEST_ASSERT_EQUAL(0, visited(graph, 4807));
    visit(graph, 4808);
    TEST_ASSERT_EQUAL(1, visited(graph, 4808));
    TEST_ASSERT_EQUAL(0, visited(graph, 4807));
}

