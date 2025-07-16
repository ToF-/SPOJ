#define MAX_DIGITS 1024

struct number {
    int digits[MAX_DIGITS];
    int length;
};

int scan_number_and_divisor(char *, struct number *);
