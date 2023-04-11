
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

int main()
{
    faster();
    int t;
	cin >> t;
	while(t--){
        string s;
		cin >> s;
		int n = s.size(), ans = 1;
		vector<vector<bool>> a(n, vector<bool>(n));
		for (int i = 0; i < n; i++)
			a[i][i] = 1;
		for (int i = 1; i < n; i++)
			for (int j = 0; j < n - i; j++)
			{
				int k = i + j;
				a[j][k] = ((j + 1 > k - 1 || a[j + 1][k - 1]) && s[j] == s[k]);
				if (a[j][k])
					ans = i + 1;
			}
		cout << ans << endl;
	}

   return 0;
}