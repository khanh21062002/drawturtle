#include<bits/stdc++.h>
const int MOD=1e9+7;
using namespace std;

long long isSoNguyenTo(long long n){
    long long count = 0;
    for (int i=2 ; i<=sqrt(n); i++){
        if (n % i == 0){
            count++;
        }
    }
    if (count ==0){
        return 1;
    }
    else{
        return 0;
    }
}

int main(){
    ios_base::sync_with_stdio(false); cin.tie(0); cout.tie(0);
    int t;
    cin >> t;
    while (t--)
    {
        long long n;
        cin >> n;
        for( long long i =n ; i <= 10*n ; ++i){
            if( i % 2 != 0){
                if( isSoNguyenTo(i-2) == 1 && isSoNguyenTo(i) == 1)
                {
                    cout << i << endl;
                    break;
                }
            }
        }
    }
    return 0;
}