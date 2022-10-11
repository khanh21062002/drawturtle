#include<stdio.h>
#include<iostream>
#include<fstream>

using namespace std;

// int tinhF(int C[], int X[] , int n ) {
//   int f = 0;
//   for (int i = 1; i <= n; i++)
//     f = f + C[i] * X[i];
//   return f;
// }

// int demthuchien(int k, int X[], int n, int b,) {
//   for (int i = 0; i <= 1; i++) {
//     X[k] = i;
//     if (k == n) {
//       if (tinhF() == b) max++;
//     } else demthuchien(k + 1, X, n);
//   }
// }

int main(){
    int n ,k;
    ofstream dataout("ketqua.out");
    ifstream data("dayso.in.");
    cin >> n;
    cin >> k;
    int a[500],b[500];
    int max =0;
    for (int i =0 ; i < n;i++){
        cin >> a[i];
    }
    
}