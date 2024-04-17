import multiprocessing
import os

def collatz_conjecture(n):
    if n == 0:
        return 0
    elif n % 2 == 0:
        return n // 2
    else:
        return 3 * n + 1

def collatz_sequence(start, end, step):
    sequence = []
    for i in range(start, end+1, step):
        current = i
        while current != 1:
            current = collatz_conjecture(current)
            sequence.append(current)
    return (sequence, len(sequence))

def main():
    num_processes = os.cpu_count()
    start = 2
    end = 1023
    step = (end - start) // num_processes
    longest_step_count = 0
    winning_number = None
    previous_winner = None
    
    while True:
        with multiprocessing.Pool(num_processes) as pool:
            futures = []
            for i in range(start, end+1, step):
                future = pool.apply_async(collatz_sequence, args=(i, min(i + step - 1, end), step))
                futures.append(future)

            results = [future.get() for future in futures]

            for sequence, steps in results:
                if steps > longest_step_count:
                    longest_step_count = steps
                    winning_number = sequence[0]
                    
        if winning_number == previous_winner:
            print(".", end="")
        else:
            print(f"\nNumber with the most steps so far: {winning_number} with {longest_step_count} steps ", end="")
            previous_winner = winning_number
        start = end+1
        end   = end + 1024

if __name__ == "__main__":
    main()
