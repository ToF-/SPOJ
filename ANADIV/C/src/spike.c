#include <stdio.h>
#include <stdlib.h>

// Check if the current number represented by `result` is divisible by 7
int divisible_by_7(char *num_str) {
    int mod = 0;
    for (int i = 0; num_str[i]; i++) {
        mod = (mod * 10 + (num_str[i] - '0')) % 7;
    }
    return mod == 0;
}

int main(int argc, char *argv[]) {
    char s[16];
    for(int i = 0; i < 10000000; i++) {
        sprintf(s, "%d", i);
        if (divisible_by_7(s) != (i % 7 == 0))
            printf("uh:%d\n", i);
    }
    exit(0);
}
