#include<bits/stdc++.h>
using namespace std;

const int MAXN = 10;
const int MOD = 1e9 + 7;

struct matrix {
    int mat[MAXN][MAXN];
};

int n, k;

matrix operator*(const matrix& a, const matrix& b) {
    matrix c;
    memset(c.mat, 0, sizeof(c.mat));
    for (int i = 0; i < n; i++) {
        for (int j = 0; j < n; j++) {
            for (int k = 0; k < n; k++) {
                c.mat[i][j] = (c.mat[i][j] + (long long)a.mat[i][k] * b.mat[k][j]) % MOD;
            }
        }
    }
    return c;
}

matrix power(matrix a, int k) {
    matrix res;
    memset(res.mat, 0, sizeof(res.mat));
    for (int i = 0; i < n; i++) {
        res.mat[i][i] = 1;
    }
    while (k > 0) {
        if (k % 2 == 1) {
            res = res * a;
        }
        a = a * a;
        k /= 2;
    }
    return res;
}

int main() {
    int t;
    cin >> t;
    while (t--) {
        cin >> n >> k;
        matrix A;
        for (int i = 0; i < n; i++) {
            for (int j = 0; j < n; j++) {
                cin >> A.mat[i][j];
            }
        }
        matrix X = power(A, k);
        int sum = 0;
        for (int i = 0; i < n; i++) {
            sum = (sum + X.mat[i][i]) % MOD;
        }
        cout << sum << endl;
    }
    return 0;
}
