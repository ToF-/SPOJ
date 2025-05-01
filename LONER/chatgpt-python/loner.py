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
        success = False

        while stack:
            state = stack.pop()
            if state in visited:
                continue
            visited.add(state)

            if bin(state).count('1') == 1:
                success = True
                break

            for i in range(n):
                # saut vers la droite : 1 1 0 -> 0 0 1
                if i + 2 < n:
                    if ((state >> i) & 0b111) == 0b011:
                        new_state = state & ~(1 << i) & ~(1 << (i + 1)) | (1 << (i + 2))
                        stack.append(new_state)
                # saut vers la gauche : 0 1 1 -> 1 0 0
                if i - 2 >= 0:
                    if ((state >> (i - 2)) & 0b111) == 0b110:
                        new_state = state & ~(1 << i) & ~(1 << (i - 1)) | (1 << (i - 2))
                        stack.append(new_state)

        print("yes" if success else "no")

threading.Thread(target=main).start()

