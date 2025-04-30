#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <string.h>

#define MAX_BOARD_SIZE 1000

// Structure to represent the board
typedef struct {
    int size;
    int* squares;
} Board;

// Function to create a board with specified size
Board* createBoard(int size) {
    Board* board = (Board*)malloc(sizeof(Board));
    if (!board) {
        fprintf(stderr, "Memory allocation failed\n");
        exit(EXIT_FAILURE);
    }
    
    board->size = size;
    board->squares = (int*)malloc(size * sizeof(int));
    if (!board->squares) {
        fprintf(stderr, "Memory allocation failed\n");
        free(board);
        exit(EXIT_FAILURE);
    }
    
    return board;
}

// Function to free memory used by board
void freeBoard(Board* board) {
    free(board->squares);
    free(board);
}

// Function to count pawns on the board
int countPawns(const Board* board) {
    int count = 0;
    for (int i = 0; i < board->size; i++) {
        if (board->squares[i] == 1) {
            count++;
        }
    }
    return count;
}

// Function to check if any moves are possible
bool canMove(const Board* board) {
    for (int i = 0; i < board->size; i++) {
        if (board->squares[i] == 1) {
            // Check if this pawn can jump right
            if (i + 2 < board->size && board->squares[i + 1] == 1 && board->squares[i + 2] == 0) {
                return true;
            }
            
            // Check if this pawn can jump left
            if (i - 2 >= 0 && board->squares[i - 1] == 1 && board->squares[i - 2] == 0) {
                return true;
            }
        }
    }
    return false;
}

// Function to make a move from position 'from' to position 'to'
// Returns true if the move is valid and was made, false otherwise
bool makeMove(Board* board, int from, int to) {
    // Check if positions are valid
    if (from < 0 || from >= board->size || to < 0 || to >= board->size) {
        return false;
    }
    
    // Check if 'from' has a pawn and 'to' is empty
    if (board->squares[from] != 1 || board->squares[to] != 0) {
        return false;
    }
    
    // Calculate the middle position (the jumped pawn)
    int middle = (from + to) / 2;
    
    // Check if the positions are valid for a jump (distance of 2)
    if (abs(from - to) != 2) {
        return false;
    }
    
    // Check if there's a pawn to jump over
    if (board->squares[middle] != 1) {
        return false;
    }
    
    // Make the move
    board->squares[from] = 0;       // Remove pawn from start
    board->squares[middle] = 0;     // Remove jumped pawn
    board->squares[to] = 1;         // Place pawn at destination
    
    return true;
}

// Clone the board for recursive search
Board* cloneBoard(const Board* original) {
    Board* clone = (Board*)malloc(sizeof(Board));
    if (!clone) {
        fprintf(stderr, "Memory allocation failed\n");
        exit(EXIT_FAILURE);
    }
    
    clone->size = original->size;
    clone->squares = (int*)malloc(clone->size * sizeof(int));
    if (!clone->squares) {
        fprintf(stderr, "Memory allocation failed\n");
        free(clone);
        exit(EXIT_FAILURE);
    }
    
    for (int i = 0; i < original->size; i++) {
        clone->squares[i] = original->squares[i];
    }
    
    return clone;
}

// Recursive function to check if the board is winnable
bool isWinnable(Board* board) {
    // If only one pawn is left, the game is won
    int pawns = countPawns(board);
    if (pawns == 1) {
        return true;
    }
    
    // If no moves are possible and more than one pawn remains, the game is lost
    if (!canMove(board)) {
        return false;
    }
    
    // Try all possible moves
    for (int from = 0; from < board->size; from++) {
        if (board->squares[from] == 1) {
            // Try jumping right
            if (from + 2 < board->size && board->squares[from + 1] == 1 && board->squares[from + 2] == 0) {
                Board* newBoard = cloneBoard(board);
                makeMove(newBoard, from, from + 2);
                
                bool result = isWinnable(newBoard);
                freeBoard(newBoard);
                
                if (result) return true;
            }
            
            // Try jumping left
            if (from - 2 >= 0 && board->squares[from - 1] == 1 && board->squares[from - 2] == 0) {
                Board* newBoard = cloneBoard(board);
                makeMove(newBoard, from, from - 2);
                
                bool result = isWinnable(newBoard);
                freeBoard(newBoard);
                
                if (result) return true;
            }
        }
    }
    
    // If no sequence of moves leads to a win
    return false;
}

int main() {
    int numTestCases;
    scanf("%d", &numTestCases);
    
    // Create a buffer for reading the board
    char boardStr[MAX_BOARD_SIZE + 1];  // +1 for null terminator
    
    for (int t = 0; t < numTestCases; t++) {
        // Read the board as a string of '0' and '1' characters
        scanf("%s", boardStr);
        
        int boardSize = strlen(boardStr);
        
        // Create and initialize the board
        Board* board = createBoard(boardSize);
        
        // Convert the string to our board representation
        for (int i = 0; i < boardSize; i++) {
            board->squares[i] = (boardStr[i] == '1') ? 1 : 0;
        }
        
        // Check if the board is winnable
        bool winnable = isWinnable(board);
        
        // Print the result
        printf("%s\n", winnable ? "yes" : "no");
        
        // Free memory
        freeBoard(board);
    }
    
    return 0;
}
