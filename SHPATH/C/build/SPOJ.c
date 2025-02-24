int doit();

#include "shpath.h"

int doit() {
    return 42;
}
#include <stdio.h>
#include "shpath.h"


int main(int argc, char *argv[]) {
  printf("%d\n", doit());
}
