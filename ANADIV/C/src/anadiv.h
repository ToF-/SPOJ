#include <stdbool.h>
#define MAX_DIGITS 1024

struct number {
    int length;
    char digits[MAX_DIGITS];
};

bool scan_input(char *, struct number *, int *);
long long number_value(struct number *);
void print_number(struct number *);
