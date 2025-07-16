#define MAX_DIGITS 1024

struct number {
    int digits[MAX_DIGITS];
    int length;
};

int scan_number_and_divisor(char *, struct number *);
void print_number(struct number *);
void max_anagram(struct number *);
void next_anagram(struct number *);
