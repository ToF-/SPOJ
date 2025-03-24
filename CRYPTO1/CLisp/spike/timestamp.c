#include <stdio.h>
#include <time.h>

int main(void) {
    const char default_format[] = "%a %b %d %T %Y";
    const char *format = default_format;
    char buffer[80];
    time_t rawtime = (int)time(NULL);
    struct tm *info = gmtime(&rawtime);
    printf("timestamp: %d\n", (int)time(NULL));
    strftime(buffer, sizeof(buffer), format, info);
    printf("%s\n", buffer);
    return 0;
}
