#define SIZE 101
#define KEY_SIZE 16
struct hash_table {
    int nb_keys;
    char items[KEY_SIZE][SIZE];
};

void initialize(struct hash_table *);
int nb_keys(struct hash_table *);
int find_key(char *, struct hash_table *);
void add_key(char *, struct hash_table *);
void delete_key(char *, struct hash_table *);
