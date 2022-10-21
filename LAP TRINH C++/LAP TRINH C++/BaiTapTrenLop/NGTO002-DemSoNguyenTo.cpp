#include<bits/stdc++.h>

using namespace std;

long long max(long long a, long long b){
    if(a>b){
        return a;
    }
    return b;
}


void sang( long long l , long long r){
    int prime[r-l+1];
    for(long long i = 0; i <= r-l+1 ; i++){
        prime[i] = 1;
    }
    for(int i =2 ; i<= sqrt(r) ; i++){
        for(long long j = max(i*i,(l+i-1)/i*i) ; j <= r ; j+=i ){
            prime[j-l] = 0;
        }
    }
    long long dem =0;
    for(int i =max(l,2); i<=r ; i++){
        if(prime[i-l]){
            dem++;
        }
    }
    cout << dem << endl;
}


int main(){
    int t;
    cin >> t;
    while (t--)
    {
        long long l,r;
        cin >> l >> r;
        sang(l,r);
    }
    return 0;
}