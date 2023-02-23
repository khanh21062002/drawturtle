#include<bits/stdc++.h>
using namespace std;

const int MOD = 1e9 + 7;

// Hàm tính ma trận X = A^k
vector<vector<int>> matrixPower(vector<vector<int>>& A, int K) {
    int N = A.size();
    vector<vector<int>> X(N, vector<int>(N, 0));
    if (K == 1) {
        return A;
    } else if (K % 2 == 1) {
        vector<vector<int>> Y = matrixPower(A, K-1);
        for (int i = 0; i < N; i++) {
            for (int j = 0; j < N; j++) {
                for (int k = 0; k < N; k++) {
                    X[i][j] = (X[i][j] + (long long)Y[i][k]*A[k][j]) % MOD;
                }
            }
        }
        return X;
    } else {
        vector<vector<int>> Y = matrixPower(A, K/2);
        for (int i = 0; i < N; i++) {
            for (int j = 0; j < N; j++) {
                for (int k = 0; k < N; k++) {
                    X[i][j] = (X[i][j] + (long long)Y[i][k]*Y[k][j]) % MOD;
                }
            }
        }
        return X;
    }
}

int main() {
    int T;
    cin >> T;
    while (T--) {
        int N, K;
        cin >> N >> K;
        vector<vector<int>> A(N, vector<int>(N));
        for (int i = 0; i < N; i++) {
            for (int j = 0; j < N; j++) {
                cin >> A[i][j];
            }
        }
        vector<vector<int>> X = matrixPower(A, K);
        int sum = 0;
        for (int i = 0; i < N; i++) {
            sum = (sum + X[i][N-i-1]) % MOD;
        }
        cout << sum << endl;
    }
    return 0;
}
