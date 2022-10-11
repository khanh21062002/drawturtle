#include<stdio.h>
#include<iostream>

using namespace std;

int main(){
    int t;
    cin >> t;
    while (t--)
    {
        int n;
        cin >>n;
        int a[n];
        for (int i = 0; i < n; i++)
        {
            cin >> a[i];
        }
        int counter = 0;
        for(int i =0; i<n;i++){
            if(a[i]<=a[i+1]){
                counter++;
            }
        }
        cout << counter<<"\n";
    }
    return 0;
}