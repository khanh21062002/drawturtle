#include<bits/stdc++.h>

using namespace std;

vector<int> fib; // Lưu trữ các số Fibonacci nhỏ hơn hoặc bằng N
int cnt = 0; // Biến đếm số cách biểu diễn N

// Hàm tìm các số Fibonacci nhỏ hơn hoặc bằng N
void generate_fib(int N) {
    fib.push_back(1);
    fib.push_back(2);
    int i = 2;
    while (fib[i-1] + fib[i-2] <= N) {
        fib.push_back(fib[i-1] + fib[i-2]);
        i++;
    }
}

// Hàm đếm số cách biểu diễn N
void count_ways(int N, int i, vector<int> chosen) {
    if (N == 0) {
        cnt++;
        return;
    }
    if (N < fib[i]) {
        return;
    }
    // Không chọn số Fibonacci F[i]
    count_ways(N, i-1, chosen);
    // Chọn số Fibonacci F[i]
    if (N >= fib[i] && find(chosen.begin(), chosen.end(), fib[i]) == chosen.end()) {
        chosen.push_back(fib[i]);
        count_ways(N-fib[i], i-2, chosen);
    }
}

int main()
{
    int t;
    cin >> t;
    while (t--)
    {
        int n;
        cin >> n;
        generate_fib(n);
        count_ways(n, fib.size()-1, vector<int>());
        cout << cnt << endl;
    }
    return 0;
}