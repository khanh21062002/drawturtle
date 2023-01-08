#include <iostream>
#include <cmath>
#include <algorithm>

using namespace std;

// Hàm kiểm tra xem số i có phải là số chính phương hay không
bool is_square(long long i) {
  long long root = sqrt(i);
  return root * root == i;
}

int main() {
  int t;
  cin >> t;
  while (t--)
  {
    long long N;
  cin >> N;
  // Khai báo mảng dp để lưu trữ số bước tối thiểu để biến đổi số N về 0
  long long dp[N + 1];

  // Gán giá trị ban đầu cho mảng dp
  dp[0] = 0;
  dp[1] = 1;
  for (long long i = 2; i <= N; i++) {
    dp[i] = dp[i - 1] + 1;
    if (i % 2 == 0) {
      dp[i] = min(dp[i], dp[i / 2] + 1);
    }
    if (i % 3 == 0) {
      dp[i] = min(dp[i], dp[i / 3] + 1);
    }
    if (is_square(i)) {
       dp[i] = min(dp[i], dp[(long long)sqrt(i)] + 1);
    }
  }

  // In ra giá trị của dp[N]
  cout << dp[N] << endl;
  }
  return 0;
}
