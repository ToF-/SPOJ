#include <regex.h>
#include <stdlib.h>
#include <stdio.h>

regex_t regex;
char msgbuf[100];

char loner_a []="0*10*";
char loner_b []="0*1100*";
char loner_c1[]="0*(11)+010*";
char loner_c2[]="0*11(01)+0*";
char loner_c3[]="0*11(01)+(11)+010*";
char loner_d1[]="0*1100(10)+110*";
char loner_d2[]="0*1100(11)+0*";
char loner_d3[]="0*1100(11)+(10)+110*";
char loner_d4[]="0*11(01)+00110*";
char loner_d5[]="0*11(01)+00(10)+110*";
char loner_d6[]="0*11(01)+00(11)+0*";
char loner_d7[]="0*11(01)+00(11)+(10)+110*";
char loner_e1[]="0*111101(10)+110*";
char loner_e2[]="0*111101(11)+0*";
char loner_e3[]="0*111101(11)+(10)+110*";
char loner_e4[]="0*11(01)+1101110*";
char loner_e5[]="0*11(01)+1101(10)+110*";
char loner_e6[]="0*11(01)+1101(11)+0*";
char loner_e7[]="0*11(01)+1101(11)+(10)+110*";

int main(int argc, char const *argv[])
{

        char patterns[500];
        sprintf(patterns, "^(%s|%s|%s|%s|%s|%s|%s|%s|%s|%s|%s|%s|%s|%s|%s|%s|%s|%s|%s)$",
loner_a , loner_b , loner_c1, loner_c2, loner_c3, loner_d1, loner_d2, loner_d3, loner_d4, loner_d5, loner_d6, loner_d7, loner_e1, loner_e2, loner_e3, loner_e4, loner_e5, loner_e6, loner_e7);
        int reti = regcomp(&regex, patterns, REG_EXTENDED);
        if (reti) {
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
        /* Execute regular expression */
        reti = regexec(&regex, board, 0, NULL, 0);
        if (!reti) {
            puts("yes");
        }
        else if (reti == REG_NOMATCH) {
            puts("no");
        }
        else {
            regerror(reti, &regex, msgbuf, sizeof(msgbuf));
            fprintf(stderr, "Regex match failed: %s\n", msgbuf);
            exit(1);
        }
        free(board);
    }
    regfree(&regex);

}
