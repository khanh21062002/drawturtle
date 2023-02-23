#include<bits/stdc++.h>

using namespace std;

void generateBinaryStrings(int ones,int zeros, string str) {
    if (ones == 0 && zeros == 0) { // nếu đã tạo ra chuỗi nhị phân đủ N bit
        cout << str << " "; // in chuỗi nhị phân đó ra màn hình
        return;
    }
    if(zeros > 0)
    {
        generateBinaryStrings(ones,zeros - 1, str + "0");
    }
    if(ones > 0)
    {
        generateBinaryStrings(ones - 1, zeros, str + "1");
    }
}

int main() {
    int t;
    cin >> t;
    while (t--)
    {
        int n;
        cin >> n;
        if (n % 2 == 1 || n <= 0)
        {
            cout << -1 ;
        }
        else{
            generateBinaryStrings(n / 2 , n / 2 , ""); // bắt đầu với chuỗi rỗng
        }
        cout << endl;
    }
    return 0;
}