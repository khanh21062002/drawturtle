#include <bits/stdc++.h>
#define ll long long
#define fi first
#define se second
#define f(i,a,b) for(int i=a; i<=b; ++i)
#define fn(i,a,b) for(int i=a; i>=b; --i)
const int MOD=1e9+7;
using namespace std;

string s[100005];
void giaithua(int n){
  int a[500005], m=1;
  ll r=0,q;
  a[0]=1;
  for(int i=2;i<=n;i++){
  for(int j=0;j<m;j++){
    q=r;
    r=(a[j]*i+r)/10;
    a[j]=(a[j]*i+q)%10;
   }
  while(r>0){
    a[m]=r%10;
    m++;
    r=r/10;
   }
   for(int p=m-1;p>=0;p--) s[i]+=char('0'+a[p]);
  }
}
int t, n;
int main () {
ios_base::sync_with_stdio(false); cin.tie(0); cout.tie(0);
  giaithua(10000);
  cin >> t;
  while(t--){
    cin >> n;
    if(n==0 || n==1) cout<<1<<'\n'; 
    else cout<<s[n]<<'\n';
  }
return 0;
}