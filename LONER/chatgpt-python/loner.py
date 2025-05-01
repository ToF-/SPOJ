import sys
import threading
from functools import lru_cache

def main():
    sys.setrecursionlimit(1 << 25)
    t = int(sys.stdin.readline())

    for _ in range(t):
        n = int(sys.stdin.readline())
        s = sys.stdin.readline().strip()
        init = int(s, 2)

        @lru_cache(None)
        def dfs(state):
            if bin(state).count('1') == 1:
                return True
            for i in range(n):
                # right jump
                if i + 2 < n:
                    if ((state >> i) & 1) and ((state >> (i+1)) & 1) and not ((state >> (i+2)) & 1):
                        new_state = state & ~(1 << i) & ~(1 << (i+1)) | (1 << (i+2))
                        if dfs(new_state):
                            return True
                # left jump
                if i - 2 >= 0:
                    if ((state >> i) & 1) and ((state >> (i-1)) & 1) and not ((state >> (i-2)) & 1):
                        new_state = state & ~(1 << i) & ~(1 << (i-1)) | (1 << (i-2))
                        if dfs(new_state):
                            return True
            return False

        print("yes" if dfs(init) else "no")

threading.Thread(target=main).start()

