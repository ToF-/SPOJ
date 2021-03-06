#include <stdio.h>

#define MAXVALUE 10000
#define MAXCASES  1000
#define MAXLINE 80

struct result {
    int size;
    unsigned char value[MAXVALUE];
} Results[MAXCASES];


int MaxN;

char Line[MAXLINE];

int get_int() {
    int result;
    fgets(Line, MAXLINE, stdin);
    sscanf(Line, "%d", &result);
    return result;
}

int main() {
    int max_cases = get_int();
    int MaxN = 0;
    Results[MaxN].value[0]=1;
    Results[MaxN].size=1;
    for(int c=0; c<max_cases; c++) {
        int n = get_int()-1;
        if (n > MaxN) {
            for(int m = MaxN; m < n; m++) { 
                for(int i = 0; i < Results[m].size; i++)
                    Results[m+1].value[i] = Results[m].value[i];
                Results[m+1].size = Results[m].size;
                int carry = 0;
                for(int i = 0; i < Results[m+1].size; i++) {
                    int prod = Results[m+1].value[i] * (m+2) + carry;
                    Results[m+1].value[i] = prod % 10;
                    carry = prod / 10;
                }
                while(carry) {
                    Results[m+1].value[Results[m+1].size] = carry % 10;
                    carry = carry / 10;
                    Results[m+1].size++;
                }
                MaxN=m+1;
            } 
        }
        for(int i = Results[n].size-1; i>=0;i--) 
            printf("%c", Results[n].value[i]+'0');
        printf("\n");
    }
    return 0;
}
