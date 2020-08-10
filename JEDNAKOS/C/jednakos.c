#include <stdio.h>

#define MAXDIGITS 1000
#define MAXSUM     500

int Table[MAXDIGITS+1][MAXSUM+1];
int Digits[MAXDIGITS+1];
char Input[MAXDIGITS+1+100];

int get_digits_and_sum(char *input, int *sum) {
    int count = 0;
    for(int i = 1; i < MAXDIGITS+1 && *input != '='; i++, input++) {
        Digits[i] = *input - '0';
        count++;
    }
    input++;
    sscanf(input,"%d", sum);
    return count;
}

void init_table() {
    for(int i = 1; i < MAXDIGITS+1; i++) 
        for(int j = 1; j <= MAXSUM+1; j++)
            Table[i][j] = 0;
}

int main() {
    int max_digit;
    int sum;
    init_table();
    gets(Input);
    max_digit = get_digits_and_sum(Input, &sum);
    printf("%d,", max_digit);
    printf("%d\n", sum);
    return 0;
}


