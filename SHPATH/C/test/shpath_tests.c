#include "assert.h"
#include "unity.h"
#include "string.h"
#include "unity_fixture.h"
#include "unity_memory.h"
#include "shpath.h"

TEST_GROUP(shpath);

struct graph *graph;

struct graph *setup_small_graph() {
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
    return graph;
}

TEST_SETUP(shpath) {
    graph = setup_small_graph();
}

TEST_TEAR_DOWN(shpath) {
    destroy_graph(graph);
}

TEST(shpath, graph_size) {
    printf("graph size\n");
    TEST_ASSERT_EQUAL(4, graph->size);
}

TEST(shpath, graph_vertex_edges) {
    printf("graph vertex and edges\n");
    TEST_ASSERT_EQUAL(2, graph->vertice[0]->size);
    TEST_ASSERT_EQUAL(3, graph->vertice[1]->size);
    TEST_ASSERT_EQUAL(3, graph->vertice[2]->size);
    TEST_ASSERT_EQUAL(2, graph->vertice[3]->size);
    TEST_ASSERT_EQUAL(1, graph->vertice[0]->edges[0]->destination);
    TEST_ASSERT_EQUAL(2, graph->vertice[0]->edges[1]->destination);
    TEST_ASSERT_EQUAL(400, graph->vertice[3]->edges[0]->cost);
}

TEST(shpath, graph_hash_table) {
    printf("graph hash table\n");
    TEST_ASSERT_EQUAL_STRING("qux", find_vertex(graph, "qux")->name);
    TEST_ASSERT_EQUAL_STRING("foo", find_vertex(graph, "foo")->name);
    TEST_ASSERT_NULL(find_vertex(graph, "gus"));
}

TEST(shpath, graph_priority_queue) {
    printf("graph priority queue\n");
    update(graph->queue, find_vertex(graph, "foo"), 4807);
    update(graph->queue, find_vertex(graph, "qux"), 2317);
    update(graph->queue, find_vertex(graph, "foo"), 10000);
    update(graph->queue, find_vertex(graph, "bar"), 42);
    TEST_ASSERT_EQUAL(3, graph->queue->size);
    int priority;
    struct vertex *vertex;
    extract_min(graph->queue, &vertex, &priority);
    TEST_ASSERT_EQUAL_STRING("bar", vertex->name);
    TEST_ASSERT_EQUAL_INT(42, priority);
    extract_min(graph->queue, &vertex, &priority);
    TEST_ASSERT_EQUAL_STRING("qux", vertex->name);
    TEST_ASSERT_EQUAL_INT(2317, priority);
    extract_min(graph->queue, &vertex, &priority);
    TEST_ASSERT_EQUAL_STRING("foo", vertex->name);
    TEST_ASSERT_EQUAL_INT(4807, priority);
    destroy_graph(graph);
    graph = create_graph();
    char buffer[10];
    for(int i = 0; i < 10000; i++) {
        sprintf(buffer, "%05d", i);
        add_vertex(graph, buffer);
        struct vertex* vertex = find_vertex(graph, buffer);
        update(graph->queue, vertex, rand() % 10000);
    }
    TEST_ASSERT_EQUAL(10000, graph->size);
    for(int i = 0; i < 1000; i++) {
        int r = rand() % 10000;
        struct vertex* vertex = graph->vertice[r];
        update(graph->queue, vertex, rand() % 10000);
    }
    int trace = -1;
    for(int i =0; i < 10000; i++) {
        extract_min(graph->queue, &vertex, &priority);
        TEST_ASSERT_TRUE(trace <= priority);
        trace = priority;
    }
}

TEST(shpath, path) {
    TEST_ASSERT_EQUAL_INT(4, graph->size);
    TEST_ASSERT_EQUAL_INT(300, path(graph, 0, 3));
    TEST_ASSERT_EQUAL_INT(200, path(graph, 1, 3));
    TEST_ASSERT_EQUAL_INT(200, path(graph, 3, 1));
}


