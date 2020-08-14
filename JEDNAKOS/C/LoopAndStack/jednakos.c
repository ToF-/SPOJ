#include <stdio.h>
#include <assert.h>

#define MAXDIGITS 1000
#define MAXSUM    5000
#define MAXVALUE  9999
#define STACKSIZE 10000

int Table[MAXDIGITS+1][MAXSUM+1];
int Digits[MAXDIGITS+1];
int Max_Digit;
char Input[MAXDIGITS+1+100];
enum action_type { COMPARE, RECURSE };

struct action { 
    int  index;
    enum action_type type;
    int  target;
};

struct action Stack[STACKSIZE];
int StackPointer;


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

void push(struct action a) {
    assert(StackPointer < STACKSIZE);
    Stack[StackPointer++] = a;
}

struct action pop() {
    assert(StackPointer > 0);
    StackPointer--;
    return Stack[StackPointer];
}

void print_stack() {
    for(int i=0; i<StackPointer; i++) {
        struct action a = Stack[i];
        printf("%s{%d,%d} ", a.type == COMPARE ? "COMPARE" : "RECURSE", a.index, a.target);
    }
    printf("\n");
}
int min(int a, int b) {
    return a < b ? a : b;
}

int additions(int sum) {
    int value; 
    int index;
    int target;
    StackPointer = 0;
    struct action compare = { .type = COMPARE, .index = 0, .target = sum};
    struct action recurse = { .type = RECURSE, .index = 0, .target = sum};
    push(compare);
    push(recurse);
    for(;;) {
        print_stack();
        struct action a = pop();
        switch(a.type) {
            case COMPARE:
                printf("comparing %d and %d at [%d][%d]\n", Table[a.index][a.target], 1+value, a.index, a.target);
                Table[a.index][a.target] = min(Table[a.index][a.target], 1+value);
                printf("Table[%d][%d] <− %d\n", a.index,a.target,Table[a.index][a.target]);
                value = Table[a.index][a.target];
                printf("value = %d\n", value);
                if (a.index == 0 && a.target == sum) 
                    return Table[a.index][a.target];
                break;
            case RECURSE:
                index = a.index;
                target= a.target;
                assert(target >=0);
                if (index == Max_Digit && target == 0) {
                    value = 0;
                    printf("found %d at Table[%d][%d]\n", value, index, target);
                    break;
                }
                if (index == Max_Digit && target > 0) {
                    value = MAXVALUE;
                    break;
                }
                if (Table[index][target]) {
                    value = Table[index][target];
                    printf("found %d at Table[%d][%d]\n", value, index, target);
                    break;
                }
                Table[index][target] = MAXVALUE;
                printf("Table[%d][%d] <- %d\n", index,target,MAXVALUE);
                int accum = 0;
                for(int j = a.index; (j < Max_Digit) && (target-accum >= 0); j++) {
                    accum = accum * 10 + Digits[j];
                    if (target-accum >=0) {
                        struct action compare = { .type = COMPARE, .index = index, .target = target};
                        struct action recurse = { .type = RECURSE, .index = j+1, .target = target-accum};
                        push(compare);
                        push(recurse);
                    }
                }
                break;
        }
    }
}

int main() {
    int sum;
    init_table();
    fgets(Input, MAXDIGITS + 10, stdin);
    Max_Digit = get_digits_and_sum(Input, &sum);
    printf("%d\n", additions(sum)-1);
    return 0;
}


