struct vertex {
    struct edge **edges;
    int size;
    int capacity;
    int distance;
    int prev_vertex;
    int visited;
}

struct graph {
    struct vertex **vertice;
    int size;
    int capacity;
}
struct graph *create_graph();
void add_vertex(struct graph *, int);
void add_edge(struct graph *, int, int, int);
void destroy_graph(struct graph *);
