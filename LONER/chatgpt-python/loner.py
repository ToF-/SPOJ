import sys
import threading

def main():
    t = int(sys.stdin.readline())
    for _ in range(t):
        n = int(sys.stdin.readline())
        s = sys.stdin.readline().strip()
        initial = int(s, 2)
        stack = [initial]
        visited = set()
        found = False

        while stack:
            state = stack.pop()
            if state in visited:
                continue
            visited.add(state)
            if bin(state).count('1') == 1:
                found = True
                break
            for i in range(n):
                # move right
                if i + 2 < n:
                    if ((state >> i) & 1) and ((state >> (i + 1)) & 1) and not ((state >> (i + 2)) & 1):
                        new_state = state & ~(1 << i) & ~(1 << (i + 1)) | (1 << (i + 2))
                        stack.append(new_state)
                # move left
                if i - 2 >= 0:
                    if ((state >> i) & 1) and ((state >> (i - 1)) & 1) and not ((state >> (i - 2)) & 1):
                        new_state = state & ~(1 << i) & ~(1 << (i - 1)) | (1 << (i - 2))
                        stack.append(new_state)

        print("yes" if found else "no")

threading.Thread(target=main).start()

