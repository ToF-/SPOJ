#include "anadiv.h"  // this line on top (building spoj)
#include <assert.h>
#include <stdbool.h>
#include <ctype.h>
#include <stdlib.h>
#include <stdio.h>

bool Strict_Anagram = false;

void swap_digits(struct number *, int, int);
bool uniform(struct number *, int);
bool next_anagram(struct number *);
int longest_descending_subsequence(struct number *, int, int *);
bool largest_anagram_multiple_of_1(struct number *, struct number *);
bool largest_anagram_multiple_of_2(struct number *, struct number *);
bool largest_anagram_multiple_of_3(struct number *, struct number *);
bool largest_anagram_multiple_of_4(struct number *, struct number *);
bool largest_anagram_multiple_of_5(struct number *, struct number *);
bool largest_anagram_multiple_of_6(struct number *, struct number *);
bool largest_anagram_multiple_of_7(struct number *, struct number *);
bool largest_anagram_multiple_of_8(struct number *, struct number *);
bool largest_anagram_multiple_of_9(struct number *, struct number *);
bool largest_anagram_multiple_of_10(struct number *, struct number *);
bool find_digit_with_predicate(struct number *, int, bool (*)(char), int *);
bool is_even(char);
bool is_odd(char);
bool is_multiple_of_4(char);
bool same_number(struct number *, struct number *);

bool scan_input(char *line, struct number *n, int *factor) {
    n->length = 0;
    *factor = 0;
    char *s = line;
    bool in_number = false;
    while (*s && isdigit(*s) && n->length < MAX_DIGITS) {
        if (*s == '0' && ! in_number) {
            s++;
        } else {
            in_number = true;
            n->digits[n->length++] = *s++ - '0';
        }
    }
    if (n->length == 0) {
        n->digits[0] = 0;
        n->length = 1;
    }
    while (*s == ' ') s++;
    while (*s && isdigit(*s))
        *factor = *factor * 10 + *s++ -'0';
    return n->length <= MAX_DIGITS;
}

long long number_value(struct number *n) {
    long long result = 0;
    for (int i = 0; i < n->length; i++) {
        result = result * 10 + n->digits[i];
    }
    return result;
}

void print_number(struct number *n) {
    bool in_number = false;
    for (int i = 0; i < n->length; i++) {
        if (n->digits[i] != 0)
            in_number = true;
        if (in_number)
            putchar(n->digits[i] + '0');
    }
}

bool uniform(struct number *n, int length) {
    if (length == 1)
        return true;
    char digit = n->digits[0];
    for(int i = 1; i < length; i++) {
        if (n->digits[i] != digit)
            return false;
    }
    return true;
}

bool same_number(struct number *a, struct number *b) {
    if (Strict_Anagram && cmp_numbers(a, b) == 0)
        return true;
    return false;
}
int cmp_char_desc(const void *arg_a, const void *arg_b) {
    char a = * (const char *)arg_a;
    char b = * (const char *)arg_b;
    return b - a;
}

void sort_subsequence(struct number *n, int pos, int len) {
    assert(pos >= 0);
    assert(len <= n->length);
    if (len < 2)
        return;
    qsort(&n->digits[pos], len, sizeof(char), cmp_char_desc);
}
void greatest_permutation(struct number *n) {
    sort_subsequence(n, 0, n->length);
}

int cmp_numbers(struct number *a, struct number *b) {
    assert(a->length == b->length);
    for (int i = 0; i < a->length; i++) {
        int cmp = a->digits[i] - b->digits[i];
        if (cmp)
            return cmp;
    }
    return 0;
}

void copy_number(struct number *a, struct number *b) {
    for (int i = 0; i < a->length; i++) {
        b->digits[i] = a->digits[i];
    }
    b->length = a->length;
}

void swap_digits(struct number *n, int pos_a, int pos_b) {
    char temp = n->digits[pos_a];
    n->digits[pos_a] = n->digits[pos_b];
    n->digits[pos_b] = temp;
}

/* e.g. n= 4807 start = 3
 * stop_pos ← 1, return 2
 */
int longest_descending_subsequence(struct number *n, int start, int *stop_pos) {
    assert(start > 0);
    *stop_pos = -1;
    int i;
    for( i = start; i > 0; i--) {
        if (n->digits[i-1] > n->digits[i]) {
            *stop_pos = i-1;
            return start - i + 1;
        }
    }
    return start-i+1;
}

