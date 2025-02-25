#define MAX_VERTICE 10001
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
    char *name;
    struct edge **edges;
    int size;
    int capacity;
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
    int visited[MAX_BITSET];
    struct link *hash_table[MAX_VERTICE];
    struct queue *queue;
    int size;
};
struct graph *create_graph();
struct vertex *add_vertex(struct graph *, char *);
void add_edge(struct graph *, int, int, int);
void destroy_graph(struct graph *);
void init_visited(struct graph *);
int visited(struct graph *, int);
void visit(struct graph *, int);
struct vertex *find_vertex(struct graph *, char *);
void update(struct queue *, struct vertex *, int);
struct record* extract_min(struct queue *);
