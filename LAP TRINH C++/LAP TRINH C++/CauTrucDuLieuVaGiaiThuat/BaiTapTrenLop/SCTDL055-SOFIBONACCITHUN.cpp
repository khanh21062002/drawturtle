#include<bits/stdc++.h>

using namespace std;

const int MOD = 1e9 + 7;

int main()
{
    int t;
    cin >> t;
    while (t--)
    {
        int n;
        cin >> n;
        int f0=0, f1=1, fn;
        if(n==0){
            fn = f0;
        }
        else if (n == 1)
        {
            fn = f1;
        }
        else
        {
            for (int i = 2; i <= n; i++) {
               fn = (f0 + f1) % MOD;
               f0 = f1;
               f1 = fn;
            }
        }
        cout << fn << endl;
    }
    return 0;
}