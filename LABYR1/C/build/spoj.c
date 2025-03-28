#include <stdbool.h>
#include <stdio.h>
#define N 1000
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
void exclude(struct bitset *, unsigned long);
bool included(struct bitset *, unsigned long);
struct labyrinth *new_labyrinth();
void init_labyrinth(struct labyrinth *);
void free_labyrinth(struct labyrinth *);
void add_line(struct labyrinth *, char *);
int rope_length(struct labyrinth *);
int process_test_case(FILE *);
void process_test_cases(FILE *);
#include <assert.h>
#include <stdlib.h>
#include <stdio.h>
#include <stdbool.h>

struct bitset *new_bitset() {
    struct bitset *result = (struct bitset *)calloc(1, sizeof(struct bitset));
    result->bits = (unsigned long *)calloc(BITSET_SIZE, sizeof(unsigned long));
    return result;
}

void free_bitset(struct bitset *bitset) {
    free(bitset->bits);
    free(bitset);
}

void init_bitset(struct bitset *bitset) {
    for(int i = 0; i < BITSET_SIZE; i++)
        bitset->bits[i] = 0;
}

void include(struct bitset *bitset, unsigned long n) {
    int pos = n / sizeof(unsigned long);
    unsigned long mask = 1 << n % sizeof(unsigned long);
    bitset->bits[pos] |= mask;
}

void exclude(struct bitset *bitset, unsigned long n) {
    int pos = n / sizeof(unsigned long);
    unsigned long mask = 1 << n % sizeof(unsigned long);
    mask ^= -1;
    bitset->bits[pos] &= mask;
}

bool included(struct bitset *bitset, unsigned long n) {
    int pos = n / sizeof(unsigned long);
    unsigned long mask = 1 << n % sizeof(unsigned long);
    return (bitset->bits[pos] & mask) > 0;
}

struct labyrinth *new_labyrinth() {
    struct labyrinth *result = malloc(sizeof(struct labyrinth));
    result->cells = new_bitset();
    result->size_x = result->size_y = 0;
    return result;
}

void init_labyrinth(struct labyrinth *labyrinth) {
    init_bitset(labyrinth->cells);
}

void free_labyrinth(struct labyrinth *labyrinth) {
    free(labyrinth->cells);
    free(labyrinth);
}

void add_line(struct labyrinth *labyrinth, char *line) {
    int row = labyrinth->size_y;
    int col;
    char *s = line;
    char c;
    for(col = 0; (c = *line++); col++) {
        if(c != '#') {
            include(labyrinth->cells, row * N + col);
        }
    }
    if(col > labyrinth->size_x) {
        labyrinth->size_x = col;
    }
    labyrinth->size_y++;
}

bool free_cell(struct labyrinth *labyrinth, int row, int col) {
    return col >= 0 && col < labyrinth->size_x
        && row >= 0 && row < labyrinth->size_y
        && included(labyrinth->cells, row * N + col);
}

void depth_first_search(struct labyrinth *labyrinth, struct bitset *visited, int x, int y, int length) {
    int next_x, next_y;
    int dir_x[] = {-1, 1, 0, 0}, dir_y[] = {0, 0, -1, 1}; 
    include(visited, y*N+x);
    if(length > labyrinth->rope_length) {
        labyrinth->rope_length = length;
        labyrinth->end_x = x;
        labyrinth->end_y = y;
    }
    for(int d = 0; d < 4; d++) {
        next_x = x + dir_x[d];
        next_y = y + dir_y[d];
        if(! free_cell(labyrinth, next_x, next_y) || included(visited, next_y*N+next_x))
                continue;
        depth_first_search(labyrinth, visited, next_x, next_y, length+1);
    }
    exclude(visited, y*N+x);
    assert(!included(visited, y*N+x));
}

void find_first_free_cell(struct labyrinth *labyrinth, int *x, int *y) {
    for(int row=0; row < labyrinth->size_y; row++) {
        for(int col=0; col < labyrinth->size_x; col++) {
            if(included(labyrinth->cells, row*N+col)) {
                *x = col;
                *y = row;
                return;
            }
        }
    }
}

void print_labyrinth(struct labyrinth *labyrinth) {
    printf("\n");
    for(int row=0; row<labyrinth->size_y; row++) {
        for(int col=0; col<labyrinth->size_x; col++) {
            if(free_cell(labyrinth, row, col)) {
                printf(".");
            } else {
                printf("#");
            }
        }
        printf("\n");
    }
}

int rope_length(struct labyrinth *labyrinth) {
    int start_x;
    int start_y;
    find_first_free_cell(labyrinth, &start_x, &start_y);
    struct bitset *visited = new_bitset();
    labyrinth->rope_length = 0;
    init_bitset(visited);
    depth_first_search(labyrinth, visited, start_x, start_y, 0);
    init_bitset(visited);
    labyrinth->rope_length = 0;
    depth_first_search(labyrinth, visited, labyrinth->end_x, labyrinth->end_y, 0);
    free(visited);
    return labyrinth->rope_length;
}

int process_test_case(FILE *file) {
    char line[N];
    char sep[2];
    int cols,rows;
    fscanf(file, "%d%1[ \t]%d", &cols, sep, &rows);

    struct labyrinth *labyrinth = new_labyrinth();
    init_labyrinth(labyrinth);
    for(int i=0; i<rows; i++) {
        fscanf(file, "%s\n", line);
        add_line(labyrinth, line);
    }
    int result = rope_length(labyrinth);
    free(labyrinth);
    return result;
}

void process() {
    int cases;
    fscanf(stdin, "%d", &cases);
    for(int i=0; i<cases; i++) {
        int result = process_test_case(stdin);
        printf("Maximum rope length is %d.\n", result);
    }
}

int main(int argc, char* argv[]) { process(); return 0; }
