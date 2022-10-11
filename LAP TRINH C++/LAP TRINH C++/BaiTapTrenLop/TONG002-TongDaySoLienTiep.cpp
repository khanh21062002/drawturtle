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
        if( l <= r){
            for( int i =l ; i<=r ;i++){
                sum = sum + i;
            }
            cout << sum << endl;
        }
    }
    return 0;
}