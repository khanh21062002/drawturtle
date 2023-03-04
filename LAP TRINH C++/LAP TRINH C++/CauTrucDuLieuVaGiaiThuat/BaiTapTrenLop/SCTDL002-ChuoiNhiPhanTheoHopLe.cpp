#include <iostream>
#include <string>

using namespace std;

void generateBinaryString(string str, int index) {
    if (index == str.length()) { // Nếu đã duyệt hết chuỗi
        cout << str << endl; // In ra chuỗi
        return;
    }

    if (str[index] == '?') { // Nếu ký tự hiện tại là '?'
        str[index] = '0'; // Thay thế bằng '0'
        generateBinaryString(str, index+1); // Gọi đệ quy cho các ký tự tiếp theo
        str[index] = '1'; // Thay thế bằng '1'
        generateBinaryString(str, index+1); // Gọi đệ quy cho các ký tự tiếp theo
    } else { // Nếu ký tự hiện tại là '0' hoặc '1'
        generateBinaryString(str, index+1); // Gọi đệ quy cho các ký tự tiếp theo
    }
}

int main() {
    int t;
    cin >> t;

    while (t--) {
        string str;
        cin >> str;

        generateBinaryString(str, 0); // Bắt đầu đệ quy từ ký tự đầu tiên
    }

    return 0;
}
