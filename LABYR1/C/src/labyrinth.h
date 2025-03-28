#include <stdbool.h>
#include <stdio.h>
#define N 1024
#define BITS_PER_UNSIGNED_LONG (sizeof(unsigned long))
#define BITSET_SIZE (N*N)

struct bitset {
    unsigned long *bits;
};

struct labyrinth {
    int size_x;
    int size_y;
    int end_x;
    int end_y;
    int rope_length;
    struct bitset *cells;
};

struct bitset *new_bitset();
void free_bitset(struct bitset *);
void init_biset(struct bitset *);
void include(struct bitset *, unsigned long);
bool included(struct bitset *, unsigned long);
struct labyrinth *new_labyrinth();
void init_labyrinth(struct labyrinth *);
void free_labyrinth(struct labyrinth *);
void add_line(struct labyrinth *, char *);
int rope_length(struct labyrinth *);
int process_test_case(FILE *);
void process_test_cases(FILE *);
