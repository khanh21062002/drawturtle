#include<stdio.h>
#include<iostream>
#include<math.h>
#include <bits/stdc++.h>

using namespace std;

int KiemTraSoChinhPhuong(long long int n){
    long long int sqr = sqrt(n);
    if (sqr*sqr==n){
        return 1;
    }
    else{
        return 0;
    }
}
bool ChuSoChinhPhuong(long long int n){
    bool cs =false;
    while(n > 0)
    {
       int t = n % 10;
        if(t == 0 || t == 1 || t == 4 || t == 9)
           {
               cs = true;
           }
        else{
            cs = false;
        }
        n = n / 10;
    }
    if (cs == true){
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
        int k;
        cin >> k;
        int dem =0;
        int temp = 0;
        long long int n = pow(10,k)-1;
        int a[n];
        for ( int i = pow(10,k-1); i<= pow(10,k)-1; i++){
            if(KiemTraSoChinhPhuong(i)==1 && ChuSoChinhPhuong(i) == true){
                a[dem++] = i;
                temp ++;
            }
        }
        if (temp == 0){
            cout << "-1\n";
        }
        else{
            int min = a[0];
            for( int i = 0 ; i < dem ; i++){
               if(min > a[i]){
                  a[i] = min;
                }
            }
        cout << min << "\n" ;
        }
    }
    return 0;
}