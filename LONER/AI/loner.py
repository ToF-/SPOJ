def can_win(board_str):
    n = len(board_str)

    # Convert board string to bitmask integer
    def to_mask(s):
        return int(s, 2)

    def count_bits(x):
        return bin(x).count('1')

    def get_bit(mask, i):
        return (mask >> i) & 1

    def set_bit(mask, i, val):
        if val:
            return mask | (1 << i)
        else:
            return mask & ~(1 << i)

    start = to_mask(board_str)
    visited = set()
    stack = [start]

    while stack:
        state = stack.pop()
        if state in visited:
            continue
        visited.add(state)

        if count_bits(state) == 1:
            return "yes"

        for i in range(n):
            # Right jump: 1(i),1(i+1),0(i+2) → 0(i),0(i+1),1(i+2)
            if i + 2 < n and get_bit(state, i) and get_bit(state, i + 1) and not get_bit(state, i + 2):
                new_state = state
                new_state = set_bit(new_state, i, 0)
                new_state = set_bit(new_state, i + 1, 0)
                new_state = set_bit(new_state, i + 2, 1)
                stack.append(new_state)

            # Left jump: 0(i-2),1(i-1),1(i) → 1(i-2),0(i-1),0(i)
            if i - 2 >= 0 and not get_bit(state, i - 2) and get_bit(state, i - 1) and get_bit(state, i):
                new_state = state
                new_state = set_bit(new_state, i, 0)
                new_state = set_bit(new_state, i - 1, 0)
                new_state = set_bit(new_state, i - 2, 1)
                stack.append(new_state)

    return "no"

# Driver
t = int(input())
for _ in range(t):
    n = int(input())
    board = input().strip()
    print(can_win(board))

