#include<iostream>
#include<bits/stdc++.h>

using namespace std;

int n,k;
int X[100];

void xuat(){
    for(int i=1 ; i<=k;i++){
        cout << X[i] << ' ';
    }
    cout << "\n";
}

void ToHop(int i){
    for (int j = X[i-1] + 1; j <= n-k+i; j++)  {
        X[i] = j;
        if (i == k)
            xuat();
        else
            ToHop(i+1);
    }
}

int main(){
    cin >> n;
    cin >> k;
    X[0]=0;
    ToHop(1);
    return 0;
}