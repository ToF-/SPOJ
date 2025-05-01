import sys
import threading

def main():
    import sys
    sys.setrecursionlimit(1000000)

    t = int(sys.stdin.readline())

    for _ in range(t):
        n = int(sys.stdin.readline())
        s = sys.stdin.readline().strip()

        init = int(s, 2)
        visited = set()

        def can_win(state):
            if state in visited:
                return False
            visited.add(state)
            if bin(state).count('1') == 1:
                return True
            for i in range(n):
                # déplacement vers la droite
                if i + 2 < n:
                    mask = (1 << i) | (1 << (i + 1))
                    if (state & mask) == mask and not (state & (1 << (i + 2))):
                        new_state = state & ~mask | (1 << (i + 2))
                        if can_win(new_state):
                            return True
                # déplacement vers la gauche
                if i - 2 >= 0:
                    mask = (1 << i) | (1 << (i - 1))
                    if (state & mask) == mask and not (state & (1 << (i - 2))):
                        new_state = state & ~mask | (1 << (i - 2))
                        if can_win(new_state):
                            return True
            return False

        print("yes" if can_win(init) else "no")

threading.Thread(target=main).start()

