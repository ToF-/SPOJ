#include <stdio.h>

#define MAXDIGITS 1000
#define MAXSUM    5000
#define MAXVALUE  9999

int Table[MAXDIGITS+1][MAXSUM+1];
int Digits[MAXDIGITS+1];
int Max_Digit;
char Input[MAXDIGITS+1+100];

int get_digits_and_sum(char *input, int *sum) {
    int count = 0;
    for(int i = 0; i < MAXDIGITS && *input != '='; i++, input++) {
        Digits[i] = *input - '0';
        count++;
    }
    input++;
    sscanf(input,"%d", sum);
    return count;
}

void init_table() {
    for(int i = 0; i < MAXDIGITS; i++) 
        for(int j = 0; j <= MAXSUM; j++)
            Table[i][j] = 0;
}

int additions(int index, int sum) {
    if (sum < 0) return MAXVALUE;
    if (index == Max_Digit) {
        if (sum == 0)
            return 0;
        else
            return MAXVALUE;
    }
    if (Table[index][sum] != 0)
        return Table[index][sum];
    int min_value = MAXVALUE;
    int acc = 0;
    for (int i = index; i < Max_Digit && (sum - acc) >= 0; i++) {
        acc = acc * 10 + Digits[i];
        int value = 1 + additions(i+1, sum - acc);
        if (value < min_value)
            min_value = value;
    }
    Table[index][sum] = min_value;
    return min_value;
}

int main() {
    int sum;
    init_table();
    fgets(Input, MAXDIGITS + 10, stdin);
    Max_Digit = get_digits_and_sum(Input, &sum);
    printf("%d\n", additions(0, sum) - 1);
    return 0;
}


