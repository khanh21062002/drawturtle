#include<stdio.h>
#include<iostream>
#include<cmath>

using namespace std ;

int main(){
    int t;
    cin >> t;
    while (t--)
    {
        int gia, tong;
        int a,b,c,sodu;
        cin >> gia;
        cin >> tong;
        int tienthua = tong - gia;
        a = tienthua / 10;
        sodu = tienthua %10;
        if (sodu>=5){
            b = sodu/5;
            c = sodu%5;
        }
        else{
            b =0;
            c = sodu;
        }
        cout << tienthua << " = " << a << " * "<< 10 << " + " << b << " * " << 5 << " + "<< c << " * "<< 1 << "\n";
    }
    return 0;
}