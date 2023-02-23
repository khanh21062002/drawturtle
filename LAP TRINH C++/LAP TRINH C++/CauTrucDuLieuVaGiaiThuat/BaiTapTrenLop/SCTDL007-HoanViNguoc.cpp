#include<bits/stdc++.h>

using namespace std;

void generatePermutations(vector<int>& arr, vector<bool>& used, vector<int>& curr, int n, vector<string>& result) {
    if (curr.size() == n) {
        string s = "";
        for (int i = 0; i < n; i++) {
            s += to_string(curr[i]);
        }
        result.push_back(s);
    } else {
        for (int i = 0; i < n; i++) {
            if (!used[i]) {
                used[i] = true;
                curr.push_back(arr[i]);
                generatePermutations(arr, used, curr, n, result);
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
        vector<int> arr(N);
        for (int i = 0; i < N; i++) {
            arr[i] = i+1;
        }
        vector<bool> used(N, false);
        vector<int> curr;
        vector<string> result;
        generatePermutations(arr, used, curr, N, result);
        for (int i = result.size() - 1; i >= 0; i--) {
            cout << result[i] << " ";
        }
        cout << endl;
    }
    return 0;
}
