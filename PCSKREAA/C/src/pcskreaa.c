#include "pcskreaa.h"
#include <stdbool.h>

int cycle_length(int n) {
    int l = 0;
    while(true) {
        l++;
        if (n == 1)
            return l;
        if (n & 1) {
            n = n * 3 + 1;
        } else {
            n /= 2;
        }
    }
    return 0;
}

int max_cycle_length(int a, int b) {
    if (a > b) {
        return max_cycle_length(b, a);
    }
    int result = 0;
    for (int n = a; n <= b; n++) {
        int l = cycle_length(n);
        if (l > result)
            result = l;
    }
    return result;
}

void scan_input(char *line, int *a, int *b) {
    sscanf(line, "%d %d", a, b);
}

void process() {
    char line[MAX_LINE];
    while(fgets(line, MAX_LINE, stdin)) {
        int a,b;
        scan_input(line, &a, &b);
        int m = max_cycle_length(a, b);
        printf("%d %d %d\n", a, b, m);
    }
}
