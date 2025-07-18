#include "anadiv.h"
#include <stdbool.h>
#include <ctype.h>
#include <stdlib.h>
#include <stdio.h>

void copy_numbers(struct number *, struct number *);
void swap_digits(struct number *, int, int);
bool next_anagram(struct number *);
int longest_descending_subsequence(struct number *, int);
void sort_subsequence(struct number *, int, int);

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

int cmp_char_desc(const void *arg_a, const void *arg_b) {
    char a = * (const char *)arg_a;
    char b = * (const char *)arg_b;
    return b - a;
}

void sort_subsequence(struct number *n, int pos, int len) {
    qsort(&n->digits[pos], len, sizeof(char), cmp_char_desc);
}
void greatest_permutation(struct number *n) {
    sort_subsequence(n, 0, n->length);
}

int cmp_numbers(struct number *a, struct number *b) {
    for (int i = 0; i < a->length; i++) {
        int cmp = a->digits[i] - b->digits[i];
        if (cmp)
            return cmp;
    }
    return 0;
}

void copy_numbers(struct number *a, struct number *b) {
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

int longest_descending_subsequence(struct number *n, int start) {
    for( int i = start; i > 0; i--) {
        if (n->digits[i-1] > n->digits[i])
            return i-1;
    }
    return -1;
}

bool next_anagram(struct number *n) {
    int next_pos = longest_descending_subsequence(n, n->length-1);
    if (next_pos < 0)
        return false;
    int to_swap = n->length -1;

    swap_digits(n, next_pos, to_swap);
    sort_subsequence(n, next_pos+1, n->length - next_pos);
    return true;
}

bool largest_anagram(struct number *n, int k) {
    struct number *original = (struct number *)malloc(sizeof(struct number));
    copy_numbers(n, original);
    greatest_permutation(n);
    if (! cmp_numbers(n, original)) {
        return next_anagram(n);
    }
    free(original);
    return true;
}
