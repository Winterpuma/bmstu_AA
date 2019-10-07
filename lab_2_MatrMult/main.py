# АА Лабораторная2 Умножение матриц
# Стандартное, Винограда, оптимизированный Винограда

def matr_mult(m1, m2):
    l = len(m1)
    res = [[0 for i in range(l)] for j in range(l)]
    
    for i in range(l):
        for j in range(l):
            for k in range(l):
                res[i][j] += m1[i][k] * m2[k][j]
    return res

m1 = [[1,4], [5, 8]]
m2 = [[1,4], [5, 8]]
