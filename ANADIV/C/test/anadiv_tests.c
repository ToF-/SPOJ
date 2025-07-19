#include "unity.h"
#include "unity_fixture.h"
#include "unity_memory.h"
#include "anadiv.h"
#include <stdlib.h>

TEST_GROUP(anadiv);

struct number *n;
int k;

TEST_SETUP(anadiv) {
    n = (struct number *)malloc(sizeof(struct number));
}

TEST_TEAR_DOWN(anadiv) {
    free(n);
}

void check_largest_anagram(long long input, int factor, long long expected) {
    char line[MAX_DIGITS+3];
    sprintf(line, "%lld %d", input, factor);
    scan_input(line, n, &k);
    TEST_ASSERT_TRUE(largest_anagram(n, k));
    TEST_ASSERT_EQUAL(expected, number_value(n));
}

void check_largest_anagram_ending_with(long long input, int nb_pos, int suffix, long long expected) {
    struct number *original = (struct number *)malloc(sizeof(struct number));
    char line[MAX_DIGITS+3];
    sprintf(line, "%lld %d", input, 1);
    scan_input(line, n, &k);
    copy_number(n, original);
    TEST_ASSERT_TRUE(largest_anagram_ending_with(n, nb_pos, suffix, original));
    TEST_ASSERT_EQUAL(expected, number_value(n));
    free(original);
}
void check_no_largest_anagram_ending_with(long long input, int nb_pos, int suffix, long long expected) {
    struct number *original = (struct number *)malloc(sizeof(struct number));
    char line[MAX_DIGITS+3];
    sprintf(line, "%lld %d", input, 1);
    scan_input(line, n, &k);
    copy_number(n, original);
    TEST_ASSERT_FALSE(largest_anagram_ending_with(n, nb_pos, suffix, original));
    free(original);
}

void check_no_solution(long long input, int factor) {
    char line[MAX_DIGITS+3];
    sprintf(line, "%lld %d", input, factor);
    scan_input(line, n, &k);
    TEST_ASSERT_FALSE(largest_anagram(n, k));
}

TEST(anadiv, scan_number_and_factor) {
    TEST_ASSERT_TRUE(scan_input("4807 7", n, &k));
    TEST_ASSERT_EQUAL(7, k);
    TEST_ASSERT_EQUAL(4807, number_value(n));
}

TEST(anadiv, greatest_permutation) {
    scan_input("4807 7", n, &k);
    greatest_permutation(n);
    TEST_ASSERT_EQUAL(8740, number_value(n));
}

TEST(anadiv, comparing_numbers) {
    struct number *m = (struct number *)malloc(sizeof(struct number));
    scan_input("4807 7", n, &k);
    scan_input("4807 7", m, &k);
    TEST_ASSERT_EQUAL(0, cmp_numbers(n, m));
    scan_input("4806 7", m, &k);
    TEST_ASSERT_TRUE(cmp_numbers(n, m) > 0);
    TEST_ASSERT_TRUE(cmp_numbers(m, n) < 0);
    free(m);
}

TEST(anadiv, largest_multiple_of_1) {
    check_largest_anagram(4807, 1, 8740);
}

TEST(anadiv, largest_multiple_of_1_different_from_N) {
    check_largest_anagram(8740, 1, 8704);
    check_largest_anagram(7321, 1, 7312);
    check_largest_anagram(7311, 1, 7131);
    check_largest_anagram(7531111, 1, 7513111);
}

TEST(anadiv, largest_multiple_of_1_impossible) {
    check_no_solution(7777, 1);
    check_no_solution(1,1);
}

TEST(anadiv, largest_multiple_of_2_obvious_solution) {
    check_largest_anagram(4082, 2, 8420);
}

TEST(anadiv, largest_multiple_of_2_no_even_digit) {
    check_no_solution(97531, 2);
}

TEST(anadiv, largest_multiple_of_2_look_for_first_even) {
    check_largest_anagram(32791, 2, 97312);
    check_largest_anagram(32741, 2, 74312);
    check_largest_anagram(4287189787312, 2, 9888777432112);
}

TEST(anadiv, largest_multiple_of_2_different_from_n) {
    check_largest_anagram(975210, 2, 975120);
    check_largest_anagram(975214, 2, 975412);
    check_largest_anagram(97510, 2, 97150);
    check_largest_anagram(9758, 2, 9578);
}

TEST(anadiv, largest_multiple_of_2_no_solution) {
    check_no_solution(58, 2);
}
TEST(anadiv, largest_multiple_of_2_no_solution_different_from_n) {
    check_no_solution(44, 2);

}

TEST(anadiv, largest_multiple_of_3_no_solution) {
    check_no_solution(4280, 3);
    check_no_solution(1111111111, 3); }

TEST(anadiv, largest_multiple_of_3_obvious_solution) {
    check_largest_anagram(3273, 3, 7332);
}

TEST(anadiv, largest_multiple_of_3_no_solution_different_from_n) {
    check_no_solution(333, 3);
}

TEST(anadiv, largest_anagram_multiple_of_4_no_solution) {
    check_no_solution(26, 4);
    check_no_solution(2626, 4);
}

TEST(anadiv, largest_anagram_multiple_of_4_obvious_solution) {
    check_largest_anagram(61, 4, 16);
    check_largest_anagram(3561, 4, 5316);
    check_largest_anagram(3261, 4, 6312);
    check_largest_anagram(1023456789, 4, 9876543120);
    check_largest_anagram(289, 4, 928);
    check_largest_anagram(196, 4, 916);
    check_largest_anagram(444442, 4, 444424);
}

TEST(anadiv, largest_anagram_ending_with) {
    check_largest_anagram_ending_with(62222, 1, 2, 26222);
    check_no_largest_anagram_ending_with(1, 1, 1, 1);
    check_no_largest_anagram_ending_with(7, 1, 1, 1);
    check_no_largest_anagram_ending_with(5, 1, 2, 1);
    check_no_largest_anagram_ending_with(14, 1, 2, 41);
    check_no_largest_anagram_ending_with(12, 1, 2, 21);
    check_largest_anagram_ending_with(27, 1, 2, 72);
    check_largest_anagram_ending_with(72, 1, 7, 27);
    check_largest_anagram_ending_with(4807, 2, 48, 7048);
    check_largest_anagram_ending_with(7048, 2, 48, 748);
    check_largest_anagram_ending_with(262, 1, 2, 622);
    check_largest_anagram_ending_with(622, 1, 2, 262);
    check_largest_anagram_ending_with(261, 1, 6, 216);
    check_largest_anagram_ending_with(7897493048, 3, 748, 9987430748);
}

TEST(anadiv, largest_anagram_multiple_of_4_different_from_n) {
    TEST_IGNORE();
    check_no_solution(96, 4);
    check_largest_anagram(424, 4, 244); // change the suffix multiple of 4
    check_largest_anagram(4224, 4, 2424); // keep the suffix, change the prefix
}