bool find_digit_with_predicate(struct number * n, int start, bool (*predicate)(char), int *pos) {
    for(int i = start; i >= 0; i--) {
        if(predicate(n->digits[i])) {
            *pos = i;
            return true;
        }
    }
    return false;
}
/* e.g. n = 4807 length = 4    n ← 4780 return true
 * n = 0478 length = 4         return false
 */
bool next_subsequence(struct number *n, int length) {
    if(uniform(n, length))
        return false;
    int next_pos;
    int size = longest_descending_subsequence(n, length-1, &next_pos);
    if (size == length)
        return false;
    int to_swap = next_pos + 1;
    for (int i = to_swap; i < length; i++) {
        if (n->digits[i] > n->digits[to_swap]
                && n->digits[i] < n->digits[next_pos]) {
            to_swap = i;
        }
    }
    swap_digits(n, next_pos, to_swap);
    sort_subsequence(n, next_pos+1, length-(next_pos+1));
    if(n->digits[0] == 0)
        return false;
    return true;
}

bool next_anagram(struct number *n) {
    int next_pos;
    int size = longest_descending_subsequence(n, n->length-1, &next_pos);
    if (size == n->length)
        return false;
    int to_swap = n->length -1;
    for (int i = to_swap; i < n->length; i++) {
        if (n->digits[i] > n->digits[to_swap]
                && n->digits[i] < n->digits[next_pos]) {
            to_swap = i;
        }
    }
    swap_digits(n, next_pos, to_swap);
    sort_subsequence(n, next_pos+1, n->length - next_pos);
    if (n->digits[0] == 0)
        return false;
    return true;
}

bool largest_anagram_multiple_of_1(struct number *n, struct number *original) {
    greatest_permutation(n);
    if (n->digits[0] == 0)
        return false;
    if (same_number(n, original)) {
        return next_subsequence(n, n->length);
    }
    return true;
}

bool is_zero(char c) {
    return c == 0;
}

bool is_even(char c) {
    return (c & 1) == 0;
}

bool is_odd(char c) {
    return (c & 1) == 1;
}
bool is_multiple_of_4(char c) {
    return (c % 4) == 0;
}
/* if digits of s are in n, finds the largest anagram with s as suffix which is not
 *  the orginal n
 * e.g. n = 4807, size = 2, s = 04   n ← 8704  return true
 * e.g. n = 7408, size = 1, s = 8    n ← 7048  return true
 * e.b. n = 12,   size = 1  s = 2              return false
 * */
bool largest_anagram_ending_with(struct number *n, int nb_pos, int s, struct number *original) {
    int found = 0;
    int suffix = s;
    for (int i = 0; i < nb_pos; i++) {
        for(int j=0; j < n->length - i; j++) {
            if( n->digits[j] == suffix % 10) {
                found++;
                swap_digits(n, n->length - 1 - i, j);
                break;
            }
        }
        suffix /= 10;
    }
    if (found < nb_pos)
        return false;
    sort_subsequence(n, 0, n->length - nb_pos);
    if (n->digits[0] == 0) {
        return false;
    }
    if (same_number(n, original)) {
        found = 0;
        if (n->length > nb_pos + 1) {
            bool result = next_subsequence(n, n->length - nb_pos);
            if (result) {
                found = nb_pos;
            }
        }
    }
    if (n->digits[0] == 0)
        return false;
    return found == nb_pos;
    
}
bool largest_anagram_multiple_of_2(struct number *n, struct number *original) {
    if (n->length == 1 && n->digits[0] == 0)
        return false;
    const int suffixes[] = { 0, 2, 4, 6, 8 };
    const int nb_pos = 1;
    const int nb_suffixes = 5;
    bool found = false;
    struct number *accum = (struct number *)malloc(sizeof(struct number));
    for (int i = 0; i < n->length; i++)
        accum->digits[i] = 0;
    accum->length = n->length;
    for (int i = 0; i < nb_suffixes; i++) {
       if (largest_anagram_ending_with(n, nb_pos, suffixes[i], original)) {
           if (cmp_numbers(n, accum) > 0) {
               copy_number(n, accum);
               found = true;
           }
       }
    }
    if(found)
        copy_number(accum, n);
    free(accum);
    if (n->digits[0] == 0)
        return false;
    return found;
}

