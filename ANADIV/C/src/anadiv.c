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

void sort_number_subsequence(struct number *number, int start_position, int length) {
    qsort(&number->digits[start_position], length, sizeof(int), int_compare_ascending);
}

void max_anagram_from(struct number *number, int start_position) {
    sort_number_subsequence(number, start_position, number->length - start_position);
}

void max_anagram(struct number *number) {
    max_anagram_from(number, 0);
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
    sort_number_subsequence(number, 0, len_prefix);
    return true;
}

void print_all_anagrams(struct number *number) {
    max_anagram(number);
    do {
        print_number(number);
    } while(next_anagram(number));
}

bool equal_numbers(struct number *number, struct number *other) {
    for(int i = 0; i < number->length; i++) {
        if (number->digits[i] != other->digits[i])
            return false;
    }
    return true;
}

bool number_contains(struct number *number, bool (*predicate)(int)) {
    for(int i = 0; i < number->length; i++) {
        if (predicate(number->digits[i]))
            return true;
    }
    return false;
}

bool is_even(int digit) {
    return (!(digit & 1));
}

bool find_digit_equal_to(struct number *number, int target, int *position) {
    for(int i = 0; i < number->length; i++) {
        if (number->digits[i] == target) {
            *position = i;
            return true;
        }
    }
    return false;
}

int compare_numbers(struct number *a, struct number *b) {
    for(int i = 0; i < a->length; i++) {
        int result = a->digits[i] - b->digits[i];
        if (result != 0)
            return result;
    }
    return 0;
}

bool find_largest_multiple_of_1(struct number *number) {
    struct number source;
    copy_number(number, &source);
    max_anagram(number);
    if(equal_numbers(number, &source)) {
        return next_anagram(number);
    }
    return true;
}

bool find_largest_multiple_of_2(struct number *number) {
    struct number source;
    struct number candidate;
    struct number buffer;

    copy_number(number, &source);
    if(!number_contains(number, &is_even))
        return false;

    copy_number(number, &candidate);
    for(int even_digit = 0; even_digit < 10; even_digit+=2) {
        int pos;
        max_anagram(number);
        if (find_digit_equal_to(number, even_digit, &pos)) {
            if(pos == 0) {
                return true;
            }
            swap(&number->digits[0], &number->digits[pos]);
            max_anagram_from(number, 1);
            if (equal_numbers(number, &source))
                continue;
            if (compare_numbers(number, &candidate) > 0) {
                copy_number(number, &candidate);
            }
        }
    }
    copy_number(&candidate, number);
    return true;
}

bool largest_multiple(struct number *number, int k) {
    switch(k) {
        case 1:
            return find_largest_multiple_of_1(number);
        case 2:
            return find_largest_multiple_of_2(number);
        default:
            return false;
    }
    return false;
}

