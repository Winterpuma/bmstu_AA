# АА Лабораторная2 Умножение матриц
# Стандартное, Винограда, оптимизированный Винограда

def matr_mult(m1, m2):
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

m1 = [[1,4], [5, 8]]
m2 = [[1,4,3], [5, 8, 3]]
