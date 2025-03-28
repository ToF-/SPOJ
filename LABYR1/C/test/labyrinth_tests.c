#include "assert.h"
#include "unity.h"
#include "string.h"
#include "unity_fixture.h"
#include "unity_memory.h"
#include "labyrinth.h"

struct labyrinth *lab;

TEST_GROUP(labyrinth);

TEST_SETUP(labyrinth) {
    lab = new_labyrinth();
}

TEST_TEAR_DOWN(labyrinth) {
    free_labyrinth(lab);
}

TEST(labyrinth, trivial_case) {
    init_labyrinth(lab);
    add_line(lab, "###");
    add_line(lab, "#.#");
    add_line(lab, "###");
    TEST_ASSERT_EQUAL(3, lab->size_x);
    TEST_ASSERT_EQUAL(3, lab->size_y);
    TEST_ASSERT_EQUAL(0, rope_length(lab));
}
TEST(labyrinth, simple_case) {
    init_labyrinth(lab);
    add_line(lab, "#######");
    add_line(lab, "#.#.###");
    add_line(lab, "#.#.###");
    add_line(lab, "#.#.#.#");
    add_line(lab, "#.....#");
    add_line(lab, "#######");
    TEST_ASSERT_EQUAL(7, lab->size_x);
    TEST_ASSERT_EQUAL(6, lab->size_y);
    TEST_ASSERT_EQUAL(8, rope_length(lab));
}
TEST(labyrinth, larger_case) {
    init_labyrinth(lab);
    add_line(lab, "############");
    add_line(lab, "#####......#");
    add_line(lab, "#.#.#.###.##");
    add_line(lab, "#.#.#.#.#..#");
    add_line(lab, "#.#.#.#.##.#");
    add_line(lab, "#.#.#.#.#..#");
    add_line(lab, "#.#.#.#.#.##");
    add_line(lab, "#.#.#.#.#..#");
    add_line(lab, "#...#.#.##.#");
    add_line(lab, "#.#...#.#..#");
    add_line(lab, "#######...##");
    add_line(lab, "#.#.#.#.#.##");
    add_line(lab, "#.#.#.#.#..#");
    add_line(lab, "#.#.#.#.##.#");
    add_line(lab, "#.#.#.#.#..#");
    add_line(lab, "#.#.#.#.#.##");
    add_line(lab, "#.#.#.#.#..#");
    add_line(lab, "#...#.#.##.#");
    add_line(lab, "#.#.....#..#");
    add_line(lab, "############");
    TEST_ASSERT_EQUAL(12, lab->size_x);
    TEST_ASSERT_EQUAL(20, lab->size_y);
    TEST_ASSERT_EQUAL(59, rope_length(lab));
}
TEST(labyrinth, process_test_case) {
    FILE *file = fopen("../test/201x201.txt","r");
    char buffer[N];
    fgets(buffer, N, file);
    int result = process_test_case(file);
    TEST_ASSERT_EQUAL(9592, result);
    fclose(file);
}


