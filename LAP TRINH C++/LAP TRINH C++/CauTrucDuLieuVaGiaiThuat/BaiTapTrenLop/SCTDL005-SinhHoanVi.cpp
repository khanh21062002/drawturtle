#include<bits/stdc++.h>

using namespace std;

void generatePermutations(vector<int>& arr, vector<bool>& used, vector<int>& curr, int n) {
    // Nếu đã tạo ra một hoán vị hợp lệ, in ra hoán vị đó
    if (curr.size() == n) {
        for (int i = 0; i < n; i++) {
            cout << curr[i];
        }
        cout << " ";
    } else {
        // Tạo ra các hoán vị bằng thuật toán quay lui
        for (int i = 0; i < n; i++) {
            if (!used[i]) {
                used[i] = true;
                curr.push_back(arr[i]);
                generatePermutations(arr, used, curr, n);
                curr.pop_back();
                used[i] = false;
            }
        }
    }
}

int main() {
    int t;
    cin >> t;
    while (t--)
    {
        int N;
        cin >> N;
        // Tạo một vector số từ 1 đến N
        vector<int> arr(N);
        for (int i = 0; i < N; i++) {
            arr[i] = i+1;
        }
        // Sử dụng thuật toán quay lui để tạo ra tất cả các hoán vị
        vector<bool> used(N, false);
        vector<int> curr;
        generatePermutations(arr, used, curr, N);
        cout << endl;
    }
    return 0;
}
