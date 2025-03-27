#include "labyrinth.h"
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
        if(c == '.') {
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
void depth_first_search(struct labyrinth *labyrinth, int start_x, int start_y) {
    struct bitset *visited = new_bitset();
    int *stack = (int *)malloc(N*N*sizeof(int));
    labyrinth->rope_length = 0;
    int sp = 0;
    init_bitset(visited);
    stack[sp++] = start_x;
    stack[sp++] = start_y;
    stack[sp++] = 0;
    while(sp) {
        int rope_length = stack[--sp];
        int y = stack[--sp];
        int x = stack[--sp];
        include(visited, y * N + x);
        if(rope_length > labyrinth->rope_length) {
            labyrinth->rope_length = rope_length;
            labyrinth->end_x = x;
            labyrinth->end_y = y;
        }
        if(free_cell(labyrinth, y-1, x) && !included(visited,(y-1)*N+x)) {
            stack[sp++] = x;
            stack[sp++] = y-1;
            stack[sp++] = rope_length+1;
        }
        if(free_cell(labyrinth, y+1, x) && !included(visited,(y+1)*N+x)) {
            stack[sp++] = x;
            stack[sp++] = y+1;
            stack[sp++] = rope_length+1;
        }
        if(free_cell(labyrinth, y, x-1) && !included(visited,(y)*N+x-1)) {
            stack[sp++] = x-1;
            stack[sp++] = y;
            stack[sp++] = rope_length+1;
        }
        if(free_cell(labyrinth, y, x+1) && !included(visited,(y)*N+x+1)) {
            stack[sp++] = x+1;
            stack[sp++] = y;
            stack[sp++] = rope_length+1;
        }
    }
    free(stack);
    free(visited);
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
int rope_length(struct labyrinth *labyrinth) {
    int start_x;
    int start_y;
    find_first_free_cell(labyrinth, &start_x, &start_y);
    depth_first_search(labyrinth, start_x, start_y);
    depth_first_search(labyrinth, labyrinth->end_x, labyrinth->end_y);
    return labyrinth->rope_length;
}
