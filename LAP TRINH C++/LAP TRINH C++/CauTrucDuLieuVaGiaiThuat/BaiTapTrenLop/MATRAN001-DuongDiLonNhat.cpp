#include <iostream>
#include <vector>
#include <algorithm>

using namespace std;

const int MAXN = 1005;

int N, M;
int a[MAXN][MAXN];
int f[MAXN][MAXN];

int main() {
    int t;
    cin >> t;
    while (t--) {
        cin >> N >> M;
        for (int i = 1; i <= N; i++) {
            for (int j = 1; j <= M; j++) {
                cin >> a[i][j];
            }
        }
        for (int j = 1; j <= M; j++) {
            f[1][j] = a[1][j];
        }
        for (int i = 2; i <= N; i++) {
            for (int j = 1; j <= M; j++) {
                f[i][j] = f[i-1][j] + a[i][j];
                if (j > 1) {
                    f[i][j] = max(f[i][j], f[i-1][j-1] + a[i][j]);
                }
                if (j < M) {
                    f[i][j] = max(f[i][j], f[i-1][j+1] + a[i][j]);
                }
            }
        }
        int ans = 0;
        for (int j = 1; j <= M; j++) {
            ans = max(ans, f[N][j]);
        }
        cout << ans << endl;
    }
    return 0;
}
