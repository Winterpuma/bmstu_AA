# АА Лабораторная1 Расстояние Левенштейна 
# Л матрично, Д-Л матр, рек


def OutputTable(table, str1, str2):

    print("   ", end = " ")
    for i in str2:
        print(i, end = " ")

    for i in range(len(table)):
        if i:
            print("\n" + str1[i-1], end = " ")
        else:
            print("\n ", end = " ")
        for j in range(len(table[i])):
            print(table[i][j], end = " ")
    print("\n")


def LevRecursion(str1, str2):
    if str1 ==  '' or str2 == '':
        return abs(len(str1) - len(str2))
    forfeit = 0 if (str1[-1] == str2[-1]) else 1
    return min(LevRecursion(str1, str2[:-1]) + 1,
               LevRecursion(str1[:-1], str2) + 1,
               LevRecursion(str1[:-1], str2[:-1]) + forfeit)
    
  
def LevTable(str1, str2, output = False):
    len_i = len(str1) + 1
    len_j = len(str2) + 1
    table = [[i + j for j in range(len_j)] for i in range(len_i)]
    
    for i in range(1, len_i):
        for j in range(1, len_j):
            forfeit = 0 if (str1[i - 1] == str2[j - 1]) else 1
            table[i][j] = min(table[i - 1][j] + 1,
                              table[i][j - 1] + 1,
                              table[i - 1][j - 1] + forfeit)
    if output:        
        OutputTable(table, str1, str2)
    return(table[-1][-1])


def DamLevRecursion(str1, str2):
    if str1 ==  '' or str2 == '':
        return abs(len(str1) - len(str2))
    forfeit = 0 if (str1[-1] == str2[-1]) else 1
    res = min(DamLevRecursion(str1, str2[:-1]) + 1,
              DamLevRecursion(str1[:-1], str2) + 1,
              DamLevRecursion(str1[:-1], str2[:-1]) + forfeit)
    if (len(str1) >= 2 and len(str2) >= 2 and str1[-1] == str2[-2] and str1[-2] == str2[-1]):
        res = min(res, DamLevRecursion(str1[:-2], str2[:-2]) + 1)
    return res 
    

def DamLevTable(str1, str2, output = False):
    len_i = len(str1) + 1
    len_j = len(str2) + 1
    table = [[i + j for j in range(len_j)] for i in range(len_i)]
    
    for i in range(1, len_i):
        for j in range(1, len_j):
            forfeit = 0 if (str1[i - 1] == str2[j - 1]) else 1
            table[i][j] = min(table[i - 1][j] + 1,
                              table[i][j - 1] + 1,
                              table[i - 1][j - 1] + forfeit)
            if (i > 1 and j > 1) and str1[i-1] == str2[j-2] and str1[i-2] == str2[j-1]:
                table[i][j] = min(table[i][j], table[i-2][j-2] + 1)
    if output:        
        OutputTable(table, str1, str2)
    return(table[-1][-1])
