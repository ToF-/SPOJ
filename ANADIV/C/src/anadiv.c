#include <assert.h>
#include "anadiv.h"
#include <stdbool.h>
#include <ctype.h>
#include <stdlib.h>
#include <stdio.h>

void swap_digits(struct number *, int, int);
bool uniform(struct number *, int);
bool next_anagram(struct number *);
int longest_descending_subsequence(struct number *, int, int *);
void sort_subsequence(struct number *, int, int);
bool next_subsequence(struct number *, int);
bool largest_anagram_multiple_of_1(struct number *, struct number *);
bool largest_anagram_multiple_of_2(struct number *, struct number *);
bool largest_anagram_multiple_of_3(struct number *, struct number *);
bool largest_anagram_multiple_of_4(struct number *, struct number *);
bool largest_anagram_multiple_of_5(struct number *, struct number *);
bool find_digit_with_predicate(struct number *, int, bool (*)(char), int *);
bool is_even(char);
bool is_odd(char);
bool is_multiple_of_4(char);

bool scan_input(char *line, struct number *n, int *factor) {
    n->length = 0;
    *factor = 0;
    char *s = line;
    while (*s && isdigit(*s) && n->length < MAX_DIGITS)
        n->digits[n->length++] = *s++ - '0';
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
    for (int i = 0; i < n->length; i++)
        putchar(n->digits[i] + '0');
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
    for( int i = start; i > 0; i--) {
        if (n->digits[i-1] > n->digits[i]) {
            *stop_pos = i-1;
            return start - i + 1;
        }
    }
    return start - *stop_pos;
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
    printf("calling longest_descending_subsequence(");print_number(n);printf(",%d)\n", length-1);
    int size = longest_descending_subsequence(n, length-1, &next_pos);
    printf("size:%d next_pos:%d", size, next_pos);putchar('\n');
    if (size == length)
        return false;
    int to_swap = next_pos + 1;
    for (int i = to_swap; i < length; i++) {
        if (n->digits[i] > n->digits[to_swap]
                && n->digits[i] < n->digits[next_pos]) {
            to_swap = i;
        }
    }
    printf("next_pos:%d to_swap:%d\n", next_pos, to_swap);
    print_number(n);putchar(' ');
    swap_digits(n, next_pos, to_swap);
    print_number(n);putchar('\n');
    printf("calling sort_subsequence(%d,%d)\n", next_pos+1, length-(next_pos+1));
    sort_subsequence(n, next_pos+1, length-(next_pos+1));
    print_number(n);putchar('\n');
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
    return true;
}

bool largest_anagram_multiple_of_1(struct number *n, struct number *original) {
    greatest_permutation(n);
    if (! cmp_numbers(n, original)) {
        return next_subsequence(n, n->length);
    }
    return true;
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
    print_number(n); putchar(':'); print_number(original);putchar('\n');
    printf("ns_pos:%d s:%d\n", nb_pos, s);
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
    print_number(n);putchar('\n');
    if (found < nb_pos)
        return false;
    sort_subsequence(n, 0, n->length - nb_pos);
    if (! cmp_numbers(n, original)) {
        found = 0;
        printf("n->length:%d nb_pos+1:%d\n", n->length, nb_pos+1);
        if (n->length > nb_pos + 1) {
            printf("calling next_subsequence(");print_number(n);printf(",%d)\n", n->length-nb_pos);
            bool result = next_subsequence(n, n->length - nb_pos);
            printf("%d ", result);print_number(n);putchar('\n');
            if (result) {
                found = nb_pos;
            }
        }
    }
    return found == nb_pos;
}
bool largest_anagram_multiple_of_2(struct number *n, struct number *original) {
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
    return found;
}

bool largest_anagram_multiple_of_3(struct number *n, struct number *original) {
    int sum_digits = 0;
    for (int i = 0; i < n->length; i++)
        sum_digits += n->digits[i];
    if (sum_digits % 3 > 0)
        return false;
    greatest_permutation(n);

    return true;
}

bool largest_anagram_multiple_of_4(struct number *n, struct number *original) {
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
    return found;
}

bool largest_anagram_multiple_of_5(struct number *n, struct number *original) {
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
    return found;
}

bool largest_anagram(struct number *n, int k) {
    bool result = false;
    if (uniform(n, n->length))
        return false;
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
    }
    free(original);
    return result;
}
