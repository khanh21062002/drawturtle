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
	ios_base::sync_with_stdio(0);
	cin.tie(0), cout.tie(0);
	int t;
	cin >> t;
	while (t--){
        int n;
        cin >>n;
        int a[10] = {1, 2, 5, 10, 20, 50, 100, 200, 500, 1000};
        ll ans=0;
        for(int i = 9; i >= 0; --i){
            ans += n/a[i];
            n %= a[i];
        }
        cout <<ans << '\n';
	}
}