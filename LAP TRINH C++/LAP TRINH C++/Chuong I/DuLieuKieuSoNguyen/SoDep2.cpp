#include<stdio.h>
#include<iostream>
#include <math.h>
#include<cmath>

using namespace std;
const int DEC_10 =10;

int isThuanNghich( long long int n){
    int a[20];
    int dem =0,i;
    do {
        a[dem++] = (n % DEC_10);
        n = n / DEC_10;
    } while (n > 0);
    for (i =0 ; i<(dem/2); i++)
    {
        if (a[i] != a[(dem - i - 1)]) 
        {
            return 0;
        };
    }
    return 1;
}
int TongChuSoChiaHetCho10(long long int n){
    int temp;
    int tong =0;
    temp = n ;
    while(temp != 0)
    {
        tong += temp % 10;
        temp /= 10;
    }
    if (tong % 10 ==0){
        return 1;
    }
    else{
        return 0;
    }
}
// int ChuSoDeuLe(long long int n){
//     int check = 1 ;
//     while (n /= 10)
//     {
//         if ((n%10)%2==0){
//             check =0;
//             break;
//         }
//     }
//     if (check == 1){
//         return 1;
//     }
//     else{
//         return 0;
//     }
// }

int main(){
    int t;
    cin >> t;
    while (t--)
    {
        long int n;
        cin >> n;
        int dem =0;
        for(int i = pow(10,n-1); i < pow(10,n); i++){
            if(isThuanNghich(i) && TongChuSoChiaHetCho10(i)){
                dem++;
            }
        }
        cout << dem << "\n";

    }
    
}