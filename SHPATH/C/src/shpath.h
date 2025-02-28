#include <stdbool.h>
#define MAX_VERTICE 10001
#define MAX_LINE 256
#define MAX_BITS_INT (sizeof(int) * 8)
#define MAX_BITSET (1 + MAX_VERTICE / MAX_BITS_INT)
#define offset(x) (x / MAX_BITS_INT)
#define bit(x) ( x % MAX_BITS_INT)

typedef int vertex_id;

struct edge {
    vertex_id destination;
    int cost;
};

struct vertex {
    int id;
    char *name;
    struct edge **edges;
    int size;
    int capacity;
    bool visited;
    int priority_index;
    vertex_id prev_vertex;
};

struct link {
    struct vertex *data;
    struct link* next;
};

struct record {
    struct vertex *data;
    int priority;
};

struct queue {
    struct record **records;
    int size;
    int capacity;
};

struct graph {
    struct vertex *vertice[MAX_VERTICE];
    struct link *hash_table[MAX_VERTICE];
    struct queue *queue;
    int size;
};
struct graph *create_graph();
struct vertex *add_vertex(struct graph *, char *);
void add_edge(struct graph *, int, int, int);
void destroy_graph(struct graph *);
void init_visited(struct graph *);
bool visited(struct graph *, struct vertex*);
void visit(struct graph *, struct vertex*);
struct vertex *find_vertex(struct graph *, char *);
void update(struct queue *, struct vertex *, int);
void extract_min(struct queue *, struct vertex **, int *);
int path(struct graph *, int, int);
int get_int(char *);
void get_name(char *);
void get_two_ints(char *, int *, int *);
void get_two_strs(char *, char *, char *);
void process();