bool largest_anagram_multiple_of_3(struct number *n, struct number *original) {
    int sum_digits = 0;
    for (int i = 0; i < n->length; i++)
        sum_digits += n->digits[i];
    if (sum_digits % 3 > 0)
        return false;
    greatest_permutation(n);
    if (same_number(n, original)) {
        return next_subsequence(n, n->length);
    }
    if (n->digits[0] == 0)
        return false;
    return true;
}

bool largest_anagram_multiple_of_4(struct number *n, struct number *original) {
    if (n->length == 1) {
        return ((n->digits[0] % 4) == 0) && (! Strict_Anagram);
    }

    const int suffixes[] = { 0, 4, 8, 12, 16, 20, 24, 28, 32, 36, 40, 44, 48, 52, 56, 60, 64, 68, 72, 76, 80, 84, 88, 92, 96 };
    const int nb_pos = 2;
    const int nb_suffixes = 25;
    bool found = false;
    struct number *accum = (struct number *)malloc(sizeof(struct number));
    for (int i = 0; i < n->length; i++)
        accum->digits[i] = 0;
    accum->length = n->length;
    for (int i = 0; i < nb_suffixes; i++) {
       if (largest_anagram_ending_with(n, nb_pos, suffixes[i], original)) {
           if (cmp_numbers(n, accum) > 0) {
               copy_number(n, accum);
               found = true;
           }
       }
    }
    if(found)
        copy_number(accum, n);
    free(accum);
    if (n->digits[0] == 0)
        return false;
    return found;
}

bool largest_anagram_multiple_of_5(struct number *n, struct number *original) {
    if (n->length == 1 && n->digits[0] == 0)
        return ! Strict_Anagram;
    const int suffixes[] = { 0, 5 };
    const int nb_pos = 1;
    const int nb_suffixes = 2;
    bool found = false;
    struct number *accum = (struct number *)malloc(sizeof(struct number));
    for (int i = 0; i < n->length; i++)
        accum->digits[i] = 0;
    accum->length = n->length;
    for (int i = 0; i < nb_suffixes; i++) {
        if (largest_anagram_ending_with(n, nb_pos, suffixes[i], original)) {
            if (cmp_numbers(n, accum) > 0) {
                copy_number(n, accum);
                found = true;
                }
        }
    }
    if(found)
        copy_number(accum, n);
    free(accum);
    if (n->digits[0] == 0)
        return false;
    return found;
}

bool largest_anagram_multiple_of_6(struct number *n, struct number *original) {
    int sum_digits = 0;
    for (int i = 0; i < n->length; i++)
        sum_digits += n->digits[i];
    if (sum_digits % 3 > 0)
        return false;
    return largest_anagram_multiple_of_2(n, original);
}

bool divisible_by_7(struct number *n) {
    int sum_groups = 0;
    int group_sign = 1;
    int group = 0;
    int factor = 1;
    int i = n->length - 1;
    int d = 0;
    do {
        group += n->digits[i] * factor;
        if (d > 0 && d % 3 == 2) {
            sum_groups = sum_groups + group * group_sign;
            group = 0;
            factor = 1;
            group_sign = group_sign == 1 ? -1 : 1;
        } else {
            factor *= 10;
        }
        d++;
        i--;
    } while (i >= 0);
    sum_groups = sum_groups + group * group_sign;
    if (n->digits[0] == 0)
        return false;
    return sum_groups % 7 == 0;
}

bool largest_anagram_multiple_of_7(struct number *n, struct number *original) {
    sort_subsequence(n, 0, n->length);
    int trials = 0;
    while (trials < MAX_TRIALS) {
        if (divisible_by_7(n)) {
            if (! same_number(n, original))
                return true;
        }
        if (! next_subsequence(n, n->length))
            return false;
        trials++;
    }
    if (n->digits[0] == 0)
        return false;
    return false;
}

