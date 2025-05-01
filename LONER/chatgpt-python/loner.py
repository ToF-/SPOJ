import sys
import threading

def main():
    t = int(sys.stdin.readline())

    for _ in range(t):
        n = int(sys.stdin.readline())
        s = sys.stdin.readline().strip()
        init = int(s, 2)
        visited = set()
        stack = [init]
        found = False

        while stack:
            state = stack.pop()
            if state in visited:
                continue
            visited.add(state)
            if bin(state).count('1') == 1:
                found = True
                break
            for i in range(n - 2):
                # saut à droite
                if (state >> i) & 0b111 == 0b110:
                    new_state = state & ~(1 << i) & ~(1 << (i + 1)) | (1 << (i + 2))
                    if new_state not in visited:
                        stack.append(new_state)
            for i in range(2, n):
                # saut à gauche
                if ((state >> (i - 2)) & 0b111) == 0b011:
                    new_state = state & ~(1 << i) & ~(1 << (i - 1)) | (1 << (i - 2))
                    if new_state not in visited:
                        stack.append(new_state)

        print("yes" if found else "no")

threading.Thread(target=main).start()

