#include <ctype.h>
#include <stdio.h>
#include "anadiv.h"
#include <stdbool.h>

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

int int_compare_ascending(const void *arg_a, const void *arg_b) {
    int a = * (const int *)arg_a;
    int b = * (const int *)arg_b;
    return a - b;
}

void sort_number_subsequence(struct number *number, int length) {
    qsort(number->digits, length, sizeof(int), int_compare_ascending);
}

void max_anagram(struct number *number) {
    sort_number_subsequence(number, number->length);
}

void swap(int *a, int *b) {
    int temp;
    temp = *a;
    *a = *b;
    *b = temp;
}

int longest_descending_sequence(struct number *number) {
    if(number->length == 1)
        return 1;
    int i = 1;
    while(i < number->length
            && number->digits[i] <= number->digits[i-1]) {
        i++;
    }
    return i;
}

int greater_digit_smaller_than(struct number *number, int len_prefix) {
    int result = len_prefix-1;
    int next_digit = number->digits[len_prefix];
    for(int i=len_prefix-1; i >= 0; i--) {
        int digit = number->digits[i];
        if (digit < next_digit && digit >= number->digits[result]) {
            result = i;
        }
    }
    return result;
}

bool next_anagram(struct number *number) {
    int len_prefix = longest_descending_sequence(number);
    if (len_prefix == number->length) {
        return false;
    }
    int new_digit_pos = greater_digit_smaller_than(number, len_prefix);
    swap(&number->digits[len_prefix],&number->digits[new_digit_pos]);
    sort_number_subsequence(number, len_prefix);
    return true;
}
