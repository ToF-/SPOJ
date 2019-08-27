# config.mk
CC = pcc
INCS =
LIBS =
CFLAGS = -O -g -std=c99 -pedantic -Wall ${INCS}
LDFLAGS = -static ${LIBS}
