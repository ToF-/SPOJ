#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#define MAX_LINE 40;

int reverse(int n) {
    int r = 0;
    while(n) {
        r = r * 10 + n % 10;
        n /= 10;
    }
    return r;
}

int main() {
    int max_case;
    scanf("%d", &max_case);
    for(int i=0; i<max_case; i++) {
        int a,b;
        scanf("%d %d", &a, &b);
        printf("%d\n", reverse(reverse(a) + reverse(b)));
    }
    return EXIT_SUCCESS;
}
