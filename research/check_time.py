from time import time, process_time, clock

def check1():
    st = time()
    summ = 0
    for i in range(50000000):
        summ += 1
    return time() - st

def check2():
    st = process_time()
    summ = 0
    for i in range(50000000):
        summ += 1
    return process_time() - st
