#include <stdio.h>

#define MAXRESULT 10000
#define MAXLINE 80

int Result[10000];
int Size=1;

char Line[MAXLINE];

int get_int() {
    int result;
    fgets(Line, MAXLINE, stdin);
    sscanf(Line, "%d", &result);
    return result;
}

int main() {
    int max_cases = get_int();
    for(int c=0; c<max_cases; c++) {
        int max = get_int();
        Result[0]=1;
        Size=1;
        int carry = 0;
        for(int n = 1; n <= max; n++) {
            for(int i = 0; i < Size; i++) {
                int prod = Result[i] * n + carry;
                Result[i] = prod % 10;
                carry = prod / 10;
            }
            while(carry) {
                Result[Size] = carry % 10;
                carry = carry / 10;
                Size++;
            } 
        } 
        for(int i = Size-1; i>=0;i--)
            printf("%c", Result[i]+'0');
        printf("\n");
    }
    return 0;
}
