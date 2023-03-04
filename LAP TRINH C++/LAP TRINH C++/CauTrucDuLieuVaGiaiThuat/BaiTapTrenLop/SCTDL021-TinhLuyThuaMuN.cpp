#include<bits/stdc++.h>
const int MOD = 1e9 + 7;
using namespace std;

int moduloExponentiation(long long a, long long n) {
    if (n == 0) {
        return 1;
    }
    int half = moduloExponentiation(a, n/2);
    long long result = (long long)half * half % MOD;
    if (n % 2 == 1) {
        result = result * a % MOD;
    }
    return result;
}

int main()
{
    int t;
    cin >> t;
    while (t--)
    {
        long long a,n;
        cin >> a >> n;
        long long result = moduloExponentiation(a,n);
        cout << result << endl;
    }
    return 0;
}