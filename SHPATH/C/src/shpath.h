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
    int distance;
    vertex_id prev_vertex;
    int visited;
};

struct graph {
    struct vertex **vertice;
    int size;
    int capacity;
};
struct graph *create_graph();
struct vertex *add_vertex(struct graph *, char *);
void add_edge(struct graph *, int, int, int);
void destroy_graph(struct graph *);
