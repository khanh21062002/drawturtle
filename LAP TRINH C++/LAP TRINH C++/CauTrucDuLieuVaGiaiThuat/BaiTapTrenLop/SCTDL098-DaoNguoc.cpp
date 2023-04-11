#include <iostream>
#include <stack>
#include <string>
using namespace std;

int main() {
    int T;
    cin >> T;
    cin.ignore(); // loại bỏ kí tự xuống dòng sau số lượng test cases

    for (int i = 0; i < T; i++) {
        string str;
        getline(cin, str);

        stack<char> s;
        for (int j = 0; j < str.length(); j++) {
            if (str[j] != ' ') {
                s.push(str[j]);
            }
            else {
                while (!s.empty()) {
                    cout << s.top();
                    s.pop();
                }
                cout << " ";
            }
        }

        while (!s.empty()) {
            cout << s.top();
            s.pop();
        }

        cout << endl;
    }

    return 0;
}
