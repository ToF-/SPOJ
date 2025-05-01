import sys
import threading

def main():
    sys.setrecursionlimit(1 << 25)
    t = int(sys.stdin.readline())

    for _ in range(t):
        n = int(sys.stdin.readline())
        s = sys.stdin.readline().strip()

        first = s.find('1')
        last = s.rfind('1')
        if first == -1:
            print("no")
            continue

        s = s[first:last+1]
        m = len(s)
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

            for i in range(m):
                # 1 1 0 -> 0 0 1
                if i + 2 < m:
                    if ((state >> i) & 0b111) == 0b011:
                        new_state = state & ~(1 << i) & ~(1 << (i + 1)) | (1 << (i + 2))
                        if new_state not in visited:
                            stack.append(new_state)
                # 0 1 1 -> 1 0 0
                if i >= 2:
                    if ((state >> (i - 2)) & 0b111) == 0b110:
                        new_state = state & ~(1 << i) & ~(1 << (i - 1)) | (1 << (i - 2))
                        if new_state not in visited:
                            stack.append(new_state)

        print("yes" if success else "no")

threading.Thread(target=main).start()

