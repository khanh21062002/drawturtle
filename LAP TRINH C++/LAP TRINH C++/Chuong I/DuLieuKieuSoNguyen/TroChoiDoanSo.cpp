#include<stdio.h>
#include<iostream>
#include<cmath>

using namespace std;

int main(){
    int t;
    cin >> t;
    while (t--)
    {
        long int tong, hieu;
        long int x,y,D,Dx,Dy;
        cin >> tong;
        cin >> hieu;
        if (tong > hieu){
            D = -2;
            Dx = tong * (-1) - hieu * 1;
            Dy = 1 * hieu - 1 * tong ;
            x = Dx /D;
            y = Dy /D;
            cout << x <<" "<< y << "\n";
        }
        else{
            cout << "impossible" << "\n";
        }
    }
    

}