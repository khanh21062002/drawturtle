#include<bits/stdc++.h>

using namespace std;

int minSum(int a[],int n)
{
    sort(a,a+n);
    int A = 0, B = 0;
    for (int i = 0; i < n; i++)
    {
        if(i & 1)
        {
            A = A*10 + a[i];
        }
        else
        {
            B = B*10 +a[i];
        }
    }
    return A + B;
}

int main()
{
    int t;
    cin >> t; 
    while (t--)
    {
        int n;
        cin >> n;
        int a[n];
        for(int i = 0 ; i < n ; i++)
        {
            cin >> a[i];
        }
        cout << minSum(a,n)<< endl;
    }
    return 0;
}