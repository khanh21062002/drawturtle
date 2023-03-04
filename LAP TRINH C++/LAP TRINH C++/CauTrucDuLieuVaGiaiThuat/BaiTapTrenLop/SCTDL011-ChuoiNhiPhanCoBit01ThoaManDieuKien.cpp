#include<bits/stdc++.h>

using namespace std;

void generateBinaryStrings(int n, string s) {
    if (n == 0) {
        // Check if the string contains "01" twice
        int count = 0;
        for (int i = 0; i < s.size() - 1; i++) {
            if (s[i] == '0' && s[i+1] == '1') {
                count++;
                if (count == 2) {
                    cout << s << endl;
                    break;
                }
            }
        }
        return;
    }
    // Recursively generate binary strings of length n-1
    generateBinaryStrings(n-1, s + "0");
    generateBinaryStrings(n-1, s + "1");
}

int main()
{
    int t;
    cin >> t;
    while (t--)
    {
        int n;
        cin >> n;
        generateBinaryStrings(n, "");
    }
    return 0;
}
