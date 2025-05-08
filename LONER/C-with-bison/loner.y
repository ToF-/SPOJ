/* loner problem */

%{
    #include <stdio.h>
    #include <stdlib.h>
    int yylex(void);
    void yyerror(char const *);
    static int line_count = 0;
    static int winnable;
    static int count = 0;
    static int test_count = 0;
%}

%define api.value.type { int }
%token COUNT
%token E
%token P
%token END

%%

input:
     COUNT '\n'    { printf("TEST_CASES\n"); }
     boards
     ;

boards:
    %empty
    | boards COUNT '\n' { winnable = 1; printf("BOARD_SIZE\n"); }
    board               
    ;

board:
    win '\n'                            { printf("yes\n"); }
  | lose  '\n'                          { printf("no\n"); }
    
win:
   P 
  | P P E
  | e_plus P P E
  | e_plus P P E e_plus
  | e_plus E P P e_plus
  ;

e_plus:
  %empty
  | E e_plus        { printf("0+\n"); }
  ;

lose:
  %empty
  | E lose
  | P lose
  ;

%%

#include <ctype.h>
#include <stdlib.h>

int yylex(void)
{
    int c = getchar();
    printf("line %d %d\n", line_count,c);
    if(c != '\n' && (line_count == 0 || line_count % 2 == 1))
    {
        count = count * 10 + c - '0';
        return COUNT;
    }
    else
    {
        switch(c)
        {
            case '0': return E;
            case '1': return P;
            case '\n':
            {
                line_count++;
                count = 0;
                return c;
            }
        }
    }
    return c;
}

int main()
{
    return yyparse();
}

#include <stdio.h>
void yyerror(char const *s)
{
    fprintf(stderr, "%s\n", s);
}

