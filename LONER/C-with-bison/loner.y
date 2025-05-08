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
     COUNT '\n'    { test_count = count; }
     boards
     ;

boards:
    %empty
    | boards COUNT '\n' { winnable = 1; }
      board
    ;

board:
    win '\n'                            { printf("yes\n"); }
  ;
    
win:
   P 
  | e_plus P e_plus
  | e_plus P P E e_plus
  | e_plus E P P e_plus
  ;

e_plus:
  %empty
  | e_plus E         { printf("0+\n"); }
  ;

lose:
    E lose
  | P lose
  | %empty
  ;

%%

#include <ctype.h>
#include <stdlib.h>

int yylex(void)
{
    int c = getchar();
    if(c==EOF) {
        exit(0);
    }
    if(c=='\n')
    {
        line_count++;
        if(test_count > 0 && line_count / 2 > test_count) exit(0);
        return c;
    }
    if(c != '\n' && (line_count == 0 || line_count % 2 == 1))
    {
        count = 0;
        while(c != '\n')
        {
            count = count * 10 + c - '0';
            c = getchar();
        }
        ungetc(c, stdin);
        return COUNT;
    }
    else
    {
        switch(c)
        {
            case '0': return E;
            case '1': return P;
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

