
#define MAX_HEAP (183*183)
#define MAX_SIZE 183

char Bitmap[MAX_SIZE][MAX_SIZE];
char Distances[MAX_SIZE][MAX_SIZE];

int Width;
int Height;

struct node {
    int x;
    int y;
    int distance;
};

struct heap {
    int capacity;
    int size;
    struct node *values;
};


void insert(struct heap*, struct node);

struct node pop(struct heap *);

struct heap *create_heap(int);
void empty_heap(struct heap *);
void destroy_heap(struct heap *);

void init_distances(int, int);
void calc_distances(char *, char *, struct heap *, int, int);
