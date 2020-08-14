#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include <string.h>



// print <- 
// take a random number S between 1 and 5000
// let C = -1
// let R = S
// subtract a random number N<=R from R, getting R', increment the count
// R = R - N
// C = C + 1
// print N
// if R = 0 we are done, 
// if R > 0 let S = R and loop
// print = 
// print S
// print -> C
//

int random_number(int range) {
    int number = rand() % (range+1);
    return number;
}

static char Buffer[2000];

int main(int argc, char *argv[]) {
    int max_term_value = 100;
    int max_sum = 5000;
    if (argc > 1) {
        sscanf(argv[1], "%d", &max_term_value);
    }
    if (argc > 2) {
        sscanf(argv[2], "%d", &max_sum);
    }
    srand(time(0));
    printf("# test case for jednakos -- random generated\n");
    printf("# lines beginning with # are comments\n");
    printf("# lines containing left arrow are part of the test input\n");
    printf("# lines beginning with right arrow are part of the expected result\n");
    
    for(int i=0; i < 25; i++) {
        int sum = random_number(max_sum);
        int target = sum;
        int count;
        int term;
        char *s = Buffer;
        for(count=-1; target>0; target-=term) {
            count++;
            term = random_number(max_term_value);
            if (term > target) 
                term = target;
            int size = random_number(1);
            sprintf(s,"%*0d",size,term);
            s += strlen(s);
        }
        if (strlen(Buffer) < 1000) {
            printf("<- %s=%d\n", Buffer, sum);
            printf("-> %d\n", count);
        }
    }
}
