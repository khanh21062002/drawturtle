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
    //freopen("input.txt","r", stdin);
    //freopen("output.txt","w", stdout);

    int t;
	cin >> t;
	while(t--){
        int n;
        cin >>n;
        priority_queue<int, vector<int>,greater<int>> q;
        f(i,1,n){
            int x;
            cin >>x;
            q.push(x);
        }
        ll sum=0;
        while(q.size() > 1){
            int k1 = q.top(); q.pop();
            int k2 = q.top(); q.pop();
            int x = k1 + k2;
            sum += x;
            q.push(x);
        }
        cout <<sum;
        cout <<'\n';

	}
   return 0;
}