#include<iostream>
#include<stdio.h>
#include<algorithm>

using namespace std;

int main(){
    int t;
    cin >> t;
    while (t--)
    {
        long long int a,b,LCM;
        cin >> a;
        cin >> b;
        long long int tich = a*b;
        for(int i = max(a,b) ; i <= tich ; i++){
            if (i%a == 0 && i%b == 0){
                LCM = i;
                break;
            }
        }
        cout << LCM << "\n";
    }
    return 0;
}