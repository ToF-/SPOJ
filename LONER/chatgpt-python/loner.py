import sys
import threading

def main():
    import sys
    sys.setrecursionlimit(1000000)
    t = int(sys.stdin.readline())

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
                # right
                if i + 2 < n and ((state >> i) & 1) and ((state >> (i + 1)) & 1) and not ((state >> (i + 2)) & 1):
                    new_state = state & ~(1 << i) & ~(1 << (i + 1)) | (1 << (i + 2))
                    stack.append(new_state)
                # left
                if i - 2 >= 0 and ((state >> i) & 1) and ((state >> (i - 1)) & 1) and not ((state >> (i - 2)) & 1):
                    new_state = state & ~(1 << i) & ~(1 << (i - 1)) | (1 << (i - 2))
                    stack.append(new_state)
        return False

    for _ in range(t):
        n = int(sys.stdin.readline())
        s = sys.stdin.readline().strip()
        i = 0
        result = True
        while i < n:
            if s[i] == '0':
                i += 1
                continue
            j = i
            while j < n and s[j] != '0':
                j += 1
            segment = s[i:j]
            if not can_win_segment(segment):
                result = False
                break
            i = j
        print("yes" if result else "no")

threading.Thread(target=main).start()

