#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#define MAX_LINE 40;

int reverse(int n) {
    int result;
    char buffer[20];
    sprintf(buffer, "%d", n);
    int l = strlen(buffer);
    int i = 0;
    int j = l-1;
    do {
        char t = buffer[i];
        buffer[i] = buffer[j];
        buffer[j] = t;
        i++;
        j--;
    } while (i<=j);
    sscanf(buffer, "%d", &result);
    return result;
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
