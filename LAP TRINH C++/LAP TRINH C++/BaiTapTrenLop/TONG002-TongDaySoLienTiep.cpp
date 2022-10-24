#include<bits/stdc++.h>

using namespace std;

int main(){
    int t;
    cin >> t;
    while (t--)
    {
        unsigned long long l,r;
        cin >> l >> r;
        long long sum = 0;
        long long so = 0;
        so = (r -l)/1 +1;
        sum = ((l + r) * so )/2;
        cout << sum << endl;
    }
    return 0;
}