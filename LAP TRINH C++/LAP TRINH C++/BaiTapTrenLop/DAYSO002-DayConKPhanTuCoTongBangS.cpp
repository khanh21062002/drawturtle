#include<bits/stdc++.h>

using namespace std;

int N,K,S;
vector<int> A;
int ans = 0;
void solve(int i, int sum, int count) {
    if (count == K) {
        if (sum == S) {
            // Tìm thấy 1 dãy con thỏa điều kiện
            ans++;
        }
        return;
    }
    if (i == N || sum > S || count > K) {
        return;
    }
    solve(i + 1, sum, count);
    solve(i + 1, sum + A[i], count + 1);
}

int main()
{
    int t;
    cin >> t;
    while (t--)
    {
        cin >> N >> K >> S;
        A.resize(N);
        for (int i = 0; i < N; i++) {
            cin >> A[i];
        }
        solve(0,0,0);
        cout << ans << endl;
    }
    return 0;
}