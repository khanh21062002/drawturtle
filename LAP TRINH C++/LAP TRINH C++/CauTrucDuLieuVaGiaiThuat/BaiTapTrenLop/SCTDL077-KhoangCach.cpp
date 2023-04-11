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
ll tinh(ll n){
    if(n == 0) return 1;
    return n*tinh(n-1);
}
int main()
{
	faster();
	int t;
	cin >> t;
	while (t--){
        int n;
        cin >>n;
        ll a[n+5], b[n+5], checka[n+5]={}, checkb[n+5]={}, posa=0, posb=0;
        f(i,1,n){
            int x = 0;
            cin >>a[i];
            checka[a[i]]++;
            for(int j = 1; j < a[i]; ++j) if(!checka[j]) x++;
            posa += x * tinh(n-i);
        }
        f(i,1,n){
            int y = 0;
            cin >>b[i];
            checkb[b[i]]++;
            for(int j = 1; j < b[i]; ++j) if(!checkb[j]) y++;
            posb += y * tinh(n-i);
        }
        cout <<abs(posa-posb) <<'\n';


	}
	return 0;
}