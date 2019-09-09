# АА Лабораторная1 Расстояние Левенштейна 
# Л матрично, Д-Л матр, рек

from time import time

def OutputTable(table, str1, str2):
    print("\n   ", end = " ")
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


def LevRecursion(str1, str2, output = False):
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


def DamLevRecursion(str1, str2, output = False):
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


def GetStrAndRun(function, output = False):
    str1 = input("Input str1: ")
    str2 = input("Input str2: ")
    res = function(str1, str2, output)
    print("Distance == ", res)


def TimeAnalysis(function, nIter, str1, str2):
    t1 = time()
    for i in range(nIter):
        function(str1, str2, False)
    t2 = time()
    return (t2 - t1) / nIter


def Menu():
    flagDo = True
    while(flagDo):
        case = input("Menu:\n \
\t1. Levenshtein distance recursion\n \
\t2. Levenshtein distance table\n \
\t3. Damerau–Levenshtein distance recursion\n \
\t4. Damerau–Levenshtein distance table\n \
\t5. Time analysis\n ")
        if (case == "1"):
            GetStrAndRun(LevRecursion, True)
        elif (case == "2"):
            GetStrAndRun(LevTable, True)
        elif (case == "3"):
            GetStrAndRun(DamLevRecursion, True)
        elif (case == "4"):
            GetStrAndRun(DamLevTable, True)
        elif (case == "5"):
            nIter = 100
            str1 = "omsges"
            str2 = "woieus"
            print("Levenshtein distance recursion: ", TimeAnalysis(LevRecursion, nIter, str1, str2))
            print("Levenshtein distance table: ", TimeAnalysis(LevTable, nIter, str1, str2))
            print("Damerau–Levenshtein distance recursion: ", TimeAnalysis(DamLevRecursion, nIter, str1, str2))
            print("Damerau–Levenshtein distance table: ", TimeAnalysis(DamLevTable, nIter, str1, str2))
        else:
            flagDo = False
            
            
if __name__ == "__main__":
    Menu()
    
            
        


    
