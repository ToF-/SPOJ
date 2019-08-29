
int main() {
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
