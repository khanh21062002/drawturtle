#include<bits/stdc++.h>

using namespace std;

long long Dec2Bin(long long n){
    long long binaryNumber = 0;
    int p = 0;
    while (n > 0)
    {
        binaryNumber += (n % 2) * pow(10, p);
        ++p;
        n /= 2;
    }
    return binaryNumber;
}

int main(){
    int t;
    cin >> t;
    while (t--)
    {
        long long n;
        cin >> n;
        cout << Dec2Bin(n) << endl;
    }
    return 0;
}