#include "anadiv.h"
#include <stdbool.h>
#include <ctype.h>
#include <stdio.h>

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

