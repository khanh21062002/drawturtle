#include <bits/stdc++.h>
using namespace std;

// Hàm đệ quy tìm tất cả các cách phân tích N thành tổng các số nhỏ hơn hoặc bằng N
void partition(int N, vector<int> &currentPartition) {
    // Nếu N = 0, in ra cách phân tích tìm được và kết thúc hàm
    if (N == 0) {
        cout << "(";
        for (int i = 0; i < currentPartition.size(); i++) {
            if (i == currentPartition.size() - 1) {
                cout << currentPartition[i];
            } else {
                cout << currentPartition[i] <<"";
            }
        }
        cout << ")";
        return;
    }
    // Tìm các số có thể thêm vào cách phân tích hiện tại
    for (int i = 1; i <= N; i++) {
        currentPartition.push_back(i);
        partition(N-i, currentPartition);
        currentPartition.pop_back();
    }
}


int main()
{
    int t;
    cin >> t;
    while (t--)
    {
        int N;
        cin >> N;
        // Tạo ra tất cả các phân tích của số N
        vector<int> currentPartition;
        partition(N, currentPartition);
        cout << endl;
    }
    return 0;
}