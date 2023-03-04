#include<bits/stdc++.h>

using namespace std;

long long const  MOD = 123456789;

long long s (long long x)
{
    return x * x;
}
// Đếm số lượng dãy số nguyên dương có tổng bằng n, và các phần tử trong dãy không vượt quá m.
long long count(long long n) {
    if(n==0) return 1%MOD;
    if(n==1) return 2%MOD;
    long long p = count(n/2);
    if ( n % 2 == 0) return (p % MOD * p % MOD ) % MOD;
    return 2*( s(p) % MOD ) % MOD ;
}

int main() {
    int T;
    cin >> T;
    while (T--) {
        long long n;
        cin >> n;
        n -= 1;
        cout << count(n) << endl;
    }
    return 0;
}
