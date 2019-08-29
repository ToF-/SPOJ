#include <string.h>
#define MAXLINE 80
#define MAXCITIES 10001
#define MAXNAME 11

char Line[80];
char Cities[MAXCITIES][MAXNAME];

int get_int(char *line) {
    int result;
    fgets(line, MAXLINE, stdin);
    return result;
}

int get_str(char *line) {
    fgets(line, MAXLINE, stdin);
    return strlen(line);
}

void get_city(char *line, int city_number) {
    assert(city_number <= MAXCITIES);
    fgets(line, MAXLINE, stdin);
    sscanf(line, "%s", Cities[city_number]);
}

int lookup_city(char *city) {
    for (int i=1; i < MAXCITIES; i++) 
        if (!strcmp(city, Cities[i]))
            return i;
    return 0;
}

int get_two_Cities(char *line, int *a, int *b) {
    fgets(line, MAXLINE, stdin);
    char name_a[MAXNAME];
    char name_b[MAXNAME];
    sscanf(line, "%s %s", name_a, name_b);
    *a = lookup_city(name_a);
    assert(*a != 0);
    *b = lookup_city(name_b);
    assert(*b != 0);
    return 0;
}

void get_vertex_and_distance(Line)
int main() {
    int max_tests = get_int(Line);
    for(int i=0; i < max_tests; i++) {
        int max_vertices = get_int(Line);
        for(int j=1; j<=max_vertices; j++) {
            get_city(Line, j);
            int max_edges = get_int(Line);
            for(int i=0; i<max_edges; i++) {
                get_vertex_and_distance(Line);
                
        }
}
    struct path *path = create_path(10);
    struct heap *heap = create_heap(10000); 
    struct graph *graph = create_graph();
    add_edge(graph, 0, 1, 7);
    add_edge(graph, 0, 2, 9);
    add_edge(graph, 0, 5, 14);
    add_edge(graph, 1, 2, 10);
    add_edge(graph, 1, 3, 15);
    add_edge(graph, 2, 3, 11);
    add_edge(graph, 2, 5, 2);
    add_edge(graph, 3, 4, 6);
    add_edge(graph, 4, 5, 9);
    dijkstra(graph, 0, 4, path);
    printf("%d steps, distance:%d  ", path->size, path->total);
    for(int i=0; i < path->size; i++) 
        printf("%d ", path->steps[i]);
    destroy_graph(graph);
    destroy_path(path);
    return 0;
}
