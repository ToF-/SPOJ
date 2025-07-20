#include <stdbool.h>
#define MAX_TRIALS 150
#define MAX_DIGITS 1024

struct number {
    int length;
    char digits[MAX_DIGITS];
};

bool scan_input(char *, struct number *, int *);
long long number_value(struct number *);
void copy_number(struct number *, struct number *);
void print_number(struct number *);
void greatest_permutation(struct number *);
int cmp_numbers(struct number *, struct number *);
bool largest_anagram(struct number *, int k);
bool largest_anagram_ending_with(struct number *, int, int, struct number *);
