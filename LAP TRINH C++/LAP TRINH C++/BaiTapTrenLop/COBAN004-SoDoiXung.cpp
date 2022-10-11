#include<bits/stdc++.h>
#include<iostream>

using namespace std;

long long isThuanNghich( long long n) {
    int a[20];
    int dem = 0, i;
    do {
        a[dem++] = (n % 10);
        n = n / 10;
    } 
    while (n > 0);
    for (i = 0; i < (dem/2); i++) {
        if (a[i] != a[(dem - i - 1)]) {
            return 0;
        }
    }
    return 1;
}

long long Ktra(int k){
    long long dau = (long long)pow(10, k - 1);
	long long cuoi = (long long)pow(10, k) - 1;
    long int dem = 0;
    if(isThuanNghich(dau)){
        dau = (long long)sqrt(dau);
    }
	else {
        dau = (long long)sqrt(dau) + 1;
    }
    for( int i= dau ; i <=cuoi;i++ ){
        if (isThuanNghich(i)){
            dem ++;
        }
    }
    return dem;
}


int main(int argc, char const *argv[])
{
    int t; 
    cin >> t;
    while (t--)
    {
        int k;
        cin >> k;
        if(k < 1 || k > 18){
            cout << "-1";
        }
        else{
            cout << Ktra(k);
        }
        cout << endl;
    }
    return 0;
}