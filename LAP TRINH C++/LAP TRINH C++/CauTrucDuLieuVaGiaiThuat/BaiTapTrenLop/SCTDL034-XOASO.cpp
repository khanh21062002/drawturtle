#include<bits/stdc++.h>

using namespace std;

bool isPrime(int n) {
    if (n < 2) return false;
    for (int i = 2; i <= sqrt(n); i++) {
        if (n % i == 0) return false;
    }
    return true;
}

int main() {
    int T;
    cin >> T;

    while (T--) {
        int n, k;
        cin >> n >> k;

        string s = "";
        int i = 2;
        while (s.length() < n) {
            if (isPrime(i)) {
                s += to_string(i);
            }
            i++;
        }

        while (k > 0) {
            char minDigit = s[0];
            int minIndex = 0;
            for (int i = 1; i < s.length() && i <= k; i++) {
                if (s[i] < minDigit) {
                    minDigit = s[i];
                    minIndex = i;
                }
            }
            s.erase(minIndex, 1);
            k--;
        }

        cout << s << endl;
    }

    return 0;
}
