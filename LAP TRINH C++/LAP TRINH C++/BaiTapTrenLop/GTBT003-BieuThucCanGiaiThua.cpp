#include<bits/stdc++.h>

using namespace std;

long long GiaiThua(int n)
{
    if(n == 0 || n == 1) return 1;
    return n * GiaiThua(n-1);
}

long double CanGiaiThua(int n)
{
    long double kq = 0;
    for ( int i = 1 ; i <= n; i++)
    {
        long long GT = GiaiThua(i);
        long double can = 1.0/ (i+1) ;
        kq = pow( kq + GT,can);
    }
    return kq;
}

int main()
{
    int t;
    cin >> t;
    while (t--)
    {
        int n;
        cin >> n;
        cout << fixed << setprecision(3) << CanGiaiThua(n) << endl;
    }
    return 0;
}