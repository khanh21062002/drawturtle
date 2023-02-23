#include<bits/stdc++.h>
using namespace std;

string s;

void next() {
    int i;
    for(i = s.length() - 1; i >= 0; i--) {
        if (s[i] == '0') {
            s[i] = '1';
            break;
        }
    }
    for(int j = i + 1; j < s.length(); j++) {
        s[j] = '0';
    }
}

void display(void) {
    for(int i = 0; i < s.length(); i++) {
        cout << s[i];
    }
    cout << endl;
}

int main() {
    int T;
    cin >> T;
    while(T--) {
        cin >> s;
        next();
        display();
    }
    return 0;
}
