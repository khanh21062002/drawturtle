#include<stdio.h>
#include<iostream>

using namespace std;

int main(){
    int t;
    cin >> t;
    while (t--)
    {
        int N,M;
        int a[100];
        int Tong =0;
        cin >> N;
        cin >> M;
        for (int i = 0; i < M ; i++){
            cin >> a[i];
        }
        for (int i =0 ; i < M ; i++){
            Tong += a[i];
        }
        if (Tong >= N){
            
        }

    }
    
}