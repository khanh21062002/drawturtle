#include<iostream>
#include<stdio.h>

using namespace std;

int main(){
    int t;
    cin >> t;
    cout << "\n";
    while(t--){
        int n;
        cin >> n;
        int dau;
        int cuoi;
        cuoi = n%10;
        while (n)
        {
            dau = n%10;
            n/=10;
        }
        if( dau == cuoi ){
            cout << "YES"<< "\n";
        }
        else{
            cout << "NO" << "\n";
        }
    }
    return 0;
}