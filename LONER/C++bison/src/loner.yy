%require "3.2"
%language "c++"


%define api.value.type variant
%define api.token.constructor

%code
{
#include <ctype.h>

    namespace yy
    {
        // reads the next token
        auto yylex() -> parser::symbol_type
        {
            static int line_count = 0;

            int c = getchar();
            while (c == ' ' || c == '\t')
                c = getchar();
            if (line_count % 2 == 1 || line_count == 0) {
                // ignore input on anything else than board lines
                return parser::token::IGNORE;

            }
            else if (c == EOF)
                return parser::token::YYEOF;
            else if (c == '0')
                return parser::token::SQUARE;
            else if (c == '1')
                return parser::token::PAWN;
            else if (c == '\n')
            {
                line_count++;
                return parser::token::NEWLINE;
            }
            return c;
        }
    }
}
%%

result:
      %empty
      | IGNORE { std::cout << "ignore" << '\n'; }
;

%token SQUARE;
%token PAWN;
%token IGNORE;
%token NEWLINE;

%%
namespace yy
{
    auto parser::error(const std::string &msg) -> void
    {
        std::cerr << msg << '\n';
    }
}

int main()
{
    yy::parser parse;
    return parse();
}

      




