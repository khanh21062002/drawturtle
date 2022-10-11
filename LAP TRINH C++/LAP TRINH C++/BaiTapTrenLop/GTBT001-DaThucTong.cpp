#include<iostream>
#include<bits/stdc++.h>
#include<math.h>

using namespace std;

float Tong(int n , float x){
    long double tu = 1;
    int mau = 0;
    float kq = 0;
    for(int k =1 ; k<=n; k++ ){
        tu = tu * x;
        mau = mau + k;
        kq = kq + tu / mau;
    }
    return kq;
}
int main(){
    int t;
    cin >> t;
    while (t--)
    {
        int n;
        float x;
        cin >> n;
        cin >> x;
        cout << fixed << setprecision(3) <<Tong(n,x) << '\n' ;
    }
    return 0;
}