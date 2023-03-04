#include <bits/stdc++.h>
using namespace std;
typedef long long ll;

ll getNumberOfDigits(ll x) {
    if (x == 0) return 1;
    return floor(log10(x)) + 1;
}

vector<ll> findAllNumbers(ll n) {
    vector<ll> res;
    if (n == 1) {
        for (ll i = 0; i <= 9; i++) {
            res.push_back(i);
        }
        return res;
    }
    ll maxFact = 1;
    for (ll i = 1; i <= n; i++) {
        maxFact *= 10;
    }
    maxFact--;
    ll minFact = ceil(pow(10, n - 1));
    for (ll i = minFact; i <= maxFact; i++) {
        ll tmp = i;
        ll numOfDigits = getNumberOfDigits(tmp);
        ll fact = 1;
        for (ll j = 2; j <= tmp; j++) {
            fact *= j;
            while (fact % 10 == 0) {
                fact /= 10;
            }
            fact %= (ll)pow(10, n - 1);
            if (j > numOfDigits && fact == 0) {
                break;
            }
        }
        if (fact == 0) {
            res.push_back(i);
        }
    }
    return res;
}

int main() {
    ll t;
    cin >> t;
    while (t--) {
        ll n;
        cin >> n;
        vector<ll> res = findAllNumbers(n);
        if (res.size() == 0) {
            cout << "NO" << endl;
        } else {
            cout << res.size() << endl;
            for (ll i = 0; i < res.size(); i++) {
                cout << res[i] << endl;
            }
        }
    }
    return 0;
}
