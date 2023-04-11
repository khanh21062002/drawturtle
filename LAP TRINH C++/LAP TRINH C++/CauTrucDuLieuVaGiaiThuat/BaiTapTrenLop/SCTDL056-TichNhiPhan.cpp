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

ll cvt(string a){
    ll ans = 0, tmp=1;
    for(int i = a.size()-1; i >= 0; --i){
        if(a[i] == '1') ans += tmp;
        tmp <<= 1;
    }
    return ans;
}
int main()
{
    faster();
    //freopen("input.txt","r", stdin);
    //freopen("output.txt","w", stdout);

    int t;
	cin >> t;
	while(t--){
        string a, b;
        cin >>a >>b;
        ll x = cvt(a);
        ll y = cvt(b);
        cout<<x*y <<'\n';


	}
   return 0;
}