bool largest_anagram_multiple_of_8(struct number *n, struct number *original) {
    if (n->length == 1)
        return ((n->digits[0] % 8) == 0) && ! Strict_Anagram;
    if (n->length == 2) {
        sort_subsequence(n, 0, n->length);
        do {
            if (number_value(n) % 8 == 0) {
                if (!same_number(n, original))
                    return true;
            }
        } while(next_subsequence(n, n->length));
        return false;
    }
    const int suffixes[] = { 
        0,   8,  16,  24,  32,  40,  48,  56,  64,  72,  80,  88,  96, 104, 112, 120,
        128, 136, 144, 152, 160, 168, 176, 184, 192, 200, 208, 216, 224, 232, 240, 248,
        256, 264, 272, 280, 288, 296, 304, 312, 320, 328, 336, 344, 352, 360, 368, 376,
        384, 392, 400, 408, 416, 424, 432, 440, 448, 456, 464, 472, 480, 488, 496, 504,
        512, 520, 528, 536, 544, 552, 560, 568, 576, 584, 592, 600, 608, 616, 624, 632, 
        640, 648, 656, 664, 672, 680, 688, 696, 704, 712, 720, 728, 736, 744, 752, 760,
        768, 776, 784, 792, 800, 808, 816, 824, 832, 840, 848, 856, 864, 872, 880, 888, 
        896, 904, 912, 920, 928, 936, 944, 952, 960, 968, 976, 984, 992 };
    const int nb_pos = 3;
    const int nb_suffixes = 125;
    bool found = false;
    struct number *accum = (struct number *)malloc(sizeof(struct number));
    for (int i = 0; i < n->length; i++)
        accum->digits[i] = 0;
    accum->length = n->length;
    for (int i = 0; i < nb_suffixes; i++) {
       if (largest_anagram_ending_with(n, nb_pos, suffixes[i], original)) {
           if (cmp_numbers(n, accum) > 0) {
               copy_number(n, accum);
               found = true;
           }
       }
    }
    if(found)
        copy_number(accum, n);
    free(accum);
    if (n->digits[0] == 0)
        return false;
    return found;
}
bool largest_anagram_multiple_of_9(struct number *n, struct number *original) {
    int sum_digits = 0;
    for (int i = 0; i < n->length; i++)
        sum_digits += n->digits[i];
    if (sum_digits % 9 > 0)
        return false;
    greatest_permutation(n);
    if (n->digits[0] == 0)
        return false;
    if (same_number(n, original)) {
        return next_subsequence(n, n->length);
    }
    if (n->digits[0] == 0)
        return false;
    return true;
}

bool largest_anagram_multiple_of_10(struct number *n, struct number *original) {
    if (n->length == 1 && n->digits[0] == 0 && ! Strict_Anagram)
        return true;

    const int suffixes[] = { 0 };
    const int nb_pos = 1;
    const int nb_suffixes = 1;
    bool found = false;
    struct number *accum = (struct number *)malloc(sizeof(struct number));
    for (int i = 0; i < n->length; i++)
        accum->digits[i] = 0;
    accum->length = n->length;
    for (int i = 0; i < nb_suffixes; i++) {
        if (largest_anagram_ending_with(n, nb_pos, suffixes[i], original)) {
            if (cmp_numbers(n, accum) > 0) {
                copy_number(n, accum);
                found = true;
                }
        }
    }
    if(found)
        copy_number(accum, n);
    free(accum);
    if (n->digits[0] == 0)
        return false;
    return found;
}

bool largest_anagram(struct number *n, int k) {
    bool result = false;
    struct number *original = (struct number *)malloc(sizeof(struct number));
    copy_number(n, original);
    switch(k) {
        case 1:
            result = largest_anagram_multiple_of_1(n, original);
            break;
        case 2:
            result = largest_anagram_multiple_of_2(n, original);
            break;
        case 3:
            result = largest_anagram_multiple_of_3(n, original);
            break;
        case 4:
            result = largest_anagram_multiple_of_4(n, original);
            break;
        case 5:
            result = largest_anagram_multiple_of_5(n, original);
            break;
        case 6:
            result = largest_anagram_multiple_of_6(n, original);
            break;
        case 7:
            result = largest_anagram_multiple_of_7(n, original);
            break;
        case 8:
            result = largest_anagram_multiple_of_8(n, original);
            break;
        case 9:
            result = largest_anagram_multiple_of_9(n, original);
            break;
        case 10:
            result = largest_anagram_multiple_of_10(n, original);
            break;
    }
    free(original);
    if (n->digits[0] == 0)
        return false;
    return result;
}

void process(bool strict) {
    Strict_Anagram = strict;
    char line[MAX_DIGITS];
    struct number *n = (struct number *)malloc(sizeof(struct number));
    while(fgets(line, MAX_DIGITS+3, stdin)) {
        int factor;
        scan_input(line, n, &factor);
        if (largest_anagram(n, factor)) {
            print_number(n);
            putchar('\n');
        } else
            printf("-1\n");
    }
    free(n);
}

