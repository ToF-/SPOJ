#include "hashit.h" // this line to be ommitted when building SPOJ.c

int hash(char *);

int hash(char *key) {
    char *s = key;
    int h = 0;
    for(int i=1; *s != '\0'; i++, s++) {
        h += *s * i;
    }
    return (19 * h) % SIZE;
}

void initialize(struct hash_table *hash_table) {
    hash_table->nb_keys = 0;
    for(int position=0; position<SIZE; position++) {
        hash_table->items[position][0] = '\0';
    }

}
int nb_keys(struct hash_table *hash_table) {
    return hash_table->nb_keys;
}

int find_key(char *key, struct hash_table *hash_table) {
    int position = hash(key);
    if (hash_table->items[position][0] == *key)
        return position;
    return -1;
}

void add_key(char *key, struct hash_table *hash_table) {
    int position = hash(key);
    hash_table->items[position][0] = *key;
    hash_table->nb_keys++;
}
void delete_key(char *key, struct hash_table *hash_table) {
    int position = hash(key);
    hash_table->items[position][0] = '\0';
    hash_table->nb_keys--;
}
