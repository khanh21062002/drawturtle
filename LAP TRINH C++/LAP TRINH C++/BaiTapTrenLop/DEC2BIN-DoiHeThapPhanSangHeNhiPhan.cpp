#include<bits/stdc++.h>

using namespace std;

// Hàm chuyển đổi số nguyên dương x từ hệ thập phân sang hệ nhị phân
string decimalToBinary(long long x)
{
    // Sử dụng bitset để lưu kết quả chuyển đổi
    bitset<32> b(x);  // Tạo một bitset có 32 bit với giá trị bằng x
    return b.to_string();  // Trả về chuỗi nhị phân từ bitset
}

int main()
{
    int t;
    cin >> t;
    while (t--)
    {
        long long n;
        cin >> n;
        string binary = decimalToBinary(n);
        binary.erase(0, binary.find_first_not_of('0'));
        cout << binary << endl;
    }
    return 0;
}