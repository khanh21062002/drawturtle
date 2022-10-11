#include<stdio.h>
#include<iostream>
#include<math.h>

using namespace std;

int KiemTraSoChinhPhuong(int n){
    int sqr = sqrt(n);
    if (sqr*sqr==n){
        return 1;
    }
    else{
        return 0;
    }
}

int main(){
    int t;
    cin >> t ;
    while (t--)
    {
        long int a,b,i;
        long int dem =0;
        cin >> a;
        cin >> b;
        for (i = a ; i<=b ; i++){
            if (KiemTraSoChinhPhuong(i)){
                dem++;
            }
        }
        cout << dem << '\n';
    }
    return 0;
}