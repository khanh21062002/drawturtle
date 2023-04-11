#include<bits/stdc++.h>
#define faster() ios_base::sync_with_stdio(false),cin.tie(0),cout.tie(0);
using namespace std;

int main()
{
    int t;
    cin >> t;
    while (t--)
    {
        int n;
        cin >> n;
        int a[n], b[n];
        for( int i = 1 ; i <=n ; i++)
        {
            cin >> a[i];
        }
        sort(a+1,a+n+1,greater<int>());
        for ( int j = 1 ; j <= n ; j++)
        {
            cin >> b[j];
        }
        sort(b+1,b+n+1);
        long long sum = 0;
        for( int i = 1 ; i<=n ; i++)
        {
            sum += a[i]*b[i];
        }
        cout << sum << endl;
    }
    return 0;
}