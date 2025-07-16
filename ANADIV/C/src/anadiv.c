#include <ctype.h>
#include <stdio.h>
#include "anadiv.h"

void reverse_number() {
    int start = 0;
    int end = Length-1;
    while (start < end) {
        int temp = Number[start];
        Number[start] = Number[end];
        Number[end] = temp;
        start++;
        end--;
    }
}

int scan_number_and_divisor(char *srce) {
    Length = 0;
    char *s = srce;
    while (*s != ' ' && Length < MAX_DIGITS) {
        Number[Length] = *s - '0';
        Length++;
        s++;
    }
    while (*s == ' ') s++;
    int result = 0;
    while (*s && isdigit(*s)) {
        result = result * 10 + *s - '0';
        s++;
    }
    reverse_number();
    return result;
}
