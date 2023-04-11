/*Make it count*/
#include <bits/stdc++.h>
#define ll long long
#define fi first
#define se second
#define pb push_back
#define all(v) v.begin(),v.end()
#define f(i,a,b) for(int i=a; i<=b; ++i)
#define fn(i,a,b) for(int i=a; i>=b; --i)
#define faster() ios_base::sync_with_stdio(false),cin.tie(0),cout.tie(0);
const int MOD=1e9+7;

using namespace std;
int a[1005][1005], b[1005][1005];
int main()
{
    faster();
    int t;
	cin >> t;
	while(t--){
        int n, m;
        cin >> n >> m;
        for (int i = 1; i <= n; i++)
            for (int j = 1; j <= m; j++)
                cin >> a[i][j];
        for (int i = 1; i <= n; i++)
        {
            for (int j = 1; j <= m; j++)
            {
                if (i == 1)
                    b[i][j] = b[i][j - 1];
                else if (j == 1)
                    b[i][j] = b[i - 1][j];
                else
                    b[i][j] = min(b[i - 1][j - 1], min(b[i - 1][j], b[i][j - 1]));
                b[i][j] += a[i][j];
            }
        }
        cout << b[n][m] << endl;
        f(i,1,n) f(j,1,m) b[i][j] = 0;

	}
   return 0;
}