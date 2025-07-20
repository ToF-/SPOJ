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

TEST(anadiv, trivial_cases) {
    check_largest_anagram(0, 1, 0);
    check_largest_anagram(0, 2, 0);
    check_largest_anagram(0, 3, 0);
    check_largest_anagram(0, 4, 0);
    check_largest_anagram(0, 5, 0);
    check_largest_anagram(0, 6, 0);
    check_largest_anagram(0, 7, 0);
    check_largest_anagram(0, 8, 0);
    check_largest_anagram(0, 9, 0);
    check_largest_anagram(0, 10, 0);
    check_largest_anagram(1, 1, 1);
    check_largest_anagram(2, 2, 2);
    check_largest_anagram(3, 3, 3);
    check_largest_anagram(4, 4, 4);
    check_largest_anagram(5, 5, 5);
    check_largest_anagram(6, 6, 6);
    check_largest_anagram(7, 7, 7);
    check_largest_anagram(8, 8, 8);
    check_largest_anagram(9, 9, 9);
    check_largest_anagram(10, 10, 10);
}
TEST(anadiv, largest_anagram_ending_with) {
    check_largest_anagram_ending_with(9758, 1, 8, 9758);
    check_largest_anagram_ending_with(62222, 1, 2, 62222);
    check_largest_anagram_ending_with(1,1,1,1);
    check_largest_anagram_ending_with(27, 1, 2, 72);
    check_largest_anagram_ending_with(72, 1, 7, 27);
    check_largest_anagram_ending_with(4807, 2, 48, 7048);
    check_largest_anagram_ending_with(7048, 2, 48, 7048);
    check_largest_anagram_ending_with(262, 1, 2, 622);
    check_largest_anagram_ending_with(622, 1, 2, 622);
    check_largest_anagram_ending_with(261, 1, 6, 216);
    check_largest_anagram_ending_with(7897493048, 3, 748, 9987430748);
}
TEST(anadiv, largest_multiple_of_1) {
    check_largest_anagram(4807, 1, 8740);
}

TEST(anadiv, largest_multiple_of_1_same_as_N) {
    check_largest_anagram(8740, 1, 8740);
    check_largest_anagram(7321, 1, 7321);
    check_largest_anagram(7777, 1, 7777);
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
    check_largest_anagram(58, 2, 58);
    check_largest_anagram(4287189787312, 2, 9888777432112);
}

TEST(anadiv, largest_multiple_of_2_same_n) {
    check_largest_anagram(975210, 2, 975210);
    check_largest_anagram(97510, 2, 97510);
}


TEST(anadiv, largest_multiple_of_3_no_solution) {
    check_no_solution(4280, 3);
    check_no_solution(1111111111, 3); }

TEST(anadiv, largest_multiple_of_3_obvious_solution) {
    check_largest_anagram(3273, 3, 7332);
    check_largest_anagram(333,3, 333);
}


TEST(anadiv, largest_anagram_multiple_of_4_no_solution) {
    check_no_solution(26, 4);
    check_no_solution(2626, 4);
}

TEST(anadiv, largest_anagram_multiple_of_4_obvious_solution) {
    check_largest_anagram(0, 4, 0);
    check_largest_anagram(8, 4, 8);
    check_largest_anagram(4, 4, 4);
    check_largest_anagram(29, 4, 92);
    check_largest_anagram(61, 4, 16);
    check_largest_anagram(3561, 4, 5316);
    check_largest_anagram(3261, 4, 6312);
    check_largest_anagram(1023456789, 4, 9876543120);
    check_largest_anagram(289, 4, 928);
    check_largest_anagram(196, 4, 916);
    check_largest_anagram(444442, 4, 444424);
    check_largest_anagram(96, 4, 96);
    check_largest_anagram(424, 4, 424);
    check_largest_anagram(4224, 4, 4224);
}

TEST(anadiv, largest_anagram_multiple_of_5_no_solution) {
    check_no_solution(487, 5);
}


TEST(anadiv, largest_anagram_multiple_of_5_obvious_solution) {
    check_largest_anagram(45, 5, 45);
    check_largest_anagram(51, 5, 15);
    check_largest_anagram(120, 5, 210);
    check_largest_anagram(210, 5, 210);
    check_largest_anagram(5028755003, 5, 8755532000);
}

TEST(anadiv, largest_anagram_multiple_of_6_no_solution) {
    check_no_solution(487, 6);
}


TEST(anadiv, prime_numbers) {
    check_largest_anagram(13, 1, 31);
    check_no_solution(13, 2);
    check_no_solution(13, 3);
    check_no_solution(13, 4);
    check_no_solution(13, 5);
    check_no_solution(13, 6);
    check_no_solution(13, 7);
    check_no_solution(13, 8);
    check_no_solution(13, 9);
    check_no_solution(13, 10);
    check_largest_anagram(4943, 2, 9434);
}
TEST(anadiv, largest_anagram_multiple_of_6_obvious_solution) {
    check_largest_anagram(72, 6, 72);
    check_largest_anagram(63, 6, 36);
    check_largest_anagram(780897087, 6, 988877700);
}
TEST(anadiv, largest_anagram_multiple_of_7_obvious_solution) {
    check_largest_anagram(14, 7, 14);
    check_largest_anagram(12, 7, 21);
    check_largest_anagram(41, 7, 14);
    check_largest_anagram(70, 7, 70);
    check_largest_anagram(510,7,105);
    check_largest_anagram(7*7*7*7,7,4102);
    check_largest_anagram(2104*2104,7,8664142);
    check_largest_anagram(1+2104*2104,7,8764124);
}

TEST(anadiv, largest_anagram_multiple_of_8_obvious_solution) {
    check_largest_anagram(61, 8, 16);
    check_largest_anagram(16, 8, 16);
    check_largest_anagram(808, 8, 880);
    check_largest_anagram(8*8*8*8*8*8*8, 8, 9752120);
}

TEST(anadiv, largest_anagram_multiple_of_8_no_solution) {
    check_no_solution(497, 8);
}

TEST(anadiv, largest_anagram_multiple_of_9_no_solution) {
    check_no_solution(497, 9);
}

TEST(anadiv, largest_anagram_multiple_of_9_obvious_solution) {
    check_largest_anagram(486, 9, 864);
    check_largest_anagram(864, 9, 864);
}

TEST(anadiv, largest_anagram_multiple_of_9_no_solution_different_from_n) {
}

TEST(anadiv, largest_anagram_multiple_of_10_no_solution) {
    check_no_solution(497, 10);
    check_largest_anagram(10, 10, 10);
}

TEST(anadiv, largest_anagram_multiple_of_10_obvious_solution) {
    check_largest_anagram(40086, 10, 86400);
}

TEST(anadiv, scan_number_without_leading_zeroes) {
    scan_input("00004807 1", n, &k);
    bool result = largest_anagram(n, k);
    TEST_ASSERT_EQUAL(8740, number_value(n));
}

TEST(anadiv, scan_10_factor) {
    scan_input("4807 10", n, &k);
    TEST_ASSERT_EQUAL(10, k);
}

TEST(anadiv, scan_number_with_only_zeroes) {
    scan_input("00000000 1", n, &k);
    bool result = largest_anagram(n, k);
    TEST_ASSERT_EQUAL(0, number_value(n));
}
