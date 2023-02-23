#include<bits/stdc++.h>

using namespace std;

void generateBinaryStrings(int n, string str) {
    if (n == 0) { 
        cout << str << " "; // in chuỗi nhị phân đó ra màn hình
        return;
    }

    // thêm A vào cuối chuỗi 
    generateBinaryStrings(n-1, str + "A");

    // thêm B vào cuối chuỗi 
    generateBinaryStrings(n-1, str + "B");
}

int main() {
    int t;
    cin >> t;
    while (t--)
    {
        int n;
        cin >> n;
        generateBinaryStrings(n, ""); // bắt đầu với chuỗi rỗng
        cout << endl;
    }
    return 0;
}
