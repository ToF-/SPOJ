#include "hashit.h" // this line to be ommitted when building SPOJ.c
#include "assert.h"
#include "stdbool.h"
#include "string.h"
#include "stdio.h"
#define MAX_LINE 256
#define EMPTY '~'


bool is_empty(int position, struct hash_table *hash_table) {
    return hash_table->items[position][0] == EMPTY;
}

int hash(char *key) {
    char *s = key;
    int h = 0;
    for(int i=1; *s != '\0'; i++, s++) {
        h += *s * i;
    }
    return (19 * h) % SIZE;
}

int next_position(int position, struct hash_table *hash_table) {
    for(int j=0; j < 20; j++) {
        int next = (position + j*j + 23 * j) % SIZE;
        if (is_empty(next, hash_table)) {
            return next;
        }
    }
    return NOT_FOUND;
}

void initialize(struct hash_table *hash_table) {
    hash_table->nb_keys = 0;
    for(int position=0; position<SIZE; position++) {
        hash_table->items[position][0] = EMPTY;
    }

}
int nb_keys(struct hash_table *hash_table) {
    return hash_table->nb_keys;
}

int find_key(char *key, struct hash_table *hash_table) {
    int position = hash(key);
    for(int j=0; j < 20; j++) {
        int next = (position + j*j + 23 * j) % SIZE;
        if (strcmp(hash_table->items[next],key) == 0) {
            return next;
        }
    }
    return NOT_FOUND;
}

void add_key(char *key, struct hash_table *hash_table) {
    if (find_key(key, hash_table) == NOT_FOUND) {
        int position = next_position(hash(key), hash_table);
        if (position != NOT_FOUND) {
            assert(strlen(key) <= KEY_SIZE);
            strcpy(hash_table->items[position], key);
            hash_table->nb_keys++;
        }
    }
}
void delete_key(char *key, struct hash_table *hash_table) {
    int position = find_key(key, hash_table);
    if (position != NOT_FOUND) {
        hash_table->items[position][0] = EMPTY;
        hash_table->nb_keys--;
    }
}

void operation(char *command, struct hash_table *hash_table) {
    if (command[0] == 'A') {
        add_key(&command[4], hash_table);
    } else {
        delete_key(&command[4], hash_table);
    }
}

int get_int(char *line) {
    int result;
    fgets(line, MAX_LINE, stdin);
    sscanf(line, "%d", &result);
    return result;
}

void get_operation(char *line) {
    static char buffer[MAX_LINE+1];
    fgets(buffer, MAX_LINE, stdin);
    sscanf(buffer, "%s", line);
}

void print_hash_table(struct hash_table *hash_table) {
    printf("%d\n", hash_table->nb_keys);
    for(int position=0; position<SIZE; position++) {
        if (!is_empty(position,hash_table)) {
            printf("%d:%s\n", position, hash_table->items[position]);
        }
    }
}
void process() {
    struct hash_table hash_table;
    static char line[MAX_LINE+1];
    int max_tests = get_int(line);
    for(int i=0; i<max_tests; i++) {
        int max_operations = get_int(line);
        initialize(&hash_table);
        for(int i=0; i<max_operations; i++) {
            get_operation(line);
            operation(line, &hash_table);
        }
        print_hash_table(&hash_table);
    }
}

