#include "stdio.h"
#include "stdlib.h"
#include "time.h"

int check1(int n)
{
	clock_t st;
	st = clock();
	int summ = 22;
	for (int i = 0; i <= n; i++)
	{
		summ *= 22;
		summ /= 22;
	}
		
	return clock() - st;	
}

int main()
{
	printf("%d\n", check1(50000000));
	return 0;
}