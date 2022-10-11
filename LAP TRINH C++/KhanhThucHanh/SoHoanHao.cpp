#include<bits/stdc++.h>
#include<iostream>

using namespace std;

bool TongUocSo(unsigned long long n){
    long long tong =1;
    if(n<=2){
        return false;
    }
    for(int i = 2 ; i <= sqrt(n) ; i++){
        if(n % i == 0){
            tong = tong + (i + (n/i)) ;
        }
    }
    if (tong == n){
        return true;
    }
    else{
        return false;
    }
} 

int main(){
    int t;
    cin >> t;
    while (t--)
    {
        unsigned long long n;
        cin >> n;
        if(TongUocSo(n)) {
            cout << "1" << endl;
        }
        else{
            cout << "0" << endl;
        }
    }
    return 0;
}