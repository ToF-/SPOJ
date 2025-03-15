#include "assert.h"
#include "unity.h"
#include "string.h"
#include "unity_fixture.h"
#include "unity_memory.h"
#include "hashit.h"

TEST_GROUP(hashit);

struct hash_table hash_table;

TEST_SETUP(hashit) {
    initialize(&hash_table);
}

TEST_TEAR_DOWN(hashit) {
}

TEST(hashit, initially_empty) {
    TEST_ASSERT_EQUAL(0, nb_keys(&hash_table));
}

TEST(hashit, initially_not_finding_any_key) {
    TEST_ASSERT_EQUAL(-1, find_key("foo", &hash_table));
}

TEST(hashit, adding_and_finding_a_key) {
    add_key("e", &hash_table);
    TEST_ASSERT_EQUAL(0, find_key("e", &hash_table));
    TEST_ASSERT_EQUAL(1, nb_keys(&hash_table));
}
TEST(hashit, adding_and_finding_any_key) {
    add_key("f", &hash_table);
    TEST_ASSERT_EQUAL(19, find_key("f", &hash_table));
    TEST_ASSERT_EQUAL(-1, find_key("e", &hash_table));
    TEST_ASSERT_EQUAL(1, nb_keys(&hash_table));
}
TEST(hashit, deleting_a_key) {
    add_key("e", &hash_table);
    delete_key("e", &hash_table);
    TEST_ASSERT_EQUAL(-1, find_key("e", &hash_table));
    TEST_ASSERT_EQUAL(0, nb_keys(&hash_table));
}
TEST(hashit, deleting_a_key_and_preseving_others) {
    add_key("e", &hash_table);
    add_key("f", &hash_table);
    delete_key("e", &hash_table);
    TEST_ASSERT_EQUAL(-1, find_key("e", &hash_table));
    TEST_ASSERT_EQUAL(19, find_key("f", &hash_table));
    TEST_ASSERT_EQUAL(1, nb_keys(&hash_table));
}
TEST(hashit, adding_distinct_keys_with_same_hash) {
    add_key("e", &hash_table);
    add_key("ee", &hash_table);
    TEST_ASSERT_EQUAL(0, find_key("e", &hash_table));
    TEST_ASSERT_EQUAL(24, find_key("ee", &hash_table));
    TEST_ASSERT_EQUAL(2, nb_keys(&hash_table));
}
TEST(hashit, cannot_delete_non_existing_key) {
    add_key("e", &hash_table);
    add_key("ee", &hash_table);
    delete_key("foo", &hash_table);
    TEST_ASSERT_EQUAL(2, nb_keys(&hash_table));
}
TEST(hashit, cannot_add_an_existing_key) {
    add_key("foo", &hash_table);
    add_key("foo", &hash_table);
    TEST_ASSERT_EQUAL(1, nb_keys(&hash_table));
}
TEST(hashit, test_sample) {
    operation("ADD:marsz", &hash_table);;
    operation("ADD:marsz", &hash_table);;
    operation("ADD:Dabrowski", &hash_table);;
    operation("ADD:z", &hash_table);;
    operation("ADD:ziemii", &hash_table);;
    operation("ADD:wloskiej", &hash_table);;
    operation("ADD:do", &hash_table);;
    operation("ADD:Polski", &hash_table);;
    operation("DEL:od", &hash_table);;
    operation("DEL:do", &hash_table);;
    operation("DEL:wloskiej", &hash_table);;
    TEST_ASSERT_EQUAL(5, nb_keys(&hash_table));
    print_hash_table(&hash_table);
}
