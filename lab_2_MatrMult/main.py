# АА Лабораторная2 Умножение матриц
# Стандартное, Винограда, оптимизированный Винограда

from random import randint

def MatrMult(m1, m2):
    r2 = len(m2)
    c1 = len(m1[0])
    if r2 != c1:
        return

    r1 = len(m1)
    c2 = len(m2[0])
    
    res = [[0 for i in range(c2)] for j in range(r1)]

    for i in range(r1):
        for j in range(c2):
            for k in range(c1):
                res[i][j] += m1[i][k] * m2[k][j]
    return res

def Gen(n):
    return [[randint(0, 9) for i in range(n)] for j in range(n)]

def OutTbl(table):
    for i in range(len(table)):
        for j in range(len(table[i])):
            print("{0:2}".format(table[i][j]), end = " ")
        print()
    print()

m1 = [[1,4], [5, 8]]
m2 = [[1,4,3], [5, 8, 3]]
