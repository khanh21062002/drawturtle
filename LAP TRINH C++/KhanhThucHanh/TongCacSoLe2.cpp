#include<bits/stdc++.h>

using namespace std;

int main(){
    int t;
    cin >> t;
    while (t--)
    {
        unsigned long long l,r;
        cin >> l >> r;
        long long sum = 0;
        long long so = 0;
        if( l % 2 == 0 && r % 2 == 0 ){
            so = ((r-1) -(l+1))/2 +1;
            sum = (((l+1) + (r-1)) * so )/2;
        }
        if( l % 2 != 0 && r % 2 == 0){
            so = ((r-1) -l)/2 +1;
            sum = ((l + (r-1)) * so )/2;
        }
        if(l % 2 == 0 && r % 2 != 0){
            so = (r -(l+1))/2 +1;
            sum = (((l+1) + r) * so )/2;
        }
        if(l % 2 != 0 && r % 2 != 0){
            so = (r -l)/2 +1;
            sum = ((l + r) * so )/2;
        }
        cout << sum << endl;
    }
    return 0;
}