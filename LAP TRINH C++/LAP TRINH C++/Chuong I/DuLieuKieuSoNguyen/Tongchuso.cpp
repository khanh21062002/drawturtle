#include<stdio.h>
#include<string.h>
#include<iostream>

using namespace std;

int main(){
    int t;
	cin >> t;
	while (t--){
        int a;
        int temp;
        int tong = 0;
        cin >> a ; 
        cout << "\n";
        temp = a;
        while(temp != 0){
            tong += temp % 10;
            temp /= 10;
        }
        cout << tong << "\n";
    }
    return 0;
}

