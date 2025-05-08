#include <regex.h>
#include <string.h>
#include <stdlib.h>
#include <stdio.h>
#include <stdbool.h>


#define NB_PATTERNS 38
#define PATTERN_BUF_SIZE 1024

char* patterns[] = { 
    "^0*(1",
    "|110",
    "|(11)+01",
    "|11(01)+",
    "|11(01)+(11)+01",
    "|1100(10)+11",
    "|1100(11)+",
    "|1100(11)+(10)+11",
    "|11(01)+0011",
    "|11(01)+00(10)+11",
    "|11(01)+00(11)+",
    "|11(01)+00(11)+(10)+11",
    "|111101(10)+11",
    "|111101(11)+",
    "|111101(11)+(10)+11",
    "|11(01)+110111",
    "|11(01)+1101(10)+11",
    "|11(01)+1101(11)+",
    "|11(01)+1101(11)+(10)+11",
    "|011",
    "|10(11)+",
    "|(10)+11",
    "|10(11)+(10)+11",
    "|11(01)+0011",
    "|(11)+0011",
    "|11(01)+(11)+0011",
    "|1100(10)+11",
    "|11(01)+00(10)+11",
    "|(11)+00(10)+11",
    "|11(01)+(11)+00(10)+11",
    "|11(01)+101111",
    "|(11)+101111",
    "|11(01)+(11)+101111",
    "|111011(10)+11",
    "|11(01)+1011(10)+11",
    "|(11)+1011(10)+11",
    "|11(01)+(11)+1011(10)+11",
    ")0*$" };

bool match(regex_t *regex, char *board) {
    int result = regexec(regex, board, 0, NULL, 0);
    if (!result)
        return true;
    else if (result == REG_NOMATCH)
        return false;
    else {
        regerror(result, regex, board, sizeof(board));
        fprintf(stderr, "Regex match failed: %s\n", board);
        exit(1);
    }
}

int main(int argc, char const *argv[])
{
    regex_t regex;
    char loner_patterns[PATTERN_BUF_SIZE] = "\0";
    for(int p=0; p<NB_PATTERNS; p++) {
        strcat(loner_patterns, patterns[p]);
    }
    int result = regcomp(&regex, loner_patterns, REG_EXTENDED);
    if (result) {
        fprintf(stderr, "Could not compile regex\n");
        exit(1);
    }
    int t;
    scanf("%d", &t);
    while (t--) {
        int n;
        scanf("%d", &n);
        char *board = malloc((n + 1) * sizeof(char));
        scanf("%s", board);
        if(match(&regex, board)) {
            puts("yes");
        } else {
            puts("no");
        }
        free(board);
    }
    regfree(&regex);
}

