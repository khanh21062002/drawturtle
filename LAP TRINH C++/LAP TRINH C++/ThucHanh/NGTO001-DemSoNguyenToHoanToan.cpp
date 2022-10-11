#include<bits/stdc++.h>
#include<iostream>

using namespace std;

bool isSoNguyenTo(long long n){
    int count = 0;
    for (int i=2 ; i<=sqrt(n); i++){
        if (n % i == 0){
            count++;
        }
    }
    if (count ==0){
        return true;
    }
    else{
        return false;
    }
}

bool ktra(long long x){
    //int tong = 0;
    bool cs = true;
    while(x > 0)
    {
       int t = x % 10;
        if(t != 2 && t != 3 && t != 5 && t != 7)
           {
               cs = false;
           }
        //tong = tong + t;
        x = x / 10;
    }
    if(cs == true){
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
        long long n,i;
        cin >> n;
        int dem = 0;
        for( i = 1; i<=n; i++){
            if(isSoNguyenTo(i)&&ktra(i)){
                dem++;
            }
        }
        cout << dem << endl;
    }
    return 0;
}