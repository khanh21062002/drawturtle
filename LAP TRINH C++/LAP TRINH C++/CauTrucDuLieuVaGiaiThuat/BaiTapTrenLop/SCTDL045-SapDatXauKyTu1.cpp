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

int main()
{
    faster();
    int t;
    cin >>t;
    while(t--){
        string s;
        cin >>s;
        int l = s.size();
        int a[27]={};
        for(int i = 0; i < l; ++i) a[s[i]-'a']++;
        int mx=0;
        for(int i = 0; i < 27; ++i) mx = max(mx,a[i]);
        if(mx*2 > l + 1) cout <<-1;
        else cout <<1;
        cout <<'\n';
    }

   return 0;
}