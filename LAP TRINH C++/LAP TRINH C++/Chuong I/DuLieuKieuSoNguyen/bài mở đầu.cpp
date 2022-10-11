#include<stdio.h>
#include<string.h>
int main()
{
	int t;
	scanf("%d", &t);
	while (t--)
	{
		char n[1000];
		scanf("%s", &n);
		int p = strlen(n);
		int dem = 0;
		for (int i = 0;i < p;i++)
		{
			if (n[p - 1] == '6'&&n[p-2]=='8')
			{
				dem++;
			}
		}
		if (dem == 0)
		{
			printf("NO\n");
		}
		else
		{
			printf("YES\n");
		}
	}
}
//_CRT_SECURE_NO_WARNINGS