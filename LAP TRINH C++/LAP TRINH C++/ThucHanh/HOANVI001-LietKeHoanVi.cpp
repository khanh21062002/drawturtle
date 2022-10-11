#include<iostream>
#include<bits/stdc++.h>
#define MAX 100
using namespace std;

int n;
int Bool[MAX] = { 0 };
int A[MAX];

void Try(int k) {
    for (int i = 1; i <= n; i++) {
        if (!Bool[i]) {
            A[k] = i; 
            Bool[i] = 1;
            if (k == n)
                {
                    for (int i = 1; i <= n; i++)
                        cout << A[i] << " ";
                    cout << endl;
                }
            else
                Try(k + 1);
            Bool[i] = 0;
        }
    }
}

int main(){
    int t;
    cin >> t;
    while (t--)
    {
        cin >> n;
        Try(1);
    }
    return 0;
}