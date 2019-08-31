#include <string.h>
#include <stdlib.h>
#define MAXLINE 80
#define MAXCITIES 10001
#define MAXNAME 11

char Line[80];

struct city {
    char name[MAXNAME];
    int index;
};

struct city Cities[MAXCITIES];

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
    fgets(line, MAXLINE, stdin);
    sscanf(line, "%s", Cities[city_number].name);
    Cities[city_number].index = city_number;
}

int compare_cities(const void *a, const void *b) {
    struct city *ca = (struct city *)a;
    struct city *cb = (struct city *)b;
    return strcmp(ca->name, cb->name);
}

void sort_cities(int max_cities) {
    qsort(Cities, max_cities, sizeof(struct city), compare_cities);
}

int lookup_city(char *key,int max_cities) {
    struct city *c=bsearch (key, Cities, max_cities, sizeof(struct city), compare_cities);
    return c->index;
}


void get_two_cities(char *line, int *a, int *b, int max_cities) {
    fgets(line, MAXLINE, stdin);
    char name_a[MAXNAME];
    char name_b[MAXNAME];
    sscanf(line, "%s %s", name_a, name_b);
    *a = lookup_city(name_a, max_cities);
    *b = lookup_city(name_b, max_cities);
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
            if (max_edges>0) {
                for(int i=0; i<max_edges; i++) {
                    int dest;
                    int distance;
                    get_vertex_and_distance(Line, &dest, &distance);
                    add_edge(g, node, dest, distance); 
                }
            } else {
                add_vertex(g, node);
            }
        }
        int start;
        int end;
        sort_cities(max_vertices);
        int distances = get_int(Line);
        for(int i=0; i<distances; i++) {
            get_two_cities(Line, &start, &end, max_vertices);
            struct path *path = create_path(max_vertices);
            if (dijkstra(g, start, end, path))
                printf("%d\n", path->total);
            else
                printf("%d\n", 0); 
            destroy_path(path);
        }
        destroy_graph(g);
        fgets(Line, MAXLINE, stdin);
    }
    return 0;
}
