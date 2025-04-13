%{
#include <stdio.h>
int yyerror(const char *);
int yylex();
%}
%token NUMBER
%%

parameter: NUMBER
         ;

%%
int yyerror(const char *str) {
    fprintf(stderr, "syntax error:%s\n", str);
    return 1;
}

