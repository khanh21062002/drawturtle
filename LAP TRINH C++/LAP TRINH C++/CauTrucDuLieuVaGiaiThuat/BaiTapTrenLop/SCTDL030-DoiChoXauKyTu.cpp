#include <iostream>
#include <string>
#include <algorithm>

using namespace std;

int main() {
    int t;
    cin >> t;

    while (t--) {
        int k;
        string s;
        cin >> k >> s;

        int n = s.length();

        // Sắp xếp các chữ số trong xâu ký tự theo thứ tự giảm dần
        sort(s.begin(), s.end(), greater<char>());

        // Đổi chỗ các chữ số để tạo ra số lớn nhất có thể
        for (int i = 0; i < n && k > 0; i++) {
            if (s[i] != s[n-1-i]) {
                swap(s[i], s[n-1-i]);
                k--;
            }
        }

        cout << s << endl;
    }

    return 0;
}
