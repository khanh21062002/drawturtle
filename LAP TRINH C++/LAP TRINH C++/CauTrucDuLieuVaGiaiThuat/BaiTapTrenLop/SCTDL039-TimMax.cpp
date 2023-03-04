#include<bits/stdc++.h>
#define faster() ios_base::sync_with_stdio(false),cin.tie(0),cout.tie(0);
const int MOD=1e9+7;

using namespace std;

int main()
{
    faster();
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
        sort(a,a+n);
        long long maxSum = 0;
        for(int i = 0 ; i < n ; i++)
        {
            maxSum = maxSum + (a[i]*i)%MOD;
            maxSum = maxSum % MOD;
        }
        cout << maxSum << endl;
    }
    return 0;
}