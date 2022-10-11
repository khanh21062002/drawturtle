#include<stdio.h>
#include<iostream>

using namespace std;

int check(int a[], int n) 
{
	for (int i = 0;i < n - 1;i++) 
	{
		if (a[i] > a[i + 1])
			return 0;
	}
	return 1;
}

int main(){
    int n, swap; 
    int a[100];
    cin >> n;
    for (int i = 0 ; i < n ; i++)
    {
        cin >> a[i];
    }
    for (int i = 0 ; i < n ; i++)
    {
        for (int j =i ; j <n ; j++){
            if (a[i] > a[j] ){
                swap = a[j];
                a[j] =a[i];
                a[i] = swap;
            }
        }
        cout << "Buoc " << i+1 << " :";
        for (int i = 0;i < n;i++) 
		{
			cout << a[i] << " ";
		}
		if (check(a, n) == 1) 
		{
			break;
		}
		cout << "\n" ;
    }
}