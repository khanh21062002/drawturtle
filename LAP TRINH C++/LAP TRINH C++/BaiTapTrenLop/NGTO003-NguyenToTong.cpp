#include<bits/stdc++.h>
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
    int t;
    cin >> t;
    while (t--)
    {
        long long n,kq[1000000000000],temp = 0;
        long long temp1 = 0;
        long long a[100000000000000];
        // long long b[100];
        cin >> n;
        for (int i = 2; i <= n ; i++){
            if(isSoNguyenTo(i) == 1){
                a[temp++] = i + n;
            }
        }
        for (int i =0 ; i < temp ; i++){
            if(isSoNguyenTo(a[i])){
                cout << a[i] << endl;
                break; 
            }
        }
    }
    return 0;
}