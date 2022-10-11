#include<stdio.h>
#include<iostream>
#include<algorithm>
#include <math.h>

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
int TongChuSoLaSoNguyenTo(long long int n){
    int temp;
    int tong =0;
    temp = n ;
    while(temp != 0)
    {
        tong += temp % 10;
        temp /= 10;
    }
    if (tong < 2 ){
        return 0;
    }
    int count =0;
    for(int i=2 ; i <= sqrt(tong); i++ ){
        if(tong % i == 0){
            count++;
        }
    }
    if (count == 0){
        return 1;
    }
    else{
        return 0;
    }   
}
int ChuSoDeuLe(long long int n){
    int check = 1 ;
    while (n /= 10)
    {
        if ((n%10)%2==0){
            check =0;
            break;
        }
    }
    if (check == 1){
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
        long long int a,b,i;
        long long int dem = 0;
        cin >> a;
        cin >> b;
         for( i=a ; i<=b ; i++ ){
            if (isThuanNghich(i) == 1 && TongChuSoLaSoNguyenTo(i) == 1 && ChuSoDeuLe(i) == 1){
                dem++;
            }
        }
        cout << dem << "\n";
    }
    return 0;
}