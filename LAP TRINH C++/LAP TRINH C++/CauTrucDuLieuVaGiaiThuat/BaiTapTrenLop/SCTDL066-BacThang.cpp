#include <bits/stdc++.h>
#define ll long long
#define fi first
#define se second
#define pb push_back
#define all(v) v.begin(), v.end()
#define f(i, a, b) for (int i = a; i <= b; ++i)
#define fn(i, a, b) for (int i = a; i >= b; --i)
#define faster() ios_base::sync_with_stdio(false), cin.tie(0), cout.tie(0);
const int MOD = 1e9 + 7;

using namespace std;

int main()
{
    faster();
    int t;
    cin >> t;
    while (t--)
    {
        ll n, k, dp[100005] = {};
        cin >> n >> k;
        dp[0] = 1;
        dp[1] = 1;
        f(i, 2, k)
        {
            f(j, 1, min((int)k, i)) dp[i] = (dp[i] + dp[i - j]) % MOD;
        }
        ll tmp = 0;
        f(i, 1, k) tmp = (tmp + dp[i]) % MOD;
        f(i, k + 1, n)
        {
            dp[i] = tmp;
            tmp = ((tmp - dp[i - k] + MOD) % MOD + dp[i]) % MOD;
        }
        cout << dp[n] << '\n';
    }

    return 0;
}