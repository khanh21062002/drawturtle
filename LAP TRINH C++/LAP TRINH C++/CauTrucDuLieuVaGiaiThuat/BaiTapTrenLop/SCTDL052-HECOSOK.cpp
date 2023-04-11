#include <iostream>
#include <string>
#include <algorithm>

using namespace std;

string convert_base(int X, int a, int b) {
    // Chuyển số X từ hệ cơ số a sang hệ cơ số thập phân
    int decimal = 0;
    int power = 1;
    while (X > 0) {
        decimal += (X % 10) * power;
        X /= 10;
        power *= a;
    }
    
    // Chuyển số thập phân sang hệ cơ số b
    if (decimal == 0) {
        return "0";
    }
    
    string digits = "";
    while (decimal > 0) {
        digits += to_string(decimal % b);
        decimal /= b;
    }
    
    reverse(digits.begin(), digits.end());
    return digits;
}

int main() {
    int X, a, b;
    cout << "Nhap so X: ";
    cin >> X;
    cout << "Nhap he co so a: ";
    cin >> a;
    cout << "Nhap he co so b: ";
    cin >> b;
    
    string result = convert_base(X, a, b);
    cout << "Ket qua: " << result << endl;
    
    return 0;
}
