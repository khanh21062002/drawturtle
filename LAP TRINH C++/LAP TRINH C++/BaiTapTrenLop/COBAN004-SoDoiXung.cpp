#include <bits/stdc++.h>
#define ll long long
using namespace std;

int t, k;
int main() {
//ios_base::sync_with_stdio(false); cin.tie(0); cout.tie(0);
  cin >> t;
  while(t--){
    cin >> k;
    ll ans=9;
    int mu=(k-1)/2;
    if(k==1 || k==2) ans=9;
    else{
        while(mu--) ans*=10;
    }
    cout<<ans<<'\n';
  }

return 0;
}