import sys
import threading
from collections import deque

def main():
    sys.setrecursionlimit(1 << 25)
    t = int(sys.stdin.readline())

    def extract_components(s):
        n = len(s)
        visited = [False] * n
        components = []

        for i in range(n):
            if s[i] == '1' and not visited[i]:
                queue = deque([i])
                visited[i] = True
                l, r = i, i
                while queue:
                    u = queue.popleft()
                    for v in [u - 1, u + 1]:
                        if 0 <= v < n and not visited[v]:
                            if s[v] == '1' or s[v] == '0':
                                visited[v] = True
                                queue.append(v)
                                l = min(l, v)
                                r = max(r, v)
                components.append(s[l:r+1])
        return components

    def can_win_segment(segment):
        n = len(segment)
        init = int(segment, 2)
        stack = [init]
        visited = set()
        while stack:
            state = stack.pop()
            if state in visited:
                continue
            visited.add(state)
            if bin(state).count('1') == 1:
                return True
            for i in range(n):
                if i + 2 < n and ((state >> i) & 1) and ((state >> (i + 1)) & 1) and not ((state >> (i + 2)) & 1):
                    new_state = state & ~(1 << i) & ~(1 << (i + 1)) | (1 << (i + 2))
                    stack.append(new_state)
                if i - 2 >= 0 and ((state >> i) & 1) and ((state >> (i - 1)) & 1) and not ((state >> (i - 2)) & 1):
                    new_state = state & ~(1 << i) & ~(1 << (i - 1)) | (1 << (i - 2))
                    stack.append(new_state)
        return False

    for _ in range(t):
        n = int(sys.stdin.readline())
        s = sys.stdin.readline().strip()
        components = extract_components(s)
        result = all(can_win_segment(seg) for seg in components)
        print("yes" if result else "no")

threading.Thread(target=main).start()

