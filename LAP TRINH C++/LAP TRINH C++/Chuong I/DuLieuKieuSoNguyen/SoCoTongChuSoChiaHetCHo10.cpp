#include<iostream>
#include<stdio.h>

using namespace std;

int main(){
    int t;
    cin >> t;
    while (t--)
    {
        long int n, temp;
        long int tong =0;
        cin >> n;
        // cout << "\n";
        temp = n;
        while (temp != 0)
        {
            tong += temp % 10;
            temp /= 10;
        }
        if (tong % 10 ==0){
            cout << "YES \n";
        }
        else{
            cout << "NO \n";
        }
    }
    return 0;
}