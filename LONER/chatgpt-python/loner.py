import sys
import threading

def main():
    sys.setrecursionlimit(1 << 25)
    t = int(sys.stdin.readline())

    def solve_segment(seg):
        n = len(seg)
        init = int(seg, 2)
        visited = set()
        stack = [init]

        while stack:
            state = stack.pop()
            if state in visited:
                continue
            visited.add(state)
            if bin(state).count('1') == 1:
                return True
            for i in range(n):
                if i + 2 < n and ((state >> i) & 0b111) == 0b011:
                    new_state = state & ~(1 << i) & ~(1 << (i + 1)) | (1 << (i + 2))
                    stack.append(new_state)
                if i >= 2 and ((state >> (i - 2)) & 0b111) == 0b110:
                    new_state = state & ~(1 << i) & ~(1 << (i - 1)) | (1 << (i - 2))
                    stack.append(new_state)
        return False

    def extract_components(s):
        n = len(s)
        components = []
        i = 0
        while i < n:
            if s[i] == '0':
                i += 1
                continue
            l = i
            while i < n and (s[i] == '1' or s[i] == '0'):
                i += 1
            r = i
            # Extend range to include possible jumps
            l = max(0, l - 2)
            r = min(n, r + 2)
            components.append(s[l:r])
        return components

    for _ in range(t):
        n = int(sys.stdin.readline())
        s = sys.stdin.readline().strip()
        segments = extract_components(s)
        result = all(solve_segment(seg) for seg in segments)
        print("yes" if result else "no")

threading.Thread(target=main).start()

