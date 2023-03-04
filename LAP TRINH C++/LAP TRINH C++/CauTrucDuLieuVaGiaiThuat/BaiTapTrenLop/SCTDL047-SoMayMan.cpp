#include <bits/stdc++.h>
using namespace std;

bool is_lucky(int n) {
    while (n > 0) {
        int d = n % 10;
        if (d != 4 && d != 7) {
            return false;
        }
        n /= 10;
    }
    return true;
}

int find_lucky(int n, string s) {
    if (s.size() > 6) {  // Vượt quá giới hạn cho phép
        return -1;
    }
    int num = stoi(s);
    if (num > 0 && is_lucky(num) && accumulate(s.begin(), s.end(), 0) - '0' == n) {
        return num;
    }
    if (s.empty()) {
        s = "0";
    }
    if (s.back() == '4') {  // Tìm số may mắn bằng cách thay đổi chữ số 4 đầu tiên bằng 7
        return find_lucky(n, s.substr(0, s.size() - 1) + "7");
    } else {
        return find_lucky(n, s.substr(0, s.size() - 1) + "4") * 10 + 4;  // Tìm số may mắn tiếp theo
    }
}

int main() {
    int t;
    cin >> t;
    while (t--) {
        int n;
        cin >> n;
        int ans = find_lucky(n, "");
        if (ans == -1) {
            cout << "-1\n";
        } else {
            cout << ans << "\n";
        }
    }
    return 0;
}
