
#define MAX_HEAP 10001

struct heap {
    int capacity;
    int size;
    int *values;
    int *keys;
    int *index;
};

struct edge {
    int vertex;
    int weight;
};

struct vertex {
    struct edge **edges;
    int size;
    int capacity;
    int distance;
    int prev;
    int visited;
};

struct graph {
    struct vertex **vertices;
    int size;
    int capacity;
};


void update(struct heap*, int, int);

int pop(struct heap*);
int dijkstra();

struct heap *create_heap(int);
void destroy_heap(struct heap *);

struct graph *create_graph();
void destroy_graph(struct graph *g);

void add_vertex(struct graph *, int);
void add_edge(struct graph *, int, int, int);

