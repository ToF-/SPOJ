#define SIZE 101
#define KEY_SIZE 16
#define NOT_FOUND -1

struct hash_table {
    int nb_keys;
    char items[SIZE][KEY_SIZE];
};

void initialize(struct hash_table *);
int nb_keys(struct hash_table *);
int find_key(char *, struct hash_table *);
void add_key(char *, struct hash_table *);
void delete_key(char *, struct hash_table *);
void operation(char *, struct hash_table *);
void print_hash_table(struct hash_table *);
