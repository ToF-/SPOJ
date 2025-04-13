%{
#include <stdio.h>
#include "tab.h"
%}

%%

[0-9]+ { return NUMBER; }
[ \t] ;
\n  return 0;
.   return yytext[0];

%%
