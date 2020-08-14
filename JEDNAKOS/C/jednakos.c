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
    int zeros = 0;
    for(int i = 0; i < MAXDIGITS && *input != '='; i++, input++) {
        int digit = *input - '0';
        if (digit) {
            Digits[count] = digit;
            count++;
            zeros = 0;
        } else if(zeros < 4) {
            Digits[count] = digit;
            count++;
            zeros++;
        }
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

int additions(int index, int target) {
    if (target < 0) return MAXVALUE;
    if (index == Max_Digit)
        return target ? MAXVALUE : 0 ;
    if (Table[index][target])
        return Table[index][target];
    int min_value = MAXVALUE;
    int accum = 0;
    for (int i = index; i < Max_Digit && (target - accum) >= 0; i++) {
        accum = accum * 10 + Digits[i];
        int value = 1 + additions(i+1, target - accum);
        if (value < min_value)
            min_value = value;
            Table[index][target] = min_value;
    }
    return Table[index][target];
}

int main() {
    int sum;
    init_table();
    fgets(Input, MAXDIGITS + 10, stdin);
    Max_Digit = get_digits_and_sum(Input, &sum);
    printf("%d\n", additions(0, sum) - 1);
    return 0;
}


