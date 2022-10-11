//#include<conio.h>
#include<stdio.h>
#include<iostream>
#include<math.h>

using namespace std;

int max(int a[], int n)
{
    int max = a[0];
    for (int i = 1; i < n; i++)
        if (max < a[i])
            max = a[i];
    return max;
}

int min(int a[], int n)
{
    int min = a[0];
    for (int i = 1; i < n; i++)
        if (min > a[i])
            min = a[i];
    return min;
}

int main()
{
    int t;
    cin >> t;
    while (t--)
    {
        int n;
        int a[100];
        int s =0;
        cin >> n;
        for (int i=0 ; i<n;i++){
            cin >> a[i];
        }
        s = (max(a,n)-min(a,n))*2;
        cout << s << "\n";
    } 
}







