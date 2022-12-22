#include <iostream>
#include <vector>

using namespace std;

int main() {
// Nhập số lượng phần tử trong dãy số
int n;
cin >> n;

// Nhập các phần tử trong dãy số
vector<long long> a(n);
for (int i = 0; i < n; i++) {
cin >> a[i];
}

// Kiểm tra xem dãy số có phải là dãy cấp số cộng hay không
bool isArithmeticProgression = true;
long long commonDifference = a[1] - a[0];
for (int i = 2; i < n; i++) {
if (a[i] - a[i - 1] != commonDifference) {
isArithmeticProgression = false;
break;
}
}

// In kết quả
if (isArithmeticProgression) {
cout << "YES" << endl;
} else {
cout << "NO" << endl;
}

return 0;
}