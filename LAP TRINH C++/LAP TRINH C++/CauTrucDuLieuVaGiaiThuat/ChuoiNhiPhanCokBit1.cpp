#include <iostream>
#include <string>

using namespace std;

void generateStrings(string s, int n, int k) {
    if (n == s.length()) {
        int count = 0;
        for (int i = 0; i < n - 1; i++) {
            if (s[i] == '1' && s[i + 1] == '1') {
                count++;
            }
        }
        if (count == k) {
            cout << s << endl;
        }
        return;
    }
    generateStrings(s + "0", n, k);
    generateStrings(s + "1", n, k);
}

int main() {
    int n, k;
    cout << "Enter the values of n and k: ";
    cin >> n >> k;
    string s = "";
    generateStrings(s, n, k);
    return 0;
}
