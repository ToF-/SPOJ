import sys

# Limiter la profondeur de récursion
sys.setrecursionlimit(10000)

def can_win(s):
    n = len(s)
    initial = int(s, 2)
    visited = set([initial])
    stack = [initial]

    while stack:
        state = stack.pop()

        # Si l'état contient un seul pion, la partie est gagnée
        if bin(state).count('1') == 1:
            return True

        for i in range(n):
            # Saut vers la droite : 1 1 0 -> 0 0 1
            if i + 2 < n and ((state >> i) & 0b111) == 0b011:
                new_state = state & ~(1 << i) & ~(1 << (i + 1)) | (1 << (i + 2))
                if new_state not in visited:
                    visited.add(new_state)
                    stack.append(new_state)

            # Saut vers la gauche : 0 1 1 -> 1 0 0
            if i - 2 >= 0 and ((state >> (i - 2)) & 0b111) == 0b110:
                new_state = state & ~(1 << i) & ~(1 << (i - 1)) | (1 << (i - 2))
                if new_state not in visited:
                    visited.add(new_state)
                    stack.append(new_state)

    return False

def main():
    t = int(sys.stdin.readline())
    for _ in range(t):
        n = int(sys.stdin.readline())
        s = sys.stdin.readline().strip()

        # Appel à la fonction pour résoudre chaque cas
        if can_win(s):
            print("yes")
        else:
            print("no")

main()

