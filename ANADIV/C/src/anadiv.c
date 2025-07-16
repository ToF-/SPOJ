#include <ctype.h>
#include <stdio.h>
#include "anadiv.h"

void reverse_number(struct number *number) {
    int start = 0;
    int end = number->length-1;
    while (start < end) {
        int temp = number->digits[start];
        number->digits[start] = number->digits[end];
        number->digits[end] = temp;
        start++;
        end--;
    }
}

void copy_number(struct number *srce, struct number *dest) {
    for(int i = 0; i < srce->length; i++) {
        dest->digits[i] = srce->digits[i];
    }
    dest->length = srce->length;
}

int scan_number_and_divisor(char *srce, struct number *number) {
    number->length = 0;
    char *s = srce;
    while (*s != ' ' && number->length < MAX_DIGITS) {
        number->digits[number->length] = *s - '0';
        number->length++;
        s++;
    }
    while (*s == ' ') s++;
    int result = 0;
    while (*s && isdigit(*s)) {
        result = result * 10 + *s - '0';
        s++;
    }
    reverse_number(number);
    return result;
}

void print_number(struct number *number) {
    struct number output;
    copy_number(number, &output);
    reverse_number(&output);
    for(int i=0; i < output.length; i++) {
        printf("%c", output.digits[i] + '0');
    }
    printf("\n");
}
