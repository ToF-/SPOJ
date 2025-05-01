import sys
import threading

def main():
    import sys
    sys.setrecursionlimit(1000000)

    t = int(sys.stdin.readline())

    for _ in range(t):
        n = int(sys.stdin.readline())
        board = sys.stdin.readline().strip()
        visited = set()

        def can_win(state):
            if state in visited:
                return False
            visited.add(state)

            if state.count('1') == 1:
                return True

            state_list = list(state)
            for i in range(n):
                # Move right: i, i+1 occupied and i+2 empty
                if i + 2 < n and state_list[i] == '1' and state_list[i+1] == '1' and state_list[i+2] == '0':
                    new_state = state_list[:]
                    new_state[i] = '0'
                    new_state[i+1] = '0'
                    new_state[i+2] = '1'
                    if can_win(''.join(new_state)):
                        return True
                # Move left: i, i-1 occupied and i-2 empty
                if i - 2 >= 0 and state_list[i] == '1' and state_list[i-1] == '1' and state_list[i-2] == '0':
                    new_state = state_list[:]
                    new_state[i] = '0'
                    new_state[i-1] = '0'
                    new_state[i-2] = '1'
                    if can_win(''.join(new_state)):
                        return True

            return False

        result = "yes" if can_win(board) else "no"
        print(result)

threading.Thread(target=main).start()

