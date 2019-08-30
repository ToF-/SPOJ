#include <string.h>
#define MAXLINE 80
#define MAXCITIES 10001
#define MAXNAME 11

char Line[80];
char Cities[MAXCITIES][MAXNAME];

int get_int(char *line) {
    int result;
    fgets(line, MAXLINE, stdin);
    sscanf(line, "%d", &result);
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

void get_two_cities(char *line, int *a, int *b) {
    fgets(line, MAXLINE, stdin);
    char name_a[MAXNAME];
    char name_b[MAXNAME];
    sscanf(line, "%s %s", name_a, name_b);
    *a = lookup_city(name_a);
    *b = lookup_city(name_b);
}

void get_vertex_and_distance(char *line, int *node, int *distance) {
    fgets(line, MAXLINE, stdin);
    int n;
    int d;
    sscanf(line, "%d %d", &n, &d);
    *node=n-1;
    *distance=d;
}

int main() {
    int max_tests = get_int(Line);
    for(int i=0; i < max_tests; i++) {
        
        struct graph *g = create_graph();
        int max_vertices = get_int(Line);
        for(int node=0; node<max_vertices; node++) {
            get_city(Line, node);
            int max_edges = get_int(Line);
            for(int i=0; i<max_edges; i++) {
                int dest;
                int distance;
                get_vertex_and_distance(Line, &dest, &distance);
                assert(dest>=0 && dest<max_vertices);
                add_edge(g, node, dest, distance); 
            }
        }
        int start;
        int end;
        int distances = get_int(Line);
        for(int i=0; i<distances; i++) {
            get_two_cities(Line, &start, &end);
            struct path *path = create_path(max_vertices);
            if (dijkstra(g, start, end, path))
                printf("%d\n", path->total);
            else
                printf("%d\n", 0);
            destroy_path(path);
        }
        destroy_graph(g);
    }
    return 0;
}
