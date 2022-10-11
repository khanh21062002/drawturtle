#include <iostream>
#include<bits/stdc++.h>

using namespace std;

#define max 20
int a[max],c[max],n;
void kq();
void Try(int j);
int kt();
int main()
{
	cout << "Nhap n: "; cin >> n;
	cout << "DL : "; 
	for (int i=0; i<n; i++)
		cin >> a[i];
	Try(0);
}
void kq()
{
	if (kt())
	{
	for (int i=0; i<n; i++)
		cout << c[i] << ' ';
	cout << endl;
	}
}
void Try(int j)
{
	if (j==n)
		kq();
	else
	{
		for (int i=0; i<n; i++)
		{
			c[j] = a[i];
			Try(j+1);
		}
	}
}
int kt()
{
	for (int i=0; i<n-1; i++)
		for (int j=i+1; j<n; j++)
			if (c[i]==c[j])
				return 0;
	return 1;
}