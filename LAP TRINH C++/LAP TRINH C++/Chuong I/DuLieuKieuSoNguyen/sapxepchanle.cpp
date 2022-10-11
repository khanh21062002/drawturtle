#include<stdio.h>
void sapxep(int a[], int n)
{
	int temp = 0;
	for (int i = 0;i < n;i++)
	{
		for (int j = i + 1;j < n - 1;j++)
		{
			int temp = a[i];
			a[i] = a[j];
			a[j] = temp;
		}
	}
}
int main()
{
	int n;
	scanf("%d", &n);
	int a[100], c[100] = {0}, l[100]={0};
	for (int i = 0;i < n;i++)
	{
		scanf("%d", &a[i]);
	}
	sapxep(a, n);
	int x = 0, y = 0;
	for (int i = 0;i < n;i++)
	{
		if (a[i] % 2 == 0)
		{
			c[a[i]] = a[i];
		}
	}
	for (int i = 0;i < n;i++)
	{
		if (a[i] % 2 == 1)
		{
			l[a[i]] = a[i];
		}
	}
	
	
}