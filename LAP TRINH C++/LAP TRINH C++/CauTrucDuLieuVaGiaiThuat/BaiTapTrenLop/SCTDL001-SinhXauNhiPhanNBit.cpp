#include<bits/stdc++.h>

using namespace std;

void generateBinaryStrings(int n, string str) {
    if (n == 0) { // nếu đã tạo ra chuỗi nhị phân đủ N bit
        cout << str << endl; // in chuỗi nhị phân đó ra màn hình
        return;
    }

    // thêm bit 0 vào cuối chuỗi và tạo tất cả các chuỗi nhị phân của N-1 bit
    generateBinaryStrings(n-1, str + "0 ");

    // thêm bit 1 vào cuối chuỗi và tạo tất cả các chuỗi nhị phân của N-1 bit
    generateBinaryStrings(n-1, str + "1 ");
}

int main() {
    int t;
    cin >> t;
    while (t--)
    {
        int n;
        cin >> n;
        generateBinaryStrings(n, ""); // bắt đầu với chuỗi rỗng
    }
    return 0;
}
