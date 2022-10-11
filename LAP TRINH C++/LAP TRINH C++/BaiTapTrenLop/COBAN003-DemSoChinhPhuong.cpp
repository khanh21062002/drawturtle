#include<bits/stdc++.h>

using namespace std;

int main(){
    int t;
    cin >> t;
    while (t--)
    {
        long long l,r;
        cin >> l >> r;
        long long dem =0;
        for(long long i=0 ; i*i <= r ; i++ ){
            if(i*i >= l && i*i <= r){
                dem++;
            }
        }
        cout << dem << endl ;
    }
    return 0;
}