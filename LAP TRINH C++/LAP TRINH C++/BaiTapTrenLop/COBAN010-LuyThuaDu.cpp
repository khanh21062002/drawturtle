#include<bits/stdc++.h>
const int MOD=1e9+7;
using namespace std;

long long kq(unsigned long long a, unsigned long long n)
{
    if(n == 0 )
    {
        return 1;
    }
    if ( n == 1)
    {
        return a;
    }
    if( n % 2 == 0){
        long long temp = kq(a,n/2);
        return (temp * temp) % MOD;
    }
    else
    {
        long long temp = kq(a,n/2);
        return ((a*temp)%MOD*temp)%MOD;
    }
}

int main()
{
    int t;
    cin >> t;
    while (t--)
    {
        unsigned long long a,n;
        cin >> a >> n;
        cout << kq(a,n) << endl;
    }
    return 0;
}